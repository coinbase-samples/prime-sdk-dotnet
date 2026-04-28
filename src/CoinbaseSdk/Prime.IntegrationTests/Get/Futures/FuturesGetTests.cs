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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Futures
{
  using CoinbaseSdk.Prime.Futures;
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class FuturesGetTests : IntegrationTestBase
  {
    public FuturesGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    private void SkipIfFcmNotEnabled()
    {
      Skip.If(
        !PrimeIntegrationEnv.IsTruthy(PrimeIntegrationEnv.Get("PRIME_FCM_ENABLED")),
        "Set PRIME_FCM_ENABLED=true on an FCM-enabled entity to run futures tests.");
    }

    [SkippableFact]
    public void GetFcmBalance_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfFcmNotEnabled();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for FCM balance.");
      new FuturesService(this.Client).GetFcmBalance(
        new GetFcmBalanceRequest.GetFcmBalanceRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId!)
          .Build());
    }

    [SkippableFact]
    public void GetFcmSettings_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfFcmNotEnabled();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for FCM settings.");
      new FuturesService(this.Client).GetFcmSettings(
        new GetFcmSettingsRequest.GetFcmSettingsRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId!)
          .Build());
    }

    [SkippableFact]
    public void GetFcmEquity_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfFcmNotEnabled();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for FCM equity.");
      new FuturesService(this.Client).GetFcmEquity(
        new GetFcmEquityRequest.GetFcmEquityRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId!)
          .Build());
    }

    [SkippableFact]
    public void GetFcmRiskLimits_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfFcmNotEnabled();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for FCM risk limits.");
      new FuturesService(this.Client).GetFcmRiskLimits(
        new GetFcmRiskLimitsRequest.GetFcmRiskLimitsRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId!)
          .Build());
    }

    [SkippableFact]
    public void GetFcmMarginCallDetails_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfFcmNotEnabled();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for FCM margin call details.");
      new FuturesService(this.Client).GetFcmMarginCallDetails(
        new GetFcmMarginCallDetailsRequest.GetFcmMarginCallDetailsRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId!)
          .Build());
    }

    [SkippableFact]
    public void ListEntityFuturesSweeps_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfFcmNotEnabled();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for futures sweeps.");
      new FuturesService(this.Client).ListEntityFuturesSweeps(
        new ListEntityFuturesSweepsRequest.ListEntityFuturesSweepsRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId!)
          .Build());
    }
  }
}
