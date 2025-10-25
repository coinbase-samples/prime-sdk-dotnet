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

namespace CoinbaseSdk.Prime.Futures
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class FuturesService(ICoinbaseClient client) : CoinbaseService(client), IFuturesService
  {
    public CancelEntityFuturesSweepResponse CancelEntityFuturesSweep(
      CancelEntityFuturesSweepRequest request,
      CallOptions? options = null)
    {
      return Request<CancelEntityFuturesSweepResponse>(
        HttpMethod.Delete,
        $"/entities/{request.EntityId}/futures/sweeps",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<CancelEntityFuturesSweepResponse> CancelEntityFuturesSweepAsync(
      CancelEntityFuturesSweepRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<CancelEntityFuturesSweepResponse>(
        HttpMethod.Delete,
        $"/entities/{request.EntityId}/futures/sweeps",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetFcmBalanceResponse GetFcmBalance(
      GetFcmBalanceRequest request,
      CallOptions? options = null)
    {
      return Request<GetFcmBalanceResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/balance_summary",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetFcmBalanceResponse> GetFcmBalanceAsync(
      GetFcmBalanceRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetFcmBalanceResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/balance_summary",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetPositionsResponse GetPositions(
      GetPositionsRequest request,
      CallOptions? options = null)
    {
      return Request<GetPositionsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/positions",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetPositionsResponse> GetPositionsAsync(
      GetPositionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetPositionsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/positions",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public ListEntityFuturesSweepsResponse ListEntityFuturesSweeps(
      ListEntityFuturesSweepsRequest request,
      CallOptions? options = null)
    {
      return Request<ListEntityFuturesSweepsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/sweeps",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<ListEntityFuturesSweepsResponse> ListEntityFuturesSweepsAsync(
      ListEntityFuturesSweepsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ListEntityFuturesSweepsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/sweeps",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public ScheduleEntityFuturesSweepResponse ScheduleEntityFuturesSweep(
      ScheduleEntityFuturesSweepRequest request,
      CallOptions? options = null)
    {
      return Request<ScheduleEntityFuturesSweepResponse>(
        HttpMethod.Post,
        $"/entities/{request.EntityId}/futures/sweeps",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ScheduleEntityFuturesSweepResponse> ScheduleEntityFuturesSweepAsync(
      ScheduleEntityFuturesSweepRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<ScheduleEntityFuturesSweepResponse>(
        HttpMethod.Post,
        $"/entities/{request.EntityId}/futures/sweeps",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public SetAutoSweepResponse SetAutoSweep(
      SetAutoSweepRequest request,
      CallOptions? options = null)
    {
      return Request<SetAutoSweepResponse>(
        HttpMethod.Post,
        $"/entities/{request.EntityId}/futures/auto_sweep",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<SetAutoSweepResponse> SetAutoSweepAsync(
      SetAutoSweepRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<SetAutoSweepResponse>(
        HttpMethod.Post,
        $"/entities/{request.EntityId}/futures/auto_sweep",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetFcmMarginCallDetailsResponse GetFcmMarginCallDetails(
      GetFcmMarginCallDetailsRequest request,
      CallOptions? options = null)
    {
      return Request<GetFcmMarginCallDetailsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/margin_call_details",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetFcmMarginCallDetailsResponse> GetFcmMarginCallDetailsAsync(
      GetFcmMarginCallDetailsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetFcmMarginCallDetailsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/margin_call_details",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetFcmRiskLimitsResponse GetFcmRiskLimits(
      GetFcmRiskLimitsRequest request,
      CallOptions? options = null)
    {
      return Request<GetFcmRiskLimitsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/risk_limits",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<GetFcmRiskLimitsResponse> GetFcmRiskLimitsAsync(
      GetFcmRiskLimitsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return RequestAsync<GetFcmRiskLimitsResponse>(
        HttpMethod.Get,
        $"/entities/{request.EntityId}/futures/risk_limits",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }
  }
}
