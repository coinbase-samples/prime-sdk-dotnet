/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  /// <summary>
  /// Create Onchain Transaction.
  /// </summary>
  public class CreateOnchainTransactionRequest(string portfolioId, string walletId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public string? RawUnsignedTxn { get; set; }

    public RpcConfig Rpc { get; set; }

    public EvmParams EvmParams { get; set; }

    public class CreateOnchainTransactionRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private string? _rawUnsignedTxn;
      private RpcConfig _rpc;
      private EvmParams _evmParams;

      public CreateOnchainTransactionRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public CreateOnchainTransactionRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public CreateOnchainTransactionRequestBuilder WithRawUnsignedTxn(string? rawUnsignedTxn)
      {
        _rawUnsignedTxn = rawUnsignedTxn;
        return this;
      }

      public CreateOnchainTransactionRequestBuilder WithRpc(RpcConfig rpc)
      {
        _rpc = rpc;
        return this;
      }

      public CreateOnchainTransactionRequestBuilder WithEvmParams(EvmParams evmParams)
      {
        _evmParams = evmParams;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(_walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }
      }

      public CreateOnchainTransactionRequest Build()
      {
        Validate();
        return new CreateOnchainTransactionRequest(_portfolioId!, _walletId!)
        {
          RawUnsignedTxn = _rawUnsignedTxn,
          Rpc = _rpc,
          EvmParams = _evmParams,
        };
      }
    }
  }
}
