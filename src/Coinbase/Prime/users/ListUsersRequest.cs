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

namespace Coinbase.Prime.Users
{
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;
  public class ListUsersRequest(string entityId) : BaseListRequest(null, entityId)
  {
    public class ListUsersRequestBuilder
    {
      private string? _entityId;
      private string? _cursor;
      private string? _sortDirection;

      public ListUsersRequestBuilder withEntityId(string entityId)
      {
        this._entityId = entityId;
        return this;
      }

      public ListUsersRequestBuilder withPagination(Pagination pagination)
      {
        this._cursor = pagination.NextCursor;
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the <see cref="_entityId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      /// <summary>
      /// Build the <see cref="ListUsersRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="ListUsersRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public ListUsersRequest Build()
      {
        this.Validate();
        return new ListUsersRequest(this._entityId!)
        {
          Cursor = this._cursor,
          SortDirection = this._sortDirection
        };
      }
    }
  }
}
