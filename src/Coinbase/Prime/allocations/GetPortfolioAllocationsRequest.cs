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
  using Coinbase.Prime.Common;
  using Coinbase.Prime.Orders;
  using System.Text.Json.Serialization;
  public class GetPortfolioAllocationsRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    [JsonPropertyName("product_ids")]
    public string[] ProductIds { get; set; } = [];
    [JsonPropertyName("order_side")]
    public OrderSide OrderSide { get; set; }
    [JsonPropertyName("start_date")]
    public string? StartDate { get; set; }
    [JsonPropertyName("end_date")]
    public string? EndDate { get; set; }
  }
}