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

namespace CoinbaseSdk.Prime.Allocations
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  public class GetAllocationsByClientNettingIdRequest(string portfolioId, string clientNettingId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string ClientNettingId { get; set; } = clientNettingId;

    public class Builder
    {
      private string? _portfolioId;
      private string? _clientNettingId;

      public Builder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public Builder WithClientNettingId(string clientNettingId)
      {
        _clientNettingId = clientNettingId;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> or <see cref="_clientNettingId"/> are null, empty
      /// or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(_clientNettingId))
        {
          throw new CoinbaseClientException("ClientNettingId is required");
        }
      }

      /// <summary>
      /// Build the <see cref="GetAllocationsByClientNettingIdRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="GetAllocationsByClientNettingIdRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public GetAllocationsByClientNettingIdRequest Build()
      {
        Validate();
        return new GetAllocationsByClientNettingIdRequest(_portfolioId!, _clientNettingId!);
      }
    }
  }
}
