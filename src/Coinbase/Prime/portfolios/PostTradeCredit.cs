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

using Newtonsoft.Json;

namespace Coinbase.Prime.Portfolios
{
  public class PostTradeCredit
  {
    [JsonProperty("portfolio_id")]
    public string? PortfolioId { get; set; }
    public string? Currency { get; set; }
    public string? Limit { get; set; }
    public string? Utilized { get; set; }
    public string? Available { get; set; }
    public bool? Frozen { get; set; }

    [JsonProperty("amounts_due")]
    public PostTradeCreditAmountDue[]? AmountsDue { get; set; }

    public PostTradeCredit() { }

    private PostTradeCredit(Builder builder)
    {
      PortfolioId = builder.PortfolioId;
      Currency = builder.Currency;
      Limit = builder.Limit;
      Utilized = builder.Utilized;
      Available = builder.Available;
      Frozen = builder.Frozen;
      AmountsDue = builder.AmountsDue;
    }

    public class Builder
    {
      public string? PortfolioId { get; private set; }
      public string? Currency { get; private set; }
      public string? Limit { get; private set; }
      public string? Utilized { get; private set; }
      public string? Available { get; private set; }
      public bool? Frozen { get; private set; }
      public PostTradeCreditAmountDue[]? AmountsDue { get; private set; }

      public Builder() { }

      public Builder SetPortfolioId(string portfolioId)
      {
        PortfolioId = portfolioId;
        return this;
      }

      public Builder SetCurrency(string currency)
      {
        Currency = currency;
        return this;
      }

      public Builder SetLimit(string limit)
      {
        Limit = limit;
        return this;
      }

      public Builder SetUtilized(string utilized)
      {
        Utilized = utilized;
        return this;
      }

      public Builder SetAvailable(string available)
      {
        Available = available;
        return this;
      }

      public Builder SetFrozen(bool frozen)
      {
        Frozen = frozen;
        return this;
      }

      public Builder SetAmountsDue(PostTradeCreditAmountDue[] amountsDue)
      {
        AmountsDue = amountsDue;
        return this;
      }

      public PostTradeCredit Build()
      {
        return new PostTradeCredit(this);
      }
    }
  }
}
