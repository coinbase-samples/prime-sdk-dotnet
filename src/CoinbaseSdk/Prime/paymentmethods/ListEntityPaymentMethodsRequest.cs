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

namespace CoinbaseSdk.Prime.PaymentMethods
{
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;

  public class ListEntityPaymentMethodsRequest(string entityId)
  : BasePrimeRequest(null, entityId)
  {
    public class ListEntityPaymentMethodsRequestBuilder
    {
      private string? _entityId;

      public ListEntityPaymentMethodsRequestBuilder WithEntityId(string entityId)
      {
        this._entityId = entityId;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_entityId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._entityId))
        {
          throw new CoinbaseClientException("EntityId cannot be null or empty");
        }
      }

      /// <summary>
      /// Build the <see cref="ListEntityPaymentMethodsRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListEntityPaymentMethodsRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public ListEntityPaymentMethodsRequest Build()
      {
        this.Validate();
        return new ListEntityPaymentMethodsRequest(this._entityId!);
      }
    }
  }
}
