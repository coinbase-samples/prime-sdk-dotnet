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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;
  public class StakingInitiateResponse
  {
    [JsonPropertyName("wallet_id")]
    public string? WalletId { get; set; }

    [JsonPropertyName("transaction_id")]
    public string? TransactionId { get; set; }

    [JsonPropertyName("activity_id")]
    public string? ActivityId { get; set; }

    public StakingInitiateResponse() { }

    public class StakingInitiateResponseBuilder
    {
      private string? _walletId;
      private string? _transactionId;
      private string? _activityId;

      public StakingInitiateResponseBuilder WithWalletId(string? walletId)
      {
        this._walletId = walletId;
        return this;
      }

      public StakingInitiateResponseBuilder WithTransactionId(string? transactionId)
      {
        this._transactionId = transactionId;
        return this;
      }

      public StakingInitiateResponseBuilder WithActivityId(string? activityId)
      {
        this._activityId = activityId;
        return this;
      }

      public StakingInitiateResponse Build()
      {
        return new StakingInitiateResponse
        {
          WalletId = this._walletId,
          TransactionId = this._transactionId,
          ActivityId = this._activityId
        };
      }
    }
  }
}