/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Positions
{
  using CoinbaseSdk.Prime.Model;

  public class ListAggregatePositionsResponse
  {
    public Position[] Positions { get; set; } = [];
    public PaginatedResponse? Pagination { get; set; }

    public ListAggregatePositionsResponse()
    {
    }

    public class ListAggregatePositionsResponseBuilder
    {
      private Position[] _positions = [];
      private PaginatedResponse? _pagination;

      public ListAggregatePositionsResponseBuilder WithPositions(Position[] positions)
      {
        _positions = positions;
        return this;
      }

      public ListAggregatePositionsResponseBuilder WithPagination(PaginatedResponse pagination)
      {
        _pagination = pagination;
        return this;
      }

      public ListAggregatePositionsResponse Build()
      {
        return new ListAggregatePositionsResponse()
        {
          Positions = _positions,
          Pagination = _pagination
        };
      }
    }
  }
}