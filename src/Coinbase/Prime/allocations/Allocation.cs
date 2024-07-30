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
  using Coinbase.Prime.Orders;
  public class Allocation
  {
    [JsonPropertyName("root_id")]
    public string? RootId { get; set; }

    [JsonPropertyName("reversal_id")]
    public string? ReversalId { get; set; }

    [JsonPropertyName("allocation_completed_at")]
    public string? AllocationCompletedAt { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    public OrderSide Side { get; set; }

    [JsonPropertyName("avg_price")]
    public string? AveragePrice { get; set; }

    [JsonPropertyName("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonPropertyName("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonPropertyName("fees_allocated")]
    public string? FeesAllocated { get; set; }

    public AllocationStatus Status { get; set; }

    public string? Source { get; set; }

    [JsonPropertyName("order_ids")]
    public string[] OrderIds { get; set; } = [];

    [JsonPropertyName("netting_id")]
    public string? NettingId { get; set; }

    public AllocationDestination[] Destinations { get; set; } = [];

    public Allocation() { }
  }
}
