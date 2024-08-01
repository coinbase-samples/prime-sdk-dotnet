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

namespace Coinbase.Prime.AddressBook
{
  using System.Text.Json.Serialization;
  public class User
  {
    public string? Id { get; set; }
    public string? Name { get; set; }

    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    public User()
    {
    }

    public class UserBuilder
    {
      private string? _id;
      private string? _name;
      private string? _avatarUrl;

      public UserBuilder WithId(string? id)
      {
        this._id = id;
        return this;
      }

      public UserBuilder WithName(string? name)
      {
        this._name = name;
        return this;
      }

      public UserBuilder WithAvatarUrl(string? avatarUrl)
      {
        this._avatarUrl = avatarUrl;
        return this;
      }

      public User Build()
      {
        return new User
        {
          Id = this._id,
          Name = this._name,
          AvatarUrl = this._avatarUrl
        };
      }
    }
  }
}
