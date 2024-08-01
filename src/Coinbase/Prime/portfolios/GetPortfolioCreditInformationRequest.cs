/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace Coinbase.Prime.Portfolios
{
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;

  public class GetPortfolioCreditInformationRequest(string portfolioId)
  : BasePrimeRequest(portfolioId, null)
  {
    public class GetPortfolioCreditInformationRequestBuilder
    {
      private string? _portfolioId;

      public GetPortfolioCreditInformationRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId cannot be null or empty");
        }
      }

      public GetPortfolioCreditInformationRequest Build()
      {
        this.Validate();
        return new GetPortfolioCreditInformationRequest(this._portfolioId!);
      }
    }
  }
}