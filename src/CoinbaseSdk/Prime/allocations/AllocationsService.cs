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
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class AllocationsService(ICoinbaseClient client) : CoinbaseService(client), IAllocationsService
  {
    /// <summary>
    /// List Portfolio Allocations.
    /// </summary>
    public ListPortfolioAllocationsResponse ListPortfolioAllocations(
      ListPortfolioAllocationsRequest request,
      CallOptions? options = null)
    {
      return Request<ListPortfolioAllocationsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/allocations",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListPortfolioAllocationsResponse> ListPortfolioAllocationsAsync(
      ListPortfolioAllocationsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListPortfolioAllocationsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/allocations",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Allocation by ID.
    /// </summary>
    public GetAllocationResponse GetAllocation(
      GetAllocationRequest request,
      CallOptions? options = null)
    {
      return Request<GetAllocationResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/allocations/{request.AllocationId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetAllocationResponse> GetAllocationAsync(
      GetAllocationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetAllocationResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/allocations/{request.AllocationId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Net Allocations by Netting ID.
    /// </summary>
    public ListAllocationsByClientNettingIdResponse ListAllocationsByClientNettingId(
      ListAllocationsByClientNettingIdRequest request,
      CallOptions? options = null)
    {
      return Request<ListAllocationsByClientNettingIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/allocations/net/{request.NettingId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<ListAllocationsByClientNettingIdResponse> ListAllocationsByClientNettingIdAsync(
      ListAllocationsByClientNettingIdRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListAllocationsByClientNettingIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/allocations/net/{request.NettingId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Create Portfolio Allocations.
    /// </summary>
    public CreateAllocationResponse CreateAllocation(
      CreateAllocationRequest request,
      CallOptions? options = null)
    {
      return Request<CreateAllocationResponse>(
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
      return RequestAsync<CreateAllocationResponse>(
        HttpMethod.Post,
        $"/allocations",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Create Portfolio Net Allocations.
    /// </summary>
    public CreateNetAllocationResponse CreateNetAllocation(
      CreateNetAllocationRequest request,
      CallOptions? options = null)
    {
      return Request<CreateNetAllocationResponse>(
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
      return RequestAsync<CreateNetAllocationResponse>(
        HttpMethod.Post,
        $"/allocations/net",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}
