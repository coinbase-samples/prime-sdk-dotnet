/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace Coinbase.Prime.Users
{
  using Coinbase.Prime.Common;
  public class ListPortfolioUsersResponse
  {
    public User[] Users { get; set; } = [];
    public Pagination? Pagination { get; set; }

    public ListPortfolioUsersResponse() { }

    public class ListPortfolioUsersResponseBuilder
    {
      private User[] _users = [];
      private Pagination? _pagination;

      public ListPortfolioUsersResponseBuilder() { }

      public ListPortfolioUsersResponseBuilder WithUsers(User[] users)
      {
        this._users = users;
        return this;
      }

      public ListPortfolioUsersResponseBuilder WithPagination(Pagination pagination)
      {
        this._pagination = pagination;
        return this;
      }

      public ListPortfolioUsersResponse Build()
      {
        return new ListPortfolioUsersResponse
        {
          Users = this._users,
          Pagination = this._pagination
        };
      }
    }
  }
}
