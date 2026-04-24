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
  using CoinbaseSdk.Prime.Client;
  using Xunit;

  [Trait("Category", "Integration")]
  public abstract class IntegrationTestBase
  {
    protected IntegrationTestBase(PrimeIntegrationFixture fixture)
    {
      this.Fixture = fixture;
    }

    protected PrimeIntegrationFixture Fixture { get; }

    protected CoinbasePrimeClient Client => this.Fixture.Client;

    protected void SkipIfNoCredentials()
    {
      Skip.If(!this.Fixture.HasCredentials, "Set PRIME_ACCESS_KEY, PRIME_PASSPHRASE, and PRIME_SIGNING_KEY to run integration tests.");
    }

    protected void RethrowIfBootstrapFailed()
    {
      this.SkipIfNoCredentials();
      if (this.Fixture.BootstrapException != null)
      {
        throw this.Fixture.BootstrapException;
      }
    }

    protected void SkipIfNullOrEmpty(string? value, string message)
    {
      Skip.If(string.IsNullOrEmpty(value), message);
    }

    protected void SkipBecause(string message)
    {
      Skip.If(true, message);
    }
  }
}
