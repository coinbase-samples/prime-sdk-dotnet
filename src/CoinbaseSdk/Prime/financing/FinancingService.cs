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

namespace CoinbaseSdk.Prime.Financing
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class FinancingService(ICoinbaseClient client) : CoinbaseService(client), IFinancingService
  {
    public CreateNewLocatesResponse CreateNewLocates(
      CreateNewLocatesRequest request,
      CallOptions? options = null)
    {
      return this.Request<CreateNewLocatesResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/locates",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<CreateNewLocatesResponse> CreateNewLocatesAsync(
      CreateNewLocatesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<CreateNewLocatesResponse>(
        HttpMethod.Post,
        $"/portfolios/{request.PortfolioId}/locates",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetEntityLocateAvailabilitiesResponse GetEntityLocateAvailabilities(
      GetEntityLocateAvailabilitiesRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetEntityLocateAvailabilitiesResponse>(
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
      return this.RequestAsync<GetEntityLocateAvailabilitiesResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/locates_availability",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetMarginInformationResponse GetMarginInformation(
      GetMarginInformationRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetMarginInformationResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/margin",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetMarginInformationResponse> GetMarginInformationAsync(
      GetMarginInformationRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetMarginInformationResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/margin",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetPortfolioBuyingPowerResponse GetPortfolioBuyingPower(
      GetPortfolioBuyingPowerRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetPortfolioBuyingPowerResponse>(
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
      return this.RequestAsync<GetPortfolioBuyingPowerResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/buying_power",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetPortfolioCreditInformationResponse GetPortfolioCreditInformation(
      GetPortfolioCreditInformationRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetPortfolioCreditInformationResponse>(
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
      return this.RequestAsync<GetPortfolioCreditInformationResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/credit",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetPortfolioWithdrawalPowerResponse GetPortfolioWithdrawalPower(
      GetPortfolioWithdrawalPowerRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetPortfolioWithdrawalPowerResponse>(
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
      return this.RequestAsync<GetPortfolioWithdrawalPowerResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/withdrawal_power",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetTradeFinanceTieredPricingFeesResponse GetTradeFinanceTieredPricingFees(
      GetTradeFinanceTieredPricingFeesRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetTradeFinanceTieredPricingFeesResponse>(
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
      return this.RequestAsync<GetTradeFinanceTieredPricingFeesResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/tf_tiered_fees",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListExistingLocatesResponse ListExistingLocates(
      ListExistingLocatesRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListExistingLocatesResponse>(
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
      return this.RequestAsync<ListExistingLocatesResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/locates",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListInterestAccrualsResponse ListInterestAccruals(
      ListInterestAccrualsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListInterestAccrualsResponse>(
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
      return this.RequestAsync<ListInterestAccrualsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/accruals",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListInterestAccrualsForPortfolioResponse ListInterestAccrualsForPortfolio(
      ListInterestAccrualsForPortfolioRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListInterestAccrualsForPortfolioResponse>(
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
      return this.RequestAsync<ListInterestAccrualsForPortfolioResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/accruals",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListMarginCallSummariesResponse ListMarginCallSummaries(
      ListMarginCallSummariesRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListMarginCallSummariesResponse>(
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
      return this.RequestAsync<ListMarginCallSummariesResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/margin_summaries",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public ListMarginConversionsResponse ListMarginConversions(
      ListMarginConversionsRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListMarginConversionsResponse>(
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
      return this.RequestAsync<ListMarginConversionsResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/margin_conversions",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}