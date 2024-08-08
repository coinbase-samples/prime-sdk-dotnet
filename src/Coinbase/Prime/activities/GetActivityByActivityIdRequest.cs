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

namespace Coinbase.Prime.Activities
{
  using System.Text.Json.Serialization;
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;
  public class GetActivityByActivityIdRequest(string portfolioId, string activityId)
  : BasePrimeRequest(portfolioId, null)
  {
    [JsonIgnore]
    public string ActivityId { get; set; } = activityId;

    public class GetActivityByActivityIdRequestBuilder
    {
      private string? _portfolioId;
      private string? _activityId;

      public GetActivityByActivityIdRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public GetActivityByActivityIdRequestBuilder WithActivityId(string activityId)
      {
        _activityId = activityId;
        return this;
      }

      /// <summary>
      /// Validates the input fields.
      /// </summary>
      /// <exception cref="CoinbaseClientException">
      /// If <see cref="_portfolioId"/> or <see cref="_activityId"/> are null, empty, or whitespace.
      /// </exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(_activityId))
        {
          throw new CoinbaseClientException("ActivityId is required");
        }
      }

      public GetActivityByActivityIdRequest Build()
      {
        this.Validate();
        return new GetActivityByActivityIdRequest(_portfolioId!, _activityId!);
      }
    }
  }
}