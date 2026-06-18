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
    /// <summary>
    /// List Portfolio Fills.
    /// </summary>
    public ListPortfolioFillsResponse ListPortfolioFills(
      ListPortfolioFillsRequest request,
      CallOptions? options = null);

    public Task<ListPortfolioFillsResponse> ListPortfolioFillsAsync(
      ListPortfolioFillsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Open Orders.
    /// </summary>
    public ListOpenOrdersResponse ListOpenOrders(
      ListOpenOrdersRequest request,
      CallOptions? options = null);

    public Task<ListOpenOrdersResponse> ListOpenOrdersAsync(
      ListOpenOrdersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Portfolio Orders.
    /// </summary>
    public ListPortfolioOrdersResponse ListPortfolioOrders(
      ListPortfolioOrdersRequest request,
      CallOptions? options = null);

    public Task<ListPortfolioOrdersResponse> ListPortfolioOrdersAsync(
      ListPortfolioOrdersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Order by Order ID.
    /// </summary>
    public GetOrderResponse GetOrder(
      GetOrderRequest request,
      CallOptions? options = null);

    public Task<GetOrderResponse> GetOrderAsync(
      GetOrderRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Order Edit History.
    /// </summary>
    public ListOrderEditHistoryResponse ListOrderEditHistory(
      ListOrderEditHistoryRequest request,
      CallOptions? options = null);

    public Task<ListOrderEditHistoryResponse> ListOrderEditHistoryAsync(
      ListOrderEditHistoryRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Order Fills.
    /// </summary>
    public ListOrderFillsResponse ListOrderFills(
      ListOrderFillsRequest request,
      CallOptions? options = null);

    public Task<ListOrderFillsResponse> ListOrderFillsAsync(
      ListOrderFillsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Accept Quote.
    /// </summary>
    public AcceptQuoteResponse AcceptQuote(
      AcceptQuoteRequest request,
      CallOptions? options = null);

    public Task<AcceptQuoteResponse> AcceptQuoteAsync(
      AcceptQuoteRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Create Order.
    /// </summary>
    public CreateOrderResponse CreateOrder(
      CreateOrderRequest request,
      CallOptions? options = null);

    public Task<CreateOrderResponse> CreateOrderAsync(
      CreateOrderRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Order Preview.
    /// </summary>
    public GetOrderPreviewResponse GetOrderPreview(
      GetOrderPreviewRequest request,
      CallOptions? options = null);

    public Task<GetOrderPreviewResponse> GetOrderPreviewAsync(
      GetOrderPreviewRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Create Quote Request.
    /// </summary>
    public CreateQuoteResponse CreateQuote(
      CreateQuoteRequest request,
      CallOptions? options = null);

    public Task<CreateQuoteResponse> CreateQuoteAsync(
      CreateQuoteRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancel Order.
    /// </summary>
    public CancelOrderResponse CancelOrder(
      CancelOrderRequest request,
      CallOptions? options = null);

    public Task<CancelOrderResponse> CancelOrderAsync(
      CancelOrderRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Edit Order (Beta).
    /// </summary>
    public EditOrderResponse EditOrder(
      EditOrderRequest request,
      CallOptions? options = null);

    public Task<EditOrderResponse> EditOrderAsync(
      EditOrderRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);
  }
}
