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

namespace CoinbaseSdk.Prime.OnchainAddressBook
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class OnchainAddressBookService(ICoinbaseClient client)
    : CoinbaseService(client),
      IOnchainAddressBookService
  {
    public ActivityCreationResponse CreateOnchainAddressBookEntry(
      CreateOnchainAddressBookEntryRequest request,
      CallOptions? options = null)
    {
      return Request<ActivityCreationResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ActivityCreationResponse> CreateOnchainAddressBookEntryAsync(
      CreateOnchainAddressBookEntryRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ActivityCreationResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ActivityCreationResponse UpdateOnchainAddressBookEntry(
      UpdateOnchainAddressBookEntryRequest request,
      CallOptions? options = null)
    {
      return Request<ActivityCreationResponse>(
        HttpMethod.Put,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ActivityCreationResponse> UpdateOnchainAddressBookEntryAsync(
      UpdateOnchainAddressBookEntryRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ActivityCreationResponse>(
        HttpMethod.Put,
        $"/portfolios/{request.PortfolioId}/onchain_address_group",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ActivityCreationResponse DeleteOnchainAddressGroup(
      DeleteOnchainAddressGroupRequest request,
      CallOptions? options = null)
    {
      return Request<ActivityCreationResponse>(
        HttpMethod.Delete,
        $"/portfolios/{request.PortfolioId}/onchain_address_group/{request.AddressGroupId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<ActivityCreationResponse> DeleteOnchainAddressGroupAsync(
      DeleteOnchainAddressGroupRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ActivityCreationResponse>(
        HttpMethod.Delete,
        $"/portfolios/{request.PortfolioId}/onchain_address_group/{request.AddressGroupId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

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
  }
}
