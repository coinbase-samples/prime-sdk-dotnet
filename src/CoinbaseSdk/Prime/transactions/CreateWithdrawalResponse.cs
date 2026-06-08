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

namespace CoinbaseSdk.Prime.Transactions
{
  using CoinbaseSdk.Prime.Model;

  /// <summary>
  /// Create Withdrawal.
  /// </summary>
  public class CreateWithdrawalResponse
  {
    public string? ActivityId { get; set; }

    public string? ApprovalUrl { get; set; }

    public string? Symbol { get; set; }

    public string? Amount { get; set; }

    public string? Fee { get; set; }

    public string? DestinationType { get; set; }

    public string? SourceType { get; set; }

    public BlockchainAddress BlockchainDestination { get; set; }

    public CounterpartyDestination CounterpartyDestination { get; set; }

    public BlockchainAddress BlockchainSource { get; set; }

    public string? TransactionId { get; set; }

    public CreateWithdrawalResponse() { }
  }
}
