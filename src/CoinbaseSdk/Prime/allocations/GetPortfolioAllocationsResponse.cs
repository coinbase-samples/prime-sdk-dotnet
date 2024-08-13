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
  using CoinbaseSdk.Prime.Common;
  public class GetPortfolioAllocationsResponse
  {
    public Allocation[] Allocations { get; set; } = [];
    public Pagination? Pagination { get; set; }

    public GetPortfolioAllocationsResponse() { }

    public class GetPortfolioAllocationsResponseBuilder
    {
      private Allocation[] _allocations = Array.Empty<Allocation>();
      private Pagination? _pagination;

      public GetPortfolioAllocationsResponseBuilder WithAllocations(Allocation[] allocations)
      {
        this._allocations = allocations;
        return this;
      }

      public GetPortfolioAllocationsResponseBuilder WithPagination(Pagination? pagination)
      {
        this._pagination = pagination;
        return this;
      }

      public GetPortfolioAllocationsResponse Build()
      {
        return new GetPortfolioAllocationsResponse
        {
          Allocations = this._allocations,
          Pagination = this._pagination
        };
      }
    }
  }
}