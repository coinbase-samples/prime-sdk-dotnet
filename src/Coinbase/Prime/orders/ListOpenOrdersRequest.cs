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

using System;
using Newtonsoft.Json;

namespace Coinbase.Prime.Orders
{
  using Coinbase.Prime.Common;

  public class ListOpenOrdersRequest : BaseListRequest
  {
    [JsonProperty("product_ids")]
    public string[]? ProductIds { get; set; }

    [JsonProperty("order_type")]
    public OrderType? OrderType { get; set; }

    [JsonProperty("start_date"), JsonRequired]
    public DateTime? StartDate { get; set; }

    [JsonProperty("order_side")]
    public OrderSide? OrderSide { get; set; }

    [JsonProperty("end_date")]
    public DateTime? EndDate { get; set; }

    public ListOpenOrdersRequest() { }
  }
}