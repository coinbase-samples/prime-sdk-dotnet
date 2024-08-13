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
  using System.Text.Json.Serialization;
  public class ListEntityPaymentMethodsResponse
  {
    [JsonPropertyName("payment_methods")]
    public EntityPaymentMethod[] PaymentMethods { get; set; } = [];

    public ListEntityPaymentMethodsResponse() { }

    public class ListEntityPaymentMethodsResponseBuilder
    {
      private EntityPaymentMethod[] _paymentMethods = [];

      public ListEntityPaymentMethodsResponseBuilder WithPaymentMethods(EntityPaymentMethod[] paymentMethods)
      {
        this._paymentMethods = paymentMethods;
        return this;
      }

      public ListEntityPaymentMethodsResponse Build()
      {
        return new ListEntityPaymentMethodsResponse
        {
          PaymentMethods = this._paymentMethods
        };
      }
    }
  }
}
