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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Transactions
{
  using System.Net;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using CoinbaseSdk.Prime.Transactions;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class TransactionsGetTests : IntegrationTestBase
  {
    public TransactionsGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public async Task ListPortfolioTransactions_Filters()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var w = IntegrationTimeWindows.Last7Days();
      var r = await new TransactionsService(this.Client).ListPortfolioTransactionsAsync(
        new ListPortfolioTransactionsRequest.ListPortfolioTransactionsRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithSymbols(new[] { "BTC" })
          .WithStartTime(w.Start)
          .WithEndTime(w.End)
          .WithLimit(20)
          .WithSortDirection(SortDirection.DESC)
          .Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public async Task ListWalletTransactions_WhenWalletExists()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.WalletId, "No wallet id for wallet transactions.");
      var r = await new TransactionsService(this.Client).ListWalletTransactionsAsync(
        new ListWalletTransactionsRequest.ListWalletTransactionsRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithWalletId(this.Fixture.Ids.WalletId)
          .WithLimit(10)
          .Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public void GetTransaction_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.TransactionId, "No transaction id.");
      var r = new TransactionsService(this.Client).GetTransaction(
        new GetTransactionRequest.GetTransactionRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithTransactionId(this.Fixture.Ids.TransactionId).Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public void GetTransactionTravelRuleData_MayReturnNotFound()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.TransactionId, "No transaction id.");
      try
      {
        new TransactionsService(this.Client).GetTransactionTravelRuleData(
          new GetTransactionTravelRuleDataRequest.GetTransactionTravelRuleDataRequestBuilder()
            .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithTransactionId(this.Fixture.Ids.TransactionId).Build());
      }
      catch (CoinbaseException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
      {
        this.SkipBecause("Travel rule data not available for this transaction.");
      }
    }
  }
}
