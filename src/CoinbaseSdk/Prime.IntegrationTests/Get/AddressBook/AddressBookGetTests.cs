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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.AddressBook
{
  using CoinbaseSdk.Prime.AddressBook;
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class AddressBookGetTests : IntegrationTestBase
  {
    public AddressBookGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public async Task ListAddressBookEntries_Filters_Sort_Cursor()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var p = this.Fixture.Ids.PortfolioId!;
      var s = new AddressBookService(this.Client);
      var a = await s.ListAddressBookEntriesAsync(
        new ListAddressBookEntriesRequest.ListAddressBookEntriesRequestBuilder()
          .WithPortfolioId(p)
          .WithSearch("a")
          .WithSortDirection(SortDirection.ASC)
          .WithLimit(10)
          .Build());
      await s.ListAddressBookEntriesAsync(
        new ListAddressBookEntriesRequest.ListAddressBookEntriesRequestBuilder()
          .WithPortfolioId(p)
          .WithCurrencySymbol("BTC")
          .WithLimit(5)
          .Build());
      if (!string.IsNullOrEmpty(a.Pagination?.NextCursor))
      {
        await s.ListAddressBookEntriesAsync(
          new ListAddressBookEntriesRequest.ListAddressBookEntriesRequestBuilder()
            .WithPortfolioId(p)
            .WithCursor(a.Pagination!.NextCursor)
            .WithLimit(5)
            .Build());
      }
    }
  }
}
