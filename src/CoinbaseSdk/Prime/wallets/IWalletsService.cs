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
  using CoinbaseSdk.Core.Http;

  public interface IWalletsService
  {
    public ListWalletsResponse ListWallets(
      ListWalletsRequest request,
      CallOptions? options = null);

    public Task<ListWalletsResponse> ListWalletsAsync(
      ListWalletsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public CreateWalletResponse CreateWallet(
      CreateWalletRequest request,
      CallOptions? options = null);

    public Task<CreateWalletResponse> CreateWalletAsync(
      CreateWalletRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public GetWalletByIdResponse GetWalletById(
      GetWalletByIdRequest request,
      CallOptions? options = null);

    public Task<GetWalletByIdResponse> GetWalletByIdAsync(
      GetWalletByIdRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public GetWalletDepositInstructionsResponse GetWalletDepositInstructions(
      GetWalletDepositInstructionsRequest request,
      CallOptions? options = null);

    public Task<GetWalletDepositInstructionsResponse> GetWalletDepositInstructionsAsync(
      GetWalletDepositInstructionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);
  }
}
