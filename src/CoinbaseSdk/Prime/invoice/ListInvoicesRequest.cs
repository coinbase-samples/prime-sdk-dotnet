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

namespace CoinbaseSdk.Prime.Invoice
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model;

  public class ListInvoicesRequest(string entityId)
  : BasePrimeRequest(null, entityId)
  {
    public InvoiceState[] States { get; set; } = [];

    [JsonPropertyName("billing_month")]
    public int? BillingMonth { get; set; }

    [JsonPropertyName("billing_year")]
    public int? BillingYear { get; set; }

    public int? Cursor { get; set; }
    public int? Limit { get; set; }

    public class ListInvoicesRequestBuilder
    {
      private string? _entityId;
      private InvoiceState[] _states = [];
      private int? _billingMonth;
      private int? _billingYear;
      private int? _cursor;
      private int? _limit;

      public ListInvoicesRequestBuilder WithEntityId(string entityId)
      {
        this._entityId = entityId;
        return this;
      }

      public ListInvoicesRequestBuilder WithStates(InvoiceState[] states)
      {
        this._states = states;
        return this;
      }

      public ListInvoicesRequestBuilder WithBillingMonth(int billingMonth)
      {
        this._billingMonth = billingMonth;
        return this;
      }

      public ListInvoicesRequestBuilder WithBillingYear(int billingYear)
      {
        this._billingYear = billingYear;
        return this;
      }

      public ListInvoicesRequestBuilder WithCursor(int cursor)
      {
        this._cursor = cursor;
        return this;
      }

      public ListInvoicesRequestBuilder WithLimit(int limit)
      {
        this._limit = limit;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_entityId" /> is null, empty, or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListInvoicesRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListInvoicesRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_entityId" /> is null, empty or whitespace.</exception>
      public ListInvoicesRequest Build()
      {
        Validate();
        return new ListInvoicesRequest(this._entityId!)
        {
          States = this._states,
          BillingMonth = this._billingMonth,
          BillingYear = this._billingYear,
          Cursor = this._cursor,
          Limit = this._limit
        };
      }
    }
  }
}
