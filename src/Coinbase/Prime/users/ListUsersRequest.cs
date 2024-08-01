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

      public ListUsersRequestBuilder withEntityId(string entityId)
      {
        this._entityId = entityId;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      public ListUsersRequest Build()
      {
        this.Validate();
        return new ListUsersRequest(this._entityId!);
      }
    }
  }
}