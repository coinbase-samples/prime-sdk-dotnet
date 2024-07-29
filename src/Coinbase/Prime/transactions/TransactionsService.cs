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
  using Coinbase.Core.Service;

  public class TransactionsService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public CreateConversionResponse CreateConversion(string portfolioId, string walletId, CreateConversionRequest request)
    {
      return Request<CreateConversionResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets/{walletId}/conversions",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request);
    }

    public CreateTransferResponse CreateTransfer(string portfolioId, string walletId, CreateTransferRequest request)
    {
      return Request<CreateTransferResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets/{walletId}/transfers",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request);
    }

    public CreateWithdrawalResponse CreateWithdrawal(string portfolioId, string walletId, CreateWithdrawalRequest request)
    {
      return Request<CreateWithdrawalResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets/{walletId}/withdrawals",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request);
    }

    public GetTransactionByTransactionIdResponse GetTransactionByTransactionId(string portfolioId, string transactionId)
    {
      return Request<GetTransactionByTransactionIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/transactions/{transactionId}",
        [HttpStatusCode.OK]);
    }

    public ListPortfolioTransactionsResponse ListPortfolioTransactions(string portfolioId, ListPortfolioTransactionsRequest request)
    {
      return Request<ListPortfolioTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/transactions",
        [HttpStatusCode.OK],
        request);
    }

    public ListWalletTransactionsResponse ListWalletTransactions(string portfolioId, string walletId, ListWalletTransactionsRequest request)
    {
      return Request<ListWalletTransactionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/transactions",
        [HttpStatusCode.OK],
        request);
    }
  }
}
