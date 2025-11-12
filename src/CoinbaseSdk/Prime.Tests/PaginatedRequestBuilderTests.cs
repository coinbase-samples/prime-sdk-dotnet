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
    using CoinbaseSdk.Prime.Common;
    using CoinbaseSdk.Prime.Model;
    using CoinbaseSdk.Prime.Model.Enums;

    // TODO: PaginatedRequestBuilder no longer exists - entire test class needs refactoring
    /* public class PaginatedRequestBuilderTests
    {
        [Fact]
        public void Builder_WithCursor_ShouldSetCursor()
        {
            // Arrange
            var builder = new TestPaginatedRequestBuilder();

            // Act
            var result = builder.WithCursor("test-cursor");

            // Assert
            Assert.Same(builder, result); // Should return same instance for chaining
            var request = builder.Build();
            Assert.Equal("test-cursor", request.Cursor);
        }

        [Fact]
        public void Builder_WithLimit_ShouldSetLimit()
        {
            // Arrange
            var builder = new TestPaginatedRequestBuilder();

            // Act
            var result = builder.WithLimit(25);

            // Assert
            Assert.Same(builder, result); // Should return same instance for chaining
            var request = builder.Build();
            Assert.Equal(25, request.Limit);
        }

        [Fact]
        public void Builder_WithPagination_ShouldSetCursorFromPaginationObject()
        {
            // Arrange
            var builder = new TestPaginatedRequestBuilder();
            var pagination = new Pagination
            {
                NextCursor = "pagination-cursor",
                SortDirection = SortDirection.ASC,
                HasNext = true
            };

            // Act
            var result = builder.WithPagination(pagination);

            // Assert
            Assert.Same(builder, result); // Should return same instance for chaining
            var request = builder.Build();
            Assert.Equal("pagination-cursor", request.Cursor);
        }

        [Fact]
        public void Builder_WithPagination_NullNextCursor_ShouldSetNullCursor()
        {
            // Arrange
            var builder = new TestPaginatedRequestBuilder();
            var pagination = new Pagination
            {
                NextCursor = null,
                SortDirection = SortDirection.DESC,
                HasNext = false
            };

            // Act
            builder.WithPagination(pagination);

            // Assert
            var request = builder.Build();
            Assert.Null(request.Cursor);
        }

        [Fact]
        public void Builder_ChainedCalls_ShouldWorkCorrectly()
        {
            // Arrange
            var builder = new TestPaginatedRequestBuilder();

            // Act
            var request = builder
                .WithCursor("chained-cursor")
                .WithLimit(100)
                .Build();

            // Assert
            Assert.Equal("chained-cursor", request.Cursor);
            Assert.Equal(100, request.Limit);
        }

        [Fact]
        public void Builder_OverridePaginationWithDirectCall_ShouldUseLastValue()
        {
            // Arrange
            var builder = new TestPaginatedRequestBuilder();
            var pagination = new Pagination
            {
                NextCursor = "pagination-cursor",
                SortDirection = SortDirection.ASC
            };

            // Act - Set pagination first, then override cursor
            var request = builder
                .WithPagination(pagination)
                .WithCursor("override-cursor")
                .WithLimit(50)
                .Build();

            // Assert
            Assert.Equal("override-cursor", request.Cursor);
            Assert.Equal(50, request.Limit);
        }

        // Test concrete implementations for testing the generic base classes
        private class TestPaginatedRequest : PaginatedRequest
        {
        }

        // TODO: PaginatedRequestBuilder no longer exists - tests need updating
        /* private class TestPaginatedRequestBuilder : PaginatedRequestBuilder<TestPaginatedRequest, TestPaginatedRequestBuilder>
        {
            public override TestPaginatedRequest Build()
            {
                var request = new TestPaginatedRequest();
                SetPaginationProperties(request);
                return request;
            }
        } */
    /* } */
}
