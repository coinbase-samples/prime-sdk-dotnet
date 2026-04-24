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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.OnchainAddressBook
{
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.OnchainAddressBook;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class OnchainAddressBookGetTests : IntegrationTestBase
  {
    public OnchainAddressBookGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public void ListOnchainAddressGroups_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      new OnchainAddressBookService(this.Client).ListOnchainAddressGroups(
        new ListOnchainAddressGroupsRequest.ListOnchainAddressGroupsRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).Build());
    }
  }
}
