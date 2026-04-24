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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Positions
{
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using CoinbaseSdk.Prime.Positions;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class PositionsGetTests : IntegrationTestBase
  {
    public PositionsGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public async Task ListEntityPositions_Cursor_Sort()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for positions.");
      var c = new PositionsService(this.Client);
      var a = await c.ListEntityPositionsAsync(
        new ListEntityPositionsRequest.ListEntityPositionsRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId)
          .WithLimit(5)
          .WithSortDirection(SortDirection.ASC)
          .Build());
      if (!string.IsNullOrEmpty(a.Pagination?.NextCursor))
      {
        await c.ListEntityPositionsAsync(
          new ListEntityPositionsRequest.ListEntityPositionsRequestBuilder()
            .WithEntityId(this.Fixture.Ids.EntityId)
            .WithCursor(a.Pagination!.NextCursor)
            .WithLimit(2)
            .Build());
      }
    }

    [SkippableFact]
    public async Task ListAggregateEntityPositions_Cursor_Sort()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Entity id required for aggregate positions.");
      var c = new PositionsService(this.Client);
      _ = await c.ListAggregateEntityPositionsAsync(
        new ListAggregateEntityPositionsRequest.ListAggregateEntityPositionsRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId)
          .WithLimit(5)
          .WithSortDirection(SortDirection.DESC)
          .Build());
    }
  }
}
