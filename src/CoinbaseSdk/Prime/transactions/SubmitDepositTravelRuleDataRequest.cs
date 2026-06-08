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
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  /// <summary>
  /// Submit Deposit Travel Rule Data.
  /// </summary>
  public class SubmitDepositTravelRuleDataRequest(string portfolioId, string transactionId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string TransactionId { get; set; } = transactionId;

    public TravelRuleParty Originator { get; set; }

    public TravelRuleParty Beneficiary { get; set; }

    public bool? IsSelf { get; set; }

    public bool? OptOutOfOwnershipVerification { get; set; }

    public class SubmitDepositTravelRuleDataRequestBuilder
    {
      private string? _portfolioId;
      private string? _transactionId;
      private TravelRuleParty _originator;
      private TravelRuleParty _beneficiary;
      private bool? _isSelf;
      private bool? _optOutOfOwnershipVerification;

      public SubmitDepositTravelRuleDataRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public SubmitDepositTravelRuleDataRequestBuilder WithTransactionId(string transactionId)
      {
        _transactionId = transactionId;
        return this;
      }

      public SubmitDepositTravelRuleDataRequestBuilder WithOriginator(TravelRuleParty originator)
      {
        _originator = originator;
        return this;
      }

      public SubmitDepositTravelRuleDataRequestBuilder WithBeneficiary(TravelRuleParty beneficiary)
      {
        _beneficiary = beneficiary;
        return this;
      }

      public SubmitDepositTravelRuleDataRequestBuilder WithIsSelf(bool? isSelf)
      {
        _isSelf = isSelf;
        return this;
      }

      public SubmitDepositTravelRuleDataRequestBuilder WithOptOutOfOwnershipVerification(bool? optOutOfOwnershipVerification)
      {
        _optOutOfOwnershipVerification = optOutOfOwnershipVerification;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(_transactionId))
        {
          throw new CoinbaseClientException("TransactionId is required");
        }
      }

      public SubmitDepositTravelRuleDataRequest Build()
      {
        Validate();
        return new SubmitDepositTravelRuleDataRequest(_portfolioId!, _transactionId!)
        {
          Originator = _originator,
          Beneficiary = _beneficiary,
          IsSelf = _isSelf,
          OptOutOfOwnershipVerification = _optOutOfOwnershipVerification,
        };
      }
    }
  }
}
