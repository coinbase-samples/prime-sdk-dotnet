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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Balances
{
  using CoinbaseSdk.Prime.Balances;
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class BalancesGetTests : IntegrationTestBase
  {
    public BalancesGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public async Task ListPortfolioBalances_Async_Baseline()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var r = await new BalancesService(this.Client).ListPortfolioBalancesAsync(
        new ListPortfolioBalancesRequest.ListPortfolioBalancesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithLimit(20).Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public async Task ListPortfolioBalances_Async_WithSymbols()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var r = await new BalancesService(this.Client).ListPortfolioBalancesAsync(
        new ListPortfolioBalancesRequest.ListPortfolioBalancesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithSymbols(new[] { "BTC", "ETH" })
          .WithLimit(20)
          .Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public async Task ListPortfolioBalances_Async_EachBalanceType()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var s = this.Fixture.Ids.PortfolioId!;
      var svc = new BalancesService(this.Client);
      foreach (var t in new[]
               {
                 PortfolioBalanceType.TRADING_BALANCES,
                 PortfolioBalanceType.VAULT_BALANCES,
                 PortfolioBalanceType.TOTAL_BALANCES,
               })
      {
        var r = await svc.ListPortfolioBalancesAsync(
          new ListPortfolioBalancesRequest.ListPortfolioBalancesRequestBuilder()
            .WithPortfolioId(s).WithBalanceType(t).WithLimit(5).Build());
        Assert.NotNull(r);
      }
    }

    [SkippableFact]
    public void ListPortfolioBalances_Sync_Smoke()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      new BalancesService(this.Client).ListPortfolioBalances(
        new ListPortfolioBalancesRequest.ListPortfolioBalancesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithLimit(5).Build());
    }

    [SkippableFact]
    public async Task ListEntityBalances_ReturnsData()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for entity balances.");
      var r = await new BalancesService(this.Client).ListEntityBalancesAsync(
        new ListEntityBalancesRequest.ListEntityBalancesRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId)
          .WithSymbols(new[] { "BTC" })
          .WithLimit(20)
          .WithSortDirection(SortDirection.DESC)
          .Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public async Task ListOnchainWalletBalances_ReturnsData()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.OnchainWalletId, "No onchain wallet id; set PRIME_ONCHAIN_WALLET_ID.");
      var r = await new BalancesService(this.Client).ListOnchainWalletBalancesAsync(
        new ListOnchainWalletBalancesRequest.ListOnchainWalletBalancesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithWalletId(this.Fixture.Ids.OnchainWalletId)
          .WithVisibilityStatuses(new[] { VisibilityStatus.VISIBLE })
          .WithLimit(10)
          .WithSortDirection(SortDirection.ASC)
          .Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public void GetWalletBalance_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.WalletId, "No wallet id for balance get.");
      var r = new BalancesService(this.Client).GetWalletBalance(
        new GetWalletBalanceRequest.GetWalletBalanceRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithWalletId(this.Fixture.Ids.WalletId).Build());
      Assert.NotNull(r);
    }
  }
}
