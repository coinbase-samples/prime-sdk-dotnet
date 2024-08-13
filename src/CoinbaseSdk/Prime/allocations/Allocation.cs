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

namespace CoinbaseSdk.Prime.Allocations
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Orders;
  public class Allocation
  {
    [JsonPropertyName("root_id")]
    public string? RootId { get; set; }

    [JsonPropertyName("reversal_id")]
    public string? ReversalId { get; set; }

    [JsonPropertyName("allocation_completed_at")]
    public string? AllocationCompletedAt { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    public OrderSide Side { get; set; }

    [JsonPropertyName("avg_price")]
    public string? AveragePrice { get; set; }

    [JsonPropertyName("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonPropertyName("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonPropertyName("fees_allocated")]
    public string? FeesAllocated { get; set; }

    public AllocationStatus Status { get; set; }

    public string? Source { get; set; }

    [JsonPropertyName("order_ids")]
    public string[] OrderIds { get; set; } = [];

    [JsonPropertyName("netting_id")]
    public string? NettingId { get; set; }

    public AllocationDestination[] Destinations { get; set; } = [];

    public Allocation() { }

    public class AllocationBuilder
    {
      private string? _rootId;
      private string? _reversalId;
      private string? _allocationCompletedAt;
      private string? _userId;
      private string? _productId;
      private OrderSide _side;
      private string? _averagePrice;
      private string? _baseQuantity;
      private string? _quoteValue;
      private string? _feesAllocated;
      private AllocationStatus _status;
      private string? _source;
      private string[] _orderIds = Array.Empty<string>();
      private string? _nettingId;
      private AllocationDestination[] _destinations = Array.Empty<AllocationDestination>();

      public AllocationBuilder WithRootId(string? rootId)
      {
        this._rootId = rootId;
        return this;
      }

      public AllocationBuilder WithReversalId(string? reversalId)
      {
        this._reversalId = reversalId;
        return this;
      }

      public AllocationBuilder WithAllocationCompletedAt(string? allocationCompletedAt)
      {
        this._allocationCompletedAt = allocationCompletedAt;
        return this;
      }

      public AllocationBuilder WithUserId(string? userId)
      {
        this._userId = userId;
        return this;
      }

      public AllocationBuilder WithProductId(string? productId)
      {
        this._productId = productId;
        return this;
      }

      public AllocationBuilder WithSide(OrderSide side)
      {
        this._side = side;
        return this;
      }

      public AllocationBuilder WithAveragePrice(string? averagePrice)
      {
        this._averagePrice = averagePrice;
        return this;
      }

      public AllocationBuilder WithBaseQuantity(string? baseQuantity)
      {
        this._baseQuantity = baseQuantity;
        return this;
      }

      public AllocationBuilder WithQuoteValue(string? quoteValue)
      {
        this._quoteValue = quoteValue;
        return this;
      }

      public AllocationBuilder WithFeesAllocated(string? feesAllocated)
      {
        this._feesAllocated = feesAllocated;
        return this;
      }

      public AllocationBuilder WithStatus(AllocationStatus status)
      {
        this._status = status;
        return this;
      }

      public AllocationBuilder WithSource(string? source)
      {
        this._source = source;
        return this;
      }

      public AllocationBuilder WithOrderIds(string[] orderIds)
      {
        this._orderIds = orderIds;
        return this;
      }

      public AllocationBuilder WithNettingId(string? nettingId)
      {
        this._nettingId = nettingId;
        return this;
      }

      public AllocationBuilder WithDestinations(AllocationDestination[] destinations)
      {
        this._destinations = destinations;
        return this;
      }

      public Allocation Build()
      {
        return new Allocation
        {
          RootId = this._rootId,
          ReversalId = this._reversalId,
          AllocationCompletedAt = this._allocationCompletedAt,
          UserId = this._userId,
          ProductId = this._productId,
          Side = this._side,
          AveragePrice = this._averagePrice,
          BaseQuantity = this._baseQuantity,
          QuoteValue = this._quoteValue,
          FeesAllocated = this._feesAllocated,
          Status = this._status,
          Source = this._source,
          OrderIds = this._orderIds,
          NettingId = this._nettingId,
          Destinations = this._destinations
        };
      }
    }
  }
}
