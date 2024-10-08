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

namespace CoinbaseSdk.Prime.Wallets
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model;

  public class CreateWalletRequest(string portfolioId)
  : BasePrimeRequest(portfolioId, null)
  {
    public string? Name { get; set; }

    public string? Symbol { get; set; }

    [JsonPropertyName("wallet_type")]
    public WalletType Type { get; set; }

    public class CreateWalletRequestBuilder
    {
      private string? _portfolioId;
      private string? _name;
      private string? _symbol;
      private WalletType _type;

      public CreateWalletRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public CreateWalletRequestBuilder WithName(string? name)
      {
        this._name = name;
        return this;
      }

      public CreateWalletRequestBuilder WithSymbol(string? symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public CreateWalletRequestBuilder WithType(WalletType type)
      {
        this._type = type;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Build the <see cref="CreateWalletRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="CreateWalletRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public CreateWalletRequest Build()
      {
        this.Validate();
        return new CreateWalletRequest(this._portfolioId!)
        {
          Name = this._name,
          Symbol = this._symbol,
          Type = this._type
        };
      }
    }
  }
}
