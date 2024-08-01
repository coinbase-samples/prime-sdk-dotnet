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

namespace Coinbase.Prime.Portfolios
{
  using System.Text.Json.Serialization;
  public class Portfolio
  {
    public string? Id { get; set; }
    public string? Name { get; set; }

    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }

    [JsonPropertyName("organization_id")]
    public string? OrganizationId { get; set; }

    [JsonPropertyName("entity_name")]
    public string? EntityName { get; set; }

    public Portfolio(string id, string name, string entityId, string organizationId, string entityName)
    {
      Id = id;
      Name = name;
      EntityId = entityId;
      OrganizationId = organizationId;
      EntityName = entityName;
    }

    public Portfolio()
    {
    }

    public class PortfolioBuilder
    {
      private string? _id;
      private string? _name;
      private string? _entityId;
      private string? _organizationId;
      private string? _entityName;

      public PortfolioBuilder WithId(string id)
      {
        this._id = id;
        return this;
      }

      public PortfolioBuilder WithName(string name)
      {
        this._name = name;
        return this;
      }

      public PortfolioBuilder WithEntityId(string entityId)
      {
        this._entityId = entityId;
        return this;
      }

      public PortfolioBuilder WithOrganizationId(string organizationId)
      {
        this._organizationId = organizationId;
        return this;
      }

      public PortfolioBuilder WithEntityName(string entityName)
      {
        this._entityName = entityName;
        return this;
      }

      public Portfolio Build()
      {
        return new Portfolio()
        {
          Id = this._id,
          Name = this._name,
          EntityId = this._entityId,
          OrganizationId = this._organizationId,
          EntityName = this._entityName
        };
      }
    }
  }
}
