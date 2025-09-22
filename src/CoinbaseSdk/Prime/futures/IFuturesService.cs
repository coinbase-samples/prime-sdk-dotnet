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
  using CoinbaseSdk.Core.Http;
  public interface IFuturesService
  {
    CancelEntityFuturesSweepResponse CancelEntityFuturesSweep(
        CancelEntityFuturesSweepRequest request,
        CallOptions? options = null);

    Task<CancelEntityFuturesSweepResponse> CancelEntityFuturesSweepAsync(
        CancelEntityFuturesSweepRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    GetFcmBalanceResponse GetFcmBalance(
        GetFcmBalanceRequest request,
        CallOptions? options = null);

    Task<GetFcmBalanceResponse> GetFcmBalanceAsync(
        GetFcmBalanceRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    GetPositionsResponse GetPositions(
        GetPositionsRequest request,
        CallOptions? options = null);

    Task<GetPositionsResponse> GetPositionsAsync(
        GetPositionsRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    ListEntityFuturesSweepsResponse ListEntityFuturesSweeps(
        ListEntityFuturesSweepsRequest request,
        CallOptions? options = null);
    Task<ListEntityFuturesSweepsResponse> ListEntityFuturesSweepsAsync(
        ListEntityFuturesSweepsRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    ScheduleEntityFuturesSweepResponse ScheduleEntityFuturesSweep(
        ScheduleEntityFuturesSweepRequest request,
        CallOptions? options = null);

    Task<ScheduleEntityFuturesSweepResponse> ScheduleEntityFuturesSweepAsync(
        ScheduleEntityFuturesSweepRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    SetAutoSweepResponse SetAutoSweep(
        SetAutoSweepRequest request,
        CallOptions? options = null);

    Task<SetAutoSweepResponse> SetAutoSweepAsync(
        SetAutoSweepRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    GetFcmMarginCallDetailsResponse GetFcmMarginCallDetails(
        GetFcmMarginCallDetailsRequest request,
        CallOptions? options = null);

    Task<GetFcmMarginCallDetailsResponse> GetFcmMarginCallDetailsAsync(
        GetFcmMarginCallDetailsRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);

    GetFcmRiskLimitsResponse GetFcmRiskLimits(
        GetFcmRiskLimitsRequest request,
        CallOptions? options = null);

    Task<GetFcmRiskLimitsResponse> GetFcmRiskLimitsAsync(
        GetFcmRiskLimitsRequest request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);
  }
}