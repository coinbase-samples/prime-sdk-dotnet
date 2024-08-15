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

namespace CoinbaseSdk.Prime.Balances
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model;

  public class ListWeb3WalletBalancesRequest(string portfolioId, string walletId)
  : BaseListRequest(portfolioId, null)
  {
    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    [JsonPropertyName("visibility_statuses")]
    public VisibilityStatus[] VisibilityStatuses { get; set; } = [];

    public class ListWeb3WalletBalancesRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private VisibilityStatus[] _visibilityStatuses = [];
      private string? _cursor;
      private string? _sortDirection;
      private int? _limit;

      public ListWeb3WalletBalancesRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListWeb3WalletBalancesRequestBuilder WithWalletId(string walletId)
      {
        this._walletId = walletId;
        return this;
      }

      public ListWeb3WalletBalancesRequestBuilder WithVisibilityStatuses(VisibilityStatus[] visibilityStatuses)
      {
        this._visibilityStatuses = visibilityStatuses;
        return this;
      }

      public ListWeb3WalletBalancesRequestBuilder WithCursor(string cursor)
      {
        this._cursor = cursor;
        return this;
      }

      public ListWeb3WalletBalancesRequestBuilder WithSortDirection(string sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public ListWeb3WalletBalancesRequestBuilder WithLimit(int limit)
      {
        this._limit = limit;
        return this;
      }

      public ListWeb3WalletBalancesRequestBuilder WithPagination(Pagination pagination)
      {
        this._cursor = pagination.NextCursor;
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> or <see cref="_walletId"/> are null, empty
      /// or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }

        if (string.IsNullOrWhiteSpace(_walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }
      }

      /// <summary>
      /// Build the <see cref="ListWeb3WalletBalancesRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListWeb3WalletBalancesRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public ListWeb3WalletBalancesRequest Build()
      {
        Validate();
        return new ListWeb3WalletBalancesRequest(_portfolioId!, this._walletId!)
        {
          VisibilityStatuses = this._visibilityStatuses,
          Cursor = this._cursor,
          SortDirection = this._sortDirection,
          Limit = this._limit
        };
      }
    }
  }
}
