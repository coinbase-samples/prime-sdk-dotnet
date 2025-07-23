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

namespace CoinbaseSdk.Prime.Futures
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  public class GetFcmRiskLimitsRequest(string entityId)
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    public class GetFcmRiskLimitsRequestBuilder
    {
      private string? _entityId;
      private string? _productId;

      public GetFcmRiskLimitsRequestBuilder WithEntityId(string entityId)
      {
        _entityId = entityId;
        return this;
      }

      public GetFcmRiskLimitsRequestBuilder WithProductId(string productId)
      {
        _productId = productId;
        return this;
      }

      /// <summary>
      /// Validates the input fields.
      /// </summary>
      /// <exception cref="CoinbaseClientException">
      /// If <see cref="_entityId"/> is null, empty, or whitespace.
      /// </exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="GetFcmRiskLimitsRequest"/>.
      /// </summary>
      /// <returns>The new <see cref="GetFcmRiskLimitsRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException"> If the required fields are not set.</exception>
      public GetFcmRiskLimitsRequest Build()
      {
        this.Validate();
        return new GetFcmRiskLimitsRequest(_entityId!)
        {
          ProductId = _productId
        };
      }
    }
  }
}