/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Orders
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// Get Order Preview.
  /// </summary>
  public class GetOrderPreviewRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? ProductId { get; set; }

    public OrderSide Side { get; set; }

    public OrderType Type { get; set; }

    public string? BaseQuantity { get; set; }

    public string? QuoteValue { get; set; }

    public string? LimitPrice { get; set; }

    public string? StartTime { get; set; }

    public string? ExpiryTime { get; set; }

    public TimeInForceType? TimeInForce { get; set; }

    public bool? IsRaiseExact { get; set; }

    public string? HistoricalPov { get; set; }

    public string? StopPrice { get; set; }

    public string? SettlCurrency { get; set; }

    public bool? PostOnly { get; set; }

    public string? DisplayQuoteSize { get; set; }

    public string? DisplayBaseSize { get; set; }

    public PegOffsetType? PegOffsetType { get; set; }

    public string? Offset { get; set; }

    public string? WigLevel { get; set; }

    public class GetOrderPreviewRequestBuilder
    {
      private string? _portfolioId;
      private string? _productId;
      private OrderSide _side;
      private OrderType _type;
      private string? _baseQuantity;
      private string? _quoteValue;
      private string? _limitPrice;
      private string? _startTime;
      private string? _expiryTime;
      private TimeInForceType? _timeInForce;
      private bool? _isRaiseExact;
      private string? _historicalPov;
      private string? _stopPrice;
      private string? _settlCurrency;
      private bool? _postOnly;
      private string? _displayQuoteSize;
      private string? _displayBaseSize;
      private PegOffsetType? _pegOffsetType;
      private string? _offset;
      private string? _wigLevel;

      public GetOrderPreviewRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithProductId(string? productId)
      {
        _productId = productId;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithSide(OrderSide side)
      {
        _side = side;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithType(OrderType type)
      {
        _type = type;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithBaseQuantity(string? baseQuantity)
      {
        _baseQuantity = baseQuantity;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithQuoteValue(string? quoteValue)
      {
        _quoteValue = quoteValue;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithLimitPrice(string? limitPrice)
      {
        _limitPrice = limitPrice;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithStartTime(string? startTime)
      {
        _startTime = startTime;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithExpiryTime(string? expiryTime)
      {
        _expiryTime = expiryTime;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithTimeInForce(TimeInForceType? timeInForce)
      {
        _timeInForce = timeInForce;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithIsRaiseExact(bool? isRaiseExact)
      {
        _isRaiseExact = isRaiseExact;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithHistoricalPov(string? historicalPov)
      {
        _historicalPov = historicalPov;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithStopPrice(string? stopPrice)
      {
        _stopPrice = stopPrice;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithSettlCurrency(string? settlCurrency)
      {
        _settlCurrency = settlCurrency;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithPostOnly(bool? postOnly)
      {
        _postOnly = postOnly;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithDisplayQuoteSize(string? displayQuoteSize)
      {
        _displayQuoteSize = displayQuoteSize;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithDisplayBaseSize(string? displayBaseSize)
      {
        _displayBaseSize = displayBaseSize;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithPegOffsetType(PegOffsetType? pegOffsetType)
      {
        _pegOffsetType = pegOffsetType;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithOffset(string? offset)
      {
        _offset = offset;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithWigLevel(string? wigLevel)
      {
        _wigLevel = wigLevel;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public GetOrderPreviewRequest Build()
      {
        Validate();
        return new GetOrderPreviewRequest(_portfolioId!)
        {
          ProductId = _productId,
          Side = _side,
          Type = _type,
          BaseQuantity = _baseQuantity,
          QuoteValue = _quoteValue,
          LimitPrice = _limitPrice,
          StartTime = _startTime,
          ExpiryTime = _expiryTime,
          TimeInForce = _timeInForce,
          IsRaiseExact = _isRaiseExact,
          HistoricalPov = _historicalPov,
          StopPrice = _stopPrice,
          SettlCurrency = _settlCurrency,
          PostOnly = _postOnly,
          DisplayQuoteSize = _displayQuoteSize,
          DisplayBaseSize = _displayBaseSize,
          PegOffsetType = _pegOffsetType,
          Offset = _offset,
          WigLevel = _wigLevel,
        };
      }
    }
  }
}
