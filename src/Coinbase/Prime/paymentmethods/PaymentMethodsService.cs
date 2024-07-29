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
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Service;

  public class PaymentMethodsService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public GetEntityPaymentMethodResponse GetEntityPaymentMethod(string entityId, string paymentMethodId)
    {
      return Request<GetEntityPaymentMethodResponse>(
        HttpMethod.Get,
        $"/entities/{entityId}/payment-methods/{paymentMethodId}",
        [HttpStatusCode.OK]);
    }

    public ListEntityPaymentMethodsResponse ListEntityPaymentMethods(string entityId)
    {
      return Request<ListEntityPaymentMethodsResponse>(
        HttpMethod.Get,
        $"/entities/{entityId}/payment-methods",
        [HttpStatusCode.OK]);
    }
  }
}