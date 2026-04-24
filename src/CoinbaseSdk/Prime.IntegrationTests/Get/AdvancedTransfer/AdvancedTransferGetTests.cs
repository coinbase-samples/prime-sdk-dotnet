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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.AdvancedTransfer
{
  using System.Linq;
  using CoinbaseSdk.Prime.AdvancedTransfer;
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class AdvancedTransferGetTests : IntegrationTestBase
  {
    public AdvancedTransferGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public async Task ListAdvancedTransfers_Paginated_Sort()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var s = this.Fixture.Ids.PortfolioId!;
      var c = new AdvancedTransferService(this.Client);
      _ = await c.ListAdvancedTransfersAsync(
        new ListAdvancedTransfersRequest.ListAdvancedTransfersRequestBuilder()
          .WithPortfolioId(s)
          .WithLimit(5)
          .WithSortDirection(SortDirection.ASC)
          .Build());
    }

    [SkippableFact]
    public async Task ListAdvancedTransferTransactions_WhenTransferExists()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var list = await new AdvancedTransferService(this.Client).ListAdvancedTransfersAsync(
        new ListAdvancedTransfersRequest.ListAdvancedTransfersRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithLimit(1).Build());
      var atId = list.AdvancedTransfers?.FirstOrDefault()?.Id;
      if (string.IsNullOrEmpty(atId))
      {
        this.SkipBecause("No advanced transfers for this portfolio.");
        return;
      }

      _ = await new AdvancedTransferService(this.Client).ListAdvancedTransferTransactionsAsync(
        new ListAdvancedTransferTransactionsRequest.ListAdvancedTransferTransactionsRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithAdvancedTransferId(atId).Build());
    }
  }
}
