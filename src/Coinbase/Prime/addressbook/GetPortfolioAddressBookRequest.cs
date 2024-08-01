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

namespace Coinbase.Prime.AddressBook
{
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;
  using System.Text.Json.Serialization;
  public class GetPortfolioAddressBookRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    [JsonPropertyName("currency_symbol")]
    public string? CurrencySymbol { get; set; }
    public string? Search { get; set; }

    public class GetPortfolioAddressBookRequestBuilder
    {
      private string? _portfolioId;
      private string? _currencySymbol;
      private string? _search;
      private string? _cursor;
      private string? _sortDirection;
      private int? _limit;

      public GetPortfolioAddressBookRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public GetPortfolioAddressBookRequestBuilder WithCurrencySymbol(string? currencySymbol)
      {
        this._currencySymbol = currencySymbol;
        return this;
      }

      public GetPortfolioAddressBookRequestBuilder WithSearch(string? search)
      {
        this._search = search;
        return this;
      }

      public GetPortfolioAddressBookRequestBuilder WithCursor(string? cursor)
      {
        this._cursor = cursor;
        return this;
      }

      public GetPortfolioAddressBookRequestBuilder WithSortDirection(string? sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public GetPortfolioAddressBookRequestBuilder WithLimit(int? limit)
      {
        this._limit = limit;
        return this;
      }

      public void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public GetPortfolioAddressBookRequest Build()
      {
        this.Validate();
        return new GetPortfolioAddressBookRequest(_portfolioId!)
        {
          CurrencySymbol = this._currencySymbol,
          Search = this._search,
          Cursor = this._cursor,
          SortDirection = this._sortDirection,
          Limit = this._limit
        };
      }
    }
  }
}