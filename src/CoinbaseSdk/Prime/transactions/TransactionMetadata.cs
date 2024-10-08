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
  public class TransactionMetadata
  {
    [JsonPropertyName("match_metadata")]
    public MatchMetadata? MatchMetadata { get; set; }

    public TransactionMetadata() { }

    public class TransactionMetadataBuilder
    {
      private MatchMetadata? _matchMetadata;

      public TransactionMetadataBuilder() { }

      public TransactionMetadataBuilder WithMatchMetadata(MatchMetadata matchMetadata)
      {
        this._matchMetadata = matchMetadata;
        return this;
      }

      public TransactionMetadata Build()
      {
        return new TransactionMetadata
        {
          MatchMetadata = this._matchMetadata
        };
      }
    }
  }
}
