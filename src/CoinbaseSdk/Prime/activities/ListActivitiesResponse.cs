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

namespace CoinbaseSdk.Prime.Activities
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  /// <summary>
  /// Response containing activities.
  /// </summary>
  public class ListActivitiesResponse
  {
    /// <summary>
    /// Array of activities.
    /// </summary>
    [JsonPropertyName("activities")]
    public Activity[] Activities { get; set; } = [];

    /// <summary>
    /// Pagination information.
    /// </summary>
    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }
    public ListActivitiesResponse()
    {
    }

    /// <summary>
    /// Builder for ListActivitiesResponse.
    /// </summary>
    public class ListActivitiesResponseBuilder
    {
      private Activity[] _activities = [];
      private Pagination? _pagination;

      public ListActivitiesResponseBuilder WithActivities(Activity[] activities)
      {
        _activities = activities;
        return this;
      }

      public ListActivitiesResponseBuilder WithPagination(Pagination pagination)
      {
        _pagination = pagination;
        return this;
      }

      public ListActivitiesResponse Build()
      {
        return new ListActivitiesResponse()
        {
          Activities = _activities,
          Pagination = _pagination
        };
      }
    }
  }
}