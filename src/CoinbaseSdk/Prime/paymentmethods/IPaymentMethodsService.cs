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
  using CoinbaseSdk.Core.Http;

  public interface IPaymentMethodsService
  {
    public GetEntityPaymentMethodResponse GetEntityPaymentMethod(
      GetEntityPaymentMethodRequest request,
      CallOptions? options = null);

    public Task<GetEntityPaymentMethodResponse> GetEntityPaymentMethodAsync(
      GetEntityPaymentMethodRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public ListEntityPaymentMethodsResponse ListEntityPaymentMethods(
      ListEntityPaymentMethodsRequest request,
      CallOptions? options = null);

    public Task<ListEntityPaymentMethodsResponse> ListEntityPaymentMethodsAsync(
      ListEntityPaymentMethodsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);
  }
}
