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
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// Request object for getting a portfolio's counterparty ID.
  /// </summary>
  public class GetPortfolioCounterpartyRequest(string portfolioId)
  {
    /// <summary>
    /// The portfolio ID.
    /// </summary>
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    /// <summary>
    /// Builder for <see cref="GetPortfolioCounterpartyRequest"/>.
    /// </summary>
    public class GetPortfolioCounterpartyRequestBuilder
    {
      private string? _portfolioId;

      /// <summary>
      /// Sets the portfolio ID.
      /// </summary>
      /// <param name="portfolioId">The portfolio ID.</param>
      /// <returns>The builder instance.</returns>
      public GetPortfolioCounterpartyRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="GetPortfolioCounterpartyRequest"/>.
      /// </summary>
      /// <returns>The <see cref="GetPortfolioCounterpartyRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public GetPortfolioCounterpartyRequest Build()
      {
        this.Validate();
        return new GetPortfolioCounterpartyRequest(this._portfolioId!);
      }
    }
  }
}