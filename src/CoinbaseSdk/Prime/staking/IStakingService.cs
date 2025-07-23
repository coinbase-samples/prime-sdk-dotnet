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
  using CoinbaseSdk.Core.Http;

  public interface IStakingService
  {
    public CreateStakeResponse CreateStake(
        CreateStakeRequest request,
        CallOptions? options = null);

    public Task<CreateStakeResponse> CreateStakeAsync(
        CreateStakeRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    public CreateUnstakeResponse CreateUnstake(
        CreateUnstakeRequest request,
        CallOptions? options = null);

    public Task<CreateUnstakeResponse> CreateUnstakeAsync(
        CreateUnstakeRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    public CreatePortfolioStakeResponse CreatePortfolioStake(
        CreatePortfolioStakeRequest request,
        CallOptions? options = null);

    public Task<CreatePortfolioStakeResponse> CreatePortfolioStakeAsync(
        CreatePortfolioStakeRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    public CreatePortfolioUnstakeResponse CreatePortfolioUnstake(
        CreatePortfolioUnstakeRequest request,
        CallOptions? options = null);

    public Task<CreatePortfolioUnstakeResponse> CreatePortfolioUnstakeAsync(
        CreatePortfolioUnstakeRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    public ClaimStakingRewardsResponse ClaimStakingRewards(
        ClaimStakingRewardsRequest request,
        CallOptions? options = null);

    public Task<ClaimStakingRewardsResponse> ClaimStakingRewardsAsync(
        ClaimStakingRewardsRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);
  }
}