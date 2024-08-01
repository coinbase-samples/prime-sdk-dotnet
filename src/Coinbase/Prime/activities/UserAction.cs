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
  using System.Text.Json.Serialization;
  public class UserAction
  {
    public UserActionType Action { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    public string? Timestamp { get; set; }

    public UserAction()
    {
    }

    public UserAction(UserActionType action, string userId, string timestamp)
    {
      Action = action;
      UserId = userId;
      Timestamp = timestamp;
    }

    public class UserActionBuilder
    {
      private UserActionType _action;
      private string? _userId;
      private string? _timestamp;

      public UserActionBuilder WithAction(UserActionType action)
      {
        _action = action;
        return this;
      }

      public UserActionBuilder WithUserId(string? userId)
      {
        _userId = userId;
        return this;
      }

      public UserActionBuilder WithTimestamp(string? timestamp)
      {
        _timestamp = timestamp;
        return this;
      }

      public UserAction Build()
      {
        return new UserAction
        {
          Action = _action,
          UserId = _userId,
          Timestamp = _timestamp
        };
      }
    }
  }
}
