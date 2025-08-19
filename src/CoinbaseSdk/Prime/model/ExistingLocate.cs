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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;

  public class ExistingLocate
  {
    /// <summary>
    /// The locate ID.
    /// </summary>
    [JsonPropertyName("locate_id")]
    public string? LocateId { get; set; }

    /// <summary>
    /// The unique ID of the entity.
    /// </summary>
    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }

    /// <summary>
    /// The unique ID of the portfolio.
    /// </summary>
    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }

    /// <summary>
    /// The currency symbol.
    /// </summary>
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// The requested locate amount.
    /// </summary>
    [JsonPropertyName("requested_amount")]
    public string? RequestedAmount { get; set; }

    /// <summary>
    /// The interest rate of PM loan.
    /// </summary>
    [JsonPropertyName("interest_rate")]
    public string? InterestRate { get; set; }

    /// <summary>
    /// The locate status.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// The approved locate amount.
    /// </summary>
    [JsonPropertyName("approved_amount")]
    public string? ApprovedAmount { get; set; }

    /// <summary>
    /// Deprecated: Use locate_date instead.
    /// </summary>
    [JsonPropertyName("conversion_date")]
    public string? ConversionDate { get; set; }

    /// <summary>
    /// The date when the locate was submitted in RFC3339 format.
    /// </summary>
    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    /// <summary>
    /// The locate date from the CreateNewLocatesRequest in RFC3339 format.
    /// </summary>
    [JsonPropertyName("locate_date")]
    public string? LocateDate { get; set; }
  }
}