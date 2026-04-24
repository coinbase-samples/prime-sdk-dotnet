/*
 * Copyright 2026-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.IntegrationTests.Infrastructure
{
  using System.Net;
  using CoinbaseSdk.Core.Error;

  public static class IntegrationRetry
  {
    public static async Task<T> ExecuteWithRetryAsync<T>(
      Func<Task<T>> action,
      int maxAttempts = 3,
      CancellationToken cancellationToken = default)
    {
      for (int attempt = 0; attempt < maxAttempts; attempt++)
      {
        try
        {
          return await action().ConfigureAwait(false);
        }
        catch (CoinbaseException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests && attempt < maxAttempts - 1)
        {
          var delay = TimeSpan.FromSeconds(Math.Pow(2, attempt));
          await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
        }
      }

      throw new InvalidOperationException("IntegrationRetry: should not reach");
    }
  }
}
