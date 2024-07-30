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
  using System.Text.Json.Serialization;
  public class CreateOrderRequest
  {
    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    public OrderSide? Side { get; set; }

    [JsonPropertyName("client_order_id")]
    public string? ClientOrderId { get; set; }

    public OrderType? Type { get; set; }

    [JsonPropertyName("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonPropertyName("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonPropertyName("limit_price")]
    public string? LimitPrice { get; set; }

    [JsonPropertyName("stop_price")]
    public string? StopPrice { get; set; }

    [JsonPropertyName("time_in_force")]
    public TimeInForce? TimeInForce { get; set; }

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("expiry_time")]
    public string? ExpiryTime { get; set; }

    [JsonPropertyName("stp_id")]
    public string? StpId { get; set; }

    [JsonPropertyName("display_quote_size")]
    public string? DisplayQuoteSize { get; set; }

    [JsonPropertyName("display_base_size")]
    public string? DisplayBaseSize { get; set; }

    [JsonPropertyName("is_raise_exact")]
    public bool? IsRaiseExact { get; set; }

    [JsonPropertyName("historical_pov")]
    public string? HistoricalPov { get; set; }

    public CreateOrderRequest() { }
  }
}