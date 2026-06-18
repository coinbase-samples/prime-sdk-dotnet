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
  /// <summary>
  /// Create Quote Request.
  /// </summary>
  public class CreateQuoteResponse
  {
    public string? QuoteId { get; set; }

    public string? ExpirationTime { get; set; }

    public string? BestPrice { get; set; }

    public string? OrderTotal { get; set; }

    public string? PriceInclusiveOfFees { get; set; }

    public string? QuoteDurationMs { get; set; }

    public CreateQuoteResponse() { }
  }
}
