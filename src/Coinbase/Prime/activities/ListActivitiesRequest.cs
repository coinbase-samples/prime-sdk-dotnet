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

namespace Coinbase.Prime.Activities
{
  using Coinbase.Prime.Common;
  using Newtonsoft.Json;
  public class ListActivitiesRequest : BaseListRequest
  {
    public string[] Symbols { get; set; } = [];
    public string[] Categories { get; set; } = [];
    public string[] Statuses { get; set; } = [];
    [JsonProperty("start_time")]
    public string? StartTime { get; set; }
    [JsonProperty("end_time")]
    public string? EndTime { get; set; }

    public ListActivitiesRequest()
    {
    }
  }
}