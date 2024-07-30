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

namespace Coinbase.Prime.Wallets
{
  using System.Text.Json.Serialization;
  public class FiatDepositInstructions
  {
    public string? Id { get; set; }
    public string? Name { get; set; }
    public DepositType Type { get; set; }

    [JsonPropertyName("account_number")]
    public string? AccountNumber { get; set; }

    [JsonPropertyName("routing_number")]
    public string? RoutingNumber { get; set; }

    [JsonPropertyName("reference_code")]
    public string? ReferenceCode { get; set; }

    public FiatDepositInstructions() { }
  }
}
