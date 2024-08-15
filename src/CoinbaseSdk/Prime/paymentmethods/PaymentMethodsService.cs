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
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class PaymentMethodsService(ICoinbaseClient client) : CoinbaseService(client), IPaymentMethodsService
  {
    public GetEntityPaymentMethodResponse GetEntityPaymentMethod(
      GetEntityPaymentMethodRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetEntityPaymentMethodResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/payment-methods/{request.PaymentMethodId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetEntityPaymentMethodResponse> GetEntityPaymentMethodAsync(
      GetEntityPaymentMethodRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetEntityPaymentMethodResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/payment-methods/{request.PaymentMethodId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public ListEntityPaymentMethodsResponse ListEntityPaymentMethods(
      ListEntityPaymentMethodsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListEntityPaymentMethodsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/payment-methods",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<ListEntityPaymentMethodsResponse> ListEntityPaymentMethodsAsync(
      ListEntityPaymentMethodsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListEntityPaymentMethodsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/payment-methods",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }
  }
}
