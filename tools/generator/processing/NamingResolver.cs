/*
 * Copyright 2026-present Coinbase Global, Inc.
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

namespace CoinbaseSdk.Tools.Generator.Processing;

public static class NamingResolver
{
  public static ServiceDefinition RequireService(GeneratorConfiguration cfg, string serviceKey)
  {
    if (!cfg.Services.TryGetValue(serviceKey, out var def))
    {
      throw new InvalidOperationException($"Unknown service key '{serviceKey}' in operation bindings.");
    }

    return def;
  }

  public static string FullNamespace(ServiceDefinition s)
  {
    return $"CoinbaseSdk.Prime.{s.Namespace}";
  }
}
