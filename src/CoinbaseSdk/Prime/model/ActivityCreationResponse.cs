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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;

  /// <summary>
  /// Response object for operations that create activities.
  /// </summary>
  public class ActivityCreationResponse
  {
    /// <summary>
    /// The activity ID for the created operation.
    /// </summary>
    [JsonPropertyName("activity_id")]
    public string? ActivityId { get; set; }

    /// <summary>
    /// Builder for <see cref="ActivityCreationResponse"/>.
    /// </summary>
    public class ActivityCreationResponseBuilder
    {
      private string? _activityId;

      /// <summary>
      /// Sets the activity ID.
      /// </summary>
      /// <param name="activityId">The activity ID.</param>
      /// <returns>The builder instance.</returns>
      public ActivityCreationResponseBuilder WithActivityId(string activityId)
      {
        this._activityId = activityId;
        return this;
      }

      /// <summary>
      /// Builds the <see cref="ActivityCreationResponse"/>.
      /// </summary>
      /// <returns>The <see cref="ActivityCreationResponse"/>.</returns>
      public ActivityCreationResponse Build()
      {
        return new ActivityCreationResponse
        {
          ActivityId = this._activityId
        };
      }
    }
  }
}