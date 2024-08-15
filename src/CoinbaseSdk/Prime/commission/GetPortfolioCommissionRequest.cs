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

namespace CoinbaseSdk.Prime.Commission
{
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;

  public class GetPortfolioCommissionRequest(string portfolioId)
  : BasePrimeRequest(portfolioId, null)
  {
    public class GetPortfolioCommissionRequestBuilder
    {
      private string? _portfolioId;

      public GetPortfolioCommissionRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Build the <see cref="GetPortfolioCommissionRequest"/>.
      /// </summary>
      /// <returns>The <see cref="GetPortfolioCommissionRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public GetPortfolioCommissionRequest Build()
      {
        this.Validate();
        return new GetPortfolioCommissionRequest(this._portfolioId!);
      }
    }
  }
}
