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

using Coinbase.Core.Error;
using Coinbase.Core.Http;
using Coinbase.Prime.Common;
using Newtonsoft.Json;

namespace Coinbase.Prime.Orders
{
  public class ListPortfolioOrdersRequest : BaseListRequest
  {
    [JsonProperty("order_statuses")]
    public OrderStatus[]? OrderStatuses { get; set; }

    [JsonProperty("product_ids")]
    public string[]? ProductIds { get; set; }

    [JsonProperty("order_type")]
    public OrderType? OrderType { get; set; }

    [JsonProperty("order_side")]
    public OrderSide? OrderSide { get; set; }

    [JsonProperty("start_date")]
    public string? StartDate { get; set; }

    [JsonProperty("end_date")]
    public string? EndDate { get; set; }

    public ListPortfolioOrdersRequest() { }
  }
}