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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;
  public class PostTradeCreditInformation
  {
    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }
    public string? Currency { get; set; }
    public string? Limit { get; set; }
    public string? Utilized { get; set; }
    public string? Available { get; set; }
    public bool? Frozen { get; set; }

    [JsonPropertyName("amounts_due")]
    public AmountDue[]? AmountsDue { get; set; }

    public PostTradeCreditInformation() { }

    public class PostTradeCreditBuilder
    {
      private string? _portfolioId;
      private string? _currency;
      private string? _limit;
      private string? _utilized;
      private string? _available;
      private bool? _frozen;
      private AmountDue[]? _amountsDue;

      public PostTradeCreditBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public PostTradeCreditBuilder WithCurrency(string currency)
      {
        this._currency = currency;
        return this;
      }

      public PostTradeCreditBuilder WithLimit(string limit)
      {
        this._limit = limit;
        return this;
      }

      public PostTradeCreditBuilder WithUtilized(string utilized)
      {
        this._utilized = utilized;
        return this;
      }

      public PostTradeCreditBuilder WithAvailable(string available)
      {
        this._available = available;
        return this;
      }

      public PostTradeCreditBuilder WithFrozen(bool frozen)
      {
        this._frozen = frozen;
        return this;
      }

      public PostTradeCreditBuilder WithAmountsDue(AmountDue[] amountsDue)
      {
        this._amountsDue = amountsDue;
        return this;
      }

      public PostTradeCreditInformation Build()
      {
        return new PostTradeCreditInformation
        {
          PortfolioId = this._portfolioId,
          Currency = this._currency,
          Limit = this._limit,
          Utilized = this._utilized,
          Available = this._available,
          Frozen = this._frozen,
          AmountsDue = this._amountsDue
        };
      }
    }
  }
}
