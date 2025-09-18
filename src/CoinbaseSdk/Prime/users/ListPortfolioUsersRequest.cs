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

namespace CoinbaseSdk.Prime.Users
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  public class ListPortfolioUsersRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("sort_direction")]
    public SortDirection? SortDirection { get; set; }

    public class ListPortfolioUsersRequestBuilder : PaginatedRequestBuilder<ListPortfolioUsersRequest, ListPortfolioUsersRequestBuilder>
    {
      private string? _portfolioId;
      private SortDirection? _sortDirection;

      public ListPortfolioUsersRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListPortfolioUsersRequestBuilder WithSortDirection(SortDirection? sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public new ListPortfolioUsersRequestBuilder WithPagination(Pagination pagination)
      {
        base.WithPagination(pagination);
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the <see cref="_portfolioId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListPortfolioUsersRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="ListPortfolioUsersRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public override ListPortfolioUsersRequest Build()
      {
        this.Validate();
        var request = new ListPortfolioUsersRequest(this._portfolioId!)
        {
          SortDirection = this._sortDirection
        };
        SetPaginationProperties(request);
        return request;
      }
    }
  }
}
