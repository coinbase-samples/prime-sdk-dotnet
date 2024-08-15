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

namespace CoinbaseSdk.Prime.Assets
{
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  public class ListAssetsRequest(string entityId) : BasePrimeRequest(null, entityId)
  {
    public class ListAssetsRequestBuilder
    {
      private string? _entityId;

      public ListAssetsRequestBuilder WithEntityId(string entityId)
      {
        this._entityId = entityId;
        return this;
      }

      /// <summary>
      /// Build the <see cref="ListAssetsRequest"/> object.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_entityId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      /// <summary>
      /// Build the <see cref="ListAssetsRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="ListAssetsRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public ListAssetsRequest Build()
      {
        this.Validate();
        return new ListAssetsRequest(this._entityId!);
      }
    }
  }
}
