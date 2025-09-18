/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Tests
{
    using Xunit;
    using CoinbaseSdk.Prime.Products;
    using CoinbaseSdk.Prime.Invoice;
    using CoinbaseSdk.Prime.Model;

    public class BackwardsCompatibilityTests
    {
        [Fact]
        public void ListPortfolioProductsRequest_ShouldMaintainExistingBuilderApi()
        {
            // Arrange & Act - Should compile without errors
            var request = new ListPortfolioProductsRequest.ListPortfolioProductsRequestBuilder()
                .WithPortfolioId("test-portfolio")
                .WithCursor("test-cursor")
                .WithLimit(50)
                .WithSortDirection(SortDirection.ASC)
                .Build();

            // Assert
            Assert.Equal("test-portfolio", request.PortfolioId);
            Assert.Equal("test-cursor", request.Cursor);
            Assert.Equal(50, request.Limit);
            Assert.Equal(SortDirection.ASC, request.SortDirection);
        }

        [Fact]
        public void ListPortfolioProductsRequest_ShouldSupportPaginationBuilder()
        {
            // Arrange
            var pagination = new Pagination
            {
                NextCursor = "pagination-cursor",
                SortDirection = SortDirection.DESC
            };

            // Act
            var request = new ListPortfolioProductsRequest.ListPortfolioProductsRequestBuilder()
                .WithPortfolioId("test-portfolio")
                .WithPagination(pagination)
                .Build();

            // Assert
            Assert.Equal("test-portfolio", request.PortfolioId);
            Assert.Equal("pagination-cursor", request.Cursor);
            Assert.Equal(SortDirection.DESC, request.SortDirection);
        }

        [Fact]
        public void ListInvoicesRequest_ShouldMaintainExistingBuilderApi()
        {
            // Arrange & Act - Should compile without errors
            var request = new ListInvoicesRequest.ListInvoicesRequestBuilder()
                .WithEntityId("test-entity")
                .WithCursor("123")
                .WithLimit(25)
                .WithBillingMonth(12)
                .WithBillingYear(2024)
                .Build();

            // Assert
            Assert.Equal("test-entity", request.EntityId);
            Assert.Equal("123", request.Cursor);
            Assert.Equal(25, request.Limit);
            Assert.Equal(12, request.BillingMonth);
            Assert.Equal(2024, request.BillingYear);
        }

        [Fact]
        public void ListPortfolioProductsRequest_ShouldInheritFromPaginatedRequest()
        {
            // Arrange
            var request = new ListPortfolioProductsRequest("test-portfolio");

            // Assert
            Assert.IsAssignableFrom<PaginatedRequest>(request);
        }

        [Fact]
        public void ListInvoicesRequest_ShouldInheritFromPaginatedRequest()
        {
            // Arrange
            var request = new ListInvoicesRequest("test-entity");

            // Assert
            Assert.IsAssignableFrom<PaginatedRequest>(request);
        }

        [Fact]
        public void PaginatedRequest_CursorPropertyShouldBeAccessible()
        {
            // Arrange
            var request = new ListPortfolioProductsRequest("test-portfolio");

            // Act
            request.Cursor = "test-cursor";
            request.Limit = 100;

            // Assert
            Assert.Equal("test-cursor", request.Cursor);
            Assert.Equal(100, request.Limit);
        }

        [Fact]
        public void ListInvoicesRequest_CursorPropertyShouldBeAccessible()
        {
            // Arrange
            var request = new ListInvoicesRequest("test-entity");

            // Act
            request.Cursor = "456";
            request.Limit = 50;

            // Assert
            Assert.Equal("456", request.Cursor);
            Assert.Equal(50, request.Limit);
        }
    }
}