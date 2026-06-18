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

namespace CoinbaseSdk.Prime.Financing
{
  using CoinbaseSdk.Core.Http;

  public interface IFinancingService
  {
    /// <summary>
    /// List Financing Eligible Assets.
    /// </summary>
    public ListFinancingEligibleAssetsResponse ListFinancingEligibleAssets(
      CallOptions? options = null);

    public Task<ListFinancingEligibleAssetsResponse> ListFinancingEligibleAssetsAsync(
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Interest Accruals.
    /// </summary>
    public ListInterestAccrualsResponse ListInterestAccruals(
      ListInterestAccrualsRequest request,
      CallOptions? options = null);

    public Task<ListInterestAccrualsResponse> ListInterestAccrualsAsync(
      ListInterestAccrualsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Exchange Cross Margin Overview.
    /// </summary>
    public GetCrossMarginOverviewResponse GetCrossMarginOverview(
      GetCrossMarginOverviewRequest request,
      CallOptions? options = null);

    public Task<GetCrossMarginOverviewResponse> GetCrossMarginOverviewAsync(
      GetCrossMarginOverviewRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Entity Locate Availabilities.
    /// </summary>
    public GetEntityLocateAvailabilitiesResponse GetEntityLocateAvailabilities(
      GetEntityLocateAvailabilitiesRequest request,
      CallOptions? options = null);

    public Task<GetEntityLocateAvailabilitiesResponse> GetEntityLocateAvailabilitiesAsync(
      GetEntityLocateAvailabilitiesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Margin Information.
    /// </summary>
    public GetMarginInformationResponse GetMarginInformation(
      GetMarginInformationRequest request,
      CallOptions? options = null);

    public Task<GetMarginInformationResponse> GetMarginInformationAsync(
      GetMarginInformationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Margin Call Summaries.
    /// </summary>
    public ListMarginCallSummariesResponse ListMarginCallSummaries(
      ListMarginCallSummariesRequest request,
      CallOptions? options = null);

    public Task<ListMarginCallSummariesResponse> ListMarginCallSummariesAsync(
      ListMarginCallSummariesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Market Data.
    /// </summary>
    public GetMarketDataResponse GetMarketData(
      GetMarketDataRequest request,
      CallOptions? options = null);

    public Task<GetMarketDataResponse> GetMarketDataAsync(
      GetMarketDataRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Trade Finance Obligations.
    /// </summary>
    public ListTradeFinanceObligationsResponse ListTradeFinanceObligations(
      ListTradeFinanceObligationsRequest request,
      CallOptions? options = null);

    public Task<ListTradeFinanceObligationsResponse> ListTradeFinanceObligationsAsync(
      ListTradeFinanceObligationsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Trade Finance Tiered Pricing Fees.
    /// </summary>
    public GetTradeFinanceTieredPricingFeesResponse GetTradeFinanceTieredPricingFees(
      GetTradeFinanceTieredPricingFeesRequest request,
      CallOptions? options = null);

    public Task<GetTradeFinanceTieredPricingFeesResponse> GetTradeFinanceTieredPricingFeesAsync(
      GetTradeFinanceTieredPricingFeesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Interest Accruals For Portfolio.
    /// </summary>
    public ListInterestAccrualsForPortfolioResponse ListInterestAccrualsForPortfolio(
      ListInterestAccrualsForPortfolioRequest request,
      CallOptions? options = null);

    public Task<ListInterestAccrualsForPortfolioResponse> ListInterestAccrualsForPortfolioAsync(
      ListInterestAccrualsForPortfolioRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Portfolio Buying Power.
    /// </summary>
    public GetPortfolioBuyingPowerResponse GetPortfolioBuyingPower(
      GetPortfolioBuyingPowerRequest request,
      CallOptions? options = null);

    public Task<GetPortfolioBuyingPowerResponse> GetPortfolioBuyingPowerAsync(
      GetPortfolioBuyingPowerRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Portfolio Credit Information.
    /// </summary>
    public GetPortfolioCreditInformationResponse GetPortfolioCreditInformation(
      GetPortfolioCreditInformationRequest request,
      CallOptions? options = null);

    public Task<GetPortfolioCreditInformationResponse> GetPortfolioCreditInformationAsync(
      GetPortfolioCreditInformationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Existing Locates.
    /// </summary>
    public ListExistingLocatesResponse ListExistingLocates(
      ListExistingLocatesRequest request,
      CallOptions? options = null);

    public Task<ListExistingLocatesResponse> ListExistingLocatesAsync(
      ListExistingLocatesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Margin Conversions.
    /// </summary>
    public ListMarginConversionsResponse ListMarginConversions(
      ListMarginConversionsRequest request,
      CallOptions? options = null);

    public Task<ListMarginConversionsResponse> ListMarginConversionsAsync(
      ListMarginConversionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Portfolio Withdrawal Power.
    /// </summary>
    public GetPortfolioWithdrawalPowerResponse GetPortfolioWithdrawalPower(
      GetPortfolioWithdrawalPowerRequest request,
      CallOptions? options = null);

    public Task<GetPortfolioWithdrawalPowerResponse> GetPortfolioWithdrawalPowerAsync(
      GetPortfolioWithdrawalPowerRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Cross Margin Risk Parameters.
    /// </summary>
    public GetCrossMarginRiskParametersResponse GetCrossMarginRiskParameters(
      GetCrossMarginRiskParametersRequest request,
      CallOptions? options = null);

    public Task<GetCrossMarginRiskParametersResponse> GetCrossMarginRiskParametersAsync(
      GetCrossMarginRiskParametersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Prime Cross Margin Overview.
    /// </summary>
    public GetCrossMarginPrimeOverviewResponse GetCrossMarginPrimeOverview(
      GetCrossMarginPrimeOverviewRequest request,
      CallOptions? options = null);

    public Task<GetCrossMarginPrimeOverviewResponse> GetCrossMarginPrimeOverviewAsync(
      GetCrossMarginPrimeOverviewRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Update Funding Settings.
    /// </summary>
    public UpdateFundingSettingsResponse UpdateFundingSettings(
      UpdateFundingSettingsRequest request,
      CallOptions? options = null);

    public Task<UpdateFundingSettingsResponse> UpdateFundingSettingsAsync(
      UpdateFundingSettingsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Create New Locates.
    /// </summary>
    public CreateNewLocatesResponse CreateNewLocates(
      CreateNewLocatesRequest request,
      CallOptions? options = null);

    public Task<CreateNewLocatesResponse> CreateNewLocatesAsync(
      CreateNewLocatesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);
  }
}
