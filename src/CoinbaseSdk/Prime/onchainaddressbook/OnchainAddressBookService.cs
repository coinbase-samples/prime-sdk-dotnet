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

namespace CoinbaseSdk.Prime.OnchainAddressBook
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class OnchainAddressBookService(ICoinbaseClient client) : CoinbaseService(client), IOnchainAddressBookService
  {
    public CreateOnchainAddressBookEntryResponse CreateOnchainAddressBookEntry(
      CreateOnchainAddressBookEntryRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateOnchainAddressBookEntryResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateOnchainAddressBookEntryResponse> CreateOnchainAddressBookEntryAsync(
      CreateOnchainAddressBookEntryRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateOnchainAddressBookEntryResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public UpdateOnchainAddressBookEntryResponse UpdateOnchainAddressBookEntry(
      UpdateOnchainAddressBookEntryRequest request,
      CallOptions? options = null)
    {
      return this.Request<UpdateOnchainAddressBookEntryResponse>(
        HttpMethod.Put,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<UpdateOnchainAddressBookEntryResponse> UpdateOnchainAddressBookEntryAsync(
        UpdateOnchainAddressBookEntryRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<UpdateOnchainAddressBookEntryResponse>(
      HttpMethod.Put,
      $"/portfolios/{request.PortfolioId}/onchain_address_group",
      [HttpStatusCode.OK],
      request,
      options,
      cancellationToken);
    }

    public DeleteOnchainAddressGroupResponse DeleteOnchainAddressGroup(
        DeleteOnchainAddressGroupRequest request,
        CallOptions? options = null)
    {
      return this.Request<DeleteOnchainAddressGroupResponse>(
        HttpMethod.Delete,
        $"/portfolios/{request.PortfolioId}/onchain_address_group/{request.AddressGroupId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<DeleteOnchainAddressGroupResponse> DeleteOnchainAddressGroupAsync(
        DeleteOnchainAddressGroupRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<DeleteOnchainAddressGroupResponse>(
        HttpMethod.Delete,
        $"/portfolios/{request.PortfolioId}/onchain_address_group/{request.AddressGroupId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }
  }
}
