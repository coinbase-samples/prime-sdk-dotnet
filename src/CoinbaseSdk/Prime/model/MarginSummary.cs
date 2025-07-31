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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;
  public class MarginSummary
  {
    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }

    [JsonPropertyName("margin_equity")]
    public string? MarginEquity { get; set; }

    [JsonPropertyName("margin_requirement")]
    public string? MarginRequirement { get; set; }

    [JsonPropertyName("excess_deficit")]
    public string? ExcessDeficit { get; set; }

    [JsonPropertyName("pm_credit_consumed")]
    public string? PmCreditConsumed { get; set; }

    [JsonPropertyName("tf_credit_limit")]
    public string? TfCreditLimit { get; set; }

    [JsonPropertyName("tf_credit_consumed")]
    public string? TfCreditConsumed { get; set; }

    [JsonPropertyName("tf_adjusted_asset_value")]
    public string? TfAdjustedAssetValue { get; set; }

    [JsonPropertyName("tf_adjusted_liability_value")]
    public string? TfAdjustedLiabilityValue { get; set; }

    [JsonPropertyName("tf_adjusted_credit_consumed")]
    public string? TfAdjustedCreditConsumed { get; set; }

    [JsonPropertyName("tf_adjusted_equity")]
    public string? TfAdjustedEquity { get; set; }

    public bool Frozen { get; set; }

    [JsonPropertyName("frozen_reason")]
    public string? FrozenReason { get; set; }

    [JsonPropertyName("tf_enabled")]
    public bool TfEnabled { get; set; }

    [JsonPropertyName("pm_enabled")]
    public bool PmEnabled { get; set; }

    [JsonPropertyName("market_rates")]
    public MarketRate[] MarketRates { get; set; } = [];

    [JsonPropertyName("asset_balances")]
    public AssetBalance[] AssetBalances { get; set; } = [];

    [JsonPropertyName("tf_loans")]
    public LoanInfo[] TfLoans { get; set; } = [];

    [JsonPropertyName("pm_loans")]
    public LoanInfo[] PmLoans { get; set; } = [];

    [JsonPropertyName("short_collateral")]
    public LoanInfo[] ShortCollateral { get; set; } = [];

    [JsonPropertyName("gross_market_value")]
    public string? GrossMarketValue { get; set; }

    [JsonPropertyName("net_market_value")]
    public string? NetMarketValue { get; set; }

    [JsonPropertyName("long_market_value")]
    public string? LongMarketValue { get; set; }

    [JsonPropertyName("non_marginable_long_market_value")]
    public string? NonMarginableLongMarketValue { get; set; }

    [JsonPropertyName("short_market_value")]
    public string? ShortMarketValue { get; set; }

    [JsonPropertyName("gross_leverage")]
    public string? GrossLeverage { get; set; }

    [JsonPropertyName("net_exposure")]
    public string? NetExposure { get; set; }

    [JsonPropertyName("portfolio_stress_triggered")]
    public MarginAddOn? PortfolioStressTriggered { get; set; }

    [JsonPropertyName("pm_asset_info")]
    public PmAssetInfo? PmAssetInfo { get; set; }

    [JsonPropertyName("pm_credit_limit")]
    public string? PmCreditLimit { get; set; }

    [JsonPropertyName("pm_margin_limit")]
    public string? PmMarginLimit { get; set; }

    [JsonPropertyName("pm_margin_consumed")]
    public string? PmMarginConsumed { get; set; }

    public class MarginSummaryBuilder
    {
      private string? _entityId;
      private string? _marginEquity;
      private string? _marginRequirement;
      private string? _excessDeficit;
      private string? _pmCreditConsumed;
      private string? _tfCreditLimit;
      private string? _tfCreditConsumed;
      private string? _tfAdjustedAssetValue;
      private string? _tfAdjustedLiabilityValue;
      private string? _tfAdjustedCreditConsumed;
      private string? _tfAdjustedEquity;
      private bool _frozen;
      private string? _frozenReason;
      private bool _tfEnabled;
      private bool _pmEnabled;
      private MarketRate[] _marketRates = [];
      private AssetBalance[] _assetBalances = [];
      private LoanInfo[] _tfLoans = [];
      private LoanInfo[] _pmLoans = [];
      private LoanInfo[] _shortCollateral = [];
      private string? _grossMarketValue;
      private string? _netMarketValue;
      private string? _longMarketValue;
      private string? _nonMarginableLongMarketValue;
      private string? _shortMarketValue;
      private string? _grossLeverage;
      private string? _netExposure;
      private MarginAddOn? _portfolioStressTriggered;
      private PmAssetInfo? _pmAssetInfo;
      private string? _pmCreditLimit;
      private string? _pmMarginLimit;
      private string? _pmMarginConsumed;

      public MarginSummaryBuilder WithEntityId(string? entityId)
      {
        this._entityId = entityId;
        return this;
      }

      public MarginSummaryBuilder WithMarginEquity(string? marginEquity)
      {
        this._marginEquity = marginEquity;
        return this;
      }

      public MarginSummaryBuilder WithMarginRequirement(string? marginRequirement)
      {
        this._marginRequirement = marginRequirement;
        return this;
      }

      public MarginSummaryBuilder WithExcessDeficit(string? excessDeficit)
      {
        this._excessDeficit = excessDeficit;
        return this;
      }

      public MarginSummaryBuilder WithPmCreditConsumed(string? pmCreditConsumed)
      {
        this._pmCreditConsumed = pmCreditConsumed;
        return this;
      }

      public MarginSummaryBuilder WithTfCreditLimit(string? tfCreditLimit)
      {
        this._tfCreditLimit = tfCreditLimit;
        return this;
      }

      public MarginSummaryBuilder WithTfCreditConsumed(string? tfCreditConsumed)
      {
        this._tfCreditConsumed = tfCreditConsumed;
        return this;
      }

      public MarginSummaryBuilder WithTfAdjustedAssetValue(string? tfAdjustedAssetValue)
      {
        this._tfAdjustedAssetValue = tfAdjustedAssetValue;
        return this;
      }

      public MarginSummaryBuilder WithTfAdjustedLiabilityValue(string? tfAdjustedLiabilityValue)
      {
        this._tfAdjustedLiabilityValue = tfAdjustedLiabilityValue;
        return this;
      }

      public MarginSummaryBuilder WithTfAdjustedCreditConsumed(string? tfAdjustedCreditConsumed)
      {
        this._tfAdjustedCreditConsumed = tfAdjustedCreditConsumed;
        return this;
      }

      public MarginSummaryBuilder WithTfAdjustedEquity(string? tfAdjustedEquity)
      {
        this._tfAdjustedEquity = tfAdjustedEquity;
        return this;
      }

      public MarginSummaryBuilder WithFrozen(bool frozen)
      {
        this._frozen = frozen;
        return this;
      }

      public MarginSummaryBuilder WithFrozenReason(string? frozenReason)
      {
        this._frozenReason = frozenReason;
        return this;
      }

      public MarginSummaryBuilder WithTfEnabled(bool tfEnabled)
      {
        this._tfEnabled = tfEnabled;
        return this;
      }

      public MarginSummaryBuilder WithPmEnabled(bool pmEnabled)
      {
        this._pmEnabled = pmEnabled;
        return this;
      }

      public MarginSummaryBuilder WithMarketRates(MarketRate[] marketRates)
      {
        this._marketRates = marketRates;
        return this;
      }

      public MarginSummaryBuilder WithAssetBalances(AssetBalance[] assetBalances)
      {
        this._assetBalances = assetBalances;
        return this;
      }

      public MarginSummaryBuilder WithTfLoans(LoanInfo[] tfLoans)
      {
        this._tfLoans = tfLoans;
        return this;
      }

      public MarginSummaryBuilder WithPmLoans(LoanInfo[] pmLoans)
      {
        this._pmLoans = pmLoans;
        return this;
      }

      public MarginSummaryBuilder WithShortCollateral(LoanInfo[] shortCollateral)
      {
        this._shortCollateral = shortCollateral;
        return this;
      }

      public MarginSummaryBuilder WithGrossMarketValue(string? grossMarketValue)
      {
        this._grossMarketValue = grossMarketValue;
        return this;
      }

      public MarginSummaryBuilder WithNetMarketValue(string? netMarketValue)
      {
        this._netMarketValue = netMarketValue;
        return this;
      }

      public MarginSummaryBuilder WithLongMarketValue(string? longMarketValue)
      {
        this._longMarketValue = longMarketValue;
        return this;
      }

      public MarginSummaryBuilder WithNonMarginableLongMarketValue(string? nonMarginableLongMarketValue)
      {
        this._nonMarginableLongMarketValue = nonMarginableLongMarketValue;
        return this;
      }

      public MarginSummaryBuilder WithShortMarketValue(string? shortMarketValue)
      {
        this._shortMarketValue = shortMarketValue;
        return this;
      }

      public MarginSummaryBuilder WithGrossLeverage(string? grossLeverage)
      {
        this._grossLeverage = grossLeverage;
        return this;
      }

      public MarginSummaryBuilder WithNetExposure(string? netExposure)
      {
        this._netExposure = netExposure;
        return this;
      }

      public MarginSummaryBuilder WithPortfolioStressTriggered(MarginAddOn? portfolioStressTriggered)
      {
        this._portfolioStressTriggered = portfolioStressTriggered;
        return this;
      }

      public MarginSummaryBuilder WithPmAssetInfo(PmAssetInfo? pmAssetInfo)
      {
        this._pmAssetInfo = pmAssetInfo;
        return this;
      }

      public MarginSummaryBuilder WithPmCreditLimit(string? pmCreditLimit)
      {
        this._pmCreditLimit = pmCreditLimit;
        return this;
      }

      public MarginSummaryBuilder WithPmMarginLimit(string? pmMarginLimit)
      {
        this._pmMarginLimit = pmMarginLimit;
        return this;
      }

      public MarginSummaryBuilder WithPmMarginConsumed(string? pmMarginConsumed)
      {
        this._pmMarginConsumed = pmMarginConsumed;
        return this;
      }

      public MarginSummary Build()
      {
        return new MarginSummary
        {
          EntityId = this._entityId,
          MarginEquity = this._marginEquity,
          MarginRequirement = this._marginRequirement,
          ExcessDeficit = this._excessDeficit,
          PmCreditConsumed = this._pmCreditConsumed,
          TfCreditLimit = this._tfCreditLimit,
          TfCreditConsumed = this._tfCreditConsumed,
          TfAdjustedAssetValue = this._tfAdjustedAssetValue,
          TfAdjustedLiabilityValue = this._tfAdjustedLiabilityValue,
          TfAdjustedCreditConsumed = this._tfAdjustedCreditConsumed,
          TfAdjustedEquity = this._tfAdjustedEquity,
          Frozen = this._frozen,
          FrozenReason = this._frozenReason,
          TfEnabled = this._tfEnabled,
          PmEnabled = this._pmEnabled,
          MarketRates = this._marketRates,
          AssetBalances = this._assetBalances,
          TfLoans = this._tfLoans,
          PmLoans = this._pmLoans,
          ShortCollateral = this._shortCollateral,
          GrossMarketValue = this._grossMarketValue,
          NetMarketValue = this._netMarketValue,
          LongMarketValue = this._longMarketValue,
          NonMarginableLongMarketValue = this._nonMarginableLongMarketValue,
          ShortMarketValue = this._shortMarketValue,
          GrossLeverage = this._grossLeverage,
          NetExposure = this._netExposure,
          PortfolioStressTriggered = this._portfolioStressTriggered,
          PmAssetInfo = this._pmAssetInfo,
          PmCreditLimit = this._pmCreditLimit,
          PmMarginLimit = this._pmMarginLimit,
          PmMarginConsumed = this._pmMarginConsumed
        };
      }
    }
  }
}