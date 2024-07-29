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

using System.Net;
using Coinbase.Core.Client;
using Coinbase.Core.Service;

namespace Coinbase.Prime.Allocations
{
  public class AllocationsService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public CreateAllocationResponse CreateAllocation(CreateAllocationRequest request)
    {
      return Request<CreateAllocationResponse>(
        HttpMethod.Post,
        $"/allocations",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request);
    }

    public CreateNetAllocationResponse CreateNetAllocation(CreateNetAllocationRequest request)
    {
      return Request<CreateNetAllocationResponse>(
        HttpMethod.Post,
        $"/allocations/net",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request);
    }

    public GetAllocationResponse GetAllocation(string portfolioId, string allocationId)
    {
      return Request<GetAllocationResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/allocations/{allocationId}",
        [HttpStatusCode.OK]);
    }

    public GetAllocationsByClientNettingIdResponse GetAllocationsByClientNettingId(string portfolioId, string nettingId)
    {
      return Request<GetAllocationsByClientNettingIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/allocations/net/{nettingId}",
        [HttpStatusCode.OK]);
    }

    public GetPortfolioAllocationsResponse GetPortfolioAllocations(string portfolioId, GetPortfolioAllocationsRequest request)
    {
      return Request<GetPortfolioAllocationsResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/allocations",
        [HttpStatusCode.OK],
        request);
    }
  }
}