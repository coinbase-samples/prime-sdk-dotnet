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

namespace CoinbaseSdk.Prime.Financing
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// Update Funding Settings.
  /// </summary>
  public class UpdateFundingSettingsRequest(string entityId)
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    public string? DesignatedFundingPortfolioId { get; set; }

    public bool? AutomaticConversionEnabled { get; set; }

    public bool? AutomaticLoanEnabled { get; set; }

    public bool? AutomaticExcessReturnEnabled { get; set; }

    public string? ExcessFundsTargetAmount { get; set; }

    public class UpdateFundingSettingsRequestBuilder
    {
      private string? _entityId;
      private string? _designatedFundingPortfolioId;
      private bool? _automaticConversionEnabled;
      private bool? _automaticLoanEnabled;
      private bool? _automaticExcessReturnEnabled;
      private string? _excessFundsTargetAmount;

      public UpdateFundingSettingsRequestBuilder WithEntityId(string entityId)
      {
        _entityId = entityId;
        return this;
      }

      public UpdateFundingSettingsRequestBuilder WithDesignatedFundingPortfolioId(string? designatedFundingPortfolioId)
      {
        _designatedFundingPortfolioId = designatedFundingPortfolioId;
        return this;
      }

      public UpdateFundingSettingsRequestBuilder WithAutomaticConversionEnabled(bool? automaticConversionEnabled)
      {
        _automaticConversionEnabled = automaticConversionEnabled;
        return this;
      }

      public UpdateFundingSettingsRequestBuilder WithAutomaticLoanEnabled(bool? automaticLoanEnabled)
      {
        _automaticLoanEnabled = automaticLoanEnabled;
        return this;
      }

      public UpdateFundingSettingsRequestBuilder WithAutomaticExcessReturnEnabled(bool? automaticExcessReturnEnabled)
      {
        _automaticExcessReturnEnabled = automaticExcessReturnEnabled;
        return this;
      }

      public UpdateFundingSettingsRequestBuilder WithExcessFundsTargetAmount(string? excessFundsTargetAmount)
      {
        _excessFundsTargetAmount = excessFundsTargetAmount;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      public UpdateFundingSettingsRequest Build()
      {
        Validate();
        return new UpdateFundingSettingsRequest(_entityId!)
        {
          DesignatedFundingPortfolioId = _designatedFundingPortfolioId,
          AutomaticConversionEnabled = _automaticConversionEnabled,
          AutomaticLoanEnabled = _automaticLoanEnabled,
          AutomaticExcessReturnEnabled = _automaticExcessReturnEnabled,
          ExcessFundsTargetAmount = _excessFundsTargetAmount,
        };
      }
    }
  }
}
