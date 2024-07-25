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

using Newtonsoft.Json;

namespace Coinbase.Prime.Common
{
  public class Pagination
  {
    [JsonProperty("next_cursor")]
    public string NextCursor { get; set; }

    [JsonProperty("sort_direction")]
    public string SortDirection { get; set; }

    [JsonProperty("has_next")]
    public bool HasNext { get; set; }

    public Pagination() { }

    public Pagination(Builder builder)
    {
      NextCursor = builder.NextCursor;
      SortDirection = builder.SortDirection;
      HasNext = builder.HasNext;
    }

    public class Builder
    {
      public string NextCursor { get; private set; }
      public string SortDirection { get; private set; }
      public bool HasNext { get; private set; }

      public Builder() { }

      public Builder WithNextCursor(string nextCursor)
      {
        NextCursor = nextCursor;
        return this;
      }

      public Builder WithSortDirection(string sortDirection)
      {
        SortDirection = sortDirection;
        return this;
      }

      public Builder WithHasNext(bool hasNext)
      {
        HasNext = hasNext;
        return this;
      }

      public Pagination Build()
      {
        return new Pagination(this);
      }
    }
  }
}