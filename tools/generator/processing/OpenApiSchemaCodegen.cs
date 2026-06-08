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

using System.Linq;
using System.Text;
using CoinbaseSdk.Tools.Generator.Spec;
using YamlDotNet.RepresentationModel;

namespace CoinbaseSdk.Tools.Generator.Processing;

public static class OpenApiSchemaCodegen
{
  public static string ToClrType(
    YamlMappingNode documentRoot,
    YamlNode? node,
    SharedTransforms transforms,
    out bool needsModelNs,
    out bool needsEnumNs)
  {
    var model = new HashSet<string>();
    var enums = new HashSet<string>();
    var t = MapType(documentRoot, node, transforms, model, enums);
    needsModelNs = model.Count > 0;
    needsEnumNs = enums.Count > 0;
    return t;
  }

  private static string MapType(
    YamlMappingNode documentRoot,
    YamlNode? node,
    SharedTransforms transforms,
    HashSet<string> modelTypes,
    HashSet<string> enumTypes)
  {
    if (node == null)
    {
      return "object?";
    }

    if (node is YamlMappingNode mm)
    {
      if (mm.Children.ContainsKey(new YamlScalarNode("$ref")))
      {
        var r = ((YamlScalarNode)mm.Children[new YamlScalarNode("$ref")]).Value!;
        var resolved = SpecParser.ResolveRef(documentRoot, r);
        var refName = SpecParser.GetRefName(r);
        if (resolved == null || refName == null)
        {
          return "object?";
        }

        if (resolved.Children.ContainsKey(new YamlScalarNode("enum")))
        {
          var clr = transforms.TransformSchemaRefToClrName(refName);
          enumTypes.Add(clr);
          return clr;
        }

        var clrModel = transforms.TransformSchemaRefToClrName(refName);
        modelTypes.Add(clrModel);
        return clrModel;
      }

      if (mm.Children.ContainsKey(new YamlScalarNode("type")))
      {
        var type = ((YamlScalarNode)mm.Children[new YamlScalarNode("type")]).Value!;
        if (type == "array")
        {
          var items = mm.Children[new YamlScalarNode("items")];
          if (items is YamlMappingNode itemsMm)
          {
            var matched = FindMatchingNamedEnum(documentRoot, itemsMm, transforms);
            if (matched != null)
            {
              enumTypes.Add(matched);
              return matched + "[]";
            }
          }

          var inner = MapType(documentRoot, items, transforms, modelTypes, enumTypes);
          return NormalizeArrayElementType(inner) + "[]";
        }

        return MapPrimitive(mm, type);
      }

      if (mm.Children.ContainsKey(new YamlScalarNode("oneOf")) ||
          mm.Children.ContainsKey(new YamlScalarNode("anyOf")))
      {
        return "object?";
      }
    }

    return "object?";
  }

  private static HashSet<string> GetInlineEnumValues(YamlMappingNode node)
  {
    if (!node.Children.ContainsKey(new YamlScalarNode("enum")))
    {
      return [];
    }

    return ((YamlSequenceNode)node.Children[new YamlScalarNode("enum")])
      .Children.OfType<YamlScalarNode>()
      .Select(s => s.Value!)
      .ToHashSet(StringComparer.Ordinal);
  }

  private static string? FindMatchingNamedEnum(
    YamlMappingNode documentRoot,
    YamlMappingNode inlineNode,
    SharedTransforms transforms)
  {
    var inlineValues = GetInlineEnumValues(inlineNode);
    if (inlineValues.Count == 0)
    {
      return null;
    }

    if (!documentRoot.Children.ContainsKey(new YamlScalarNode("components")))
    {
      return null;
    }

    var schemas = (YamlMappingNode)((YamlMappingNode)documentRoot.Children[new YamlScalarNode("components")])
      .Children[new YamlScalarNode("schemas")];
    foreach (var (key, value) in schemas.Children)
    {
      if (value is not YamlMappingNode schema)
      {
        continue;
      }

      if (!schema.Children.ContainsKey(new YamlScalarNode("enum")))
      {
        continue;
      }

      var schemaValues = GetInlineEnumValues(schema);
      if (inlineValues.IsSubsetOf(schemaValues))
      {
        return transforms.TransformSchemaRefToClrName(((YamlScalarNode)key).Value!);
      }
    }

    return null;
  }

  private static string MapPrimitive(YamlMappingNode mm, string type)
  {
    return type switch
    {
      "string" => "string?",
      "integer" => FormatInteger(mm),
      "number" => "string?",
      "boolean" => "bool?",
      "object" => "object?",
      _ => "string?"
    };
  }

  private static string FormatInteger(YamlMappingNode mm)
  {
    if (mm.Children.ContainsKey(new YamlScalarNode("format")))
    {
      var f = ((YamlScalarNode)mm.Children[new YamlScalarNode("format")]).Value;
      if (f == "int64")
      {
        return "long?";
      }
    }

    return "int?";
  }

  public static List<SchemaProperty> ListProperties(
    YamlMappingNode documentRoot,
    YamlMappingNode? schema,
    SharedTransforms transforms)
  {
    var list = new List<SchemaProperty>();
    if (schema == null || !schema.Children.ContainsKey(new YamlScalarNode("properties")))
    {
      return list;
    }

    var props = (YamlMappingNode)schema.Children[new YamlScalarNode("properties")];
    foreach (var prop in props.Children)
    {
      var jsonName = ((YamlScalarNode)prop.Key).Value!;
      var pnode = prop.Value;
      var clrName = ToPascalCase(jsonName);
      var model = new HashSet<string>();
      var enums = new HashSet<string>();
      var clr = MapType(documentRoot, pnode, transforms, model, enums);
      var required = schema.Children.ContainsKey(new YamlScalarNode("required")) &&
                     ((YamlSequenceNode)schema.Children[new YamlScalarNode("required")]).Children
                     .Any(n => ((YamlScalarNode)n).Value == jsonName);
      var usesEnum = enums.Count > 0;
      if (!required && usesEnum && !clr.EndsWith("[]", StringComparison.Ordinal))
      {
        clr = OptionalScalarEnumClr(clr);
      }

      list.Add(new SchemaProperty(jsonName, clrName, clr, required, model.Count > 0, usesEnum));
    }

    return list;
  }

  /// <summary>
  /// Builds a CLR identifier from OpenAPI names that may use underscores and dotted segments (e.g. <c>network.id</c> → <c>NetworkId</c>).
  /// </summary>
  public static string ToPascalCase(string snakeOrKebabOrDotted)
  {
    var segments = snakeOrKebabOrDotted.Split('.', StringSplitOptions.RemoveEmptyEntries);
    var sb = new StringBuilder();
    foreach (var segment in segments)
    {
      var parts = segment.Split('_', StringSplitOptions.RemoveEmptyEntries);
      foreach (var p in parts)
      {
        if (p.Length == 0)
        {
          continue;
        }

        sb.Append(char.ToUpperInvariant(p[0]));
        if (p.Length > 1)
        {
          sb.Append(p.AsSpan(1));
        }
      }
    }

    var s = sb.ToString();
    return s.Length == 0 ? snakeOrKebabOrDotted : s;
  }

  /// <summary>
  /// OpenAPI <c>string</c> items are modeled as <c>string?</c>; for arrays we use non-nullable elements (<c>string[]</c>) to match handwritten SDK conventions.
  /// </summary>
  private static string NormalizeArrayElementType(string elementClrType)
  {
    return elementClrType switch
    {
      "string?" => "string",
      _ => elementClrType
    };
  }

  /// <summary>
  /// Optional body/query enum scalars must be nullable so default is omitted from JSON instead of serializing enum value 0.
  /// </summary>
  private static string OptionalScalarEnumClr(string clr)
  {
    if (clr.EndsWith("?", StringComparison.Ordinal))
    {
      return clr;
    }

    return clr + "?";
  }

  /// <summary>
  /// Returns true when the schema resolves to an enum <c>$ref</c> (including <c>array</c> of enum).
  /// </summary>
  public static bool TypeIsEnumRef(
    YamlMappingNode documentRoot,
    YamlNode? node)
  {
    if (node == null)
    {
      return false;
    }

    if (node is YamlMappingNode mm && mm.Children.ContainsKey(new YamlScalarNode("type")))
    {
      var type = ((YamlScalarNode)mm.Children[new YamlScalarNode("type")]).Value!;
      if (type == "array" && mm.Children.ContainsKey(new YamlScalarNode("items")))
      {
        return TypeIsEnumRef(documentRoot, mm.Children[new YamlScalarNode("items")]);
      }

      if (type == "string" && mm.Children.ContainsKey(new YamlScalarNode("enum")))
      {
        return true;
      }
    }

    if (node is YamlMappingNode mm2 && mm2.Children.ContainsKey(new YamlScalarNode("$ref")))
    {
      var r = ((YamlScalarNode)mm2.Children[new YamlScalarNode("$ref")]).Value!;
      var resolved = SpecParser.ResolveRef(documentRoot, r);
      return resolved?.Children.ContainsKey(new YamlScalarNode("enum")) == true;
    }

    return false;
  }
}

public record SchemaProperty(
  string JsonName,
  string ClrName,
  string ClrType,
  bool Required,
  bool UsesModel,
  bool UsesEnum);
