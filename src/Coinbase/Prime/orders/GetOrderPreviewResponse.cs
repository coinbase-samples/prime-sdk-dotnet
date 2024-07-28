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
  public class GetOrderPreviewResponse
  {
    [JsonProperty("portfolio_id")]
    public string? PortfolioId { get; set; }

    [JsonProperty("product_id")]
    public string? ProductId { get; set; }

    public OrderSide? Side { get; set; }
    public OrderType? Type { get; set; }

    [JsonProperty("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonProperty("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonProperty("limit_price")]
    public string? LimitPrice { get; set; }

    [JsonProperty("start_time")]
    public string? StartTime { get; set; }

    [JsonProperty("expiry_time")]
    public string? ExpiryTime { get; set; }

    [JsonProperty("time_in_force")]
    public TimeInForce TimeInForce { get; set; }

    public string? Commission { get; set; }
    public string? Slippage { get; set; }

    [JsonProperty("best_bid")]
    public string? BestBid { get; set; }

    [JsonProperty("best_ask")]
    public string? BestAsk { get; set; }

    [JsonProperty("average_filled_price")]
    public string? AverageFilledPrice { get; set; }

    [JsonProperty("order_total")]
    public string? OrderTotal { get; set; }


    public GetOrderPreviewResponse() { }
  }
}
