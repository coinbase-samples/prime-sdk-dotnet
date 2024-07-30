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

namespace Coinbase.Prime.Wallets
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Http;
  using Coinbase.Core.Service;
  public class WalletsService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public ListWalletsResponse ListWallets(
      string portfolioId,
      ListWalletsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListWalletsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListWalletsResponse> ListWalletsAsync(
      string portfolioId,
      ListWalletsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListWalletsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreateWalletResponse CreateWallet(
      string portfolioId,
      CreateWalletRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateWalletResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateWalletResponse> CreateWalletAsync(
      string portfolioId,
      CreateWalletRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateWalletResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetWalletByIdResponse GetWalletById(
      string portfolioId,
      string walletId,
      CallOptions? options = null)
    {
      return this.Request<GetWalletByIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetWalletByIdResponse> GetWalletByIdAsync(
      string portfolioId,
      string walletId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetWalletByIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetWalletDepositInstructionsResponse GetWalletDepositInstructions(
      string portfolioId,
      string walletId,
      CallOptions? options = null)
    {
      return this.Request<GetWalletDepositInstructionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/deposit_instructions",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetWalletDepositInstructionsResponse> GetWalletDepositInstructionsAsync(
      string portfolioId,
      string walletId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetWalletDepositInstructionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/deposit_instructions",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }
  }
}