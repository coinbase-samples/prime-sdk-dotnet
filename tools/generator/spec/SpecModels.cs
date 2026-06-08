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

public class ParsedOpenApiDocument
{
  public YamlMappingNode Root { get; init; } = null!;
  public Dictionary<string, ParsedOperation> OperationsById { get; init; } = new();
}

public class ParsedOperation
{
  public string OperationId { get; init; } = "";
  public string HttpMethod { get; init; } = "";
  public string Path { get; init; } = "";
  public List<string> Tags { get; init; } = new();
  public List<ParsedParameter> Parameters { get; init; } = new();
  public YamlMappingNode? RequestBodyJsonSchema { get; init; }
  public string? SuccessResponseSchemaRef { get; init; }

  /// <summary>
  /// HTTP success status codes present in the OpenAPI spec for this operation (e.g. 200, 201).
  /// </summary>
  public List<int> SuccessStatusCodes { get; init; } = new();

  /// <summary>
  /// OpenAPI <c>summary</c> field, used for XML documentation on generated request/response/service members.
  /// </summary>
  public string? Summary { get; init; }

  /// <summary>
  /// Optional extension <c>x-sdk-method-name</c> on the operation, if present in the spec.
  /// </summary>
  public string? ExtensionSdkMethodName { get; init; }
}

public class ParsedParameter
{
  public string Name { get; init; } = "";
  public string In { get; init; } = "";
  public bool Required { get; init; }
  public YamlMappingNode Schema { get; init; } = null!;
}
