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

using YamlDotNet.RepresentationModel;

namespace CoinbaseSdk.Tools.Generator.Spec;

public static class SpecParser
{
  private static readonly string[] HttpVerbs = { "get", "post", "put", "patch", "delete", "head", "options" };

  public static async Task<ParsedOpenApiDocument> LoadAsync(string yamlPath)
  {
    var text = await File.ReadAllTextAsync(yamlPath);
    using var reader = new StringReader(text);
    var yaml = new YamlStream();
    yaml.Load(reader);
    var root = (YamlMappingNode)yaml.Documents[0].RootNode;
    var ops = new Dictionary<string, ParsedOperation>(StringComparer.Ordinal);
    var paths = (YamlMappingNode)root.Children[new YamlScalarNode("paths")];
    foreach (var pathEntry in paths.Children)
    {
      var pathKey = ((YamlScalarNode)pathEntry.Key).Value!;
      var pathItem = (YamlMappingNode)pathEntry.Value;
      foreach (var methodEntry in pathItem.Children)
      {
        var methodKey = ((YamlScalarNode)methodEntry.Key).Value!.ToLowerInvariant();
        if (!HttpVerbs.Contains(methodKey))
        {
          continue;
        }

        var opNode = (YamlMappingNode)methodEntry.Value;
        if (!opNode.Children.ContainsKey(new YamlScalarNode("operationId")))
        {
          continue;
        }

        var operationId = ((YamlScalarNode)opNode.Children[new YamlScalarNode("operationId")]).Value!;
        string? summary = null;
        if (opNode.Children.ContainsKey(new YamlScalarNode("summary")))
        {
          summary = ((YamlScalarNode)opNode.Children[new YamlScalarNode("summary")]).Value;
        }

        var tags = new List<string>();
        if (opNode.Children.ContainsKey(new YamlScalarNode("tags")))
        {
          var tagSeq = (YamlSequenceNode)opNode.Children[new YamlScalarNode("tags")];
          foreach (var t in tagSeq.Children)
          {
            if (t is YamlScalarNode sn && !string.IsNullOrEmpty(sn.Value))
            {
              tags.Add(sn.Value);
            }
          }
        }

        var parameters = new List<ParsedParameter>();
        if (opNode.Children.ContainsKey(new YamlScalarNode("parameters")))
        {
          var plist = (YamlSequenceNode)opNode.Children[new YamlScalarNode("parameters")];
          foreach (var p in plist.Children)
          {
            var pm = (YamlMappingNode)p;
            var name = ((YamlScalarNode)pm.Children[new YamlScalarNode("name")]).Value!;
            var inn = ((YamlScalarNode)pm.Children[new YamlScalarNode("in")]).Value!;
            var req = pm.Children.ContainsKey(new YamlScalarNode("required")) &&
                      ((YamlScalarNode)pm.Children[new YamlScalarNode("required")]).Value == "true";
            var schema = (YamlMappingNode)pm.Children[new YamlScalarNode("schema")];
            parameters.Add(new ParsedParameter
            {
              Name = name,
              In = inn,
              Required = req,
              Schema = schema
            });
          }
        }

        YamlMappingNode? bodySchema = null;
        if (opNode.Children.ContainsKey(new YamlScalarNode("requestBody")))
        {
          var rb = (YamlMappingNode)opNode.Children[new YamlScalarNode("requestBody")];
          if (rb.Children.ContainsKey(new YamlScalarNode("content")))
          {
            var content = (YamlMappingNode)rb.Children[new YamlScalarNode("content")];
            if (content.Children.ContainsKey(new YamlScalarNode("application/json")))
            {
              var appJson = (YamlMappingNode)content.Children[new YamlScalarNode("application/json")];
              if (appJson.Children.ContainsKey(new YamlScalarNode("schema")))
              {
                var sch = appJson.Children[new YamlScalarNode("schema")];
                bodySchema = ResolveSchemaNode(root, sch);
              }
            }
          }
        }

        string? successRef = null;
        var successStatusCodes = new List<int>();
        if (opNode.Children.ContainsKey(new YamlScalarNode("responses")))
        {
          var responses = (YamlMappingNode)opNode.Children[new YamlScalarNode("responses")];
          foreach (var (codeStr, codeInt) in new[]
                   { ("200", 200), ("201", 201), ("202", 202), ("204", 204) })
          {
            if (!responses.Children.ContainsKey(new YamlScalarNode(codeStr)))
            {
              continue;
            }

            var resp = (YamlMappingNode)responses.Children[new YamlScalarNode(codeStr)];
            if (codeInt == 204)
            {
              successStatusCodes.Add(codeInt);
              continue;
            }

            if (!resp.Children.ContainsKey(new YamlScalarNode("content")))
            {
              continue;
            }

            var respContent = (YamlMappingNode)resp.Children[new YamlScalarNode("content")];
            if (!respContent.Children.ContainsKey(new YamlScalarNode("application/json")))
            {
              continue;
            }

            var aj = (YamlMappingNode)respContent.Children[new YamlScalarNode("application/json")];
            if (!aj.Children.ContainsKey(new YamlScalarNode("schema")))
            {
              continue;
            }

            successStatusCodes.Add(codeInt);
            if (successRef == null)
            {
              var schemaNode = aj.Children[new YamlScalarNode("schema")];
              successRef = ExtractRef(schemaNode);
            }
          }
        }

        string? extensionSdkMethodName = null;
        if (opNode.Children.ContainsKey(new YamlScalarNode("x-sdk-method-name")))
        {
          var ext = opNode.Children[new YamlScalarNode("x-sdk-method-name")];
          if (ext is YamlScalarNode extSn && !string.IsNullOrEmpty(extSn.Value))
          {
            extensionSdkMethodName = extSn.Value;
          }
        }

        ops[operationId] = new ParsedOperation
        {
          OperationId = operationId,
          HttpMethod = methodKey.ToUpperInvariant(),
          Path = pathKey,
          Tags = tags,
          Parameters = parameters,
          RequestBodyJsonSchema = bodySchema,
          SuccessResponseSchemaRef = successRef,
          SuccessStatusCodes = successStatusCodes,
          Summary = summary,
          ExtensionSdkMethodName = extensionSdkMethodName
        };
      }
    }

    return new ParsedOpenApiDocument { Root = root, OperationsById = ops };
  }

  private static string? ExtractRef(YamlNode schemaNode)
  {
    if (schemaNode is YamlMappingNode mm && mm.Children.ContainsKey(new YamlScalarNode("$ref")))
    {
      return ((YamlScalarNode)mm.Children[new YamlScalarNode("$ref")]).Value;
    }

    return null;
  }

  private static YamlMappingNode? ResolveSchemaNode(YamlMappingNode root, YamlNode schemaNode)
  {
    if (schemaNode is YamlMappingNode mm)
    {
      if (mm.Children.ContainsKey(new YamlScalarNode("$ref")))
      {
        var r = ((YamlScalarNode)mm.Children[new YamlScalarNode("$ref")]).Value!;
        return ResolveRef(root, r);
      }

      return mm;
    }

    return null;
  }

  public static YamlMappingNode? ResolveRef(YamlMappingNode root, string refValue)
  {
    if (!refValue.StartsWith("#/components/schemas/", StringComparison.Ordinal))
    {
      return null;
    }

    var name = refValue["#/components/schemas/".Length..];
    var components = (YamlMappingNode)root.Children[new YamlScalarNode("components")];
    var schemas = (YamlMappingNode)components.Children[new YamlScalarNode("schemas")];
    var key = new YamlScalarNode(name);
    if (!schemas.Children.ContainsKey(key))
    {
      return null;
    }

    return (YamlMappingNode)schemas.Children[key];
  }

  public static string? GetRefName(string? refValue)
  {
    if (string.IsNullOrEmpty(refValue) || !refValue.StartsWith("#/components/schemas/", StringComparison.Ordinal))
    {
      return null;
    }

    return refValue["#/components/schemas/".Length..];
  }
}
