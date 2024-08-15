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
  using CoinbaseSdk.Core.Http;

  public interface IOrdersService
  {
    public CreateOrderResponse CreateOrder(
      CreateOrderRequest request,
      CallOptions? options = null);

    public Task<CreateOrderResponse> CreateOrderAsync(
      CreateOrderRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public CancelOrderResponse CancelOrder(
      CancelOrderRequest request,
      CallOptions? options = null);

    public Task<CancelOrderResponse> CancelOrderAsync(
      CancelOrderRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public GetOrderByOrderIdResponse GetOrderByOrderId(
      GetOrderByOrderIdRequest request,
      CallOptions? options = null);

    public Task<GetOrderByOrderIdResponse> GetOrderByOrderIdAsync(
      GetOrderByOrderIdRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public GetOrderPreviewResponse GetOrderPreview(
      GetOrderPreviewRequest request,
      CallOptions? options = null);

    public Task<GetOrderPreviewResponse> GetOrderPreviewAsync(
      GetOrderPreviewRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public ListOpenOrdersResponse ListOpenOrders(
      ListOpenOrdersRequest request,
      CallOptions? options = null);

    public Task<ListOpenOrdersResponse> ListOpenOrdersAsync(
      ListOpenOrdersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public ListOrderFillsResponse ListOrderFills(
      ListOrderFillsRequest request,
      CallOptions? options = null);

    public Task<ListOrderFillsResponse> ListOrderFillsAsync(
      ListOrderFillsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public ListPortfolioOrdersResponse ListPortfolioOrders(
      ListPortfolioOrdersRequest request,
      CallOptions? options = null);

    public Task<ListPortfolioOrdersResponse> ListPortfolioOrdersAsync(
      ListPortfolioOrdersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);
  }
}
