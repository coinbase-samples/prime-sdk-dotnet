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
  /// Create Portfolio Allocations.
  /// </summary>
  public class CreateAllocationRequest()
  {
    public string? AllocationId { get; set; }

    public string? SourcePortfolioId { get; set; }

    public string? ProductId { get; set; }

    public string[] OrderIds { get; set; } = [];

    public AllocationLeg[] AllocationLegs { get; set; } = [];

    public AllocationSizeType? SizeType { get; set; }

    public string? RemainderDestinationPortfolio { get; set; }

    public class CreateAllocationRequestBuilder
    {
      private string? _allocationId;
      private string? _sourcePortfolioId;
      private string? _productId;
      private string[] _orderIds;
      private AllocationLeg[] _allocationLegs;
      private AllocationSizeType? _sizeType;
      private string? _remainderDestinationPortfolio;

      public CreateAllocationRequestBuilder WithAllocationId(string? allocationId)
      {
        _allocationId = allocationId;
        return this;
      }

      public CreateAllocationRequestBuilder WithSourcePortfolioId(string? sourcePortfolioId)
      {
        _sourcePortfolioId = sourcePortfolioId;
        return this;
      }

      public CreateAllocationRequestBuilder WithProductId(string? productId)
      {
        _productId = productId;
        return this;
      }

      public CreateAllocationRequestBuilder WithOrderIds(string[] orderIds)
      {
        _orderIds = orderIds;
        return this;
      }

      public CreateAllocationRequestBuilder WithAllocationLegs(AllocationLeg[] allocationLegs)
      {
        _allocationLegs = allocationLegs;
        return this;
      }

      public CreateAllocationRequestBuilder WithSizeType(AllocationSizeType? sizeType)
      {
        _sizeType = sizeType;
        return this;
      }

      public CreateAllocationRequestBuilder WithRemainderDestinationPortfolio(string? remainderDestinationPortfolio)
      {
        _remainderDestinationPortfolio = remainderDestinationPortfolio;
        return this;
      }

      private void Validate()
      {
      }

      public CreateAllocationRequest Build()
      {
        Validate();
        return new CreateAllocationRequest()
        {
          AllocationId = _allocationId,
          SourcePortfolioId = _sourcePortfolioId,
          ProductId = _productId,
          OrderIds = _orderIds ?? [],
          AllocationLegs = _allocationLegs ?? [],
          SizeType = _sizeType,
          RemainderDestinationPortfolio = _remainderDestinationPortfolio,
        };
      }
    }
  }
}
