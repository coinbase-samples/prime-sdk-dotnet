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
  using CoinbaseSdk.Prime.Model;

  public class CreateWalletResponse
  {
    [JsonPropertyName("activity_id")]
    public string? ActivityId { get; set; }

    public string? Name { get; set; }

    public string? Symbol { get; set; }

    [JsonPropertyName("wallet_type")]
    public WalletType Type { get; set; }

    public CreateWalletResponse() { }

    public class CreateWalletResponseBuilder
    {
      private string? _activityId;
      private string? _name;
      private string? _symbol;
      private WalletType _type;

      public CreateWalletResponseBuilder() { }

      public CreateWalletResponseBuilder WithActivityId(string activityId)
      {
        this._activityId = activityId;
        return this;
      }

      public CreateWalletResponseBuilder WithName(string name)
      {
        this._name = name;
        return this;
      }

      public CreateWalletResponseBuilder WithSymbol(string symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public CreateWalletResponseBuilder WithType(WalletType type)
      {
        this._type = type;
        return this;
      }

      public CreateWalletResponse Build()
      {
        return new CreateWalletResponse
        {
          ActivityId = this._activityId,
          Name = this._name,
          Symbol = this._symbol,
          Type = this._type
        };
      }
    }
  }
}
