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

namespace Coinbase.Prime.Allocations
{
  using System.Text.Json.Serialization;
  public class AllocationLeg
  {
    [JsonPropertyName("allocation_leg_id")]
    public string? AllocationLegId { get; set; }

    [JsonPropertyName("destination_portfolio_id")]
    public string? DestinationPortfolioId { get; set; }

    public string? Amount { get; set; }

    public AllocationLeg() { }

    public class AllocationLegBuilder
    {
      private string? _allocationLegId;
      private string? _destinationPortfolioId;
      private string? _amount;

      public AllocationLegBuilder WithAllocationLegId(string? allocationLegId)
      {
        this._allocationLegId = allocationLegId;
        return this;
      }

      public AllocationLegBuilder WithDestinationPortfolioId(string? destinationPortfolioId)
      {
        this._destinationPortfolioId = destinationPortfolioId;
        return this;
      }

      public AllocationLegBuilder WithAmount(string? amount)
      {
        this._amount = amount;
        return this;
      }

      public AllocationLeg Build()
      {
        return new AllocationLeg
        {
          AllocationLegId = this._allocationLegId,
          DestinationPortfolioId = this._destinationPortfolioId,
          Amount = this._amount
        };
      }
    }
  }
}
