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

namespace CoinbaseSdk.Prime.Allocations
{
  using System.Text.Json.Serialization;

  public class CreateNetAllocationResponse
  {
    public bool? Success { get; set; }

    [JsonPropertyName("netting_id")]
    public string? NettingId { get; set; }

    [JsonPropertyName("buy_allocation_id")]
    public string? BuyAllocationId { get; set; }

    [JsonPropertyName("sell_allocation_id")]
    public string? SellAllocationId { get; set; }

    [JsonPropertyName("failure_reason")]
    public string? FailureReason { get; set; }

    public CreateNetAllocationResponse() { }

    public class CreateNetAllocationResponseBuilder
    {
      private bool? _success;
      private string? _nettingId;
      private string? _buyAllocationId;
      private string? _sellAllocationId;
      private string? _failureReason;

      public CreateNetAllocationResponseBuilder WithSuccess(bool? success)
      {
        this._success = success;
        return this;
      }

      public CreateNetAllocationResponseBuilder WithNettingId(string? nettingId)
      {
        this._nettingId = nettingId;
        return this;
      }

      public CreateNetAllocationResponseBuilder WithBuyAllocationId(string? buyAllocationId)
      {
        this._buyAllocationId = buyAllocationId;
        return this;
      }

      public CreateNetAllocationResponseBuilder WithSellAllocationId(string? sellAllocationId)
      {
        this._sellAllocationId = sellAllocationId;
        return this;
      }

      public CreateNetAllocationResponseBuilder WithFailureReason(string? failureReason)
      {
        this._failureReason = failureReason;
        return this;
      }

      public CreateNetAllocationResponse Build()
      {
        return new CreateNetAllocationResponse
        {
          Success = this._success,
          NettingId = this._nettingId,
          BuyAllocationId = this._buyAllocationId,
          SellAllocationId = this._sellAllocationId,
          FailureReason = this._failureReason
        };
      }
    }
  }
}
