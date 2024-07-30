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

namespace Coinbase.Prime.Balances
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Http;
  using Coinbase.Core.Service;
  public class BalancesService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public GetWalletBalanceResponse GetWalletBalance(
      string portfolioId,
      string walletId,
      CallOptions? options = null)
    {
      return this.Request<GetWalletBalanceResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/balance",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetWalletBalanceResponse> GetWalletBalanceAsync(
      string portfolioId,
      string walletId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetWalletBalanceResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/balance",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public ListPortfolioBalancesResponse ListPortfolioBalances(
      string portfolioId,
      ListPortfolioBalancesRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListPortfolioBalancesResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/balances",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListPortfolioBalancesResponse> ListPortfolioBalancesAsync(
      string portfolioId,
      ListPortfolioBalancesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListPortfolioBalancesResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/balances",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListWeb3WalletBalancesResponse ListWeb3WalletBalances(
      string portfolioId,
      string walletId,
      ListWeb3WalletBalancesRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListWeb3WalletBalancesResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/web3_balances",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListWeb3WalletBalancesResponse> ListWeb3WalletBalancesAsync(
      string portfolioId,
      string walletId,
      ListWeb3WalletBalancesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListWeb3WalletBalancesResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/web3_balances",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}