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

namespace Coinbase.Prime.Allocations
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Http;
  using Coinbase.Core.Service;
  public class AllocationsService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public CreateAllocationResponse CreateAllocation(
      CreateAllocationRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateAllocationResponse>(
        HttpMethod.Post,
        $"/allocations",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateAllocationResponse> CreateAllocationAsync(
      CreateAllocationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateAllocationResponse>(
        HttpMethod.Post,
        $"/allocations",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public CreateNetAllocationResponse CreateNetAllocation(
      CreateNetAllocationRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateNetAllocationResponse>(
        HttpMethod.Post,
        $"/allocations/net",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateNetAllocationResponse> CreateNetAllocationAsync(
      CreateNetAllocationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateNetAllocationResponse>(
        HttpMethod.Post,
        $"/allocations/net",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetAllocationResponse GetAllocation(
      string portfolioId,
      string allocationId,
      CallOptions? options = null)
    {
      return this.Request<GetAllocationResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/allocations/{allocationId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetAllocationResponse> GetAllocationAsync(
      string portfolioId,
      string allocationId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetAllocationResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/allocations/{allocationId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetAllocationsByClientNettingIdResponse GetAllocationsByClientNettingId(
      string portfolioId,
      string nettingId,
      CallOptions? options = null)
    {
      return this.Request<GetAllocationsByClientNettingIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/allocations/net/{nettingId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetAllocationsByClientNettingIdResponse> GetAllocationsByClientNettingIdAsync(
      string portfolioId,
      string nettingId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetAllocationsByClientNettingIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/allocations/net/{nettingId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetPortfolioAllocationsResponse GetPortfolioAllocations(
      string portfolioId,
      GetPortfolioAllocationsRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetPortfolioAllocationsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/allocations",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetPortfolioAllocationsResponse> GetPortfolioAllocationsAsync(
      string portfolioId,
      GetPortfolioAllocationsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetPortfolioAllocationsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/allocations",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}