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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;

  /// <summary>
  /// Optional metadata for wallet staking requests.
  /// </summary>
  public class WalletStakingMetadata
  {
    /// <summary>
    /// An optional custom identifier (up to 255 bytes) to attach to the transaction.
    /// </summary>
    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    public WalletStakingMetadata() { }

    public WalletStakingMetadata(Builder builder)
    {
      this.ExternalId = builder.externalId;
    }

    public class Builder
    {
#pragma warning disable SA1307, SA1401
      internal string? externalId;
#pragma warning restore SA1307, SA1401

      public Builder WithExternalId(string? externalId)
      {
        this.externalId = externalId;
        return this;
      }

      public WalletStakingMetadata Build()
      {
        return new WalletStakingMetadata(this);
      }
    }
  }
}
