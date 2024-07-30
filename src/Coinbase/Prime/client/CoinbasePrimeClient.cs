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

namespace Coinbase.Prime.Client
{
  using Coinbase.Core.Client;
  using Coinbase.Core.Credentials;

  public class CoinbasePrimeClient : CoinbaseClient
  {
    private const string DefaultApiBasePath = "api.prime.coinbase.com/v1";

    public CoinbasePrimeClient(CoinbaseCredentials credentials) : base(credentials, DefaultApiBasePath)
    {
    }

    public CoinbasePrimeClient(CoinbaseCredentials credentials, string apiBasePath) : base(credentials, apiBasePath)
    {
    }
  }
}