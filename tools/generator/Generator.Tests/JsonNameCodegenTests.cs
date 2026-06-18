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

public class JsonNameCodegenTests
{
  [Theory]
  [InlineData("product_id", "ProductId", false)]
  [InlineData("sort_direction", "SortDirection", false)]
  [InlineData("cursor", "Cursor", false)]
  [InlineData("portfolioId", "PortfolioId", false)]
  [InlineData("web3", "OnchainBalance", true)]
  [InlineData("web3_transaction_metadata", "OnchainTransactionMetadata", true)]
  public void NeedsJsonPropertyName_MatchesExpected(string jsonName, string clrName, bool expected)
  {
    Assert.Equal(expected, JsonNameCodegen.NeedsJsonPropertyName(jsonName, clrName));
  }

  [Fact]
  public void StripRedundantJsonPropertyNames_RemovesPolicyCoveredAttributes()
  {
    const string input = """
      [JsonPropertyName("product_id")]
          public string? ProductId { get; set; }

      [JsonPropertyName("portfolioId")]
          public string? PortfolioId { get; set; }

      [JsonPropertyName("web3")]
          public string? OnchainId { get; set; }
      """;

    var output = JsonNameCodegen.StripRedundantJsonPropertyNames(input);

    Assert.DoesNotContain("[JsonPropertyName(\"product_id\")]", output);
    Assert.DoesNotContain("[JsonPropertyName(\"portfolioId\")]", output);
    Assert.Contains("[JsonPropertyName(\"web3\")]", output);
  }

  [Fact]
  public void PostProcessEmittedSource_PreservesWeb3WireNameAfterOnchainRename()
  {
    var transforms = new SharedTransforms(new GeneratorConfiguration());
    const string input = """
      using System.Text.Json.Serialization;

      public class TransactionMetadata
      {
        [JsonPropertyName("web3_transaction_metadata")]
        public Web3TransactionMetadata? Web3TransactionMetadata { get; set; }
      }
      """;

    var renamed = transforms.ApplyWeb3ToOnchainContent(input, "TransactionMetadata");
    var output = JsonNameCodegen.PostProcessEmittedSource(renamed);

    Assert.Contains("[JsonPropertyName(\"web3_transaction_metadata\")]", output);
    Assert.Contains("OnchainTransactionMetadata?", output);
    Assert.DoesNotContain("onchain_transaction_metadata", output);
  }

  [Fact]
  public void ApplyWeb3ToOnchainContent_PreservesJsonPropertyNameWireNames()
  {
    var transforms = new SharedTransforms(new GeneratorConfiguration());
    const string input = """
      public class TransactionMetadata
      {
        [JsonPropertyName("web3_transaction_metadata")]
        public Web3TransactionMetadata? Web3TransactionMetadata { get; set; }
      }
      """;

    var output = transforms.ApplyWeb3ToOnchainContent(input, "TransactionMetadata");

    Assert.Contains("[JsonPropertyName(\"web3_transaction_metadata\")]", output);
    Assert.Contains("OnchainTransactionMetadata?", output);
    Assert.DoesNotContain("onchain_transaction_metadata", output);
  }

  [Fact]
  public void StripUnusedSerializationUsings_RemovesImportWhenNoAttributesRemain()
  {
    const string input = """
      namespace Foo
      {
        using System.Text.Json.Serialization;

        public class Bar
        {
          public string? PortfolioId { get; set; }
        }
      }
      """;

    var output = JsonNameCodegen.StripUnusedSerializationUsings(input);

    Assert.DoesNotContain("using System.Text.Json.Serialization;", output);
  }
}
