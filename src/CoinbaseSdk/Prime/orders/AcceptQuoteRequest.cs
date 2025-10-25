/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Orders
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model.Enums;

  public class AcceptQuoteRequest(string portfolioId, string productId, string quoteId, string clientQuoteId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("quote_id")]
    public string QuoteId { get; set; } = quoteId;

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; } = productId;

    [JsonPropertyName("client_order_id")]
    public string? ClientOrderId { get; set; } = clientQuoteId;

    [JsonPropertyName("settl_currency")]
    public string? SettlCurrency { get; set; }

    public OrderSide? Side { get; set; }
  }
}
