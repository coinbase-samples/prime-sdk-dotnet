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

namespace CoinbaseSdk.Prime.OnchainAddressBook
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class OnchainAddressBookService(ICoinbaseClient client) : CoinbaseService(client), IOnchainAddressBookService
  {
    /// <summary>
    /// List Onchain Address Groups.
    /// </summary>
    public ListOnchainAddressGroupsResponse ListOnchainAddressGroups(
      ListOnchainAddressGroupsRequest request,
      CallOptions? options = null)
    {
      return Request<ListOnchainAddressGroupsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/onchain_address_groups",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<ListOnchainAddressGroupsResponse> ListOnchainAddressGroupsAsync(
      ListOnchainAddressGroupsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListOnchainAddressGroupsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/onchain_address_groups",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Create Onchain Address Book Entry.
    /// </summary>
    public CreateOnchainAddressBookEntryResponse CreateOnchainAddressBookEntry(
      CreateOnchainAddressBookEntryRequest request,
      CallOptions? options = null)
    {
      return Request<CreateOnchainAddressBookEntryResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateOnchainAddressBookEntryResponse> CreateOnchainAddressBookEntryAsync(
      CreateOnchainAddressBookEntryRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CreateOnchainAddressBookEntryResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Update Onchain Address Book Entry.
    /// </summary>
    public UpdateOnchainAddressBookEntryResponse UpdateOnchainAddressBookEntry(
      UpdateOnchainAddressBookEntryRequest request,
      CallOptions? options = null)
    {
      return Request<UpdateOnchainAddressBookEntryResponse>(
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
      return RequestAsync<UpdateOnchainAddressBookEntryResponse>(
        HttpMethod.Put,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Delete Onchain Address Group.
    /// </summary>
    public DeleteOnchainAddressGroupResponse DeleteOnchainAddressGroup(
      DeleteOnchainAddressGroupRequest request,
      CallOptions? options = null)
    {
      return Request<DeleteOnchainAddressGroupResponse>(
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
      return RequestAsync<DeleteOnchainAddressGroupResponse>(
        HttpMethod.Delete,
        $"/portfolios/{request.PortfolioId}/onchain_address_group/{request.AddressGroupId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }
  }
}
