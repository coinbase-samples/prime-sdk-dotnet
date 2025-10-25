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
  using CoinbaseSdk.Core.Http;

  public interface IOnchainAddressBookService
  {
    public ActivityCreationResponse CreateOnchainAddressBookEntry(
        CreateOnchainAddressBookEntryRequest request,
        CallOptions? options = null);

    public Task<ActivityCreationResponse> CreateOnchainAddressBookEntryAsync(
        CreateOnchainAddressBookEntryRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    public ActivityCreationResponse UpdateOnchainAddressBookEntry(
        UpdateOnchainAddressBookEntryRequest request,
        CallOptions? options = null);

    public Task<ActivityCreationResponse> UpdateOnchainAddressBookEntryAsync(
        UpdateOnchainAddressBookEntryRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    public ActivityCreationResponse DeleteOnchainAddressGroup(
        DeleteOnchainAddressGroupRequest request,
        CallOptions? options = null);

    public Task<ActivityCreationResponse> DeleteOnchainAddressGroupAsync(
        DeleteOnchainAddressGroupRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all onchain address groups for a given portfolio ID.
    /// </summary>
    /// <param name="request">The request object containing the portfolio ID.</param>
    /// <param name="options">Optional call options.</param>
    /// <returns>A response containing the list of onchain address groups.</returns>
    ListOnchainAddressGroupsResponse ListOnchainAddressGroups(
      ListOnchainAddressGroupsRequest request,
      CallOptions? options = null);

    /// <summary>
    /// Lists all onchain address groups for a given portfolio ID asynchronously.
    /// </summary>
    /// <param name="request">The request object containing the portfolio ID.</param>
    /// <param name="options">Optional call options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that returns a response containing the list of onchain address groups.</returns>
    Task<ListOnchainAddressGroupsResponse> ListOnchainAddressGroupsAsync(
      ListOnchainAddressGroupsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);
  }
}
