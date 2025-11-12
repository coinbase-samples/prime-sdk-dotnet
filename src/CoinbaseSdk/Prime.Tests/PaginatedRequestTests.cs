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
    using System.Text.Json;

    public class PaginatedRequestTests
    {
        [Fact]
        public void PaginatedRequest_ShouldHaveCursorProperty()
        {
            // Arrange & Act
            var request = new TestPaginatedRequest();
            
            // Assert
            Assert.Null(request.Cursor);
            
            // Act
            request.Cursor = "test-cursor";
            
            // Assert
            Assert.Equal("test-cursor", request.Cursor);
        }

        [Fact]
        public void PaginatedRequest_ShouldHaveLimitProperty()
        {
            // Arrange & Act
            var request = new TestPaginatedRequest();
            
            // Assert
            Assert.Null(request.Limit);
            
            // Act
            request.Limit = 25;
            
            // Assert
            Assert.Equal(25, request.Limit);
        }

        [Fact]
        public void PaginatedRequest_CursorShouldBeStringType()
        {
            // Arrange
            var request = new TestPaginatedRequest();
            
            // Act & Assert
            Assert.True(typeof(TestPaginatedRequest).GetProperty("Cursor")?.PropertyType == typeof(string));
        }

        [Fact]
        public void PaginatedRequest_LimitShouldBeNullableIntType()
        {
            // Arrange
            var request = new TestPaginatedRequest();
            
            // Act & Assert
            Assert.True(typeof(TestPaginatedRequest).GetProperty("Limit")?.PropertyType == typeof(int?));
        }

        [Fact]
        public void PaginatedRequest_ShouldSerializeToJson()
        {
            // Arrange
            var request = new TestPaginatedRequest
            {
                Cursor = "test-cursor",
                Limit = 50
            };

            // Act
            var json = JsonSerializer.Serialize(request);
            var deserialized = JsonSerializer.Deserialize<TestPaginatedRequest>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Equal("test-cursor", deserialized.Cursor);
            Assert.Equal(50, deserialized.Limit);
        }

        [Fact]
        public void PaginatedRequest_ShouldSerializeNullValues()
        {
            // Arrange
            var request = new TestPaginatedRequest
            {
                Cursor = null,
                Limit = null
            };

            // Act
            var json = JsonSerializer.Serialize(request);
            var deserialized = JsonSerializer.Deserialize<TestPaginatedRequest>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Null(deserialized.Cursor);
            Assert.Null(deserialized.Limit);
        }

        // Test concrete class for testing the abstract base class
        private class TestPaginatedRequest : PaginatedRequest
        {
        }
    }
}