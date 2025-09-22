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
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  /// <summary>
  /// Response containing FCM positions.
  /// </summary>
  public class GetPositionsResponse
  {
    /// <summary>
    /// Array of FCM positions.
    /// </summary>
    [JsonPropertyName("positions")]
    public FcmPosition[] Positions { get; set; } = [];

    [JsonPropertyName("clearing_account_id")]
    public string? ClearingAccountId { get; set; }

    public GetPositionsResponse() { }

    public class GetPositionsResponseBuilder
    {
      private FcmPosition[] _positions = [];
      private string? _clearingAccountId;

      public GetPositionsResponseBuilder WithPositions(FcmPosition[] positions)
      {
        _positions = positions;
        return this;
      }

      public GetPositionsResponseBuilder WithClearingAccountId(string clearingAccountId)
      {
        _clearingAccountId = clearingAccountId;
        return this;
      }

      public GetPositionsResponse Build()
      {
        return new GetPositionsResponse
        {
          Positions = _positions,
          ClearingAccountId = _clearingAccountId,
        };
      }
    }
  }
}