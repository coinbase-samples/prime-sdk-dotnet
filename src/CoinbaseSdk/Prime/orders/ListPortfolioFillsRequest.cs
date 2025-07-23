/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Orders
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  public class ListPortfolioFillsRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("start_date")]
    public string? StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public string? EndDate { get; set; }

    [JsonPropertyName("sort_direction")]
    public SortDirection? SortDirection { get; set; }

    public class ListPortfolioFillsRequestBuilder : PaginatedRequestBuilder<ListPortfolioFillsRequest, ListPortfolioFillsRequestBuilder>
    {
      private string? _portfolioId;
      private string? _startDate;
      private string? _endDate;
      private SortDirection? _sortDirection;

      public ListPortfolioFillsRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListPortfolioFillsRequestBuilder WithStartDate(string startDate)
      {
        this._startDate = startDate;
        return this;
      }

      public ListPortfolioFillsRequestBuilder WithEndDate(string? endDate)
      {
        this._endDate = endDate;
        return this;
      }

      public ListPortfolioFillsRequestBuilder WithSortDirection(SortDirection? sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      /// <summary>
      /// Validate the builder.
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
      /// Build the <see cref="ListPortfolioFillsRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="ListPortfolioFillsRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public override ListPortfolioFillsRequest Build()
      {
        this.Validate();
        var request = new ListPortfolioFillsRequest(this._portfolioId!)
        {
          StartDate = this._startDate!,
          EndDate = this._endDate,
          SortDirection = this._sortDirection,
        };
        SetPaginationProperties(request);
        return request;
      }
    }
  }
}
