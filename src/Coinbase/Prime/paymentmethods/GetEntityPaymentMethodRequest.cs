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

namespace Coinbase.Prime.PaymentMethods
{
  using System.Text.Json.Serialization;
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;

  public class GetEntityPaymentMethodRequest(string entityId, string paymentMethodId)
  : BasePrimeRequest(null, entityId)
  {
    [JsonIgnore]
    public string PaymentMethodId { get; set; } = paymentMethodId;

    public class GetEntityPaymentMethodRequestBuilder
    {
      private string? _entityId;
      private string? _paymentMethodId;

      public GetEntityPaymentMethodRequestBuilder WithEntityId(string entityId)
      {
        this._entityId = entityId;
        return this;
      }

      public GetEntityPaymentMethodRequestBuilder WithPaymentMethodId(string paymentMethodId)
      {
        this._paymentMethodId = paymentMethodId;
        return this;
      }

      public void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }

        if (string.IsNullOrWhiteSpace(this._paymentMethodId))
        {
          throw new CoinbaseClientException("PaymentMethodId is required");
        }
      }

      public GetEntityPaymentMethodRequest Build()
      {
        this.Validate();
        return new GetEntityPaymentMethodRequest(this._entityId!, this._paymentMethodId!);
      }
    }
  }
}