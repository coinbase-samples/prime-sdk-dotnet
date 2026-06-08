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

namespace CoinbaseSdk.Prime.Orders
{
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// Get Order Preview.
  /// </summary>
  public class GetOrderPreviewResponse
  {
    public string? PortfolioId { get; set; }

    public string? ProductId { get; set; }

    public OrderSide? Side { get; set; }

    public OrderType? Type { get; set; }

    public string? BaseQuantity { get; set; }

    public string? QuoteValue { get; set; }

    public string? LimitPrice { get; set; }

    public string? StartTime { get; set; }

    public string? ExpiryTime { get; set; }

    public TimeInForceType? TimeInForce { get; set; }

    public string? Commission { get; set; }

    public string? Slippage { get; set; }

    public string? BestBid { get; set; }

    public string? BestAsk { get; set; }

    public string? AverageFilledPrice { get; set; }

    public string? OrderTotal { get; set; }

    public string? HistoricalPov { get; set; }

    public bool? IsRaiseExact { get; set; }

    public string? StopPrice { get; set; }

    public string? DisplaySize { get; set; }

    public string? DisplayQuoteSize { get; set; }

    public string? DisplayBaseSize { get; set; }

    public GetOrderPreviewResponse() { }
  }
}
