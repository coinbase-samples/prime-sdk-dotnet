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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Staking
{
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Staking;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class StakingGetTests : IntegrationTestBase
  {
    public StakingGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public void GetStakingStatus_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(PrimeIntegrationEnv.Get("PRIME_STAKING_WALLET_ID"), "Set PRIME_STAKING_WALLET_ID to a staking-eligible wallet to run this test.");
      new StakingService(this.Client).GetStakingStatus(
        new GetStakingStatusRequest.GetStakingStatusRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithWalletId(PrimeIntegrationEnv.Get("PRIME_STAKING_WALLET_ID")!)
          .Build());
    }

    [SkippableFact]
    public void GetUnstakingStatus_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(PrimeIntegrationEnv.Get("PRIME_STAKING_WALLET_ID"), "Set PRIME_STAKING_WALLET_ID to a staking-eligible wallet to run this test.");
      new StakingService(this.Client).GetUnstakingStatus(
        new GetUnstakingStatusRequest.GetUnstakingStatusRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithWalletId(PrimeIntegrationEnv.Get("PRIME_STAKING_WALLET_ID")!)
          .Build());
    }
  }
}
