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

namespace CoinbaseSdk.Prime.IntegrationTests.Get.Orders
{
  using CoinbaseSdk.Prime.IntegrationTests.Infrastructure;
  using CoinbaseSdk.Prime.Model.Enums;
  using CoinbaseSdk.Prime.Orders;
  using Xunit;

  [Collection(PrimeIntegrationConstants.CollectionName)]
  public class OrdersGetTests : IntegrationTestBase
  {
    public OrdersGetTests(PrimeIntegrationFixture fixture)
      : base(fixture)
    {
    }

    [SkippableFact]
    public void ListPortfolioOrders_Sync_Smoke()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      new OrdersService(this.Client).ListPortfolioOrders(
        new ListPortfolioOrdersRequest.ListPortfolioOrdersRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithLimit(5).Build());
    }

    [SkippableFact]
    public async Task ListPortfolioOrders_Filters_Dates_Products()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var w = IntegrationTimeWindows.Last7Days();
      var r = await new OrdersService(this.Client).ListPortfolioOrdersAsync(
        new ListPortfolioOrdersRequest.ListPortfolioOrdersRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithProductIds(new[] { this.Fixture.Ids.ProductId })
          .WithOrderType(OrderType.LIMIT)
          .WithOrderSide(OrderSide.BUY)
          .WithOrderStatuses(new[] { OrderStatus.OPEN, OrderStatus.FILLED })
          .WithStartDate(w.Start)
          .WithEndDate(w.End)
          .WithLimit(20)
          .WithSortDirection(SortDirection.DESC)
          .Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public async Task ListOpenOrders_FilterVariations()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var w = IntegrationTimeWindows.Last7Days();
      var p = this.Fixture.Ids.PortfolioId!;
      var svc = new OrdersService(this.Client);
      _ = await svc.ListOpenOrdersAsync(
        new ListOpenOrdersRequest.ListOpenOrdersRequestBuilder()
          .WithPortfolioId(p)
          .WithProductIds(new[] { this.Fixture.Ids.ProductId })
          .WithOrderType(OrderType.LIMIT)
          .WithOrderSide(OrderSide.SELL)
          .WithStartDate(w.Start)
          .WithEndDate(w.End)
          .WithLimit(10)
          .Build());
      _ = await svc.ListOpenOrdersAsync(
        new ListOpenOrdersRequest.ListOpenOrdersRequestBuilder()
          .WithPortfolioId(p).WithOrderType(OrderType.MARKET).WithLimit(5).Build());
    }

    [SkippableFact]
    public void GetOrder_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.OrderId, "No order id; set PRIME_ORDER_ID or have listed orders.");
      var r = new OrdersService(this.Client).GetOrder(
        new GetOrderRequest.GetOrderRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithOrderId(this.Fixture.Ids.OrderId).Build());
      Assert.NotNull(r);
    }

    [SkippableFact]
    public void ListOrderFills_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.OrderId, "No order id for order fills.");
      new OrdersService(this.Client).ListOrderFills(
        new ListOrderFillsRequest.ListOrderFillsRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithOrderId(this.Fixture.Ids.OrderId).WithLimit(10).Build());
    }

    [SkippableFact]
    public void ListPortfolioFills_DateRange_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      var w = IntegrationTimeWindows.Last7Days();
      new OrdersService(this.Client).ListPortfolioFills(
        new ListPortfolioFillsRequest.ListPortfolioFillsRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!)
          .WithStartDate(w.Start)
          .WithEndDate(w.End)
          .WithSortDirection(SortDirection.ASC)
          .WithLimit(20)
          .Build());
    }

    [SkippableFact]
    public void ListOrderEditHistory_Sync()
    {
      this.SkipIfNoCredentials();
      this.RethrowIfBootstrapFailed();
      this.SkipIfNullOrEmpty(this.Fixture.Ids.OrderId, "No order id for edit history.");
      new OrdersService(this.Client).ListOrderEditHistory(
        new ListOrderEditHistoryRequest.ListOrderEditHistoryRequestBuilder()
          .WithPortfolioId(this.Fixture.Ids.PortfolioId!).WithOrderId(this.Fixture.Ids.OrderId).Build());
    }
  }
}
