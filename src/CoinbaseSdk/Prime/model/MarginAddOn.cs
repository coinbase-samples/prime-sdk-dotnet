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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;

  public class MarginAddOn
  {
    public string? Amount { get; set; }

    [JsonPropertyName("add_on_type")]
    public MarginAddOnType? AddOnType { get; set; }

    public MarginAddOn() { }

    public class MarginAddOnBuilder
    {
      private string? _amount;
      private MarginAddOnType? _addOnType;

      public MarginAddOnBuilder WithAmount(string amount)
      {
        this._amount = amount;
        return this;
      }

      public MarginAddOnBuilder WithAddOnType(MarginAddOnType addOnType)
      {
        this._addOnType = addOnType;
        return this;
      }

      public MarginAddOn Build()
      {
        return new MarginAddOn { Amount = this._amount, AddOnType = this._addOnType };
      }
    }
  }
}

