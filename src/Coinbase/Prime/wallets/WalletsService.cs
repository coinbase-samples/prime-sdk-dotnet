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

using System.Net;
using Coinbase.Core.Client;
using Coinbase.Core.Service;

namespace Coinbase.Prime.Wallets
{
  public class WalletsService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public ListWalletsResponse ListWallets(string portfolioId, ListWalletsRequest request)
    {
      return Request<ListWalletsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets",
        [HttpStatusCode.OK],
        request);
    }

    public CreateWalletResponse CreateWallet(string portfolioId, CreateWalletRequest request)
    {
      return Request<CreateWalletResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/wallets",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request);
    }

    public GetWalletByIdResponse GetWalletById(string portfolioId, string walletId)
    {
      return Request<GetWalletByIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}",
        [HttpStatusCode.OK]);
    }

    public GetWalletDepositInstructionsResponse GetWalletDepositInstructions(
      string portfolioId,
      string walletId)
    {
      return Request<GetWalletDepositInstructionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/wallets/{walletId}/deposit_instructions",
        [HttpStatusCode.OK]);
    }
  }
}