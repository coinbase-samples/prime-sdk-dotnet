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

namespace Coinbase.Prime.Allocations
{
  using System.Text.Json.Serialization;
  public class CreateAllocationRequest
  {
    [JsonPropertyName("allocation_id")]
    public string? AllocationId { get; set; }

    [JsonPropertyName("source_portfolio_id")]
    public string? SourcePortfolioId { get; set; }

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    [JsonPropertyName("order_ids")]
    public string[] OrderIds { get; set; } = [];

    [JsonPropertyName("allocation_legs")]
    public AllocationLeg[] AllocationLegs { get; set; } = [];

    [JsonPropertyName("size_type")]
    public SizeType SizeType { get; set; }

    [JsonPropertyName("remainder_destination_portfolio")]
    public string? RemainderDestinationPortfolio { get; set; }

    [JsonPropertyName("netting_id")]
    public string? NettingId { get; set; }

    public CreateAllocationRequest() { }

    public class CreateAllocationRequestBuilder
    {
      private string? _allocationId;
      private string? _sourcePortfolioId;
      private string? _productId;
      private string[] _orderIds = [];
      private AllocationLeg[] _allocationLegs = [];
      private SizeType _sizeType;
      private string? _remainderDestinationPortfolio;
      private string? _nettingId;

      public CreateAllocationRequestBuilder WithAllocationId(string? allocationId)
      {
        this._allocationId = allocationId;
        return this;
      }

      public CreateAllocationRequestBuilder WithSourcePortfolioId(string? sourcePortfolioId)
      {
        this._sourcePortfolioId = sourcePortfolioId;
        return this;
      }

      public CreateAllocationRequestBuilder WithProductId(string? productId)
      {
        this._productId = productId;
        return this;
      }

      public CreateAllocationRequestBuilder WithOrderIds(string[] orderIds)
      {
        this._orderIds = orderIds;
        return this;
      }

      public CreateAllocationRequestBuilder WithAllocationLegs(AllocationLeg[] allocationLegs)
      {
        this._allocationLegs = allocationLegs;
        return this;
      }

      public CreateAllocationRequestBuilder WithSizeType(SizeType sizeType)
      {
        this._sizeType = sizeType;
        return this;
      }

      public CreateAllocationRequestBuilder WithRemainderDestinationPortfolio(string? remainderDestinationPortfolio)
      {
        this._remainderDestinationPortfolio = remainderDestinationPortfolio;
        return this;
      }

      public CreateAllocationRequestBuilder WithNettingId(string? nettingId)
      {
        this._nettingId = nettingId;
        return this;
      }

      public CreateAllocationRequest Build()
      {
        return new CreateAllocationRequest
        {
          AllocationId = this._allocationId,
          SourcePortfolioId = this._sourcePortfolioId,
          ProductId = this._productId,
          OrderIds = this._orderIds,
          AllocationLegs = this._allocationLegs,
          SizeType = this._sizeType,
          RemainderDestinationPortfolio = this._remainderDestinationPortfolio,
          NettingId = this._nettingId
        };
      }
    }
  }
}
