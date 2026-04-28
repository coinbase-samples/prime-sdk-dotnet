/*
 * Copyright 2026-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.IntegrationTests.Infrastructure
{
  public sealed class DiscoveredIds
  {
    public string? PortfolioId { get; set; }

    public string? EntityId { get; set; }

    public string? WalletId { get; set; }

    public string? OnchainWalletId { get; set; }

    public string? OrderId { get; set; }

    public string? ActivityId { get; set; }

    public string? TransactionId { get; set; }

    public string? AllocationId { get; set; }

    public string? AddressBookEntryId { get; set; }

    public string? PaymentMethodId { get; set; }

    public string? OnchainAddressGroupId { get; set; }

    public string? ClientNettingId { get; set; }

    public string AssetSymbol { get; set; } = "BTC";

    public string ProductId { get; set; } = "BTC-USD";
  }
}
