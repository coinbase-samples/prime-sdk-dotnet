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

namespace CoinbaseSdk.Prime.Futures
{
  using CoinbaseSdk.Core.Http;

  public interface IFuturesService
  {
    /// <summary>
    /// Get Entity FCM Balance.
    /// </summary>
    public GetFcmBalanceResponse GetFcmBalance(
      GetFcmBalanceRequest request,
      CallOptions? options = null);

    public Task<GetFcmBalanceResponse> GetFcmBalanceAsync(
      GetFcmBalanceRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get FCM Equity.
    /// </summary>
    public GetFcmEquityResponse GetFcmEquity(
      GetFcmEquityRequest request,
      CallOptions? options = null);

    public Task<GetFcmEquityResponse> GetFcmEquityAsync(
      GetFcmEquityRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get FCM Margin Call Details.
    /// </summary>
    public GetFcmMarginCallDetailsResponse GetFcmMarginCallDetails(
      GetFcmMarginCallDetailsRequest request,
      CallOptions? options = null);

    public Task<GetFcmMarginCallDetailsResponse> GetFcmMarginCallDetailsAsync(
      GetFcmMarginCallDetailsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Entity Positions.
    /// </summary>
    public GetPositionsResponse GetPositions(
      GetPositionsRequest request,
      CallOptions? options = null);

    public Task<GetPositionsResponse> GetPositionsAsync(
      GetPositionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get FCM Risk Limits.
    /// </summary>
    public GetFcmRiskLimitsResponse GetFcmRiskLimits(
      GetFcmRiskLimitsRequest request,
      CallOptions? options = null);

    public Task<GetFcmRiskLimitsResponse> GetFcmRiskLimitsAsync(
      GetFcmRiskLimitsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get FCM Settings.
    /// </summary>
    public GetFcmSettingsResponse GetFcmSettings(
      GetFcmSettingsRequest request,
      CallOptions? options = null);

    public Task<GetFcmSettingsResponse> GetFcmSettingsAsync(
      GetFcmSettingsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Entity Futures Sweeps.
    /// </summary>
    public ListEntityFuturesSweepsResponse ListEntityFuturesSweeps(
      ListEntityFuturesSweepsRequest request,
      CallOptions? options = null);

    public Task<ListEntityFuturesSweepsResponse> ListEntityFuturesSweepsAsync(
      ListEntityFuturesSweepsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Set Auto Sweep.
    /// </summary>
    public SetAutoSweepResponse SetAutoSweep(
      SetAutoSweepRequest request,
      CallOptions? options = null);

    public Task<SetAutoSweepResponse> SetAutoSweepAsync(
      SetAutoSweepRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Set FCM Settings.
    /// </summary>
    public SetFcmSettingsResponse SetFcmSettings(
      SetFcmSettingsRequest request,
      CallOptions? options = null);

    public Task<SetFcmSettingsResponse> SetFcmSettingsAsync(
      SetFcmSettingsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Schedule Entity Futures Sweep.
    /// </summary>
    public ScheduleEntityFuturesSweepResponse ScheduleEntityFuturesSweep(
      ScheduleEntityFuturesSweepRequest request,
      CallOptions? options = null);

    public Task<ScheduleEntityFuturesSweepResponse> ScheduleEntityFuturesSweepAsync(
      ScheduleEntityFuturesSweepRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancel Entity Futures Sweep.
    /// </summary>
    public CancelEntityFuturesSweepResponse CancelEntityFuturesSweep(
      CancelEntityFuturesSweepRequest request,
      CallOptions? options = null);

    public Task<CancelEntityFuturesSweepResponse> CancelEntityFuturesSweepAsync(
      CancelEntityFuturesSweepRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);
  }
}
