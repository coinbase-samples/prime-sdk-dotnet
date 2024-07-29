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
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Service;

  public class UsersService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public ListPortfolioUsersResponse ListPortfolioUsers(string portfolioId)
    {
      return Request<ListPortfolioUsersResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/users",
        [HttpStatusCode.OK]);
    }

    public ListUsersResponse ListUsers(string entityId, ListUsersRequest request)
    {
      return Request<ListUsersResponse>(
        HttpMethod.Get,
        $"/entities/{entityId}/users",
        [HttpStatusCode.OK],
        request);
    }
  }
}
