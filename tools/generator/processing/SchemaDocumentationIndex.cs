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

using CoinbaseSdk.Tools.Generator.Spec;
using YamlDotNet.RepresentationModel;

namespace CoinbaseSdk.Tools.Generator.Processing;

public sealed class SchemaDocEntry
{
  public string ClrName { get; init; } = string.Empty;

  public bool IsEnum { get; init; }

  public string? TypeDoc { get; init; }

  public IReadOnlyDictionary<string, string> PropertyDocs { get; init; } =
    new Dictionary<string, string>(StringComparer.Ordinal);

  public IReadOnlyDictionary<string, string> EnumValueDocs { get; init; } =
    new Dictionary<string, string>(StringComparer.Ordinal);
}

/// <summary>
/// Indexes OpenAPI component schema documentation keyed by emitted CLR type names.
/// </summary>
public sealed class SchemaDocumentationIndex
{
  private readonly Dictionary<string, SchemaDocEntry> _byClrName =
    new(StringComparer.Ordinal);

  public static async Task<SchemaDocumentationIndex> LoadAsync(
    string yamlPath,
    SharedTransforms transforms,
    IReadOnlyDictionary<string, string> commonModels,
    IReadOnlyDictionary<string, string> enumNameMappings)
  {
    var text = await File.ReadAllTextAsync(yamlPath);
    using var reader = new StringReader(text);
    var yaml = new YamlStream();
    yaml.Load(reader);
    var root = (YamlMappingNode)yaml.Documents[0].RootNode;

    if (!root.Children.ContainsKey(new YamlScalarNode("components")))
    {
      return new SchemaDocumentationIndex();
    }

    var components = (YamlMappingNode)root.Children[new YamlScalarNode("components")];
    if (!components.Children.ContainsKey(new YamlScalarNode("schemas")))
    {
      return new SchemaDocumentationIndex();
    }

    var schemas = (YamlMappingNode)components.Children[new YamlScalarNode("schemas")];
    var index = new SchemaDocumentationIndex();

    foreach (var entry in schemas.Children)
    {
      var schemaKey = ((YamlScalarNode)entry.Key).Value!;
      if (entry.Value is not YamlMappingNode schemaNode)
      {
        continue;
      }

      var clrName = DeriveClrName(schemaKey, transforms, commonModels, enumNameMappings);
      if (string.IsNullOrWhiteSpace(clrName))
      {
        continue;
      }

      var isEnum = IsEnumSchema(schemaNode);
      var typeDoc = FirstNonBlank(
        StringFromYaml(schemaNode, "description"),
        StringFromYaml(schemaNode, "title"));

      Dictionary<string, string> propertyDocs = new(StringComparer.Ordinal);
      Dictionary<string, string> enumValueDocs = new(StringComparer.Ordinal);

      if (isEnum)
      {
        var description = StringFromYaml(schemaNode, "description");
        if (!string.IsNullOrWhiteSpace(description))
        {
          foreach (var (key, value) in EnumXmlDocEnhancer.ParseEnumValueDescriptions(description))
          {
            enumValueDocs[key] = value;
          }
        }
      }
      else if (schemaNode.Children.ContainsKey(new YamlScalarNode("properties")))
      {
        var properties = (YamlMappingNode)schemaNode.Children[new YamlScalarNode("properties")];
        foreach (var propEntry in properties.Children)
        {
          var wireName = ((YamlScalarNode)propEntry.Key).Value!;
          if (propEntry.Value is not YamlMappingNode propSchema)
          {
            continue;
          }

          var doc = ResolvePropertyDoc(propSchema, root);
          if (!string.IsNullOrWhiteSpace(doc))
          {
            propertyDocs[wireName] = doc;
          }
        }
      }

      index._byClrName[clrName] = new SchemaDocEntry
      {
        ClrName = clrName,
        IsEnum = isEnum,
        TypeDoc = typeDoc,
        PropertyDocs = propertyDocs,
        EnumValueDocs = enumValueDocs,
      };
    }

    return index;
  }

  public SchemaDocEntry? TryGet(string clrName)
  {
    return _byClrName.TryGetValue(clrName, out var entry) ? entry : null;
  }

  internal static string DeriveClrName(
    string schemaKey,
    SharedTransforms transforms,
    IReadOnlyDictionary<string, string> commonModels,
    IReadOnlyDictionary<string, string> enumNameMappings)
  {
    var clrName = transforms.TransformSchemaRefToClrName(schemaKey);

    foreach (var (source, target) in commonModels)
    {
      if (clrName.Contains(source, StringComparison.Ordinal))
      {
        clrName = target;
        break;
      }
    }

    foreach (var (strippedName, actualEnumName) in enumNameMappings)
    {
      if (string.Equals(clrName, strippedName, StringComparison.Ordinal))
      {
        clrName = actualEnumName;
        break;
      }
    }

    return clrName;
  }

  private static bool IsEnumSchema(YamlMappingNode schemaNode)
  {
    var type = StringFromYaml(schemaNode, "type");
    return string.Equals(type, "string", StringComparison.Ordinal) &&
           schemaNode.Children.ContainsKey(new YamlScalarNode("enum"));
  }

  private static string? ResolvePropertyDoc(YamlMappingNode propSchema, YamlMappingNode root)
  {
    var direct = FirstNonBlank(
      StringFromYaml(propSchema, "description"),
      StringFromYaml(propSchema, "title"));
    if (direct != null)
    {
      return direct;
    }

    if (!propSchema.Children.ContainsKey(new YamlScalarNode("$ref")))
    {
      return null;
    }

    var refValue = ((YamlScalarNode)propSchema.Children[new YamlScalarNode("$ref")]).Value!;
    var resolved = SpecParser.ResolveRef(root, refValue);
    if (resolved == null)
    {
      return null;
    }

    return FirstNonBlank(
      StringFromYaml(resolved, "description"),
      StringFromYaml(resolved, "title"));
  }

  private static string? StringFromYaml(YamlMappingNode node, string key)
  {
    if (!node.Children.ContainsKey(new YamlScalarNode(key)))
    {
      return null;
    }

    var value = node.Children[new YamlScalarNode(key)];
    return value switch
    {
      YamlScalarNode scalar => scalar.Value,
      _ => null,
    };
  }

  private static string? FirstNonBlank(params string?[] values)
  {
    foreach (var value in values)
    {
      if (!string.IsNullOrWhiteSpace(value))
      {
        return value!.Trim();
      }
    }

    return null;
  }
}
