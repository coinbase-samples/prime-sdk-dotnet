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

namespace CoinbaseSdk.Prime.AddressBook
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  public class ListAddressBookEntriesRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("currency_symbol")]
    public string? CurrencySymbol { get; set; }
    public string? Search { get; set; }
    [JsonPropertyName("sort_direction")]
    public SortDirection? SortDirection { get; set; }

    public class ListAddressBookEntriesRequestBuilder : PaginatedRequestBuilder<ListAddressBookEntriesRequest, ListAddressBookEntriesRequestBuilder>
    {
      private string? _portfolioId;
      private string? _currencySymbol;
      private string? _search;
      private SortDirection? _sortDirection;

      public ListAddressBookEntriesRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListAddressBookEntriesRequestBuilder WithCurrencySymbol(string? currencySymbol)
      {
        this._currencySymbol = currencySymbol;
        return this;
      }

      public ListAddressBookEntriesRequestBuilder WithSearch(string? search)
      {
        this._search = search;
        return this;
      }

      public ListAddressBookEntriesRequestBuilder WithSortDirection(SortDirection? sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public new ListAddressBookEntriesRequestBuilder WithPagination(Pagination pagination)
      {
        base.WithPagination(pagination);
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Build the <see cref="ListAddressBookEntriesRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListAddressBookEntriesRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public override ListAddressBookEntriesRequest Build()
      {
        this.Validate();
        var request = new ListAddressBookEntriesRequest(_portfolioId!)
        {
          CurrencySymbol = this._currencySymbol,
          Search = this._search,
          SortDirection = this._sortDirection
        };
        SetPaginationProperties(request);
        return request;
      }
    }
  }
}
