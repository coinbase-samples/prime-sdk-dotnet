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
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// Create Portfolio Net Allocations.
  /// </summary>
  public class CreateNetAllocationRequest()
  {
    public string? SourcePortfolioId { get; set; }

    public string? ProductId { get; set; }

    public string[] OrderIds { get; set; } = [];

    public AllocationLeg[] AllocationLegs { get; set; } = [];

    public AllocationSizeType? SizeType { get; set; }

    public string? RemainderDestinationPortfolio { get; set; }

    public string? NettingId { get; set; }

    public class CreateNetAllocationRequestBuilder
    {
      private string? _sourcePortfolioId;
      private string? _productId;
      private string[] _orderIds;
      private AllocationLeg[] _allocationLegs;
      private AllocationSizeType? _sizeType;
      private string? _remainderDestinationPortfolio;
      private string? _nettingId;

      public CreateNetAllocationRequestBuilder WithSourcePortfolioId(string? sourcePortfolioId)
      {
        _sourcePortfolioId = sourcePortfolioId;
        return this;
      }

      public CreateNetAllocationRequestBuilder WithProductId(string? productId)
      {
        _productId = productId;
        return this;
      }

      public CreateNetAllocationRequestBuilder WithOrderIds(string[] orderIds)
      {
        _orderIds = orderIds;
        return this;
      }

      public CreateNetAllocationRequestBuilder WithAllocationLegs(AllocationLeg[] allocationLegs)
      {
        _allocationLegs = allocationLegs;
        return this;
      }

      public CreateNetAllocationRequestBuilder WithSizeType(AllocationSizeType? sizeType)
      {
        _sizeType = sizeType;
        return this;
      }

      public CreateNetAllocationRequestBuilder WithRemainderDestinationPortfolio(string? remainderDestinationPortfolio)
      {
        _remainderDestinationPortfolio = remainderDestinationPortfolio;
        return this;
      }

      public CreateNetAllocationRequestBuilder WithNettingId(string? nettingId)
      {
        _nettingId = nettingId;
        return this;
      }

      private void Validate()
      {
      }

      public CreateNetAllocationRequest Build()
      {
        Validate();
        return new CreateNetAllocationRequest()
        {
          SourcePortfolioId = _sourcePortfolioId,
          ProductId = _productId,
          OrderIds = _orderIds ?? [],
          AllocationLegs = _allocationLegs ?? [],
          SizeType = _sizeType,
          RemainderDestinationPortfolio = _remainderDestinationPortfolio,
          NettingId = _nettingId,
        };
      }
    }
  }
}
