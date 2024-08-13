/*
 * Copyright 2024-present Coinbase Global, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Text.Json.Serialization;
  public class MatchMetadata
  {
    [JsonPropertyName("reference_id")]
    public string? ReferenceId { get; set; }

    [JsonPropertyName("settlement_date")]
    public string? SettlementDate { get; set; }

    public MatchMetadata() { }

    public class MatchMetadataBuilder
    {
      private string? ReferenceId;
      private string? SettlementDate;

      public MatchMetadataBuilder() { }

      public MatchMetadataBuilder WithReferenceId(string referenceId)
      {
        this.ReferenceId = referenceId;
        return this;
      }

      public MatchMetadataBuilder WithSettlementDate(string settlementDate)
      {
        this.SettlementDate = settlementDate;
        return this;
      }

      public MatchMetadata Build()
      {
        return new MatchMetadata
        {
          ReferenceId = this.ReferenceId,
          SettlementDate = this.SettlementDate
        };
      }
    }
  }
}
