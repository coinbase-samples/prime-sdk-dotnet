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

namespace CoinbaseSdk.Prime.Common
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model.Enums;

  public class Pagination
  {
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }

    [JsonPropertyName("sort_direction")]
    public SortDirection? SortDirection { get; set; }

    [JsonPropertyName("has_next")]
    public bool HasNext { get; set; }

    public Pagination() { }

    public class Builder
    {
      private string? nextCursor;
      private SortDirection? sortDirection;
      private bool hasNext;

      public Builder() { }

      public Builder NextCursor(string? nextCursor)
      {
        this.nextCursor = nextCursor;
        return this;
      }

      public Builder SortDirection(SortDirection? sortDirection)
      {
        this.sortDirection = sortDirection;
        return this;
      }

      public Builder HasNext(bool hasNext)
      {
        this.hasNext = hasNext;
        return this;
      }

      public Pagination Build()
      {
        return new Pagination
        {
          NextCursor = nextCursor,
          SortDirection = sortDirection,
          HasNext = hasNext
        };
      }
    }
  }
}
