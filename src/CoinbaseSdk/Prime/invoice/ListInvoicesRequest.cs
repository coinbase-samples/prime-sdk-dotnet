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

namespace CoinbaseSdk.Prime.Invoice
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Invoices.
  /// </summary>
  public class ListInvoicesRequest(string entityId) : PaginatedRequest
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    public InvoiceState[] States { get; set; } = [];

    public int? BillingYear { get; set; }

    public int? BillingMonth { get; set; }

    public class ListInvoicesRequestBuilder
    {
      private string? _entityId;
      private InvoiceState[]? _states;
      private int? _billingYear;
      private int? _billingMonth;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public ListInvoicesRequestBuilder WithEntityId(string entityId)
      {
        _entityId = entityId;
        return this;
      }

      public ListInvoicesRequestBuilder WithStates(InvoiceState[] states)
      {
        _states = states;
        return this;
      }

      public ListInvoicesRequestBuilder WithBillingYear(int? billingYear)
      {
        _billingYear = billingYear;
        return this;
      }

      public ListInvoicesRequestBuilder WithBillingMonth(int? billingMonth)
      {
        _billingMonth = billingMonth;
        return this;
      }

      public ListInvoicesRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListInvoicesRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListInvoicesRequestBuilder WithLimit(int limit)
      {
        _limit = limit;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      public ListInvoicesRequest Build()
      {
        Validate();
        return new ListInvoicesRequest(_entityId!)
        {
          States = _states ?? [],
          BillingYear = _billingYear,
          BillingMonth = _billingMonth,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
      }
    }
  }
}
