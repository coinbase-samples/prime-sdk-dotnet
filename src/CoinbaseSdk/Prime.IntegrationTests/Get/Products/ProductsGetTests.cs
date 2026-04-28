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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Products
{
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using CoinbaseSdk.Prime.Products;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class ProductsGetTests : IntegrationTestBase
  {
    public ProductsGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public async Task ListPortfolioProducts_Filters_Cursor_Sort()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var p = this.Fixture.Ids.PortfolioId!;
      var c = new ProductsService(this.Client);
      var a = await c.ListPortfolioProductsAsync(
        new ListPortfolioProductsRequest.ListPortfolioProductsRequestBuilder()
          .WithPortfolioId(p)
          .WithProductType("SPOT")
          .WithLimit(5)
          .WithSortDirection(SortDirection.ASC)
          .Build());
      if (!string.IsNullOrEmpty(a.Pagination?.NextCursor))
      {
        await c.ListPortfolioProductsAsync(
          new ListPortfolioProductsRequest.ListPortfolioProductsRequestBuilder()
            .WithPortfolioId(p)
            .WithCursor(a.Pagination!.NextCursor)
            .WithLimit(2)
            .Build());
      }
    }

    [SkippableFact]
    public void GetCandles_Granularity_Dates()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var shortWindow = IntegrationTimeWindows.LastFiveHours();
      var dailyWindow = IntegrationTimeWindows.Last7Days();
      new ProductsService(this.Client).GetCandles(
        new GetCandlesRequest.GetCandlesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithProductId(this.Fixture.Ids.ProductId)
          .WithStartTime(shortWindow.Start)
          .WithEndTime(shortWindow.End)
          .WithGranularity("ONE_MINUTE")
          .Build());
      new ProductsService(this.Client).GetCandles(
        new GetCandlesRequest.GetCandlesRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithProductId(this.Fixture.Ids.ProductId)
          .WithStartTime(dailyWindow.Start)
          .WithEndTime(dailyWindow.End)
          .WithGranularity("FIVE_MINUTE")
          .Build());
    }
  }
}
