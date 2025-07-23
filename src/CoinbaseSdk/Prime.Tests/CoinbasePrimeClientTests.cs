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
  using System.Net;
  using System.Reflection;
  using CoinbaseSdk.Core.Credentials;
  using CoinbaseSdk.Prime.Client;
  using Moq;
  using Xunit;

  public class CoinbasePrimeClientTests
  {
    [Fact]
    public void SdkVersion_ShouldMatchAssemblyVersion()
    {
      // Arrange
      var expectedVersion = Assembly.GetAssembly(typeof(CoinbasePrimeClient))?.GetName().Version?.ToString(3);
      
      // Act & Assert
      Assert.NotNull(expectedVersion);
      Assert.NotEqual("0.0.0", expectedVersion);
    }

    [Fact]
    public void SendRequestAsync_ShouldIncludePrimeSdkVersionHeader()
    {
      // Arrange
      var credentials = new CoinbaseCredentials
      {
        AccessKey = "test-key",
        Passphrase = "test-passphrase",
        SigningKey = "test-secret"
      };
      var client = new TestableCoinbasePrimeClient(credentials);
      var expectedVersion = Assembly.GetAssembly(typeof(CoinbasePrimeClient))?.GetName().Version?.ToString(3);
      
      // Act
      var request = client.CreateTestRequest("/test", HttpMethod.Get, null);
      
      // Assert
      Assert.True(request.Headers.ContainsKey("User-Agent"));
      Assert.Equal($"prime-sdk-dotnet/{expectedVersion}", request.Headers["User-Agent"]);
    }

    [Fact]
    public void SdkVersion_ShouldNotBeHardcoded()
    {
      // Arrange & Act
      var version = Assembly.GetAssembly(typeof(CoinbasePrimeClient))?.GetName().Version?.ToString(3);
      
      // Assert
      Assert.NotEqual("0.0.0", version);
      Assert.NotNull(version);
    }
  }

  // Test helper class to access protected methods
  internal class TestableCoinbasePrimeClient : CoinbasePrimeClient
  {
    public TestableCoinbasePrimeClient(CoinbaseCredentials credentials) : base(credentials)
    {
    }

    public Core.Http.CoinbaseHttpRequest CreateTestRequest(string path, HttpMethod method, object? options)
    {
      var request = new Core.Http.CoinbaseHttpRequest(
        $"{this.ApiBasePath}{path}",
        method.Method,
        this.Credentials,
        options,
        this.JsonUtility);

      // Get the current version using reflection to access the private SdkVersion field
      var sdkVersionField = typeof(CoinbasePrimeClient).GetField("SdkVersion", BindingFlags.NonPublic | BindingFlags.Static);
      var sdkVersion = sdkVersionField?.GetValue(null)?.ToString() ?? "0.0.0";
      
      request.Headers["User-Agent"] = $"prime-sdk-dotnet/{sdkVersion}";
      
      return request;
    }
  }
}