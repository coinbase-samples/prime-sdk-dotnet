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

using Newtonsoft.Json;

namespace Coinbase.Prime.Portfolios
{
  public class Portfolio
  {
    public string? Id { get; set; }
    public string? Name { get; set; }

    [JsonProperty("entity_id")]
    public string? EntityId { get; set; }

    [JsonProperty("organization_id")]
    public string? OrganizationId { get; set; }

    [JsonProperty("entity_name")]
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

    public Portfolio(Builder builder)
    {
      Id = builder.Id;
      Name = builder.Name;
      EntityId = builder.EntityId;
      OrganizationId = builder.OrganizationId;
      EntityName = builder.EntityName;
    }

    public class Builder
    {
      public string? Id { get; private set; }
      public string? Name { get; private set; }
      public string? EntityId { get; private set; }
      public string? OrganizationId { get; private set; }
      public string? EntityName { get; private set; }

      public Builder() { }

      public Builder WithId(string id)
      {
        Id = id;
        return this;
      }

      public Builder WithName(string name)
      {
        Name = name;
        return this;
      }

      public Builder WithEntityId(string entityId)
      {
        EntityId = entityId;
        return this;
      }

      public Builder WithOrganizationId(string organizationId)
      {
        OrganizationId = organizationId;
        return this;
      }

      public Builder WithEntityName(string entityName)
      {
        EntityName = entityName;
        return this;
      }

      public Portfolio Build()
      {
        return new Portfolio(this);
      }
    }
  }
}
