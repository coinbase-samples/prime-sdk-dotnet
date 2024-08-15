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

namespace CoinbaseSdk.Prime.Balances
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  public class Web3WalletBalance
  {
    public Web3WalletAsset? Asset { get; set; }

    public string? Amount { get; set; }

    [JsonPropertyName("visibility_status")]
    public VisibilityStatus VisibilityStatus { get; set; }

    public Web3WalletBalance() { }

    public Web3WalletBalance(Web3WalletAsset asset, string amount, VisibilityStatus visibilityStatus)
    {
      Asset = asset;
      Amount = amount;
      VisibilityStatus = visibilityStatus;
    }

    public class Web3WalletBalanceBuilder
    {
      private Web3WalletAsset? _asset;
      private string? _amount;
      private VisibilityStatus _visibilityStatus;

      public Web3WalletBalanceBuilder WithAsset(Web3WalletAsset? asset)
      {
        this._asset = asset;
        return this;
      }

      public Web3WalletBalanceBuilder WithAmount(string? amount)
      {
        this._amount = amount;
        return this;
      }

      public Web3WalletBalanceBuilder WithVisibilityStatus(VisibilityStatus visibilityStatus)
      {
        this._visibilityStatus = visibilityStatus;
        return this;
      }

      public Web3WalletBalance Build()
      {
        return new Web3WalletBalance
        {
          Asset = this._asset,
          Amount = this._amount,
          VisibilityStatus = this._visibilityStatus
        };
      }
    }
  }
}
