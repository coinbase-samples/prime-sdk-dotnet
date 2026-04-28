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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Financing
{
  using CoinbaseSdk.Prime.Financing;
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class FinancingGetTests : IntegrationTestBase
  {
    public FinancingGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public void GetPortfolioBuyingPower_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      new FinancingService(this.Client).GetPortfolioBuyingPower(
        new GetPortfolioBuyingPowerRequest.GetPortfolioBuyingPowerRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithBaseCurrency("BTC")
          .WithQuoteCurrency("USD")
          .Build());
    }

    [SkippableFact]
    public void GetPortfolioCreditInformation_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      new FinancingService(this.Client).GetPortfolioCreditInformation(
        new GetPortfolioCreditInformationRequest.GetPortfolioCreditInformationRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .Build());
    }

    [SkippableFact]
    public void GetPortfolioWithdrawalPower_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(PrimeIntegrationEnv.Get("PRIME_MARGIN_ENABLED"), "Set PRIME_MARGIN_ENABLED=true on a margin-enabled entity to run this test.");
      new FinancingService(this.Client).GetPortfolioWithdrawalPower(
        new GetPortfolioWithdrawalPowerRequest.GetPortfolioWithdrawalPowerRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .Build());
    }

    [SkippableFact]
    public void GetCrossMarginOverview_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(PrimeIntegrationEnv.Get("PRIME_MARGIN_ENABLED"), "Set PRIME_MARGIN_ENABLED=true on a margin-enabled entity to run this test.");
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for cross margin overview.");
      new FinancingService(this.Client).GetCrossMarginOverview(
        new GetCrossMarginOverviewRequest.GetCrossMarginOverviewRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId!)
          .Build());
    }

    [SkippableFact]
    public void GetMarginInformation_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(PrimeIntegrationEnv.Get("PRIME_MARGIN_ENABLED"), "Set PRIME_MARGIN_ENABLED=true on a margin-enabled entity to run this test.");
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for margin information.");
      new FinancingService(this.Client).GetMarginInformation(
        new GetMarginInformationRequest.GetMarginInformationRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId!)
          .Build());
    }

    [SkippableFact]
    public void ListFinancingEligibleAssets_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      new FinancingService(this.Client).ListFinancingEligibleAssets();
    }

    [SkippableFact]
    public void ListInterestAccruals_WithEntityId()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for interest accruals.");
      var window = IntegrationTimeWindows.Last30Days();
      new FinancingService(this.Client).ListInterestAccruals(
        new ListInterestAccrualsRequest.ListInterestAccrualsRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId!)
          .WithStartDate(window.Start)
          .WithEndDate(window.End)
          .Build());
    }

    [SkippableFact]
    public void ListInterestAccrualsForPortfolio_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var window = IntegrationTimeWindows.Last30Days();
      new FinancingService(this.Client).ListInterestAccrualsForPortfolio(
        new ListInterestAccrualsForPortfolioRequest.ListInterestAccrualsForPortfolioRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithStartDate(window.Start)
          .WithEndDate(window.End)
          .Build());
    }
  }
}
