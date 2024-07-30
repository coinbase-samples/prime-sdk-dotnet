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

namespace Coinbase.Prime.Transactions
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Http;
  using Coinbase.Core.Service;

  public class TransactionsService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public CreateConversionResponse CreateConversion(
      string portfolioId,
      string walletId,
      CreateConversionRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateConversionResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets/{walletId}/conversions",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateConversionResponse> CreateConversionAsync(
      string portfolioId,
      string walletId,
      CreateConversionRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateConversionResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets/{walletId}/conversions",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreateTransferResponse CreateTransfer(
      string portfolioId,
      string walletId,
      CreateTransferRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateTransferResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets/{walletId}/transfers",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateTransferResponse> CreateTransferAsync(
      string portfolioId,
      string walletId,
      CreateTransferRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateTransferResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets/{walletId}/transfers",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreateWithdrawalResponse CreateWithdrawal(
      string portfolioId,
      string walletId,
      CreateWithdrawalRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateWithdrawalResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets/{walletId}/withdrawals",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateWithdrawalResponse> CreateWithdrawalAsync(
      string portfolioId,
      string walletId,
      CreateWithdrawalRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateWithdrawalResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets/{walletId}/withdrawals",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetTransactionByTransactionIdResponse GetTransactionByTransactionId(
      string portfolioId,
      string transactionId,
      CallOptions? options = null)
    {
      return this.Request<GetTransactionByTransactionIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/transactions/{transactionId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetTransactionByTransactionIdResponse> GetTransactionByTransactionIdAsync(
      string portfolioId,
      string transactionId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetTransactionByTransactionIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/transactions/{transactionId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public ListPortfolioTransactionsResponse ListPortfolioTransactions(
      string portfolioId,
      ListPortfolioTransactionsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListPortfolioTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/transactions",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListPortfolioTransactionsResponse> ListPortfolioTransactionsAsync(
      string portfolioId,
      ListPortfolioTransactionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListPortfolioTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/transactions",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListWalletTransactionsResponse ListWalletTransactions(
      string portfolioId,
      string walletId,
      ListWalletTransactionsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListWalletTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/transactions",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListWalletTransactionsResponse> ListWalletTransactionsAsync(
      string portfolioId,
      string walletId,
      ListWalletTransactionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListWalletTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/transactions",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}
