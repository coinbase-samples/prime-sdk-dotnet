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

using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace CoinbaseSdk.Tools.Generator.Processing;

/// <summary>
/// Copyright year handling: resolves years from Git first-commit dates where possible, and
/// normalizes generated SDK file content via <see cref="ApplyCopyrightYear"/>.
/// </summary>
public static class CopyrightHelper
{
  private static int _sdkEmittedCopyrightYear;

  /// <summary>
  /// Calendar year used in newly emitted C# file headers (requests, responses, services, examples).
  /// Set by <see cref="InitializeSdkEmittedCopyrightYear"/> from Git; falls back to UTC year if unset.
  /// </summary>
  public static int SdkEmittedCopyrightYear =>
    _sdkEmittedCopyrightYear > 0 ? _sdkEmittedCopyrightYear : DateTime.UtcNow.Year;

  /// <summary>
  /// Resolves the year from the first commit that added <c>tools/generator/phases/RequestPhase.cs</c>,
  /// used as the copyright year for all holistically generated C# surface files.
  /// </summary>
  public static void InitializeSdkEmittedCopyrightYear(string projectRoot)
  {
    var anchor = Path.Combine(projectRoot, "tools", "generator", "phases", "RequestPhase.cs");
    _sdkEmittedCopyrightYear = GetFirstCommitYearForPath(projectRoot, anchor);
  }

  /// <summary>
  /// Returns the commit year (UTC) of the first commit that added <paramref name="absolutePath"/>,
  /// or <see cref="DateTime.UtcNow"/> year if Git is unavailable or the file is not yet tracked.
  /// </summary>
  public static int GetFirstCommitYearForPath(string gitRepositoryRoot, string absolutePath)
  {
    try
    {
      var full = Path.GetFullPath(absolutePath);
      var rel = Path.GetRelativePath(gitRepositoryRoot, full);
      if (rel.StartsWith("..", StringComparison.Ordinal))
      {
        return DateTime.UtcNow.Year;
      }

      var relPosix = rel.Replace('\\', '/');
      var psi = new ProcessStartInfo
      {
        FileName = "git",
        WorkingDirectory = gitRepositoryRoot,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true,
        Arguments = $"log --diff-filter=A --format=%cs --reverse -- \"{relPosix}\"",
      };

      using var proc = Process.Start(psi);
      if (proc == null)
      {
        return DateTime.UtcNow.Year;
      }

      var output = proc.StandardOutput.ReadToEnd();
      proc.WaitForExit(30_000);
      var line = output.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .FirstOrDefault();
      if (string.IsNullOrEmpty(line) || line.Length < 4 || !int.TryParse(line.AsSpan(0, 4), out var y))
      {
        return DateTime.UtcNow.Year;
      }

      return y;
    }
    catch
    {
      return DateTime.UtcNow.Year;
    }
  }

  /// <summary>
  /// Appends the standard Apache 2.0 comment block for generated C# under <c>src/CoinbaseSdk/Prime</c>.
  /// </summary>
  public static void AppendEmittedCsFileLicense(StringBuilder sb, int copyrightYear)
  {
    sb.AppendLine("/*");
    sb.AppendLine($" * Copyright {copyrightYear}-present Coinbase Global, Inc.");
    sb.AppendLine(" *");
    sb.AppendLine(" *  Licensed under the Apache License, Version 2.0 (the \"License\");");
    sb.AppendLine(" *  you may not use this file except in compliance with the License.");
    sb.AppendLine(" *  You may obtain a copy of the License at");
    sb.AppendLine(" *");
    sb.AppendLine(" *  http://www.apache.org/licenses/LICENSE-2.0");
    sb.AppendLine(" *");
    sb.AppendLine(" *  Unless required by applicable law or agreed to in writing, software");
    sb.AppendLine(" *  distributed under the License is distributed on an \"AS IS\" BASIS,");
    sb.AppendLine(" *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.");
    sb.AppendLine(" *  See the License for the specific language governing permissions and");
    sb.AppendLine(" *  limitations under the License.");
    sb.AppendLine(" */");
    sb.AppendLine();
  }

  /// <summary>
  /// Replaces the hardcoded copyright year in <paramref name="content"/> with:
  /// - The existing year from the on-disk file at <paramref name="outputPath"/> (if it exists), or
  /// - <see cref="SdkEmittedCopyrightYear"/> for brand-new generated files (Git-anchored year).
  /// </summary>
  public static string ApplyCopyrightYear(string outputPath, string content)
  {
    string targetYear;
    if (File.Exists(outputPath))
    {
      var onDisk = File.ReadAllText(outputPath);
      var yearMatch = Regex.Match(onDisk, @"Copyright (\d{4})-present");
      targetYear = yearMatch.Success ? yearMatch.Groups[1].Value : SdkEmittedCopyrightYear.ToString();
    }
    else
    {
      targetYear = SdkEmittedCopyrightYear.ToString();
    }

    return Regex.Replace(
      content,
      @"Copyright \d{4}-present",
      $"Copyright {targetYear}-present");
  }
}
