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
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Credentials;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Core.Http;

  public class CoinbasePrimeClient : CoinbaseClient
  {
    private const string DefaultApiBasePath = "api.prime.coinbase.com/v1";

    public CoinbasePrimeClient(CoinbaseCredentials credentials) : base(credentials, DefaultApiBasePath)
    {
    }

    public CoinbasePrimeClient(CoinbaseCredentials credentials, string apiBasePath) : base(credentials, apiBasePath)
    {
    }

    public override async Task<T> SendRequestAsync<T>(
      HttpMethod method,
      string path,
      object options,
      HttpStatusCode[] expectedStatusCodes,
      CancellationToken cancellationToken,
      CallOptions? callOptions = null)
    {
      CoinbaseHttpRequest request = new CoinbaseHttpRequest(
        $"{this.ApiBasePath}{path}",
        method.Method,
        this.Credentials,
        options,
        this.JsonUtility);

      // Send the HTTP request
      CoinbaseResponse response;
      try
      {
        response = await this.HttpClient.SendAsyncRequest(request, callOptions, cancellationToken);
      }
      catch (Exception ex)
      {
        throw new CoinbaseClientException(ex.Message, ex);
      }

      // If the response is successful return the content as type T
      if (!expectedStatusCodes.Contains(response.StatusCode))
      {
        CoinbasePrimeErrorMessage errorMessage;
        try
        {
          errorMessage = this.JsonUtility.Deserialize<CoinbasePrimeErrorMessage>(response.Content);
        }
        catch (Exception)
        {
          throw new CoinbaseException(response.StatusCode, response.Content);
        }
        throw errorMessage.CreateCoinbaseException();
      }

      return this.JsonUtility.Deserialize<T>(response.Content);
    }

  }
}
