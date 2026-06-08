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
using Xunit;

namespace CoinbaseSdk.Tools.Generator.Tests;

public class ModelXmlDocEnhancerTests
{
  [Fact]
  public void Apply_InsertsClassAndPropertyDocs()
  {
    const string source = """
      namespace CoinbaseSdk.Prime.Model
      {
        public class Sample
        {
          public string? Name { get; set; }
          public string? Side { get; set; }
        }
      }
      """;

    var index = BuildIndex(
      "Sample",
      typeDoc: "Sample model description",
      propertyDocs: new Dictionary<string, string>(StringComparer.Ordinal)
      {
        ["name"] = "Legal name",
        ["side"] = "Order side",
      });

    var result = ModelXmlDocEnhancer.Apply(source, "Sample", index);

    Assert.Contains("/// Sample model description.", result);
    Assert.Contains("/// Legal name.", result);
    Assert.Contains("/// Order side.", result);
  }

  [Fact]
  public void Apply_SkipsPropertiesWithExistingDocs()
  {
    const string source = """
      namespace CoinbaseSdk.Prime.Model
      {
        public class Sample
        {
          /// <summary>
          /// Existing doc.
          /// </summary>
          public string? Name { get; set; }
        }
      }
      """;

    var index = BuildIndex(
      "Sample",
      propertyDocs: new Dictionary<string, string>(StringComparer.Ordinal)
      {
        ["name"] = "Should not overwrite",
      });

    var result = ModelXmlDocEnhancer.Apply(source, "Sample", index);

    Assert.Contains("/// Existing doc.", result);
    Assert.DoesNotContain("Should not overwrite", result);
  }

  [Fact]
  public void Apply_UsesJsonPropertyNameForWireLookup()
  {
    const string source = """
      namespace CoinbaseSdk.Prime.Model
      {
        public class Sample
        {
          [JsonPropertyName("product_id")]
          public string? ProductId { get; set; }
        }
      }
      """;

    var index = BuildIndex(
      "Sample",
      propertyDocs: new Dictionary<string, string>(StringComparer.Ordinal)
      {
        ["product_id"] = "The product ID",
      });

    var result = ModelXmlDocEnhancer.Apply(source, "Sample", index);

    Assert.Contains("/// The product ID.", result);
  }

  private static SchemaDocumentationIndex BuildIndex(
    string clrName,
    string? typeDoc = null,
    IReadOnlyDictionary<string, string>? propertyDocs = null)
  {
    var field = typeof(SchemaDocumentationIndex).GetField(
      "_byClrName",
      System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
    var index = new SchemaDocumentationIndex();
    field.SetValue(index, new Dictionary<string, SchemaDocEntry>(StringComparer.Ordinal)
    {
      [clrName] = new SchemaDocEntry
      {
        ClrName = clrName,
        TypeDoc = typeDoc,
        PropertyDocs = propertyDocs ?? new Dictionary<string, string>(StringComparer.Ordinal),
      },
    });
    return index;
  }
}
