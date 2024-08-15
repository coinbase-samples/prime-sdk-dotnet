/*
 * Copyright 2024-present Coinbase Global, Inc.
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

namespace CoinbaseSdk.Prime.Orders
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class OrdersService(ICoinbaseClient client) :
   CoinbaseService(client), IOrdersService
  {
    public CreateOrderResponse CreateOrder(
      CreateOrderRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/order",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateOrderResponse> CreateOrderAsync(
      CreateOrderRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/order",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CancelOrderResponse CancelOrder(
      CancelOrderRequest request,
      CallOptions? options = null)
    {
      return this.Request<CancelOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/orders/{request.OrderId}/cancel",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<CancelOrderResponse> CancelOrderAsync(
      CancelOrderRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CancelOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/orders/{request.OrderId}/cancel",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetOrderByOrderIdResponse GetOrderByOrderId(
      GetOrderByOrderIdRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetOrderByOrderIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/orders/{request.OrderId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetOrderByOrderIdResponse> GetOrderByOrderIdAsync(
      GetOrderByOrderIdRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetOrderByOrderIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/orders/{request.OrderId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetOrderPreviewResponse GetOrderPreview(
      GetOrderPreviewRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetOrderPreviewResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/order_preview",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetOrderPreviewResponse> GetOrderPreviewAsync(
      GetOrderPreviewRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetOrderPreviewResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/order_preview",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListOpenOrdersResponse ListOpenOrders(
      ListOpenOrdersRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListOpenOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/open_orders",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListOpenOrdersResponse> ListOpenOrdersAsync(
      ListOpenOrdersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListOpenOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/open_orders",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListOrderFillsResponse ListOrderFills(
      ListOrderFillsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListOrderFillsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/orders/{request.OrderId}/fills",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListOrderFillsResponse> ListOrderFillsAsync(
      ListOrderFillsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListOrderFillsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/orders/{request.OrderId}/fills",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListPortfolioOrdersResponse ListPortfolioOrders(
      ListPortfolioOrdersRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListPortfolioOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/orders",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListPortfolioOrdersResponse> ListPortfolioOrdersAsync(
      ListPortfolioOrdersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListPortfolioOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/orders",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}
