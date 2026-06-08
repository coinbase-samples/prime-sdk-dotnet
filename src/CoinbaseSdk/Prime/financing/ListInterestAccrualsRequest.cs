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
  /// List Interest Accruals.
  /// </summary>
  public class ListInterestAccrualsRequest(string entityId)
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    public string? PortfolioId { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public class ListInterestAccrualsRequestBuilder
    {
      private string? _entityId;
      private string? _portfolioId;
      private string? _startDate;
      private string? _endDate;

      public ListInterestAccrualsRequestBuilder WithEntityId(string entityId)
      {
        _entityId = entityId;
        return this;
      }

      public ListInterestAccrualsRequestBuilder WithPortfolioId(string? portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListInterestAccrualsRequestBuilder WithStartDate(string? startDate)
      {
        _startDate = startDate;
        return this;
      }

      public ListInterestAccrualsRequestBuilder WithEndDate(string? endDate)
      {
        _endDate = endDate;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      public ListInterestAccrualsRequest Build()
      {
        Validate();
        return new ListInterestAccrualsRequest(_entityId!)
        {
          PortfolioId = _portfolioId,
          StartDate = _startDate,
          EndDate = _endDate,
        };
      }
    }
  }
}
