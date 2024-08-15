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

namespace CoinbaseSdk.Prime.Users
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  public class User
  {
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }

    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }

    public Role? Role { get; set; }

    public User() { }

    public class UserBuilder
    {
      private string? Id;
      private string? Name;
      private string? Email;
      private string? EntityId;
      private string? PortfolioId;
      private Role? Role;

      public UserBuilder() { }

      public UserBuilder WithId(string id)
      {
        this.Id = id;
        return this;
      }

      public UserBuilder WithName(string name)
      {
        this.Name = name;
        return this;
      }

      public UserBuilder WithEmail(string email)
      {
        this.Email = email;
        return this;
      }

      public UserBuilder WithEntityId(string entityId)
      {
        this.EntityId = entityId;
        return this;
      }

      public UserBuilder WithPortfolioId(string portfolioId)
      {
        this.PortfolioId = portfolioId;
        return this;
      }

      public UserBuilder WithRole(Role role)
      {
        this.Role = role;
        return this;
      }

      public User Build()
      {
        return new User
        {
          Id = this.Id,
          Name = this.Name,
          Email = this.Email,
          EntityId = this.EntityId,
          PortfolioId = this.PortfolioId,
          Role = this.Role
        };
      }
    }
  }
}
