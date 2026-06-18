/*
 * Copyright 2026-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.AdvancedTransfer
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class AdvancedTransferService(ICoinbaseClient client) : CoinbaseService(client), IAdvancedTransferService
  {
    /// <summary>
    /// List Advanced Transfers.
    /// </summary>
    public ListAdvancedTransfersResponse ListAdvancedTransfers(
      ListAdvancedTransfersRequest request,
      CallOptions? options = null)
    {
      return Request<ListAdvancedTransfersResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/advanced_transfers",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListAdvancedTransfersResponse> ListAdvancedTransfersAsync(
      ListAdvancedTransfersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListAdvancedTransfersResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/advanced_transfers",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Portfolio Counterparty ID.
    /// </summary>
    public GetPortfolioCounterpartyIDResponse GetPortfolioCounterpartyID(
      GetPortfolioCounterpartyIDRequest request,
      CallOptions? options = null)
    {
      return Request<GetPortfolioCounterpartyIDResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/counterparty",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetPortfolioCounterpartyIDResponse> GetPortfolioCounterpartyIDAsync(
      GetPortfolioCounterpartyIDRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetPortfolioCounterpartyIDResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/counterparty",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// List transactions associated with an Advanced Transfer.
    /// </summary>
    public ListAdvancedTransferTransactionsResponse ListAdvancedTransferTransactions(
      ListAdvancedTransferTransactionsRequest request,
      CallOptions? options = null)
    {
      return Request<ListAdvancedTransferTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/advanced_transfers/{request.AdvancedTransferId}/transactions",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<ListAdvancedTransferTransactionsResponse> ListAdvancedTransferTransactionsAsync(
      ListAdvancedTransferTransactionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListAdvancedTransferTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/advanced_transfers/{request.AdvancedTransferId}/transactions",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Create Advanced Transfer.
    /// </summary>
    public CreateAdvancedTransferResponse CreateAdvancedTransfer(
      CreateAdvancedTransferRequest request,
      CallOptions? options = null)
    {
      return Request<CreateAdvancedTransferResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/advanced_transfers",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateAdvancedTransferResponse> CreateAdvancedTransferAsync(
      CreateAdvancedTransferRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CreateAdvancedTransferResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/advanced_transfers",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Cancel Advanced Transfer.
    /// </summary>
    public CancelAdvancedTransferResponse CancelAdvancedTransfer(
      CancelAdvancedTransferRequest request,
      CallOptions? options = null)
    {
      return Request<CancelAdvancedTransferResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/advanced_transfers/{request.AdvancedTransferId}/cancel",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CancelAdvancedTransferResponse> CancelAdvancedTransferAsync(
      CancelAdvancedTransferRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CancelAdvancedTransferResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/advanced_transfers/{request.AdvancedTransferId}/cancel",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}
