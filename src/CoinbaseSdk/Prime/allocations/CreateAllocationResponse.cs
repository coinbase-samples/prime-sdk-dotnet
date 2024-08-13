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
  public class CreateAllocationResponse
  {
    public bool? Success { get; set; }

    [JsonPropertyName("allocation_id")]
    public string? AllocationId { get; set; }

    [JsonPropertyName("failure_reason")]
    public string? FailureReason { get; set; }

    [JsonPropertyName("netting_id")]
    public string? NettingId { get; set; }

    public CreateAllocationResponse() { }

    public class CreateAllocationResponseBuilder
    {
      private bool? _success;
      private string? _allocationId;
      private string? _failureReason;
      private string? _nettingId;

      public CreateAllocationResponseBuilder WithSuccess(bool? success)
      {
        this._success = success;
        return this;
      }

      public CreateAllocationResponseBuilder WithAllocationId(string? allocationId)
      {
        this._allocationId = allocationId;
        return this;
      }

      public CreateAllocationResponseBuilder WithFailureReason(string? failureReason)
      {
        this._failureReason = failureReason;
        return this;
      }

      public CreateAllocationResponseBuilder WithNettingId(string? nettingId)
      {
        this._nettingId = nettingId;
        return this;
      }

      public CreateAllocationResponse Build()
      {
        return new CreateAllocationResponse
        {
          Success = this._success,
          AllocationId = this._allocationId,
          FailureReason = this._failureReason,
          NettingId = this._nettingId
        };
      }
    }
  }
}
