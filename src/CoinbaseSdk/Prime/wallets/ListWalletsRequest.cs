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
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  public class ListWalletsRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("type")]
    public WalletType Type { get; set; }

    public string[] Symbols { get; set; } = [];

    [JsonPropertyName("sort_direction")]
    public SortDirection? SortDirection { get; set; }

    public class ListWalletsRequestBuilder : PaginatedRequestBuilder<ListWalletsRequest, ListWalletsRequestBuilder>
    {
      private string? _portfolioId;
      private WalletType _type;
      private string[] _symbols = [];
      private SortDirection? _sortDirection;

      public ListWalletsRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListWalletsRequestBuilder WithType(WalletType type)
      {
        this._type = type;
        return this;
      }

      public ListWalletsRequestBuilder WithSymbols(string[] symbols)
      {
        this._symbols = symbols;
        return this;
      }


      public ListWalletsRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public new ListWalletsRequestBuilder WithPagination(Pagination pagination)
      {
        base.WithPagination(pagination);
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> is null, empty or whitespace, or when
      /// <see cref="_type"/> is not set.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (!Enum.IsDefined(typeof(WalletType), this._type))
        {
          throw new CoinbaseClientException("Type is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListWalletsRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListWalletsRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required field is not set.</exception>
      public override ListWalletsRequest Build()
      {
        this.Validate();
        var request = new ListWalletsRequest(this._portfolioId!)
        {
          Type = this._type,
          Symbols = this._symbols,
          SortDirection = this._sortDirection
        };
        SetPaginationProperties(request);
        return request;
      }
    }
  }
}
