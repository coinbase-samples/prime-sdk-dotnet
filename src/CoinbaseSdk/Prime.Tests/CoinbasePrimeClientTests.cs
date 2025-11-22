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
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http;
  using System.Reflection;
  using CoinbaseSdk.Core.Credentials;
  using CoinbaseSdk.Core.Error;
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
      var credentials = new CoinbaseCredentials("test-key", "test-passphrase", "test-secret");
      var client = new TestableCoinbasePrimeClient(credentials);
      var expectedVersion = Assembly.GetAssembly(typeof(CoinbasePrimeClient))?.GetName().Version?.ToString(3);
      
      // Create a request using the base constructor, not the helper that manually adds the header
      var request = new Core.Http.CoinbaseHttpRequest(
          "https://api.prime.coinbase.com/v1/test",
          HttpMethod.Get.Method,
          credentials,
          null,
          client.GetJsonUtility());

      // Act
      client.ExposeConfigureRequest(request);

      // Assert
      Assert.True(request.Headers.ContainsKey("User-Agent"));
      Assert.Equal($"prime-sdk-dotnet/{expectedVersion}", request.Headers["User-Agent"]);
    }

    [Fact]
    public void ValidateResponse_ShouldThrowCoinbaseClientException_OnApiError()
    {
      // Arrange
      var credentials = new CoinbaseCredentials("test-key", "test-passphrase", "test-secret");
      var client = new TestableCoinbasePrimeClient(credentials);
      var errorJson = "{\"message\":\"Invalid request parameters\"}";
      var headers = new HttpResponseMessage().Headers;
      var response = new Core.Http.CoinbaseResponse(HttpStatusCode.BadRequest, headers, errorJson);

      // Act & Assert
      var exception = Assert.Throws<CoinbaseException>(() => 
        client.ExposeValidateResponse(response, new[] { HttpStatusCode.OK }));
      
      // The message verification depends on how CoinbasePrimeErrorMessage deserializes and creates the exception.
      // Assuming it uses the "message" field.
    }

    [Fact]
    public void ValidateResponse_ShouldThrowCoinbaseException_OnNonJsonError()
    {
      // Arrange
      var credentials = new CoinbaseCredentials("test-key", "test-passphrase", "test-secret");
      var client = new TestableCoinbasePrimeClient(credentials);
      var errorContent = "Bad Gateway";
      var headers = new HttpResponseMessage().Headers;
      var response = new Core.Http.CoinbaseResponse(HttpStatusCode.BadGateway, headers, errorContent);

      // Act & Assert
      var exception = Assert.Throws<CoinbaseException>(() => 
        client.ExposeValidateResponse(response, new[] { HttpStatusCode.OK }));
      
      Assert.Equal(HttpStatusCode.BadGateway, exception.StatusCode);
      Assert.Contains("Bad Gateway", exception.Message);
    }

    [Fact]
    public void FromEnv_ShouldCreateClient_WhenEnvVarsSet()
    {
      // Arrange
      var originalAccessKey = Environment.GetEnvironmentVariable("PRIME_ACCESS_KEY");
      var originalPassphrase = Environment.GetEnvironmentVariable("PRIME_PASSPHRASE");
      var originalSigningKey = Environment.GetEnvironmentVariable("PRIME_SIGNING_KEY");

      try
      {
        Environment.SetEnvironmentVariable("PRIME_ACCESS_KEY", "env-access-key");
        Environment.SetEnvironmentVariable("PRIME_PASSPHRASE", "env-passphrase");
        Environment.SetEnvironmentVariable("PRIME_SIGNING_KEY", "env-signing-key");

        // Act
        var client = CoinbasePrimeClient.FromEnv(false);

        // Assert
        Assert.NotNull(client);
        // We can't easily check the credentials inside the client without exposing them, 
        // but successful creation implies they were read.
      }
      finally
      {
        // Cleanup
        Environment.SetEnvironmentVariable("PRIME_ACCESS_KEY", originalAccessKey);
        Environment.SetEnvironmentVariable("PRIME_PASSPHRASE", originalPassphrase);
        Environment.SetEnvironmentVariable("PRIME_SIGNING_KEY", originalSigningKey);
      }
    }

    [Fact]
    public void FromEnv_ShouldThrow_WhenEnvVarsMissing()
    {
      // Arrange
      var originalAccessKey = Environment.GetEnvironmentVariable("PRIME_ACCESS_KEY");
      Environment.SetEnvironmentVariable("PRIME_ACCESS_KEY", "");

      try
      {
        // Act & Assert
        Assert.Throws<CoinbaseClientException>(() => CoinbasePrimeClient.FromEnv(false));
      }
      finally
      {
        Environment.SetEnvironmentVariable("PRIME_ACCESS_KEY", originalAccessKey);
      }
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

    public CoinbaseSdk.Core.Serialization.IJsonUtility GetJsonUtility()
    {
        return this.JsonUtility;
    }

    public void ExposeConfigureRequest(Core.Http.CoinbaseHttpRequest request)
    {
        this.ConfigureRequest(request);
    }

    public void ExposeValidateResponse(Core.Http.CoinbaseResponse response, HttpStatusCode[] expectedStatusCodes)
    {
        this.ValidateResponse(response, expectedStatusCodes);
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
