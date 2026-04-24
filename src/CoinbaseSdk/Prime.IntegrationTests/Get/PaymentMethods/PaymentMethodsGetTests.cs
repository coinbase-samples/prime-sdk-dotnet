/*
 * Copyright 2026-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.PaymentMethods
{
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.PaymentMethods;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class PaymentMethodsGetTests : IntegrationTestBase
  {
    public PaymentMethodsGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public void ListEntityPaymentMethods_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for payment methods.");
      new PaymentMethodsService(this.Client).ListEntityPaymentMethods(
        new ListEntityPaymentMethodsRequest.ListEntityPaymentMethodsRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId).Build());
    }

    [SkippableFact]
    public void GetEntityPaymentMethod_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required.");
      this.SkipIfNullOrEmpty(this.Fixture.Ids.PaymentMethodId, "No payment method id; set PRIME_PAYMENT_METHOD_ID or discover via list.");
      new PaymentMethodsService(this.Client).GetEntityPaymentMethod(
        new GetEntityPaymentMethodRequest.GetEntityPaymentMethodRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId)
          .WithPaymentMethodId(this.Fixture.Ids.PaymentMethodId)
          .Build());
    }
  }
}
