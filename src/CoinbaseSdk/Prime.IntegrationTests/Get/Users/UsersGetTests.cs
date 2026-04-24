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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Users
{
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using CoinbaseSdk.Prime.Users;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class UsersGetTests : IntegrationTestBase
  {
    public UsersGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public async Task ListUsers_Pagination_Sort()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required to list users.");
      var c = new UsersService(this.Client);
      var a = await c.ListUsersAsync(
        new ListUsersRequest.ListUsersRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId)
          .WithLimit(3)
          .WithSortDirection(SortDirection.ASC)
          .Build());
      if (!string.IsNullOrEmpty(a.Pagination?.NextCursor))
      {
        await c.ListUsersAsync(
          new ListUsersRequest.ListUsersRequestBuilder()
            .WithEntityId(this.Fixture.Ids.EntityId)
            .WithCursor(a.Pagination!.NextCursor)
            .WithLimit(2)
            .Build());
      }
    }

    [SkippableFact]
    public async Task ListPortfolioUsers_Cursor_Sort()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var c = new UsersService(this.Client);
      var a = await c.ListPortfolioUsersAsync(
        new ListPortfolioUsersRequest.ListPortfolioUsersRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithLimit(5)
          .WithSortDirection(SortDirection.DESC)
          .Build());
      if (!string.IsNullOrEmpty(a.Pagination?.NextCursor))
      {
        await c.ListPortfolioUsersAsync(
          new ListPortfolioUsersRequest.ListPortfolioUsersRequestBuilder()
            .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
            .WithCursor(a.Pagination!.NextCursor)
            .WithLimit(2)
            .Build());
      }
    }
  }
}
