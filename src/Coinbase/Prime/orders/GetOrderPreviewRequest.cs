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

namespace Coinbase.Prime.Orders
{

  using Newtonsoft.Json;

  public class GetOrderPreviewRequest(
      string productId,
      OrderSide side,
      OrderType type)
  {
    [JsonProperty("product_id")]
    public string? ProductId { get; set; } = productId;

    public OrderSide? Side { get; set; } = side;
    public OrderType? Type { get; set; } = type;

    [JsonProperty("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonProperty("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonProperty("limit_price")]
    public string? LimitPrice { get; set; }

    [JsonProperty("stop_price")]
    public string? StopPrice { get; set; }

    [JsonProperty("time_in_force")]
    public TimeInForce? TimeInForce { get; set; }

    [JsonProperty("start_time")]
    public string? StartTime { get; set; }

    [JsonProperty("expiry_time")]
    public string? ExpiryTime { get; set; }

    [JsonProperty("is_raise_exact")]
    public bool? IsRaiseExact { get; set; }

    [JsonProperty("historical_pov")]
    public string? HistoricalPov { get; set; }
  }
}