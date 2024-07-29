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
  using Coinbase.Prime.Orders;
  public class Allocation
  {
    [JsonProperty("root_id")]
    public string? RootId { get; set; }

    [JsonProperty("reversal_id")]
    public string? ReversalId { get; set; }

    [JsonProperty("allocation_completed_at")]
    public string? AllocationCompletedAt { get; set; }

    [JsonProperty("user_id")]
    public string? UserId { get; set; }

    [JsonProperty("product_id")]
    public string? ProductId { get; set; }

    public OrderSide Side { get; set; }

    [JsonProperty("avg_price")]
    public string? AveragePrice { get; set; }

    [JsonProperty("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonProperty("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonProperty("fees_allocated")]
    public string? FeesAllocated { get; set; }

    public AllocationStatus Status { get; set; }

    public string? Source { get; set; }

    [JsonProperty("order_ids")]
    public string[] OrderIds { get; set; } = [];

    [JsonProperty("netting_id")]
    public string? NettingId { get; set; }

    public AllocationDestination[] Destinations { get; set; } = [];

    public Allocation() { }
  }
}
