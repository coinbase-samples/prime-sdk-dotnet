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

using CoinbaseSdk.Tools.Generator.Processing;
using CoinbaseSdk.Tools.Generator.Spec;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CoinbaseSdk.Tools.Generator.Phases;

/// <summary>
/// Holistic generation of request DTOs, response DTOs, service interfaces, and service implementations
/// for all configured SDK operations (see <see cref="ResponsePhase"/>, <see cref="RequestPhase"/>, <see cref="ServicePhase"/>).
/// </summary>
public sealed class ClientSurfacePhase
{
  private readonly ILogger<ClientSurfacePhase> _logger;
  private readonly ParsedOpenApiDocument _doc;
  private readonly GeneratorConfiguration _cfg;
  private readonly SharedTransforms _transforms;
  private readonly string _primeSrcRoot;

  public ClientSurfacePhase(
    ILogger<ClientSurfacePhase> logger,
    ParsedOpenApiDocument doc,
    GeneratorConfiguration cfg,
    SharedTransforms transforms,
    string primeSrcRoot)
  {
    _logger = logger;
    _doc = doc;
    _cfg = cfg;
    _transforms = transforms;
    _primeSrcRoot = primeSrcRoot;
  }

  public async Task RunAsync(
    IReadOnlyList<SdkOperationBinding> bindings,
    bool dryRun,
    bool diffMode)
  {
    var expectedPaths = new HashSet<string>(StringComparer.Ordinal);
    var knownEnumTypeNames = LoadEnumTypeNames(_primeSrcRoot);
    var byService = new Dictionary<string, List<(SdkOperationBinding B, ParsedOperation Op)>>(StringComparer.Ordinal);
    foreach (var b in bindings)
    {
      if (!_doc.OperationsById.TryGetValue(b.OperationId, out var op))
      {
        _logger.LogWarning("OpenAPI spec missing operationId {Id}; skipping.", b.OperationId);
        continue;
      }

      if (!byService.TryGetValue(b.Service, out var list))
      {
        list = new List<(SdkOperationBinding, ParsedOperation)>();
        byService[b.Service] = list;
      }

      list.Add((b, op));
    }

    foreach (var serviceKey in byService.Keys.ToList())
    {
      byService[serviceKey] = SortOperationsForService(byService[serviceKey]);
    }

    foreach (var (_, ops) in byService.OrderBy(kv => kv.Key, StringComparer.Ordinal))
    {
      foreach (var (b, op) in ops)
      {
        var serviceFolder = NamingResolver.RequireService(_cfg, b.Service).Folder;
        if (!b.OmitRequest)
        {
          var req = RequestPhase.EmitRequest(_doc, _cfg, _transforms, b, op, knownEnumTypeNames);
          var reqPath = Path.Combine(_primeSrcRoot, serviceFolder, $"{b.SdkMethod}Request.cs");
          expectedPaths.Add(reqPath);
          await WriteOrDiffAsync(reqPath, req, dryRun, diffMode);
        }

        var resp = ResponsePhase.EmitResponse(_doc, _cfg, _transforms, b, op);
        var respPath = Path.Combine(_primeSrcRoot, serviceFolder, $"{b.SdkMethod}Response.cs");
        expectedPaths.Add(respPath);
        await WriteOrDiffAsync(respPath, resp, dryRun, diffMode);
      }
    }

    foreach (var (serviceKey, ops) in byService.OrderBy(kv => kv.Key, StringComparer.Ordinal))
    {
      var svcDef = NamingResolver.RequireService(_cfg, serviceKey);
      var iface = ServicePhase.EmitInterface(svcDef, ops);
      var impl = ServicePhase.EmitService(svcDef, ops);
      var ifacePath = Path.Combine(_primeSrcRoot, svcDef.Folder, svcDef.InterfaceName + ".cs");
      var implPath = Path.Combine(_primeSrcRoot, svcDef.Folder, svcDef.ClassName + ".cs");
      expectedPaths.Add(ifacePath);
      expectedPaths.Add(implPath);
      await WriteOrDiffAsync(ifacePath, iface, dryRun, diffMode);
      await WriteOrDiffAsync(implPath, impl, dryRun, diffMode);
    }

    CleanupOrphanedClientFiles(expectedPaths, dryRun, diffMode);
  }

  private void CleanupOrphanedClientFiles(
    HashSet<string> expectedPaths,
    bool dryRun,
    bool diffMode)
  {
    var serviceFolders = _cfg.Services.Values
      .Select(s => Path.Combine(_primeSrcRoot, s.Folder))
      .Distinct(StringComparer.Ordinal)
      .ToList();

    foreach (var folder in serviceFolders)
    {
      if (!Directory.Exists(folder))
      {
        continue;
      }

      foreach (var pattern in new[] { "*Request.cs", "*Response.cs", "I*Service.cs", "*Service.cs" })
      {
        foreach (var file in Directory.GetFiles(folder, pattern))
        {
          if (expectedPaths.Contains(file))
          {
            continue;
          }

          if (dryRun || diffMode)
          {
            _logger.LogInformation("CLIENT orphan would delete: {Path}", file);
            continue;
          }

          File.Delete(file);
          _logger.LogInformation("CLIENT deleted orphan: {Path}", file);
        }
      }
    }
  }

  private async Task WriteOrDiffAsync(string path, string content, bool dryRun, bool diffMode)
  {
    content = EmittedSourceNormalizer.Normalize(
      CopyrightHelper.ApplyCopyrightYear(path, content));

    if (diffMode)
    {
      if (!File.Exists(path))
      {
        _logger.LogInformation("DIFF missing file would be created: {Path}", path);
        return;
      }

      var existing = await File.ReadAllTextAsync(path);
      var normExisting = EmittedSourceNormalizer.Normalize(NormalizeNl(existing));
      var normContent = EmittedSourceNormalizer.Normalize(NormalizeNl(content));
      if (!string.Equals(normExisting, normContent, StringComparison.Ordinal))
      {
        _logger.LogWarning("DIFF differs: {Path}", path);
        ShowUnifiedDiff(path, normExisting, normContent);
      }

      return;
    }

    if (dryRun)
    {
      _logger.LogInformation("DRY-RUN would write {Path} ({Len} chars)", path, content.Length);
      return;
    }

    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
    await File.WriteAllTextAsync(path, content);
  }

  private void ShowUnifiedDiff(string path, string existing, string generated)
  {
    var existingLines = existing.Split('\n');
    var generatedLines = generated.Split('\n');

    var diffs = new List<string>();
    var maxLines = Math.Max(existingLines.Length, generatedLines.Length);
    for (var i = 0; i < maxLines; i++)
    {
      var eL = i < existingLines.Length ? existingLines[i] : null;
      var gL = i < generatedLines.Length ? generatedLines[i] : null;
      if (eL != gL)
      {
        if (eL != null)
        {
          diffs.Add($"  - {eL}");
        }

        if (gL != null)
        {
          diffs.Add($"  + {gL}");
        }

        if (diffs.Count >= 20)
        {
          diffs.Add("  ... (truncated)");
          break;
        }
      }
    }

    foreach (var d in diffs)
    {
      _logger.LogInformation("{Diff}", d);
    }
  }

  private static string NormalizeNl(string s)
  {
    return s.Replace("\r\n", "\n", StringComparison.Ordinal);
  }

  private static List<(SdkOperationBinding B, ParsedOperation Op)> SortOperationsForService(
    List<(SdkOperationBinding B, ParsedOperation Op)> ops)
  {
    return ops
      .OrderBy(x => HttpVerbRank(x.Op.HttpMethod))
      .ThenBy(x => PathDepth(x.Op.Path))
      .ThenBy(x => x.Op.Path, StringComparer.Ordinal)
      .ThenBy(x => x.B.SdkMethod, StringComparer.Ordinal)
      .ToList();
  }

  private static int HttpVerbRank(string method) => method.ToUpperInvariant() switch
  {
    "GET" => 0,
    "POST" => 1,
    "PUT" => 2,
    "PATCH" => 3,
    "DELETE" => 4,
    _ => 5
  };

  private static int PathDepth(string path)
  {
    if (string.IsNullOrEmpty(path))
    {
      return 0;
    }

    return path.Count(c => c == '/');
  }

  private static HashSet<string> LoadEnumTypeNames(string primeSrcRoot)
  {
    var dir = Path.Combine(primeSrcRoot, "model", "enums");
    if (!Directory.Exists(dir))
    {
      return new HashSet<string>(StringComparer.Ordinal);
    }

    return Directory.GetFiles(dir, "*.cs")
      .Select(f => Path.GetFileNameWithoutExtension(f))
      .Where(s => !string.IsNullOrEmpty(s))
      .Select(s => s!)
      .ToHashSet(StringComparer.Ordinal);
  }
}
