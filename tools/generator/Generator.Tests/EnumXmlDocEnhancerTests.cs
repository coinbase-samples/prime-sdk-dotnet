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

public class EnumXmlDocEnhancerTests
{
  [Fact]
  public void ParseEnumValueDescriptions_ParsesBulletLines()
  {
    const string description = """
      - UNKNOWN_ORDER_SIDE: nil value
       - BUY: Buy order
       - SELL: Sell order
      """;

    var parsed = EnumXmlDocEnhancer.ParseEnumValueDescriptions(description);

    Assert.Equal("nil value", parsed["UNKNOWN_ORDER_SIDE"]);
    Assert.Equal("Buy order", parsed["BUY"]);
    Assert.Equal("Sell order", parsed["SELL"]);
  }

  [Fact]
  public void Apply_InsertsTypeAndMemberDocs()
  {
    const string source = """
      namespace CoinbaseSdk.Prime.Model.Enums
      {
        public enum OrderSide
        {
          BUY,
          SELL
        }
      }
      """;

    var index = BuildIndex(
      "OrderSide",
      isEnum: true,
      typeDoc: "Order side",
      enumValueDocs: new Dictionary<string, string>
      {
        ["BUY"] = "Buy order",
        ["SELL"] = "Sell order",
      });

    var result = EnumXmlDocEnhancer.Apply(source, "OrderSide", index);

    Assert.Contains("/// Order side.", result);
    Assert.Contains("/// Buy order.", result);
    Assert.Contains("/// Sell order.", result);
  }

  [Fact]
  public void Apply_InsertsTypeDocBeforeAttributes()
  {
    const string source = """
      namespace CoinbaseSdk.Prime.Model.Enums
      {
        [JsonConverter(typeof(JsonStringEnumConverter<OrderSide>))]
        public enum OrderSide
        {
          BUY,
        }
      }
      """;

    var index = BuildIndex("OrderSide", isEnum: true, typeDoc: "Order side");

    var result = EnumXmlDocEnhancer.Apply(source, "OrderSide", index);

    var lines = result.Split('\n');
    var summaryLine = Array.FindIndex(lines, line => line.Contains("Order side", StringComparison.Ordinal));
    var converterLine = Array.FindIndex(lines, line => line.Contains("[JsonConverter", StringComparison.Ordinal));
    var enumLine = Array.FindIndex(lines, line => line.Contains("public enum OrderSide", StringComparison.Ordinal));

    Assert.True(summaryLine >= 0);
    Assert.True(converterLine > summaryLine);
    Assert.True(enumLine > converterLine);
  }

  private static SchemaDocumentationIndex BuildIndex(
    string clrName,
    bool isEnum,
    string? typeDoc = null,
    IReadOnlyDictionary<string, string>? enumValueDocs = null,
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
        IsEnum = isEnum,
        TypeDoc = typeDoc,
        PropertyDocs = propertyDocs ?? new Dictionary<string, string>(StringComparer.Ordinal),
        EnumValueDocs = enumValueDocs ?? new Dictionary<string, string>(StringComparer.Ordinal),
      },
    });
    return index;
  }
}
