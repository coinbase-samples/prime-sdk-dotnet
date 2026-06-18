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

namespace CoinbaseSdk.Prime.Financing
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// List Interest Accruals For Portfolio.
  /// </summary>
  public class ListInterestAccrualsForPortfolioRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public class ListInterestAccrualsForPortfolioRequestBuilder
    {
      private string? _portfolioId;
      private string? _startDate;
      private string? _endDate;

      public ListInterestAccrualsForPortfolioRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListInterestAccrualsForPortfolioRequestBuilder WithStartDate(string? startDate)
      {
        _startDate = startDate;
        return this;
      }

      public ListInterestAccrualsForPortfolioRequestBuilder WithEndDate(string? endDate)
      {
        _endDate = endDate;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public ListInterestAccrualsForPortfolioRequest Build()
      {
        Validate();
        return new ListInterestAccrualsForPortfolioRequest(_portfolioId!)
        {
          StartDate = _startDate,
          EndDate = _endDate,
        };
      }
    }
  }
}
