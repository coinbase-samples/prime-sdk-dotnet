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

namespace CoinbaseSdk.Prime.Activities
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  public class GetActivityRequest(string activityId)
  {
    [JsonIgnore]
    public string ActivityId { get; set; } = activityId;

    public class Builder
    {
      private string? _activityId;

      public Builder WithActivityId(string activityId)
      {
        _activityId = activityId;
        return this;
      }

      /// <summary>
      /// Validates the input fields.
      /// </summary>
      /// <exception cref="CoinbaseClientException">
      /// If <see cref="_activityId"/> is null, empty, or whitespace.
      /// </exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_activityId))
        {
          throw new CoinbaseClientException("ActivityId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="GetActivityRequest"/>.
      /// </summary>
      /// <returns>The new <see cref="GetActivityRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException"> If the required fields are not set.</exception>
      public GetActivityRequest Build()
      {
        Validate();
        return new GetActivityRequest(_activityId!);
      }
    }
  }
}
