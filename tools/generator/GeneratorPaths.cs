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

namespace CoinbaseSdk.Tools.Generator;

public static class GeneratorPaths
{
  public static string FindProjectRoot()
  {
    var current = new DirectoryInfo(Directory.GetCurrentDirectory());
    while (current != null)
    {
      if (Directory.GetFiles(current.FullName, "*.sln").Length > 0)
      {
        return current.FullName;
      }

      current = current.Parent;
    }

    var cwd = Directory.GetCurrentDirectory();
    if (cwd.Contains("tools", StringComparison.Ordinal) && cwd.Contains("generator", StringComparison.Ordinal))
    {
      return Path.GetFullPath(Path.Combine(cwd, "..", ".."));
    }

    throw new InvalidOperationException("Could not find project root (looking for .sln file).");
  }

  public static string ToolDirectory(string projectRoot)
  {
    return Path.Combine(projectRoot, "tools", "generator");
  }

  public static string ConfigDirectory(string projectRoot)
  {
    return Path.Combine(ToolDirectory(projectRoot), "config");
  }

  public static string TemplatesDirectory(string projectRoot)
  {
    return Path.Combine(ToolDirectory(projectRoot), "templates");
  }

  public static string DefaultCommittedSpecPath(string projectRoot)
  {
    return Path.Combine(projectRoot, "apiSpec", "prime-public-api-spec.yaml");
  }

  public static string CachedSpecPath(string projectRoot)
  {
    return Path.Combine(projectRoot, "generated", "openapi.yaml");
  }
}
