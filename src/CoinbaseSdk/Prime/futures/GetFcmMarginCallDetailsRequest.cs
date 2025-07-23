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

  public class GetFcmMarginCallDetailsRequest(string entityId)
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    public class GetFcmMarginCallDetailsRequestBuilder
    {
      private string? _entityId;

      public GetFcmMarginCallDetailsRequestBuilder WithEntityId(string entityId)
      {
        _entityId = entityId;
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
      /// Builds the <see cref="GetFcmMarginCallDetailsRequest"/>.
      /// </summary>
      /// <returns>The new <see cref="GetFcmMarginCallDetailsRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException"> If the required fields are not set.</exception>
      public GetFcmMarginCallDetailsRequest Build()
      {
        this.Validate();
        return new GetFcmMarginCallDetailsRequest(_entityId!);
      }
    }
  }
}