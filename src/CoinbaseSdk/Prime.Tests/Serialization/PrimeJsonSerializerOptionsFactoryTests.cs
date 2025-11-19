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

namespace CoinbaseSdk.Prime.Tests.Serialization
{
  using System.Linq;
  using System.Text.Json;
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Serialization;
  using CoinbaseSdk.Prime.Serialization;
  using Xunit;

  public class PrimeJsonSerializerOptionsFactoryTests
  {
    [Fact]
    public void Default_ShouldConfigurePrimePolicies()
    {
      JsonSerializerOptions options = PrimeJsonSerializerOptionsFactory.Default;

      Assert.True(options.PropertyNameCaseInsensitive);
      Assert.Equal(JsonNamingPolicy.CamelCase, options.PropertyNamingPolicy);
      Assert.Contains(options.Converters, converter => converter is JsonStringEnumConverter);
      Assert.Contains(options.Converters, converter => converter is NullOnUnknownEnumConverter);
      Assert.Contains(options.Converters, converter => converter is UtcIso8601DateTimeOffsetConverter);
      Assert.NotNull(options.TypeInfoResolver);
    }

    [Fact]
    public void Clone_ShouldReturnIndependentInstance()
    {
      JsonSerializerOptions clone = PrimeJsonSerializerOptionsFactory.Clone();
      clone.PropertyNameCaseInsensitive = false;

      Assert.NotSame(PrimeJsonSerializerOptionsFactory.Default, clone);
      Assert.True(PrimeJsonSerializerOptionsFactory.Default.PropertyNameCaseInsensitive);
      Assert.False(clone.PropertyNameCaseInsensitive);
    }
  }
}

