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
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// Get Portfolio Commission.
  /// </summary>
  public class GetPortfolioCommissionRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? ProductId { get; set; }

    public class GetPortfolioCommissionRequestBuilder
    {
      private string? _portfolioId;
      private string? _productId;

      public GetPortfolioCommissionRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public GetPortfolioCommissionRequestBuilder WithProductId(string? productId)
      {
        _productId = productId;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public GetPortfolioCommissionRequest Build()
      {
        Validate();
        return new GetPortfolioCommissionRequest(_portfolioId!)
        {
          ProductId = _productId,
        };
      }
    }
  }
}
