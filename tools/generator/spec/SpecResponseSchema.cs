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

/// <summary>
/// Optional helpers for inspecting resolved success-response schemas. Pagination for generated
/// requests is driven primarily by query parameters and <c>operations-overrides.json</c>
/// (<c>forcePaginated</c>) because POST bodies may embed cursor fields without query params.
/// </summary>
public static class SpecResponseSchema
{
  private static readonly string[] PaginationPropertyNames =
  {
    "pagination",
    "has_next",
    "hasNext",
    "next_cursor",
    "nextCursor",
    "cursor"
  };

  public static bool ResponseSchemaSuggestsPagination(
    YamlMappingNode root,
    ParsedOperation op)
  {
    var resolved = TryResolveSuccessResponseSchema(root, op);
    if (resolved == null)
    {
      return false;
    }

    return SchemaNodeSuggestsPagination(root, resolved);
  }

  public static YamlMappingNode? TryResolveSuccessResponseSchema(
    YamlMappingNode root,
    ParsedOperation op)
  {
    if (string.IsNullOrEmpty(op.SuccessResponseSchemaRef))
    {
      return null;
    }

    var node = SpecParser.ResolveRef(root, op.SuccessResponseSchemaRef);
    return node == null ? null : UnwrapSchemaObject(root, node);
  }

  private static YamlMappingNode? UnwrapSchemaObject(YamlMappingNode root, YamlMappingNode schema)
  {
    if (schema.Children.ContainsKey(new YamlScalarNode("allOf")))
    {
      var allOf = (YamlSequenceNode)schema.Children[new YamlScalarNode("allOf")];
      foreach (var child in allOf.Children)
      {
        if (child is YamlMappingNode mm)
        {
          var inner = mm.Children.ContainsKey(new YamlScalarNode("$ref"))
            ? SpecParser.ResolveRef(root, ((YamlScalarNode)mm.Children[new YamlScalarNode("$ref")]).Value!)
            : mm;
          if (inner != null && inner.Children.ContainsKey(new YamlScalarNode("properties")))
          {
            return inner;
          }
        }
      }
    }

    return schema;
  }

  private static bool SchemaNodeSuggestsPagination(YamlMappingNode root, YamlMappingNode schema)
  {
    if (!schema.Children.ContainsKey(new YamlScalarNode("properties")))
    {
      return false;
    }

    var props = (YamlMappingNode)schema.Children[new YamlScalarNode("properties")];
    foreach (var hint in PaginationPropertyNames)
    {
      if (!props.Children.ContainsKey(new YamlScalarNode(hint)))
      {
        continue;
      }

      var propSchema = props.Children[new YamlScalarNode(hint)];
      if (hint == "pagination" && propSchema is YamlMappingNode pm)
      {
        if (pm.Children.ContainsKey(new YamlScalarNode("$ref")))
        {
          var r = ((YamlScalarNode)pm.Children[new YamlScalarNode("$ref")]).Value!;
          if (r.Contains("PaginatedResponse", StringComparison.Ordinal))
          {
            return true;
          }
        }
      }
      else if (hint is "has_next" or "next_cursor" or "cursor" or "hasNext" or "nextCursor")
      {
        return true;
      }
    }

    return false;
  }
}
