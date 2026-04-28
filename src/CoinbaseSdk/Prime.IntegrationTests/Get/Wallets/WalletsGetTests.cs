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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Wallets
{
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using CoinbaseSdk.Prime.Wallets;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class WalletsGetTests : IntegrationTestBase
  {
    public WalletsGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public void ListWallets_Types_Symbols_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var s = this.Fixture.Ids.PortfolioId!;
      var client = new WalletsService(this.Client);
      _ = client.ListWallets(
        new ListWalletsRequest.ListWalletsRequestBuilder()
          .WithPortfolioId(s).WithType("VAULT").WithLimit(5).Build());
      _ = client.ListWallets(
        new ListWalletsRequest.ListWalletsRequestBuilder()
          .WithPortfolioId(s).WithSymbols(new[] { "BTC" }).WithLimit(5).Build());
    }

    [SkippableFact]
    public async Task ListWallets_Cursor_Sort_Pagination()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var s = this.Fixture.Ids.PortfolioId!;
      var c = new WalletsService(this.Client);
      var first = await c.ListWalletsAsync(
        new ListWalletsRequest.ListWalletsRequestBuilder()
          .WithPortfolioId(s).WithLimit(1).WithSortDirection(SortDirection.ASC).Build());
      if (!string.IsNullOrEmpty(first.Pagination?.NextCursor))
      {
        _ = await c.ListWalletsAsync(
          new ListWalletsRequest.ListWalletsRequestBuilder()
            .WithPortfolioId(s)
            .WithCursor(first.Pagination!.NextCursor)
            .WithLimit(1)
            .Build());
      }
    }

    [SkippableFact]
    public void GetWallet_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.WalletId, "No wallet id.");
      var r = new WalletsService(this.Client).GetWallet(
        new GetWalletRequest.GetWalletRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithWalletId(this.Fixture.Ids.WalletId).Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public void ListWalletAddresses_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.WalletId, "No wallet id.");
      new WalletsService(this.Client).ListWalletAddresses(
        new ListWalletAddressesRequest.ListWalletAddressesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithWalletId(this.Fixture.Ids.WalletId).WithLimit(10).Build());
    }

    [SkippableFact]
    public void GetWalletDepositInstructions_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.WalletId, "No wallet id.");
      new WalletsService(this.Client).GetWalletDepositInstructions(
        new GetWalletDepositInstructionsRequest.GetWalletDepositInstructionsRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithWalletId(this.Fixture.Ids.WalletId)
          .WithDepositType("CRYPTO")
          .Build());
    }
  }
}
