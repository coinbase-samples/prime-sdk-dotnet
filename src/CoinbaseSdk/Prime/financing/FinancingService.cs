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
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class FinancingService(ICoinbaseClient client) : CoinbaseService(client), IFinancingService
  {
    /// <summary>
    /// List Financing Eligible Assets.
    /// </summary>
    public ListFinancingEligibleAssetsResponse ListFinancingEligibleAssets(
      CallOptions? options = null)
    {
      return Request<ListFinancingEligibleAssetsResponse>(
        HttpMethod.Get,
        $"/financing/eligible-assets",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<ListFinancingEligibleAssetsResponse> ListFinancingEligibleAssetsAsync(
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListFinancingEligibleAssetsResponse>(
        HttpMethod.Get,
        $"/financing/eligible-assets",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// List Interest Accruals.
    /// </summary>
    public ListInterestAccrualsResponse ListInterestAccruals(
      ListInterestAccrualsRequest request,
      CallOptions? options = null)
    {
      return Request<ListInterestAccrualsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/accruals",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListInterestAccrualsResponse> ListInterestAccrualsAsync(
      ListInterestAccrualsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListInterestAccrualsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/accruals",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Exchange Cross Margin Overview.
    /// </summary>
    public GetCrossMarginOverviewResponse GetCrossMarginOverview(
      GetCrossMarginOverviewRequest request,
      CallOptions? options = null)
    {
      return Request<GetCrossMarginOverviewResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/cross_margin",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetCrossMarginOverviewResponse> GetCrossMarginOverviewAsync(
      GetCrossMarginOverviewRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetCrossMarginOverviewResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/cross_margin",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Entity Locate Availabilities.
    /// </summary>
    public GetEntityLocateAvailabilitiesResponse GetEntityLocateAvailabilities(
      GetEntityLocateAvailabilitiesRequest request,
      CallOptions? options = null)
    {
      return Request<GetEntityLocateAvailabilitiesResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/locates_availability",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetEntityLocateAvailabilitiesResponse> GetEntityLocateAvailabilitiesAsync(
      GetEntityLocateAvailabilitiesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetEntityLocateAvailabilitiesResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/locates_availability",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Margin Information.
    /// </summary>
    public GetMarginInformationResponse GetMarginInformation(
      GetMarginInformationRequest request,
      CallOptions? options = null)
    {
      return Request<GetMarginInformationResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/margin",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetMarginInformationResponse> GetMarginInformationAsync(
      GetMarginInformationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetMarginInformationResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/margin",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// List Margin Call Summaries.
    /// </summary>
    public ListMarginCallSummariesResponse ListMarginCallSummaries(
      ListMarginCallSummariesRequest request,
      CallOptions? options = null)
    {
      return Request<ListMarginCallSummariesResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/margin_summaries",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListMarginCallSummariesResponse> ListMarginCallSummariesAsync(
      ListMarginCallSummariesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListMarginCallSummariesResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/margin_summaries",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Market Data.
    /// </summary>
    public GetMarketDataResponse GetMarketData(
      GetMarketDataRequest request,
      CallOptions? options = null)
    {
      return Request<GetMarketDataResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/market_data",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetMarketDataResponse> GetMarketDataAsync(
      GetMarketDataRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetMarketDataResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/market_data",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// List Trade Finance Obligations.
    /// </summary>
    public ListTradeFinanceObligationsResponse ListTradeFinanceObligations(
      ListTradeFinanceObligationsRequest request,
      CallOptions? options = null)
    {
      return Request<ListTradeFinanceObligationsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/tf_obligations",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<ListTradeFinanceObligationsResponse> ListTradeFinanceObligationsAsync(
      ListTradeFinanceObligationsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListTradeFinanceObligationsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/tf_obligations",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Trade Finance Tiered Pricing Fees.
    /// </summary>
    public GetTradeFinanceTieredPricingFeesResponse GetTradeFinanceTieredPricingFees(
      GetTradeFinanceTieredPricingFeesRequest request,
      CallOptions? options = null)
    {
      return Request<GetTradeFinanceTieredPricingFeesResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/tf_tiered_fees",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetTradeFinanceTieredPricingFeesResponse> GetTradeFinanceTieredPricingFeesAsync(
      GetTradeFinanceTieredPricingFeesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetTradeFinanceTieredPricingFeesResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/tf_tiered_fees",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// List Interest Accruals For Portfolio.
    /// </summary>
    public ListInterestAccrualsForPortfolioResponse ListInterestAccrualsForPortfolio(
      ListInterestAccrualsForPortfolioRequest request,
      CallOptions? options = null)
    {
      return Request<ListInterestAccrualsForPortfolioResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/accruals",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListInterestAccrualsForPortfolioResponse> ListInterestAccrualsForPortfolioAsync(
      ListInterestAccrualsForPortfolioRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListInterestAccrualsForPortfolioResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/accruals",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Portfolio Buying Power.
    /// </summary>
    public GetPortfolioBuyingPowerResponse GetPortfolioBuyingPower(
      GetPortfolioBuyingPowerRequest request,
      CallOptions? options = null)
    {
      return Request<GetPortfolioBuyingPowerResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/buying_power",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetPortfolioBuyingPowerResponse> GetPortfolioBuyingPowerAsync(
      GetPortfolioBuyingPowerRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetPortfolioBuyingPowerResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/buying_power",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Portfolio Credit Information.
    /// </summary>
    public GetPortfolioCreditInformationResponse GetPortfolioCreditInformation(
      GetPortfolioCreditInformationRequest request,
      CallOptions? options = null)
    {
      return Request<GetPortfolioCreditInformationResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/credit",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetPortfolioCreditInformationResponse> GetPortfolioCreditInformationAsync(
      GetPortfolioCreditInformationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetPortfolioCreditInformationResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/credit",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// List Existing Locates.
    /// </summary>
    public ListExistingLocatesResponse ListExistingLocates(
      ListExistingLocatesRequest request,
      CallOptions? options = null)
    {
      return Request<ListExistingLocatesResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/locates",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListExistingLocatesResponse> ListExistingLocatesAsync(
      ListExistingLocatesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListExistingLocatesResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/locates",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// List Margin Conversions.
    /// </summary>
    public ListMarginConversionsResponse ListMarginConversions(
      ListMarginConversionsRequest request,
      CallOptions? options = null)
    {
      return Request<ListMarginConversionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/margin_conversions",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListMarginConversionsResponse> ListMarginConversionsAsync(
      ListMarginConversionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListMarginConversionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/margin_conversions",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Portfolio Withdrawal Power.
    /// </summary>
    public GetPortfolioWithdrawalPowerResponse GetPortfolioWithdrawalPower(
      GetPortfolioWithdrawalPowerRequest request,
      CallOptions? options = null)
    {
      return Request<GetPortfolioWithdrawalPowerResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/withdrawal_power",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetPortfolioWithdrawalPowerResponse> GetPortfolioWithdrawalPowerAsync(
      GetPortfolioWithdrawalPowerRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetPortfolioWithdrawalPowerResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/withdrawal_power",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Cross Margin Risk Parameters.
    /// </summary>
    public GetCrossMarginRiskParametersResponse GetCrossMarginRiskParameters(
      GetCrossMarginRiskParametersRequest request,
      CallOptions? options = null)
    {
      return Request<GetCrossMarginRiskParametersResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/cross_margin/risk_parameters",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetCrossMarginRiskParametersResponse> GetCrossMarginRiskParametersAsync(
      GetCrossMarginRiskParametersRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetCrossMarginRiskParametersResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/cross_margin/risk_parameters",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Get Prime Cross Margin Overview.
    /// </summary>
    public GetCrossMarginPrimeOverviewResponse GetCrossMarginPrimeOverview(
      GetCrossMarginPrimeOverviewRequest request,
      CallOptions? options = null)
    {
      return Request<GetCrossMarginPrimeOverviewResponse>(
        HttpMethod.Get,
        $"/v2/entities/{request.EntityId}/cross_margin/prime",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetCrossMarginPrimeOverviewResponse> GetCrossMarginPrimeOverviewAsync(
      GetCrossMarginPrimeOverviewRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetCrossMarginPrimeOverviewResponse>(
        HttpMethod.Get,
        $"/v2/entities/{request.EntityId}/cross_margin/prime",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Update Funding Settings.
    /// </summary>
    public UpdateFundingSettingsResponse UpdateFundingSettings(
      UpdateFundingSettingsRequest request,
      CallOptions? options = null)
    {
      return Request<UpdateFundingSettingsResponse>(
        HttpMethod.Post,
        $"/entities/{request.EntityId}/funding_settings",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<UpdateFundingSettingsResponse> UpdateFundingSettingsAsync(
      UpdateFundingSettingsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<UpdateFundingSettingsResponse>(
        HttpMethod.Post,
        $"/entities/{request.EntityId}/funding_settings",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    /// <summary>
    /// Create New Locates.
    /// </summary>
    public CreateNewLocatesResponse CreateNewLocates(
      CreateNewLocatesRequest request,
      CallOptions? options = null)
    {
      return Request<CreateNewLocatesResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/locates",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateNewLocatesResponse> CreateNewLocatesAsync(
      CreateNewLocatesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CreateNewLocatesResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/locates",
        [HttpStatusCode.Created, HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}
