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