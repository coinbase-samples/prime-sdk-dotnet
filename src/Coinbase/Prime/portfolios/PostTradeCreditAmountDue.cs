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

    public class PostTradeCreditAmountDueBuilder
    {
      private string? _currency;
      private string? _amount;
      private DateTime? _dueDate;

      public PostTradeCreditAmountDueBuilder WithCurrency(string currency)
      {
        this._currency = currency;
        return this;
      }

      public PostTradeCreditAmountDueBuilder WithAmount(string amount)
      {
        this._amount = amount;
        return this;
      }

      public PostTradeCreditAmountDueBuilder WithDueDate(DateTime dueDate)
      {
        this._dueDate = dueDate;
        return this;
      }

      public PostTradeCreditAmountDue Build()
      {
        return new PostTradeCreditAmountDue
        {
          Currency = this._currency,
          Amount = this._amount,
          DueDate = this._dueDate
        };
      }
    }
  }
}
