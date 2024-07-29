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
  using Coinbase.Core.Service;

  public class OrdersService(ICoinbaseClient client) :
   CoinbaseService(client)
  {
    public CreateOrderResponse CreateOrder(string portfolioId, CreateOrderRequest request)
    {
      return Request<CreateOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/order",
        request,
        [HttpStatusCode.Created, HttpStatusCode.OK]);
    }

    public CancelOrderResponse CancelOrder(string portfolioId, string orderId)
    {
      return Request<CancelOrderResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/orders/{orderId}/cancel",
        null,
        [HttpStatusCode.OK]);
    }

    public GetOrderByOrderIdResponse GetOrderByOrderId(string portfolioId, string orderId)
    {
      return Request<GetOrderByOrderIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders/{orderId}",
        null,
        [HttpStatusCode.OK]);
    }

    public GetOrderPreviewResponse GetOrderPreview(string portfolioId, GetOrderPreviewRequest request)
    {
      return Request<GetOrderPreviewResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/order_preview",
        request,
        [HttpStatusCode.OK]);
    }

    public ListOpenOrdersResponse ListOpenOrders(string portfolioId, ListOpenOrdersRequest request)
    {
      return Request<ListOpenOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/open_orders",
        request,
        [HttpStatusCode.OK]);
    }

    public ListOrderFillsResponse ListOrderFills(string portfolioId, string orderId, ListOrderFillsRequest request)
    {
      return Request<ListOrderFillsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders/{orderId}/fills",
        request,
        [HttpStatusCode.OK]);
    }

    public ListPortfolioOrdersResponse ListPortfolioOrders(string portfolioId, ListPortfolioOrdersRequest request)
    {
      return Request<ListPortfolioOrdersResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/orders",
        request,
        [HttpStatusCode.OK]);
    }
  }
}