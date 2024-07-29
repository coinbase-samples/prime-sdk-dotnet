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
  using Newtonsoft.Json;
  public class CreateNetAllocationRequest
  {
    [JsonProperty("allocation_id")]
    public string? AllocationId { get; set; }

    [JsonProperty("source_portfolio_id")]
    public string? SourcePortfolioId { get; set; }

    [JsonProperty("product_id")]
    public string? ProductId { get; set; }

    [JsonProperty("order_ids")]
    public string[] OrderIds { get; set; } = [];

    [JsonProperty("allocation_legs")]
    public AllocationLeg[] AllocationLegs { get; set; } = [];

    [JsonProperty("size_type")]
    public SizeType SizeType { get; set; }

    [JsonProperty("remainder_destination_portfolio")]
    public string? RemainderDestinationPortfolio { get; set; }

    [JsonProperty("netting_id")]
    public string? NettingId { get; set; }

    public CreateNetAllocationRequest() { }
  }
}
