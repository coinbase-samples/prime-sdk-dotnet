/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class TransactionsService(ICoinbaseClient client) : CoinbaseService(client), ITransactionsService
  {
    public CreateConversionResponse CreateConversion(
      CreateConversionRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateConversionResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/conversions",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateConversionResponse> CreateConversionAsync(
      CreateConversionRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateConversionResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/conversions",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreateTransferResponse CreateTransfer(
      CreateTransferRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateTransferResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/transfers",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateTransferResponse> CreateTransferAsync(
      CreateTransferRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateTransferResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/transfers",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreateWithdrawalResponse CreateWithdrawal(
      CreateWithdrawalRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateWithdrawalResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/withdrawals",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateWithdrawalResponse> CreateWithdrawalAsync(
      CreateWithdrawalRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateWithdrawalResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/withdrawals",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetTransactionByTransactionIdResponse GetTransactionByTransactionId(
      GetTransactionByTransactionIdRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetTransactionByTransactionIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/transactions/{request.TransactionId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetTransactionByTransactionIdResponse> GetTransactionByTransactionIdAsync(
      GetTransactionByTransactionIdRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetTransactionByTransactionIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/transactions/{request.TransactionId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public ListPortfolioTransactionsResponse ListPortfolioTransactions(
      ListPortfolioTransactionsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListPortfolioTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/transactions",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListPortfolioTransactionsResponse> ListPortfolioTransactionsAsync(
      ListPortfolioTransactionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListPortfolioTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/transactions",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListWalletTransactionsResponse ListWalletTransactions(
      ListWalletTransactionsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListWalletTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/transactions",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListWalletTransactionsResponse> ListWalletTransactionsAsync(
      ListWalletTransactionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListWalletTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/transactions",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}
