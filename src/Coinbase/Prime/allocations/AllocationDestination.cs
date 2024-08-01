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
  public class AllocationDestination
  {
    [JsonPropertyName("leg_id")]
    public string? LegId { get; set; }

    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }

    [JsonPropertyName("allocation_base")]
    public string? AllocationBase { get; set; }

    [JsonPropertyName("allocation_quote")]
    public string? AllocationQuote { get; set; }

    [JsonPropertyName("fees_allocated_leg")]
    public string? FeesAllocatedLeg { get; set; }

    public AllocationDestination() { }

    public class AllocationDestinationBuilder
    {
      private string? _legId;
      private string? _portfolioId;
      private string? _allocationBase;
      private string? _allocationQuote;
      private string? _feesAllocatedLeg;

      public AllocationDestinationBuilder WithLegId(string? legId)
      {
        this._legId = legId;
        return this;
      }

      public AllocationDestinationBuilder WithPortfolioId(string? portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public AllocationDestinationBuilder WithAllocationBase(string? allocationBase)
      {
        this._allocationBase = allocationBase;
        return this;
      }

      public AllocationDestinationBuilder WithAllocationQuote(string? allocationQuote)
      {
        this._allocationQuote = allocationQuote;
        return this;
      }

      public AllocationDestinationBuilder WithFeesAllocatedLeg(string? feesAllocatedLeg)
      {
        this._feesAllocatedLeg = feesAllocatedLeg;
        return this;
      }

      public AllocationDestination Build()
      {
        return new AllocationDestination
        {
          LegId = this._legId,
          PortfolioId = this._portfolioId,
          AllocationBase = this._allocationBase,
          AllocationQuote = this._allocationQuote,
          FeesAllocatedLeg = this._feesAllocatedLeg
        };
      }
    }
  }
}
