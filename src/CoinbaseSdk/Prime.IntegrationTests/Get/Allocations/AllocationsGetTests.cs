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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Allocations
{
  using CoinbaseSdk.Prime.Allocations;
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class AllocationsGetTests : IntegrationTestBase
  {
    public AllocationsGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public async Task ListPortfolioAllocations_Filters_Sort_Cursor()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var w = IntegrationTimeWindows.Last7Days();
      var p = this.Fixture.Ids.PortfolioId!;
      var s = new AllocationsService(this.Client);
      var a = await s.ListPortfolioAllocationsAsync(
        new ListPortfolioAllocationsRequest.ListPortfolioAllocationsRequestBuilder()
          .WithPortfolioId(p)
          .WithOrderSide(OrderSide.BUY)
          .WithStartDate(w.Start)
          .WithEndDate(w.End)
          .WithProductIds(new[] { this.Fixture.Ids.ProductId })
          .WithLimit(5)
          .WithSortDirection(SortDirection.DESC)
          .Build());
      if (!string.IsNullOrEmpty(a.Pagination?.NextCursor))
      {
        await s.ListPortfolioAllocationsAsync(
          new ListPortfolioAllocationsRequest.ListPortfolioAllocationsRequestBuilder()
            .WithPortfolioId(p)
            .WithCursor(a.Pagination!.NextCursor)
            .WithLimit(3)
            .Build());
      }
    }

    [SkippableFact]
    public void GetAllocation_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.AllocationId, "No allocation id available.");
      new AllocationsService(this.Client).GetAllocation(
        new GetAllocationRequest.GetAllocationRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithAllocationId(this.Fixture.Ids.AllocationId).Build());
    }

    [SkippableFact]
    public void ListAllocationsByClientNettingId_WhenNettingId()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.ClientNettingId, "Set PRIME_CLIENT_NETTING_ID or have allocations with netting_id.");
      new AllocationsService(this.Client).ListAllocationsByClientNettingId(
        new ListAllocationsByClientNettingIdRequest.ListAllocationsByClientNettingIdRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithNettingId(this.Fixture.Ids.ClientNettingId).Build());
    }
  }
}
