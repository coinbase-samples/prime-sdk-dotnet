/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Staking
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class StakingService(ICoinbaseClient client) : CoinbaseService(client), IStakingService
  {
    public CreateStakeResponse CreateStake(
      CreateStakeRequest request,
      CallOptions? options = null)
    {
      return Request<CreateStakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/initiate",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateStakeResponse> CreateStakeAsync(
      CreateStakeRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CreateStakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/initiate",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreateUnstakeResponse CreateUnstake(
      CreateUnstakeRequest request,
      CallOptions? options = null)
    {
      return Request<CreateUnstakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/unstake",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateUnstakeResponse> CreateUnstakeAsync(
      CreateUnstakeRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CreateUnstakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/unstake",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreatePortfolioStakeResponse CreatePortfolioStake(
      CreatePortfolioStakeRequest request,
      CallOptions? options = null)
    {
      return Request<CreatePortfolioStakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/staking/initiate",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreatePortfolioStakeResponse> CreatePortfolioStakeAsync(
      CreatePortfolioStakeRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CreatePortfolioStakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/staking/initiate",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreatePortfolioUnstakeResponse CreatePortfolioUnstake(
      CreatePortfolioUnstakeRequest request,
      CallOptions? options = null)
    {
      return Request<CreatePortfolioUnstakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/staking/unstake",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreatePortfolioUnstakeResponse> CreatePortfolioUnstakeAsync(
      CreatePortfolioUnstakeRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CreatePortfolioUnstakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/staking/unstake",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ClaimStakingRewardsResponse ClaimStakingRewards(
      ClaimStakingRewardsRequest request,
      CallOptions? options = null)
    {
      return Request<ClaimStakingRewardsResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/claim_rewards",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ClaimStakingRewardsResponse> ClaimStakingRewardsAsync(
      ClaimStakingRewardsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ClaimStakingRewardsResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/claim_rewards",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListTransactionValidatorsResponse ListTransactionValidators(
      ListTransactionValidatorsRequest request,
      CallOptions? options = null)
    {
      return Request<ListTransactionValidatorsResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/staking/transaction-validators/query",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListTransactionValidatorsResponse> ListTransactionValidatorsAsync(
      ListTransactionValidatorsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListTransactionValidatorsResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/staking/transaction-validators/query",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetStakingStatusResponse GetStakingStatus(
      GetStakingStatusRequest request,
      CallOptions? options = null)
    {
      return Request<GetStakingStatusResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/status",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetStakingStatusResponse> GetStakingStatusAsync(
      GetStakingStatusRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetStakingStatusResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/status",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public PreviewUnstakeResponse PreviewUnstake(
      PreviewUnstakeRequest request,
      CallOptions? options = null)
    {
      return Request<PreviewUnstakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/unstake/preview",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<PreviewUnstakeResponse> PreviewUnstakeAsync(
      PreviewUnstakeRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<PreviewUnstakeResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/unstake/preview",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetUnstakingStatusResponse GetUnstakingStatus(
      GetUnstakingStatusRequest request,
      CallOptions? options = null)
    {
      return Request<GetUnstakingStatusResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/unstake/status",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetUnstakingStatusResponse> GetUnstakingStatusAsync(
      GetUnstakingStatusRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetUnstakingStatusResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/wallets/{request.WalletId}/staking/unstake/status",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }
  }
}
