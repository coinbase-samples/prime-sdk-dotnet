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
  using CoinbaseSdk.Prime.Common;

  public class GetAllocationRequest(string portfolioId, string allocationId)
  : BasePrimeRequest(portfolioId, null)
  {
    [JsonIgnore]
    public string AllocationId { get; set; } = allocationId;

    public class GetAllocationRequestBuilder
    {
      private string? _portfolioId;
      private string? _allocationId;

      public GetAllocationRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public GetAllocationRequestBuilder WithAllocationId(string allocationId)
      {
        this._allocationId = allocationId;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> or <see cref="_allocationId"/> are null, empty
      /// or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(this._allocationId))
        {
          throw new CoinbaseClientException("AllocationId is required");
        }
      }

      /// <summary>
      /// Build the <see cref="GetAllocationRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="GetAllocationRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public GetAllocationRequest Build()
      {
        this.Validate();
        return new GetAllocationRequest(this._portfolioId!, this._allocationId!);
      }
    }
  }
}