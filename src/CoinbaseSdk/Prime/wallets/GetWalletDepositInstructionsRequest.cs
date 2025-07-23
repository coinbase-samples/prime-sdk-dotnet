/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Wallets
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  public class GetWalletDepositInstructionsRequest(string portfolioId, string walletId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    [JsonPropertyName("deposit_type")]
    public WalletDepositInstructionType? DepositType { get; set; }

    public class GetWalletDepositInstructionsRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private WalletDepositInstructionType? _depositType;

      public GetWalletDepositInstructionsRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public GetWalletDepositInstructionsRequestBuilder WithWalletId(string walletId)
      {
        this._walletId = walletId;
        return this;
      }

      public GetWalletDepositInstructionsRequestBuilder WithDepositType(WalletDepositInstructionType depositType)
      {
        this._depositType = depositType;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/>, <see cref="_walletId"/>, or <see cref="_depositType"/> are null, empty
      /// or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }

        if (string.IsNullOrWhiteSpace(this._walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }

        if (this._depositType == null)
        {
          throw new CoinbaseClientException("DepositType is required");
        }
      }

      /// <summary>
      /// Build the <see cref="GetWalletDepositInstructionsRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="GetWalletDepositInstructionsRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public GetWalletDepositInstructionsRequest Build()
      {
        this.Validate();
        return new GetWalletDepositInstructionsRequest(this._portfolioId!, this._walletId!)
        {
          DepositType = this._depositType
        };
      }
    }
  }
}
