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

namespace CoinbaseSdk.Prime.Orders
{
  using System.Text.Json.Serialization;

  public class OrderEdit
  {
    [JsonPropertyName("edit_id")]
    public string? EditId { get; set; }

    [JsonPropertyName("order_id")]
    public string? OrderId { get; set; }

    [JsonPropertyName("edit_timestamp")]
    public string? EditTimestamp { get; set; }

    [JsonPropertyName("edit_type")]
    public string? EditType { get; set; }

    [JsonPropertyName("previous_values")]
    public OrderEditValues? PreviousValues { get; set; }

    [JsonPropertyName("new_values")]
    public OrderEditValues? NewValues { get; set; }
  }
}