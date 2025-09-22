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

namespace CoinbaseSdk.Prime.Portfolios
{
  using System.Text.Json.Serialization;

  /// <summary>
  /// Response object for getting a portfolio's counterparty ID.
  /// </summary>
  public class GetPortfolioCounterpartyResponse
  {
    /// <summary>
    /// The counterparty ID for the portfolio.
    /// </summary>
    [JsonPropertyName("counterparty_id")]
    public string? CounterpartyId { get; set; }

    /// <summary>
    /// Builder for <see cref="GetPortfolioCounterpartyResponse"/>.
    /// </summary>
    public class GetPortfolioCounterpartyResponseBuilder
    {
      private string? _counterpartyId;

      /// <summary>
      /// Sets the counterparty ID.
      /// </summary>
      /// <param name="counterpartyId">The counterparty ID.</param>
      /// <returns>The builder instance.</returns>
      public GetPortfolioCounterpartyResponseBuilder WithCounterpartyId(string counterpartyId)
      {
        this._counterpartyId = counterpartyId;
        return this;
      }

      /// <summary>
      /// Builds the <see cref="GetPortfolioCounterpartyResponse"/>.
      /// </summary>
      /// <returns>The <see cref="GetPortfolioCounterpartyResponse"/>.</returns>
      public GetPortfolioCounterpartyResponse Build()
      {
        return new GetPortfolioCounterpartyResponse
        {
          CounterpartyId = this._counterpartyId
        };
      }
    }
  }
}