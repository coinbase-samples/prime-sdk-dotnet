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

  public class Wallet
  {
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Symbol { get; set; }

    public WalletType Type { get; set; }

    public string? Address { get; set; }

    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    public Wallet() { }

    public class WalletBuilder
    {
      private string? Id;
      private string? Name;
      private string? Symbol;
      private WalletType Type;
      private string? Address;
      private string? CreatedAt;

      public WalletBuilder() { }

      public WalletBuilder WithId(string id)
      {
        this.Id = id;
        return this;
      }

      public WalletBuilder WithName(string name)
      {
        this.Name = name;
        return this;
      }

      public WalletBuilder WithSymbol(string symbol)
      {
        this.Symbol = symbol;
        return this;
      }

      public WalletBuilder WithType(WalletType type)
      {
        this.Type = type;
        return this;
      }

      public WalletBuilder WithAddress(string address)
      {
        this.Address = address;
        return this;
      }

      public WalletBuilder WithCreatedAt(string createdAt)
      {
        this.CreatedAt = createdAt;
        return this;
      }

      public Wallet Build()
      {
        return new Wallet
        {
          Id = this.Id,
          Name = this.Name,
          Symbol = this.Symbol,
          Type = this.Type,
          Address = this.Address,
          CreatedAt = this.CreatedAt
        };
      }
    }
  }
}
