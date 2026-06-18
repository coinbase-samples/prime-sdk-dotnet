/*
 * Copyright 2026-present Coinbase Global, Inc.
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
  /// Get Transaction Travel Rule Data.
  /// </summary>
  public class GetTransactionTravelRuleDataResponse
  {
    public bool? Fulfilled { get; set; }

    public bool? IsSelf { get; set; }

    public TravelRuleParty Originator { get; set; }

    public TravelRuleParty Beneficiary { get; set; }

    public string? Amount { get; set; }

    public string? AmountCurrency { get; set; }

    public string? FiatAmount { get; set; }

    public string? FiatAmountCurrency { get; set; }

    public string? BlockchainNetwork { get; set; }

    public GetTransactionTravelRuleDataResponse() { }
  }
}
