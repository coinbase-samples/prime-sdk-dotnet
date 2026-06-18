/*
 * Copyright 2026-present Coinbase Global, Inc.
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System.Text;
using CoinbaseSdk.Tools.Generator.Processing;
using CoinbaseSdk.Tools.Generator.Spec;
using Microsoft.Extensions.Logging;

namespace CoinbaseSdk.Tools.Generator.Phases;

/// <summary>
/// Emits runnable <c>.cs</c> example scripts (shebang + System.CommandLine) for each SDK operation
/// that does not already have an example file on disk.
/// </summary>
public sealed class ExamplePhase
{
  private readonly ILogger<ExamplePhase> _logger;
  private readonly ParsedOpenApiDocument _doc;
  private readonly GeneratorConfiguration _cfg;
  private readonly SharedTransforms _transforms;
  private readonly string _examplesRoot;

  public ExamplePhase(
    ILogger<ExamplePhase> logger,
    ParsedOpenApiDocument doc,
    GeneratorConfiguration cfg,
    SharedTransforms transforms,
    string examplesRoot)
  {
    _logger = logger;
    _doc = doc;
    _cfg = cfg;
    _transforms = transforms;
    _examplesRoot = examplesRoot;
  }

  public async Task RunAsync(
    IReadOnlyList<SdkOperationBinding> bindings,
    bool dryRun,
    bool diffMode)
  {
    foreach (var b in bindings)
    {
      if (!_doc.OperationsById.TryGetValue(b.OperationId, out var op))
      {
        continue;
      }

      var svc = NamingResolver.RequireService(_cfg, b.Service);
      var examplePath = Path.Combine(_examplesRoot, svc.Folder, $"{b.SdkMethod}.cs");

      if (File.Exists(examplePath))
      {
        // Never overwrite hand-crafted examples.
        if (!diffMode)
        {
          _logger.LogInformation("EXAMPLE skipping (already exists): {Path}", examplePath);
        }

        continue;
      }

      var content = EmitExample(b, op, svc);

      if (dryRun)
      {
        _logger.LogInformation("EXAMPLE DRY-RUN would create: {Path} ({Len} chars)", examplePath, content.Length);
        continue;
      }

      if (diffMode)
      {
        _logger.LogInformation("EXAMPLE DIFF missing: {Path}", examplePath);
        continue;
      }

      Directory.CreateDirectory(Path.GetDirectoryName(examplePath)!);
      await File.WriteAllTextAsync(examplePath, content);
      _logger.LogInformation("EXAMPLE created: {Path}", examplePath);
    }
  }

  private string EmitExample(
    SdkOperationBinding b,
    ParsedOperation op,
    ServiceDefinition svc)
  {
    var pathParams = op.Parameters.Where(p => p.In == "path").ToList();
    var bodyProps = op.RequestBodyJsonSchema != null
      ? OpenApiSchemaCodegen.ListProperties(_doc.Root, op.RequestBodyJsonSchema, _transforms)
      : new List<SchemaProperty>();

    // Determine which path params need special env-var fallback (portfolioId / entityId).
    bool HasPathParam(string name) => pathParams.Any(p =>
      string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

    var csharpNs = $"CoinbaseSdk.Prime.{svc.Namespace}";
    var description = BuildDescription(b.SdkMethod, op.HttpMethod);

    var sb = new StringBuilder();

    // ── Shebang + license ──────────────────────────────────────────────────────
    sb.AppendLine("#!/usr/bin/env -S dotnet run --file");
    CopyrightHelper.AppendEmittedCsFileLicense(sb, CopyrightHelper.SdkEmittedCopyrightYear);
    sb.AppendLine("#:project ../../../Prime");
    sb.AppendLine("#:project ../../");
    sb.AppendLine("#:package Newtonsoft.Json@13.0.3");
    sb.AppendLine();
    sb.AppendLine("using System.CommandLine;");
    sb.AppendLine($"using {csharpNs};");
    sb.AppendLine("using CoinbaseSdk.Prime.Client;");
    sb.AppendLine("using CoinbaseSdk.Prime.Common;");
    sb.AppendLine();
    sb.AppendLine("// Load environment variables");
    sb.AppendLine("DotNetEnv.Env.TraversePath().Load();");
    sb.AppendLine();

    // ── Option declarations ────────────────────────────────────────────────────
    var allParams = new List<(string VarName, string OptionName, string Description)>();

    foreach (var p in pathParams)
    {
      var clrName = OpenApiSchemaCodegen.ToPascalCase(p.Name);
      var varName = char.ToLowerInvariant(clrName[0]) + clrName[1..] + "Option";
      var desc = ToParamDescription(p.Name);
      allParams.Add((varName, $"--{char.ToLowerInvariant(clrName[0]) + clrName[1..]}", desc));
      sb.AppendLine($"var {varName} = new Option<string?>(");
      sb.AppendLine($"    name: \"--{char.ToLowerInvariant(clrName[0]) + clrName[1..]}\",");
      sb.AppendLine($"    description: \"{desc}\");");
      sb.AppendLine();
    }

    // For POST operations with body, emit required body params as options.
    var requiredBody = bodyProps.Where(p => p.Required).ToList();
    var optionalBody = bodyProps.Where(p => !p.Required).ToList();
    foreach (var p in requiredBody)
    {
      var varName = char.ToLowerInvariant(p.ClrName[0]) + p.ClrName[1..] + "Option";
      var optName = "--" + char.ToLowerInvariant(p.ClrName[0]) + p.ClrName[1..];
      allParams.Add((varName, optName, ToBodyParamDescription(p.JsonName)));
      sb.AppendLine($"var {varName} = new Option<string?>(");
      sb.AppendLine($"    name: \"{optName}\",");
      sb.AppendLine($"    description: \"{ToBodyParamDescription(p.JsonName)}\");");
      sb.AppendLine();
    }

    // ── RootCommand ──────────────────────────────────────────────────────────
    sb.AppendLine($"var rootCommand = new RootCommand(\"{description}\")");
    sb.AppendLine("{");
    foreach (var (varName, _, _) in allParams)
    {
      sb.AppendLine($"    {varName},");
    }

    sb.AppendLine("};");
    sb.AppendLine();

    // ── SetHandler ────────────────────────────────────────────────────────────
    bool multipleOptions = allParams.Count > 1 || allParams.Count == 0;
    if (allParams.Count == 0)
    {
      sb.AppendLine("rootCommand.SetHandler(() =>");
    }
    else if (allParams.Count == 1)
    {
      sb.AppendLine($"rootCommand.SetHandler(({allParams[0].VarName.Replace("Option", string.Empty)}) =>");
    }
    else
    {
      sb.AppendLine("rootCommand.SetHandler((context) =>");
    }

    sb.AppendLine("{");

    if (allParams.Count > 1)
    {
      foreach (var (varName, _, _) in allParams)
      {
        var local = varName.Replace("Option", string.Empty);
        sb.AppendLine($"    var {local} = context.ParseResult.GetValueForOption({varName});");
      }

      sb.AppendLine();
    }

    // Env-var fallbacks for well-known path params.
    if (HasPathParam("portfolio_id"))
    {
      sb.AppendLine("    portfolioId ??= Environment.GetEnvironmentVariable(\"PRIME_PORTFOLIO_ID\");");
      sb.AppendLine();
    }

    if (HasPathParam("entity_id"))
    {
      sb.AppendLine("    entityId ??= Environment.GetEnvironmentVariable(\"PRIME_ENTITY_ID\");");
      sb.AppendLine();
    }

    // Required-param guards.
    foreach (var (varName, _, desc) in allParams)
    {
      var local = varName.Replace("Option", string.Empty);
      var opt = "--" + local;
      string envHint = local == "portfolioId" ? " (or set PRIME_PORTFOLIO_ID env var)" :
                       local == "entityId" ? " (or set PRIME_ENTITY_ID env var)" : string.Empty;
      sb.AppendLine($"    if (string.IsNullOrEmpty({local}))");
      sb.AppendLine("    {");
      sb.AppendLine($"        Console.Error.WriteLine(\"Error: {opt} is required{envHint}.\");");
      sb.AppendLine("        Environment.ExitCode = 1;");
      sb.AppendLine("        return;");
      sb.AppendLine("    }");
      sb.AppendLine();
    }

    // ── try block ──────────────────────────────────────────────────────────────
    sb.AppendLine("    try");
    sb.AppendLine("    {");

    // Console.WriteLine for each path param.
    foreach (var p in pathParams)
    {
      var clrName = OpenApiSchemaCodegen.ToPascalCase(p.Name);
      var local = char.ToLowerInvariant(clrName[0]) + clrName[1..];
      sb.AppendLine($"        Console.WriteLine($\"Using {clrName}: {{{local}}}\");");
    }

    sb.AppendLine();
    sb.AppendLine("        var client = CoinbasePrimeClient.FromEnv();");
    sb.AppendLine($"        var {LowerFirst(svc.ClassName)} = new {svc.ClassName}(client);");
    sb.AppendLine();

    // Build request.
    if (b.OmitRequest)
    {
      // No request object needed.
    }
    else if (bodyProps.Count == 0 && !requiredBody.Any())
    {
      // Simple constructor-only request.
      var ctorArgs = string.Join(", ", pathParams.Select(p =>
      {
        var clrName = OpenApiSchemaCodegen.ToPascalCase(p.Name);
        return char.ToLowerInvariant(clrName[0]) + clrName[1..];
      }));
      sb.AppendLine($"        var request = new {b.SdkMethod}Request({ctorArgs});");
    }
    else
    {
      // Builder pattern.
      sb.AppendLine($"        var request = new {b.SdkMethod}Request.{b.SdkMethod}RequestBuilder()");
      foreach (var p in pathParams)
      {
        var clrName = OpenApiSchemaCodegen.ToPascalCase(p.Name);
        var local = char.ToLowerInvariant(clrName[0]) + clrName[1..];
        sb.AppendLine($"            .With{clrName}({local})");
      }

      foreach (var p in requiredBody)
      {
        var local = char.ToLowerInvariant(p.ClrName[0]) + p.ClrName[1..];
        sb.AppendLine($"            .With{p.ClrName}({local})");
      }

      sb.AppendLine("            .Build();");
    }

    if (!b.OmitRequest)
    {
      sb.AppendLine();
      sb.AppendLine($"        PrettyPrinter.PrintResponse(\"{b.SdkMethod}Request\", request);");
      sb.AppendLine();
    }

    // Invoke service method.
    if (b.OmitRequest)
    {
      sb.AppendLine($"        var response = {LowerFirst(svc.ClassName)}.{b.SdkMethod}();");
    }
    else
    {
      sb.AppendLine($"        var response = {LowerFirst(svc.ClassName)}.{b.SdkMethod}(request);");
    }

    sb.AppendLine();
    sb.AppendLine($"        PrettyPrinter.PrintResponse(\"{b.SdkMethod}Response\", response);");
    sb.AppendLine();
    sb.AppendLine("        Environment.ExitCode = 0;");
    sb.AppendLine("    }");
    sb.AppendLine("    catch (Exception ex)");
    sb.AppendLine("    {");
    sb.AppendLine($"        PrettyPrinter.PrintError(\"Error calling {b.SdkMethod}\", ex);");
    sb.AppendLine("        Environment.ExitCode = 1;");
    sb.AppendLine("    }");

    // Close SetHandler.
    if (allParams.Count == 0)
    {
      sb.AppendLine("});");
    }
    else if (allParams.Count == 1)
    {
      sb.AppendLine($"}}, {allParams[0].VarName});");
    }
    else
    {
      sb.AppendLine("});");
    }

    sb.AppendLine();
    sb.AppendLine("return rootCommand.Invoke(args);");

    return sb.ToString();
  }

  private static string BuildDescription(string sdkMethod, string httpMethod)
  {
    // Convert PascalCase to human-readable words.
    var words = System.Text.RegularExpressions.Regex.Replace(sdkMethod, @"([A-Z])", " $1").Trim();
    return words;
  }

  private static string ToParamDescription(string paramName) => paramName switch
  {
    "portfolio_id" => "The Portfolio ID",
    "entity_id" => "The Entity ID",
    "wallet_id" => "The Wallet ID",
    "transaction_id" => "The Transaction ID",
    "advanced_transfer_id" => "The Advanced Transfer ID",
    "activity_id" => "The Activity ID",
    "allocation_id" => "The Allocation ID",
    "order_id" => "The Order ID",
    _ => $"The {OpenApiSchemaCodegen.ToPascalCase(paramName).Replace("Id", " ID")}",
  };

  private static string ToBodyParamDescription(string jsonName) =>
    string.Join(" ", jsonName.Split('_').Select(w =>
      w.Length > 0 ? char.ToUpperInvariant(w[0]) + w[1..] : w));

  private static string LowerFirst(string s) =>
    s.Length == 0 ? s : char.ToLowerInvariant(s[0]) + s[1..];
}
