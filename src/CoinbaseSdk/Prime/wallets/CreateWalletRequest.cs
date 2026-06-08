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
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// Create Wallet.
  /// </summary>
  public class CreateWalletRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? Name { get; set; }

    public string? Symbol { get; set; }

    public WalletType? WalletType { get; set; }

    public string? IdempotencyKey { get; set; }

    public NetworkFamily? NetworkFamily { get; set; }

    public Network Network { get; set; }

    public class CreateWalletRequestBuilder
    {
      private string? _portfolioId;
      private string? _name;
      private string? _symbol;
      private WalletType? _walletType;
      private string? _idempotencyKey;
      private NetworkFamily? _networkFamily;
      private Network _network;

      public CreateWalletRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public CreateWalletRequestBuilder WithName(string? name)
      {
        _name = name;
        return this;
      }

      public CreateWalletRequestBuilder WithSymbol(string? symbol)
      {
        _symbol = symbol;
        return this;
      }

      public CreateWalletRequestBuilder WithWalletType(WalletType? walletType)
      {
        _walletType = walletType;
        return this;
      }

      public CreateWalletRequestBuilder WithIdempotencyKey(string? idempotencyKey)
      {
        _idempotencyKey = idempotencyKey;
        return this;
      }

      public CreateWalletRequestBuilder WithNetworkFamily(NetworkFamily? networkFamily)
      {
        _networkFamily = networkFamily;
        return this;
      }

      public CreateWalletRequestBuilder WithNetwork(Network network)
      {
        _network = network;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public CreateWalletRequest Build()
      {
        Validate();
        return new CreateWalletRequest(_portfolioId!)
        {
          Name = _name,
          Symbol = _symbol,
          WalletType = _walletType,
          IdempotencyKey = _idempotencyKey,
          NetworkFamily = _networkFamily,
          Network = _network,
        };
      }
    }
  }
}
