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

namespace CoinbaseSdk.Prime.Allocations
{
  using CoinbaseSdk.Core.Http;
  public interface IAllocationsService
  {
    public CreateAllocationResponse CreateAllocation(
      CreateAllocationRequest request,
      CallOptions? options = null);

    public Task<CreateAllocationResponse> CreateAllocationAsync(
      CreateAllocationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public CreateNetAllocationResponse CreateNetAllocation(
      CreateNetAllocationRequest request,
      CallOptions? options = null);

    public Task<CreateNetAllocationResponse> CreateNetAllocationAsync(
      CreateNetAllocationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public GetAllocationResponse GetAllocation(
      GetAllocationRequest request,
      CallOptions? options = null);

    public Task<GetAllocationResponse> GetAllocationAsync(
      GetAllocationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public GetAllocationsByClientNettingIdResponse GetAllocationsByClientNettingId(
      GetAllocationsByClientNettingIdRequest request,
      CallOptions? options = null);

    public Task<GetAllocationsByClientNettingIdResponse> GetAllocationsByClientNettingIdAsync(
      GetAllocationsByClientNettingIdRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    public GetPortfolioAllocationsResponse GetPortfolioAllocations(
      GetPortfolioAllocationsRequest request,
      CallOptions? options = null);

    public Task<GetPortfolioAllocationsResponse> GetPortfolioAllocationsAsync(
      GetPortfolioAllocationsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);
  }
}
