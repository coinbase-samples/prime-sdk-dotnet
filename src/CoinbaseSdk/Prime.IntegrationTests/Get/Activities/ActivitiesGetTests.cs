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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Activities
{
  using CoinbaseSdk.Prime.Activities;
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class ActivitiesGetTests : IntegrationTestBase
  {
    public ActivitiesGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public void ListActivities_Sync_ReturnsData()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var p = this.Fixture.Ids.PortfolioId!;
      var svc = new ActivitiesService(this.Client);
      var r = svc.ListActivities(
        new ListActivitiesRequest.ListActivitiesRequestBuilder().WithPortfolioId(p).WithLimit(5).Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public async Task ListActivities_Async_ReturnsData()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var p = this.Fixture.Ids.PortfolioId!;
      var svc = new ActivitiesService(this.Client);
      var r = await svc.ListActivitiesAsync(
        new ListActivitiesRequest.ListActivitiesRequestBuilder().WithPortfolioId(p).WithLimit(5).Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public async Task ListActivities_FilterCategories_ReturnsData()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var w = IntegrationTimeWindows.Last7Days();
      var r = await new ActivitiesService(this.Client).ListActivitiesAsync(
        new ListActivitiesRequest.ListActivitiesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithCategories(new ActivityCategory?[] { ActivityCategory.ACTIVITY_CATEGORY_ORDER, ActivityCategory.ACTIVITY_CATEGORY_TRANSACTION })
          .WithStartTime(w.Start)
          .WithEndTime(w.End)
          .WithLimit(10)
          .Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public async Task ListActivities_FilterStatuses_ReturnsData()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var w = IntegrationTimeWindows.Last7Days();
      var r = await new ActivitiesService(this.Client).ListActivitiesAsync(
        new ListActivitiesRequest.ListActivitiesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithStatuses(new ActivityStatus?[] { ActivityStatus.ACTIVITY_STATUS_COMPLETED, ActivityStatus.ACTIVITY_STATUS_PROCESSING })
          .WithStartTime(w.Start)
          .WithEndTime(w.End)
          .WithLimit(10)
          .Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public async Task ListActivities_SortAscDesc_ReturnsData()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var b = new ListActivitiesRequest.ListActivitiesRequestBuilder()
        .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithLimit(3);
      var a = await new ActivitiesService(this.Client).ListActivitiesAsync(
        b.WithSortDirection(SortDirection.ASC).Build());
      var d = await new ActivitiesService(this.Client).ListActivitiesAsync(
        new ListActivitiesRequest.ListActivitiesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithLimit(3).WithSortDirection(SortDirection.DESC).Build());
      Assert.NotNull(a);
      Assert.NotNull(d);
    }

    [SkippableFact]
    public async Task ListActivities_Pagination_UsesCursor()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var s = new ActivitiesService(this.Client);
      var first = await s.ListActivitiesAsync(
        new ListActivitiesRequest.ListActivitiesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithLimit(1).Build());
      if (string.IsNullOrEmpty(first.Pagination?.NextCursor))
      {
        this.SkipBecause("No second page of activities for pagination test.");
        return;
      }

      var second = await s.ListActivitiesAsync(
        new ListActivitiesRequest.ListActivitiesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithLimit(1)
          .WithCursor(first.Pagination!.NextCursor)
          .Build());
      Assert.NotNull(second);
    }

    [SkippableFact]
    public void GetActivity_Sync_ById()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.ActivityId, "No activity id; set PRIME_ACTIVITY_ID or ensure portfolio has activities.");
      var r = new ActivitiesService(this.Client).GetActivity(
        new GetActivityRequest.GetActivityRequestBuilder().WithActivityId(this.Fixture.Ids.ActivityId).Build());
      Assert.NotNull(r);
      Assert.NotNull(r.Activity);
    }

    [SkippableFact]
    public void GetPortfolioActivity_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.ActivityId, "No activity id for portfolio-activity get.");
      var r = new ActivitiesService(this.Client).GetPortfolioActivity(
        new GetPortfolioActivityRequest.GetPortfolioActivityRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithActivityId(this.Fixture.Ids.ActivityId).Build());
      Assert.NotNull(r);
      Assert.NotNull(r.Activity);
    }

    [SkippableFact]
    public async Task ListEntityActivities_ReturnsData()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.EntityId, "Set PRIME_ENTITY_ID or use a portfolio with entity metadata.");
      var w = IntegrationTimeWindows.Last7Days();
      var r = await new ActivitiesService(this.Client).ListEntityActivitiesAsync(
        new ListEntityActivitiesRequest.ListEntityActivitiesRequestBuilder()
          .WithEntityId(this.Fixture.Ids.EntityId)
          .WithActivityLevel(ActivityLevel.ACTIVITY_LEVEL_ALL)
          .WithSymbols(new[] { "BTC" })
          .WithCategories(new ActivityCategory?[] { ActivityCategory.ACTIVITY_CATEGORY_ACCOUNT })
          .WithStatuses(new ActivityStatus?[] { ActivityStatus.ACTIVITY_STATUS_COMPLETED })
          .WithStartTime(w.Start)
          .WithEndTime(w.End)
          .WithLimit(10)
          .WithSortDirection(SortDirection.ASC)
          .Build());
      Assert.NotNull(r);
    }
  }
}
