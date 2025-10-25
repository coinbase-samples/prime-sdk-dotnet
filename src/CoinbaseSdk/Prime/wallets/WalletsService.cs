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

namespace CoinbaseSdk.Prime.Wallets
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class WalletsService(ICoinbaseClient client) : CoinbaseService(client), IWalletsService
  {
    public ListWalletsResponse ListWallets(
      ListWalletsRequest request,
      CallOptions? options = null)
    {
      return Request<ListWalletsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListWalletsResponse> ListWalletsAsync(
      ListWalletsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListWalletsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreateWalletResponse CreateWallet(
      CreateWalletRequest request,
      CallOptions? options = null)
    {
      return Request<CreateWalletResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateWalletResponse> CreateWalletAsync(
      CreateWalletRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CreateWalletResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetWalletResponse GetWallet(
      GetWalletRequest request,
      CallOptions? options = null)
    {
      return Request<GetWalletResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetWalletResponse> GetWalletAsync(
      GetWalletRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetWalletResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetWalletDepositInstructionsResponse GetWalletDepositInstructions(
      GetWalletDepositInstructionsRequest request,
      CallOptions? options = null)
    {
      return Request<GetWalletDepositInstructionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/deposit_instructions",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetWalletDepositInstructionsResponse> GetWalletDepositInstructionsAsync(
      GetWalletDepositInstructionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetWalletDepositInstructionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/deposit_instructions",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public CreateWalletDepositAddressResponse CreateWalletDepositAddress(
      CreateWalletDepositAddressRequest request,
      CallOptions? options = null)
    {
      return Request<CreateWalletDepositAddressResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/addresses",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateWalletDepositAddressResponse> CreateWalletDepositAddressAsync(
      CreateWalletDepositAddressRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CreateWalletDepositAddressResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/addresses",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListWalletAddressesResponse ListWalletAddresses(
      ListWalletAddressesRequest request,
      CallOptions? options = null)
    {
      return Request<ListWalletAddressesResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/addresses",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListWalletAddressesResponse> ListWalletAddressesAsync(
      ListWalletAddressesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListWalletAddressesResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/addresses",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}
