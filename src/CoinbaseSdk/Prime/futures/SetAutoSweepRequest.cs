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
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// Set Auto Sweep.
  /// </summary>
  public class SetAutoSweepRequest(string entityId)
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    public bool? AutoSweep { get; set; }

    public class SetAutoSweepRequestBuilder
    {
      private string? _entityId;
      private bool? _autoSweep;

      public SetAutoSweepRequestBuilder WithEntityId(string entityId)
      {
        _entityId = entityId;
        return this;
      }

      public SetAutoSweepRequestBuilder WithAutoSweep(bool? autoSweep)
      {
        _autoSweep = autoSweep;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      public SetAutoSweepRequest Build()
      {
        Validate();
        return new SetAutoSweepRequest(_entityId!)
        {
          AutoSweep = _autoSweep,
        };
      }
    }
  }
}
