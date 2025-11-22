/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Client
{
  using System.Net;
  using System.Reflection;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Credentials;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Core.Http;

  public class CoinbasePrimeClient : CoinbaseClient
  {
    private const string DefaultApiBasePath = "api.prime.coinbase.com/v1";
    private static readonly string SdkVersion =
      Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "0.0.0";

    public CoinbasePrimeClient(CoinbaseCredentials credentials)
      : base(credentials, DefaultApiBasePath) { }

    public CoinbasePrimeClient(CoinbaseCredentials credentials, string apiBasePath)
      : base(credentials, apiBasePath) { }

    public static CoinbasePrimeClient FromEnv(bool loadEnvFile = true)
    {
      if (loadEnvFile)
      {
        DotNetEnv.Env.TraversePath().Load();
      }

      var accessKey = Environment.GetEnvironmentVariable("PRIME_ACCESS_KEY");
      if (string.IsNullOrWhiteSpace(accessKey))
      {
        throw new CoinbaseClientException("PRIME_ACCESS_KEY is required");
      }

      var passphrase = Environment.GetEnvironmentVariable("PRIME_PASSPHRASE");
      if (string.IsNullOrWhiteSpace(passphrase))
      {
        throw new CoinbaseClientException("PRIME_PASSPHRASE is required");
      }

      var signingKey = Environment.GetEnvironmentVariable("PRIME_SIGNING_KEY");
      if (string.IsNullOrWhiteSpace(signingKey))
      {
        throw new CoinbaseClientException("PRIME_SIGNING_KEY is required");
      }

      var credentials = new CoinbaseCredentials(accessKey, passphrase, signingKey);

      return new CoinbasePrimeClient(credentials);
    }

    /// <summary>
    /// Configures the request by adding Prime SDK-specific headers.
    /// </summary>
    protected override void ConfigureRequest(CoinbaseHttpRequest request)
    {
      // Attach SDK version header to all requests
      request.Headers["User-Agent"] = $"prime-sdk-dotnet/{SdkVersion}";
    }

    /// <summary>
    /// Validates the response and handles Prime-specific error deserialization.
    /// </summary>
    protected override void ValidateResponse(CoinbaseResponse response, HttpStatusCode[] expectedStatusCodes)
    {
      if (!expectedStatusCodes.Contains(response.StatusCode))
      {
        CoinbasePrimeErrorMessage errorMessage;
        try
        {
          errorMessage = JsonUtility.Deserialize<CoinbasePrimeErrorMessage>(response.Content);
        }
        catch (Exception)
        {
          throw new CoinbaseException(response.StatusCode, response.Content);
        }
        throw errorMessage.CreateCoinbaseException();
      }
    }
  }
}
