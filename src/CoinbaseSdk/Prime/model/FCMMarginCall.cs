/*
 * Copyright 2024-present Coinbase Global, Inc.
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
  using System;
  using System.Text.Json.Serialization;

  public class FCMMarginCall
  {
    public FcmMarginCallType? Type { get; set; }
    public FcmMarginCallState? State { get; set; }

    [JsonPropertyName("initial_amount")]
    public string? InitialAmount { get; set; }

    [JsonPropertyName("remaining_amount")]
    public string? RemainingAmount { get; set; }

    [JsonPropertyName("business_date")]
    public DateTime? BusinessDate { get; set; }

    [JsonPropertyName("cure_deadline")]
    public DateTime? CureDeadline { get; set; }

    public FCMMarginCall() { }

    public class FCMMarginCallBuilder
    {
      private FcmMarginCallType? _type;
      private FcmMarginCallState? _state;
      private string? _initialAmount;
      private string? _remainingAmount;
      private DateTime? _businessDate;
      private DateTime? _cureDeadline;

      public FCMMarginCallBuilder WithType(FcmMarginCallType? type)
      {
        this._type = type;
        return this;
      }

      public FCMMarginCallBuilder WithState(FcmMarginCallState? state)
      {
        this._state = state;
        return this;
      }

      public FCMMarginCallBuilder WithInitialAmount(string? initialAmount)
      {
        this._initialAmount = initialAmount;
        return this;
      }

      public FCMMarginCallBuilder WithRemainingAmount(string? remainingAmount)
      {
        this._remainingAmount = remainingAmount;
        return this;
      }

      public FCMMarginCallBuilder WithBusinessDate(DateTime? businessDate)
      {
        this._businessDate = businessDate;
        return this;
      }

      public FCMMarginCallBuilder WithCureDeadline(DateTime? cureDeadline)
      {
        this._cureDeadline = cureDeadline;
        return this;
      }

      public FCMMarginCall Build()
      {
        return new FCMMarginCall
        {
          Type = this._type,
          State = this._state,
          InitialAmount = this._initialAmount,
          RemainingAmount = this._remainingAmount,
          BusinessDate = this._businessDate,
          CureDeadline = this._cureDeadline
        };
      }
    }
  }
}