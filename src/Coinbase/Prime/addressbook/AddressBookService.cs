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

namespace Coinbase.Prime.AddressBook
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Service;
  public class AddressBookService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public CreateAddressBookEntryResponse CreateAddressBookEntry(string portfolioId, CreateAddressBookEntryRequest request)
    {
      return Request<CreateAddressBookEntryResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/address_book",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request);
    }

    public GetPortfolioAddressBookResponse GetPortfolioAddressBook(string portfolioId, GetPortfolioAddressBookRequest request)
    {
      return Request<GetPortfolioAddressBookResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/address_book",
        [HttpStatusCode.OK],
        request);
    }
  }
}