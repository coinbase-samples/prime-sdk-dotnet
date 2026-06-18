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

namespace CoinbaseSdk.Tools.Generator.Phases;

public static class ResponsePhase
{
  public static string EmitResponse(
    ParsedOpenApiDocument doc,
    GeneratorConfiguration cfg,
    SharedTransforms transforms,
    SdkOperationBinding b,
    ParsedOperation op)
  {
    var ns = NamingResolver.FullNamespace(NamingResolver.RequireService(cfg, b.Service));
    var sb = new StringBuilder();
    CopyrightHelper.AppendEmittedCsFileLicense(sb, CopyrightHelper.SdkEmittedCopyrightYear);
    sb.AppendLine($"namespace {ns}");
    sb.AppendLine("{");

    if (string.IsNullOrEmpty(op.SuccessResponseSchemaRef))
    {
      var emptyDoc = GeneratorXmlDoc.FormatTypeSummary(op.Summary);
      if (emptyDoc.Length > 0)
      {
        sb.Append(emptyDoc);
      }

      sb.AppendLine($"  public class {b.SdkMethod}Response");
      sb.AppendLine("  {");
      sb.AppendLine($"    public {b.SdkMethod}Response() {{ }}");
      sb.AppendLine("  }");
      sb.AppendLine("}");
      return JsonNameCodegen.PostProcessEmittedSource(sb.ToString());
    }

    var schema = SpecParser.ResolveRef(doc.Root, op.SuccessResponseSchemaRef);
    var props = OpenApiSchemaCodegen.ListProperties(doc.Root, schema, transforms);
    var useModel = props.Any(p => p.UsesModel);
    var useEnums = props.Any(p => p.UsesEnum);
    // Pagination is in CoinbaseSdk.Prime.Common, not Model
    var useCommon = props.Any(p =>
      string.Equals(p.ClrType, "Pagination", StringComparison.Ordinal) ||
      string.Equals(p.ClrType, "Pagination?", StringComparison.Ordinal));
    var useJson = props.Any(p => JsonNameCodegen.NeedsJsonPropertyName(p.JsonName, p.ClrName));
    if (useJson)
    {
      sb.AppendLine("  using System.Text.Json.Serialization;");
    }

    if (useCommon)
    {
      sb.AppendLine("  using CoinbaseSdk.Prime.Common;");
    }

    if (useModel)
    {
      sb.AppendLine("  using CoinbaseSdk.Prime.Model;");
    }

    if (useEnums)
    {
      sb.AppendLine("  using CoinbaseSdk.Prime.Model.Enums;");
    }

    if (useJson || useCommon || useModel || useEnums)
    {
      sb.AppendLine();
    }

    var respDoc = GeneratorXmlDoc.FormatTypeSummary(op.Summary);
    if (respDoc.Length > 0)
    {
      sb.Append(respDoc);
    }

    sb.AppendLine($"  public class {b.SdkMethod}Response");
    sb.AppendLine("  {");
    var needRespSep = false;
    foreach (var p in props)
    {
      if (needRespSep)
      {
        sb.AppendLine();
      }

      needRespSep = true;
      JsonNameCodegen.AppendJsonPropertyNameIfNeeded(sb, p.JsonName, p.ClrName, "    ");
      var def = DefaultForResponseProperty(p.ClrType);
      sb.AppendLine($"    public {p.ClrType} {p.ClrName} {{ get; set; }}{def}");
    }

    sb.AppendLine();
    sb.AppendLine($"    public {b.SdkMethod}Response() {{ }}");
    sb.AppendLine("  }");
    sb.AppendLine("}");
    return JsonNameCodegen.PostProcessEmittedSource(sb.ToString());
  }

  private static string DefaultForResponseProperty(string clrType)
  {
    if (clrType.EndsWith("[]", StringComparison.Ordinal))
    {
      return " = [];";
    }

    return string.Empty;
  }
}
