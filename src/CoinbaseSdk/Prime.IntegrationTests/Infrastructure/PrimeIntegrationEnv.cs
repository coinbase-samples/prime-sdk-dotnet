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
  internal static class PrimeIntegrationEnv
  {
    static PrimeIntegrationEnv()
    {
      var dir = AppContext.BaseDirectory;
      while (!string.IsNullOrEmpty(dir))
      {
        var candidate = Path.Combine(dir, ".env");
        if (File.Exists(candidate))
        {
          DotNetEnv.Env.Load(candidate, new DotNetEnv.LoadOptions(clobberExistingVars: false));
          break;
        }

        dir = Path.GetDirectoryName(dir);
      }
    }

    public static string? Get(string name) => Environment.GetEnvironmentVariable(name);

    public static bool IsTruthy(string? value) =>
      !string.IsNullOrWhiteSpace(value) &&
      (string.Equals(value, "1", StringComparison.OrdinalIgnoreCase) ||
       string.Equals(value, "true", StringComparison.OrdinalIgnoreCase) ||
       string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase));

    public static string DescribeSource(string? envOverride, string state) => envOverride != null ? "env" : state;
  }
}
