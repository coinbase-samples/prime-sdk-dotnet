/*
 * Copyright 2025-present Coinbase Global, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace CoinbaseSdk.Prime.Futures
{
  using System.Text.Json.Serialization;

  /// <summary>
  /// Response containing FCM balance summary.
  /// </summary>
  public class GetFcmBalanceResponse
  {
    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }

    [JsonPropertyName("cfm_usd_balance")]
    public string? CfmUsdBalance { get; set; }

    [JsonPropertyName("unrealized_pnl")]
    public string? UnrealizedPnl { get; set; }

    [JsonPropertyName("daily_realized_pnl")]
    public string? DailyRealizedPnl { get; set; }

    [JsonPropertyName("excess_liquidity")]
    public string? ExcessLiquidity { get; set; }

    [JsonPropertyName("futures_buying_power")]
    public string? FuturesBuyingPower { get; set; }

    [JsonPropertyName("initial_margin")]
    public string? InitialMargin { get; set; }

    [JsonPropertyName("maintenance_margin")]
    public string? MaintenanceMargin { get; set; }

    [JsonPropertyName("clearing_account_id")]
    public string? ClearingAccountId { get; set; }

    /// <summary>
    /// CFM unsettled accrued funding PnL.
    /// </summary>
    [JsonPropertyName("cfm_unsettled_accrued_funding_pnl")]
    public string? CfmUnsettledAccruedFundingPnl { get; set; }

    public GetFcmBalanceResponse() { }
  }
}