namespace CoinbaseSdk.Prime.Futures
{
  using System.Text.Json.Serialization;

  /// <summary>
  /// Response containing FCM balance summary.
  /// </summary>
  public class GetFcmBalanceResponse
  {
    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }

    [JsonPropertyName("cfm_usd_balance")]
    public string? CfmUsdBalance { get; set; }

    [JsonPropertyName("unrealized_pnl")]
    public string? UnrealizedPnl { get; set; }

    [JsonPropertyName("daily_realized_pnl")]
    public string? DailyRealizedPnl { get; set; }

    [JsonPropertyName("excess_liquidity")]
    public string? ExcessLiquidity { get; set; }

    [JsonPropertyName("futures_buying_power")]
    public string? FuturesBuyingPower { get; set; }

    [JsonPropertyName("initial_margin")]
    public string? InitialMargin { get; set; }

    [JsonPropertyName("maintenance_margin")]
    public string? MaintenanceMargin { get; set; }

    [JsonPropertyName("clearing_account_id")]
    public string? ClearingAccountId { get; set; }

    /// <summary>
    /// CFM unsettled accrued funding PnL.
    /// </summary>
    [JsonPropertyName("cfm_unsettled_accrued_funding_pnl")]
    public string? CfmUnsettledAccruedFundingPnl { get; set; }

    public GetFcmBalanceResponse() { }

    public class GetFcmBalanceResponseBuilder
    {
      private string? _portfolioId;
      private string? _cfmUsdBalance;
      private string? _unrealizedPnl;
      private string? _dailyRealizedPnl;
      private string? _excessLiquidity;
      private string? _futuresBuyingPower;
      private string? _initialMargin;
      private string? _maintenanceMargin;
      private string? _clearingAccountId;
      private string? _cfmUnsettledAccruedFundingPnl;

      public GetFcmBalanceResponseBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public GetFcmBalanceResponseBuilder WithCfmUsdBalance(string cfmUsdBalance)
      {
        this._cfmUsdBalance = cfmUsdBalance;
        return this;
      }

      public GetFcmBalanceResponseBuilder WithUnrealizedPnl(string unrealizedPnl)
      {
        this._unrealizedPnl = unrealizedPnl;
        return this;
      }

      public GetFcmBalanceResponseBuilder WithDailyRealizedPnl(string dailyRealizedPnl)
      {
        this._dailyRealizedPnl = dailyRealizedPnl;
        return this;
      }

      public GetFcmBalanceResponseBuilder WithExcessLiquidity(string excessLiquidity)
      {
        this._excessLiquidity = excessLiquidity;
        return this;
      }

      public GetFcmBalanceResponseBuilder WithFuturesBuyingPower(string futuresBuyingPower)
      {
        this._futuresBuyingPower = futuresBuyingPower;
        return this;
      }

      public GetFcmBalanceResponseBuilder WithInitialMargin(string initialMargin)
      {
        this._initialMargin = initialMargin;
        return this;
      }

      public GetFcmBalanceResponseBuilder WithMaintenanceMargin(string maintenanceMargin)
      {
        this._maintenanceMargin = maintenanceMargin;
        return this;
      }

      public GetFcmBalanceResponseBuilder WithClearingAccountId(string clearingAccountId)
      {
        this._clearingAccountId = clearingAccountId;
        return this;
      }

      public GetFcmBalanceResponseBuilder WithCfmUnsettledAccruedFundingPnl(string cfmUnsettledAccruedFundingPnl)
      {
        this._cfmUnsettledAccruedFundingPnl = cfmUnsettledAccruedFundingPnl;
        return this;
      }

      public GetFcmBalanceResponse Build()
      {
        return new GetFcmBalanceResponse
        {
          PortfolioId = this._portfolioId,
          CfmUsdBalance = this._cfmUsdBalance,
          UnrealizedPnl = this._unrealizedPnl,
          DailyRealizedPnl = this._dailyRealizedPnl,
          ExcessLiquidity = this._excessLiquidity,
          FuturesBuyingPower = this._futuresBuyingPower,
          InitialMargin = this._initialMargin,
          MaintenanceMargin = this._maintenanceMargin,
          ClearingAccountId = this._clearingAccountId,
          CfmUnsettledAccruedFundingPnl = this._cfmUnsettledAccruedFundingPnl
        };
      }
    }
  }
}