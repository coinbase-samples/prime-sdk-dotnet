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

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Text.Json.Serialization;
  public class EstimatedNetworkFees
  {
    [JsonPropertyName("lower_bound")]
    public string? LowerBound { get; set; }

    [JsonPropertyName("upper_bound")]
    public string? UpperBound { get; set; }

    public EstimatedNetworkFees() { }

    public class EstimatedNetworkFeesBuilder
    {
      private string? LowerBound;
      private string? UpperBound;

      public EstimatedNetworkFeesBuilder() { }

      public EstimatedNetworkFeesBuilder WithLowerBound(string lowerBound)
      {
        this.LowerBound = lowerBound;
        return this;
      }

      public EstimatedNetworkFeesBuilder WithUpperBound(string upperBound)
      {
        this.UpperBound = upperBound;
        return this;
      }

      public EstimatedNetworkFees Build()
      {
        return new EstimatedNetworkFees
        {
          LowerBound = this.LowerBound,
          UpperBound = this.UpperBound
        };
      }
    }
  }
}
