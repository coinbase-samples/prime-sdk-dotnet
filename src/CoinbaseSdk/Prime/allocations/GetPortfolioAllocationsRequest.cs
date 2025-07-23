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
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  public class GetPortfolioAllocationsRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("product_ids")]
    public string[] ProductIds { get; set; } = [];

    [JsonPropertyName("order_side")]
    public OrderSide OrderSide { get; set; }

    [JsonPropertyName("start_date")]
    public string? StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public string? EndDate { get; set; }

    [JsonPropertyName("sort_direction")]
    public SortDirection? SortDirection { get; set; }

    public class GetPortfolioAllocationsRequestBuilder : PaginatedRequestBuilder<GetPortfolioAllocationsRequest, GetPortfolioAllocationsRequestBuilder>
    {
      private string? _portfolioId;
      private string[] _productIds = Array.Empty<string>();
      private OrderSide _orderSide;
      private string? _startDate;
      private string? _endDate;
      private SortDirection? _sortDirection;

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

      public GetPortfolioAllocationsRequestBuilder WithSortDirection(SortDirection? sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public new GetPortfolioAllocationsRequestBuilder WithPagination(Pagination pagination)
      {
        base.WithPagination(pagination);
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> or <see cref="_startDate"/> are null, empty
      /// or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(this._startDate))
        {
          throw new CoinbaseClientException("StartDate is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="GetPortfolioAllocationsRequest"/>.
      /// </summary>
      /// <returns>The <see cref="GetPortfolioAllocationsRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public override GetPortfolioAllocationsRequest Build()
      {
        this.Validate();
        var request = new GetPortfolioAllocationsRequest(this._portfolioId!)
        {
          ProductIds = this._productIds,
          OrderSide = this._orderSide,
          StartDate = this._startDate!,
          EndDate = this._endDate,
          SortDirection = this._sortDirection
        };
        SetPaginationProperties(request);
        return request;
      }
    }
  }
}
