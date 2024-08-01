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

namespace Coinbase.Prime.Orders
{
  using System.Text.Json.Serialization;
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;
  public class ListOrderFillsRequest(string portfolioId, string orderId)
  : BaseListRequest(portfolioId, null)
  {
    [JsonIgnore]
    public string OrderId { get; set; } = orderId;

    public class ListOrderFillsRequestBuilder
    {
      private string? _portfolioId;
      private string? _orderId;

      public ListOrderFillsRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListOrderFillsRequestBuilder WithOrderId(string orderId)
      {
        this._orderId = orderId;
        return this;
      }

      public void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(this._orderId))
        {
          throw new CoinbaseClientException("OrderId is required");
        }
      }

      public ListOrderFillsRequest Build()
      {
        this.Validate();
        return new ListOrderFillsRequest(this._portfolioId!, this._orderId!);
      }
    }
  }
}