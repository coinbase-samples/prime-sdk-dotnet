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

namespace CoinbaseSdk.Prime.Common
{
  using System.Text.Json.Serialization;
  public class Pagination
  {
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }

    [JsonPropertyName("sort_direction")]
    public string? SortDirection { get; set; }

    [JsonPropertyName("has_next")]
    public bool? HasNext { get; set; }

    public Pagination() { }

    public class PaginationBuilder
    {
      private string? _nextCursor;
      private string? _sortDirection;
      private bool? _hasNext;

      public PaginationBuilder WithNextCursor(string? nextCursor)
      {
        this._nextCursor = nextCursor;
        return this;
      }

      public PaginationBuilder WithSortDirection(string? sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public PaginationBuilder WithHasNext(bool? hasNext)
      {
        this._hasNext = hasNext;
        return this;
      }

      public Pagination Build()
      {
        return new Pagination
        {
          NextCursor = this._nextCursor,
          SortDirection = this._sortDirection,
          HasNext = this._hasNext
        };
      }
    }
  }
}