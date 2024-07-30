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

namespace Coinbase.Prime.Orders
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Http;
  using Coinbase.Core.Service;

  public class OrdersService(ICoinbaseClient client) :
   CoinbaseService(client)
  {
    public CreateOrderResponse CreateOrder(
      string portfolioId,
      CreateOrderRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/order",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateOrderResponse> CreateOrderAsync(
      string portfolioId,
      CreateOrderRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/order",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CancelOrderResponse CancelOrder(
      string portfolioId,
      string orderId,
      CallOptions? options = null)
    {
      return this.Request<CancelOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/orders/{orderId}/cancel",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<CancelOrderResponse> CancelOrderAsync(
      string portfolioId,
      string orderId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CancelOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/orders/{orderId}/cancel",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetOrderByOrderIdResponse GetOrderByOrderId(
      string portfolioId,
      string orderId,
      CallOptions? options = null)
    {
      return this.Request<GetOrderByOrderIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders/{orderId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetOrderByOrderIdResponse> GetOrderByOrderIdAsync(
      string portfolioId,
      string orderId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetOrderByOrderIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders/{orderId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetOrderPreviewResponse GetOrderPreview(
      string portfolioId,
      GetOrderPreviewRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetOrderPreviewResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/order_preview",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetOrderPreviewResponse> GetOrderPreviewAsync(
      string portfolioId,
      GetOrderPreviewRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetOrderPreviewResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/order_preview",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListOpenOrdersResponse ListOpenOrders(
      string portfolioId,
      ListOpenOrdersRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListOpenOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/open_orders",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListOpenOrdersResponse> ListOpenOrdersAsync(
      string portfolioId,
      ListOpenOrdersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListOpenOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/open_orders",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListOrderFillsResponse ListOrderFills(
      string portfolioId,
      string orderId,
      ListOrderFillsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListOrderFillsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders/{orderId}/fills",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListOrderFillsResponse> ListOrderFillsAsync(
      string portfolioId,
      string orderId,
      ListOrderFillsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListOrderFillsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders/{orderId}/fills",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListPortfolioOrdersResponse ListPortfolioOrders(
      string portfolioId,
      ListPortfolioOrdersRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListPortfolioOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListPortfolioOrdersResponse> ListPortfolioOrdersAsync(
      string portfolioId,
      ListPortfolioOrdersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListPortfolioOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}