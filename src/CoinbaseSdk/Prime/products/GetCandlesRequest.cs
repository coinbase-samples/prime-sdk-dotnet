/*
 * Copyright 2026-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Products
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// Get Public Product Candles (Beta).
  /// </summary>
  public class GetCandlesRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? ProductId { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public string? Granularity { get; set; }

    public class GetCandlesRequestBuilder
    {
      private string? _portfolioId;
      private string? _productId;
      private string? _startTime;
      private string? _endTime;
      private string? _granularity;

      public GetCandlesRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public GetCandlesRequestBuilder WithProductId(string? productId)
      {
        _productId = productId;
        return this;
      }

      public GetCandlesRequestBuilder WithStartTime(string? startTime)
      {
        _startTime = startTime;
        return this;
      }

      public GetCandlesRequestBuilder WithEndTime(string? endTime)
      {
        _endTime = endTime;
        return this;
      }

      public GetCandlesRequestBuilder WithGranularity(string? granularity)
      {
        _granularity = granularity;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public GetCandlesRequest Build()
      {
        Validate();
        return new GetCandlesRequest(_portfolioId!)
        {
          ProductId = _productId,
          StartTime = _startTime,
          EndTime = _endTime,
          Granularity = _granularity,
        };
      }
    }
  }
}
