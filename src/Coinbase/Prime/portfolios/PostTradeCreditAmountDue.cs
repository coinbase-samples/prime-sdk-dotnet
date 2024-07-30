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

namespace Coinbase.Prime.Portfolios
{
  using System.Text.Json.Serialization;
  public class PostTradeCreditAmountDue
  {
    public string? Currency { get; set; }
    public string? Amount { get; set; }

    [JsonPropertyName("due_date")]
    public DateTime? DueDate { get; set; }

    public PostTradeCreditAmountDue() { }

    private PostTradeCreditAmountDue(Builder builder)
    {
      Currency = builder.Currency;
      Amount = builder.Amount;
      DueDate = builder.DueDate;
    }

    public class Builder
    {
      public string? Currency { get; private set; }
      public string? Amount { get; private set; }
      public DateTime? DueDate { get; private set; }

      public Builder() { }

      public Builder SetCurrency(string currency)
      {
        Currency = currency;
        return this;
      }

      public Builder SetAmount(string amount)
      {
        Amount = amount;
        return this;
      }

      public Builder SetDueDate(DateTime dueDate)
      {
        DueDate = dueDate;
        return this;
      }

      public PostTradeCreditAmountDue Build()
      {
        return new PostTradeCreditAmountDue(this);
      }
    }
  }
}
