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
using System.Text.RegularExpressions;
using CoinbaseSdk.Tools.Generator.Processing;
using CoinbaseSdk.Tools.Generator.Spec;

namespace CoinbaseSdk.Tools.Generator.Phases;

public static class RequestPhase
{
  private static bool EnumClrReferencesKnownName(string clrType, IReadOnlySet<string> knownEnumTypeNames)
  {
    foreach (var en in knownEnumTypeNames)
    {
      if (Regex.IsMatch(clrType, $@"\b{Regex.Escape(en)}\b"))
      {
        return true;
      }
    }

    return false;
  }

  public static string EmitRequest(
    ParsedOpenApiDocument doc,
    GeneratorConfiguration cfg,
    SharedTransforms transforms,
    SdkOperationBinding b,
    ParsedOperation op,
    IReadOnlySet<string> knownEnumTypeNames)
  {
    string MapParamToClr(ParsedParameter p)
    {
      if (b.ParamTypeOverrides.TryGetValue(p.Name, out var ov) && !string.IsNullOrWhiteSpace(ov))
      {
        return ov;
      }

      return OpenApiSchemaCodegen.ToClrType(doc.Root, p.Schema, transforms, out _, out _);
    }

    string MapQueryParamClr(ParsedParameter p)
    {
      var clr = MapParamToClr(p);
      if (!p.Required &&
          OpenApiSchemaCodegen.TypeIsEnumRef(doc.Root, p.Schema) &&
          !clr.EndsWith("[]", StringComparison.Ordinal))
      {
        return clr.EndsWith("?", StringComparison.Ordinal) ? clr : clr + "?";
      }

      return clr;
    }

    // Path params are always required (they're in the URL); use non-nullable CLR types.
    string MapPathParamToClr(ParsedParameter p)
    {
      var t = MapParamToClr(p);
      return t.TrimEnd('?');
    }

    var svc = NamingResolver.RequireService(cfg, b.Service);
    var ns = NamingResolver.FullNamespace(svc);
    var pathParams = op.Parameters.Where(p => p.In == "path").ToList();
    var queryParams = op.Parameters.Where(p => p.In == "query").ToList();
    var bodyProps = op.RequestBodyJsonSchema != null
      ? OpenApiSchemaCodegen.ListProperties(doc.Root, op.RequestBodyJsonSchema, transforms)
      : new List<SchemaProperty>();

    var paginated = b.ForcePaginated ||
                    queryParams.Any(p =>
                      p.Name is "cursor" or "sort_direction");

    // Pagination query params are modeled on PaginatedRequest (cursor, limit, sort_direction).
    var queryParamsForMembers = paginated
      ? queryParams.Where(p => p.Name is not ("cursor" or "limit" or "sort_direction")).ToList()
      : queryParams;

    var ctorArgs = string.Join(", ", pathParams.Select(p =>
      $"{MapPathParamToClr(p)} {CamelParam(p.Name)}"));

    var usesJsonPropertyName = queryParamsForMembers.Any(p =>
                              JsonNameCodegen.NeedsJsonPropertyName(p.Name, OpenApiSchemaCodegen.ToPascalCase(p.Name))) ||
                            bodyProps.Any(p => JsonNameCodegen.NeedsJsonPropertyName(p.JsonName, p.ClrName));
    var usesJsonSerialization = pathParams.Count > 0 || usesJsonPropertyName;

    var sb = new StringBuilder();
    CopyrightHelper.AppendEmittedCsFileLicense(sb, CopyrightHelper.SdkEmittedCopyrightYear);
    sb.AppendLine($"namespace {ns}");
    sb.AppendLine("{");
    if (usesJsonSerialization)
    {
      sb.AppendLine("  using System.Text.Json.Serialization;");
    }

    sb.AppendLine("  using CoinbaseSdk.Core.Error;");
    if (paginated)
    {
      sb.AppendLine("  using CoinbaseSdk.Prime.Common;");
    }

    // Always include Model.Enums when paginated (SortDirection is in that namespace)
    var overrideUsesEnums = knownEnumTypeNames.Count > 0 &&
                            b.ParamTypeOverrides.Values.Any(clr =>
                              EnumClrReferencesKnownName(clr, knownEnumTypeNames));
    var usesEnums = paginated ||
                   overrideUsesEnums ||
                   pathParams.Concat(queryParamsForMembers).Any(p => p.Schema != null &&
                     OpenApiSchemaCodegen.ToClrType(doc.Root, p.Schema, transforms, out _, out var isEnum) is not null && isEnum) ||
                   bodyProps.Any(p => p.UsesEnum);
    var usesModel = bodyProps.Any(p => p.UsesModel);
    if (usesModel)
    {
      sb.AppendLine("  using CoinbaseSdk.Prime.Model;");
    }

    if (usesEnums)
    {
      sb.AppendLine("  using CoinbaseSdk.Prime.Model.Enums;");
    }

    sb.AppendLine();
    var typeDoc = GeneratorXmlDoc.FormatTypeSummary(op.Summary);
    if (typeDoc.Length > 0)
    {
      sb.Append(typeDoc);
    }

    var basePart = paginated ? " : PaginatedRequest" : string.Empty;
    sb.AppendLine(
      $"  public class {b.SdkMethod}Request({ctorArgs}){basePart}");
    sb.AppendLine("  {");
    var needPropSep = false;
    foreach (var p in pathParams)
    {
      if (needPropSep)
      {
        sb.AppendLine();
      }

      needPropSep = true;
      var pn = OpenApiSchemaCodegen.ToPascalCase(p.Name);
      var cn = CamelParam(p.Name);
      sb.AppendLine("    [JsonIgnore]");
      sb.AppendLine($"    public {MapPathParamToClr(p)} {pn} {{ get; set; }} = {cn};");
    }

    foreach (var p in queryParamsForMembers)
    {
      if (needPropSep)
      {
        sb.AppendLine();
      }

      needPropSep = true;
      var pn = OpenApiSchemaCodegen.ToPascalCase(p.Name);
      var clr = MapQueryParamClr(p);
      JsonNameCodegen.AppendJsonPropertyNameIfNeeded(sb, p.Name, pn, "    ");
      sb.AppendLine($"    public {clr} {pn} {{ get; set; }}{DefaultForQuery(clr)}");
    }

    foreach (var p in bodyProps)
    {
      if (needPropSep)
      {
        sb.AppendLine();
      }

      needPropSep = true;
      JsonNameCodegen.AppendJsonPropertyNameIfNeeded(sb, p.JsonName, p.ClrName, "    ");
      sb.AppendLine($"    public {p.ClrType} {p.ClrName} {{ get; set; }}{DefaultForBody(p)}");
    }

    if (cfg.EmitRequestBuilders)
    {
      sb.AppendLine();
      EmitBuilder(sb, b.SdkMethod, pathParams, queryParamsForMembers, bodyProps, paginated, MapQueryParamClr);
    }

    sb.AppendLine("  }");
    sb.AppendLine("}");
    return JsonNameCodegen.PostProcessEmittedSource(sb.ToString());
  }

  private static string CamelParam(string openapiName)
  {
    var pc = OpenApiSchemaCodegen.ToPascalCase(openapiName);
    return char.ToLowerInvariant(pc[0]) + pc[1..];
  }

  /// <summary>
  /// Backing field for optional builder state; avoids <c>string??</c> when <paramref name="clr"/> is already nullable.
  /// </summary>
  private static string BuilderBackingFieldType(string clr)
  {
    var t = clr.TrimEnd();
    if (t.EndsWith("?", StringComparison.Ordinal))
    {
      return t;
    }

    return t + "?";
  }

  private static string DefaultForQuery(string clr)
  {
    if (clr.EndsWith("[]", StringComparison.Ordinal))
    {
      return " = [];";
    }

    return string.Empty;
  }

  private static string DefaultForBody(SchemaProperty p)
  {
    if (p.ClrType.EndsWith("[]", StringComparison.Ordinal))
    {
      return " = [];";
    }

    return string.Empty;
  }

  private static void EmitBuilder(
    StringBuilder sb,
    string sdkMethod,
    List<ParsedParameter> pathParams,
    List<ParsedParameter> queryParams,
    List<SchemaProperty> bodyProps,
    bool paginated,
    Func<ParsedParameter, string> mapParam)
  {
    // Nested builder name matches historical SDK / example pattern (e.g. CreateOrderRequestBuilder).
    var nestedBuilder = $"{sdkMethod}RequestBuilder";

    // Builder backing fields: path params are non-nullable (required), query/body optional.
    string PathFieldType(ParsedParameter p) => mapParam(p).TrimEnd('?') + "?";

    sb.AppendLine($"    public class {nestedBuilder}");
    sb.AppendLine("    {");
    foreach (var p in pathParams)
    {
      var f = "_" + CamelParam(p.Name);
      sb.AppendLine($"      private {PathFieldType(p)} {f};");
    }

    foreach (var p in queryParams)
    {
      var f = "_" + CamelParam(p.Name);
      sb.AppendLine($"      private {BuilderBackingFieldType(mapParam(p))} {f};");
    }

    foreach (var p in bodyProps)
    {
      var field = "_" + char.ToLowerInvariant(p.ClrName[0]) + p.ClrName[1..];
      sb.AppendLine($"      private {p.ClrType} {field};");
    }

    if (paginated)
    {
      sb.AppendLine("      private string? _cursor;");
      sb.AppendLine("      private SortDirection? _sortDirection;");
      sb.AppendLine("      private int? _limit;");
    }

    sb.AppendLine();
    foreach (var p in pathParams)
    {
      var pn = OpenApiSchemaCodegen.ToPascalCase(p.Name);
      var f = "_" + CamelParam(p.Name);
      var t = mapParam(p).TrimEnd('?');
      var paramVar = CamelParam(p.Name);
      sb.AppendLine($"      public {nestedBuilder} With{pn}({t} {paramVar})");
      sb.AppendLine("      {");
      sb.AppendLine($"        {f} = {paramVar};");
      sb.AppendLine("        return this;");
      sb.AppendLine("      }");
      sb.AppendLine();
    }

    foreach (var p in queryParams)
    {
      var pn = OpenApiSchemaCodegen.ToPascalCase(p.Name);
      var f = "_" + CamelParam(p.Name);
      var paramVar = CamelParam(p.Name);
      sb.AppendLine($"      public {nestedBuilder} With{pn}({mapParam(p)} {paramVar})");
      sb.AppendLine("      {");
      sb.AppendLine($"        {f} = {paramVar};");
      sb.AppendLine("        return this;");
      sb.AppendLine("      }");
      sb.AppendLine();
    }

    foreach (var p in bodyProps)
    {
      var f = "_" + char.ToLowerInvariant(p.ClrName[0]) + p.ClrName[1..];
      var paramVar = char.ToLowerInvariant(p.ClrName[0]) + p.ClrName[1..];
      sb.AppendLine($"      public {nestedBuilder} With{p.ClrName}({p.ClrType} {paramVar})");
      sb.AppendLine("      {");
      sb.AppendLine($"        {f} = {paramVar};");
      sb.AppendLine("        return this;");
      sb.AppendLine("      }");
      sb.AppendLine();
    }

    if (paginated)
    {
      sb.AppendLine($"      public {nestedBuilder} WithCursor(string cursor)");
      sb.AppendLine("      {");
      sb.AppendLine("        _cursor = cursor;");
      sb.AppendLine("        return this;");
      sb.AppendLine("      }");
      sb.AppendLine();
      sb.AppendLine($"      public {nestedBuilder} WithSortDirection(SortDirection sortDirection)");
      sb.AppendLine("      {");
      sb.AppendLine("        _sortDirection = sortDirection;");
      sb.AppendLine("        return this;");
      sb.AppendLine("      }");
      sb.AppendLine();
      sb.AppendLine($"      public {nestedBuilder} WithLimit(int limit)");
      sb.AppendLine("      {");
      sb.AppendLine("        _limit = limit;");
      sb.AppendLine("        return this;");
      sb.AppendLine("      }");
      sb.AppendLine();
    }

    sb.AppendLine("      private void Validate()");
    sb.AppendLine("      {");
    foreach (var p in pathParams.Where(x => x.Required))
    {
      var pn = OpenApiSchemaCodegen.ToPascalCase(p.Name);
      var f = "_" + CamelParam(p.Name);
      var clr = mapParam(p);
      if (clr == "string?" || clr == "string")
      {
        sb.AppendLine($"        if (string.IsNullOrWhiteSpace({f}))");
        sb.AppendLine("        {");
        sb.AppendLine($"          throw new CoinbaseClientException(\"{pn} is required\");");
        sb.AppendLine("        }");
      }
    }

    sb.AppendLine("      }");
    sb.AppendLine();
    sb.AppendLine($"      public {sdkMethod}Request Build()");
    sb.AppendLine("      {");
    sb.AppendLine("        Validate();");
    var ctor = string.Join(", ", pathParams.Select(p =>
    {
      var f = "_" + CamelParam(p.Name);
      return $"{f}!";
    }));
    sb.AppendLine($"        return new {sdkMethod}Request({ctor})");
    sb.AppendLine("        {");
    foreach (var p in queryParams)
    {
      var pn = OpenApiSchemaCodegen.ToPascalCase(p.Name);
      var f = "_" + CamelParam(p.Name);
      var clr = mapParam(p);
      if (clr.EndsWith("[]", StringComparison.Ordinal))
      {
        sb.AppendLine($"          {pn} = {f} ?? [],");
      }
      else
      {
        sb.AppendLine($"          {pn} = {f},");
      }
    }

    foreach (var p in bodyProps)
    {
      var f = "_" + char.ToLowerInvariant(p.ClrName[0]) + p.ClrName[1..];
      if (p.ClrType.EndsWith("[]", StringComparison.Ordinal))
      {
        sb.AppendLine($"          {p.ClrName} = {f} ?? [],");
      }
      else
      {
        sb.AppendLine($"          {p.ClrName} = {f},");
      }
    }

    if (paginated)
    {
      sb.AppendLine("          Cursor = _cursor,");
      sb.AppendLine("          SortDirection = _sortDirection,");
      sb.AppendLine("          Limit = _limit,");
    }

    sb.AppendLine("        };");
    sb.AppendLine("      }");
    sb.AppendLine("    }");
  }
}
