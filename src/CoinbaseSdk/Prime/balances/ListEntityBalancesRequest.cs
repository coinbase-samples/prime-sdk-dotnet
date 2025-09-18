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

namespace CoinbaseSdk.Prime.Balances
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  public class ListEntityBalancesRequest(string entityId) : PaginatedRequest
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    public string[] Symbols { get; set; } = [];


    [JsonPropertyName("aggregation_type")]
    public BalanceType? AggregationType { get; set; }

    public class ListEntityBalancesRequestBuilder(string entityId) : PaginatedRequestBuilder<ListEntityBalancesRequest, ListEntityBalancesRequestBuilder>
    {
      private string _entityId = entityId;
      private string[] _symbols = [];
      private BalanceType? _aggregationType;

      public ListEntityBalancesRequestBuilder WithSymbols(string[] symbols)
      {
        this._symbols = symbols;
        return this;
      }

      public ListEntityBalancesRequestBuilder WithAggregationType(BalanceType? aggregationType)
      {
        this._aggregationType = aggregationType;
        return this;
      }

      public new ListEntityBalancesRequestBuilder WithPagination(Pagination pagination)
      {
        base.WithPagination(pagination);
        return this;
      }

      public override ListEntityBalancesRequest Build()
      {
        var request = new ListEntityBalancesRequest(this._entityId)
        {
          Symbols = this._symbols,
          AggregationType = this._aggregationType
        };
        SetPaginationProperties(request);
        return request;
      }
    }
  }
}