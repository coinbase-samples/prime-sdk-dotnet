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

namespace CoinbaseSdk.Prime.Allocations
{
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Orders;
  using System.Text.Json.Serialization;
  public class GetPortfolioAllocationsRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    [JsonPropertyName("product_ids")]
    public string[] ProductIds { get; set; } = [];
    [JsonPropertyName("order_side")]
    public OrderSide OrderSide { get; set; }
    [JsonPropertyName("start_date")]
    public string? StartDate { get; set; }
    [JsonPropertyName("end_date")]
    public string? EndDate { get; set; }

    public class GetPortfolioAllocationsRequestBuilder
    {
      private string? _portfolioId;
      private string[] _productIds = Array.Empty<string>();
      private OrderSide _orderSide;
      private string? _startDate;
      private string? _endDate;
      private string? _cursor;
      private string? _sortDirection;
      private int? _limit;

      public GetPortfolioAllocationsRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public GetPortfolioAllocationsRequestBuilder WithProductIds(string[] productIds)
      {
        this._productIds = productIds;
        return this;
      }

      public GetPortfolioAllocationsRequestBuilder WithOrderSide(OrderSide orderSide)
      {
        this._orderSide = orderSide;
        return this;
      }

      public GetPortfolioAllocationsRequestBuilder WithStartDate(string? startDate)
      {
        this._startDate = startDate;
        return this;
      }

      public GetPortfolioAllocationsRequestBuilder WithEndDate(string? endDate)
      {
        this._endDate = endDate;
        return this;
      }

      public GetPortfolioAllocationsRequestBuilder WithCursor(string? cursor)
      {
        this._cursor = cursor;
        return this;
      }

      public GetPortfolioAllocationsRequestBuilder WithSortDirection(string? sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public GetPortfolioAllocationsRequestBuilder WithLimit(int? limit)
      {
        this._limit = limit;
        return this;
      }

      public GetPortfolioAllocationsRequestBuilder WithPagination(Pagination pagination)
      {
        this._cursor = pagination.NextCursor;
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="GetPortfolioAllocationsRequest"/>.
      /// </summary>
      /// <returns>The <see cref="GetPortfolioAllocationsRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public GetPortfolioAllocationsRequest Build()
      {
        this.Validate();
        return new GetPortfolioAllocationsRequest(this._portfolioId!)
        {
          ProductIds = this._productIds,
          OrderSide = this._orderSide,
          StartDate = this._startDate,
          EndDate = this._endDate,
          Cursor = this._cursor,
          SortDirection = this._sortDirection,
          Limit = this._limit
        };
      }
    }
  }
}
