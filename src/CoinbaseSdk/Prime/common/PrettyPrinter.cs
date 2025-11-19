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

namespace CoinbaseSdk.Prime.Common
{
  using System.Linq;
  using System.Text.Json;
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Serialization;

  /// <summary>
  /// Utility class for pretty printing objects to the console with JSON formatting.
  /// </summary>
  public static class PrettyPrinter
  {
    private static readonly JsonSerializerOptions PrettySettings = CreatePrettySettings();

    /// <summary>
    /// Pretty prints an object to the console with a title header.
    /// </summary>
    /// <param name="title">Title to display above the JSON output.</param>
    /// <param name="obj">Object to serialize and print.</param>
    public static void PrintResponse(string title, object? obj)
    {
      Console.WriteLine();
      Console.WriteLine($"=== {title} ===");

      if (obj == null)
      {
        Console.WriteLine("null");
        return;
      }

      try
      {
        string json = JsonSerializer.Serialize(obj, PrettySettings);
        Console.WriteLine(json);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error serializing object: {ex.Message}");
        Console.WriteLine(ex);
        Console.WriteLine(obj.ToString());
      }

      Console.WriteLine();
    }

    /// <summary>
    /// Pretty prints an object to the console without a title.
    /// </summary>
    /// <param name="obj">Object to serialize and print.</param>
    public static void Print(object? obj)
    {
      if (obj == null)
      {
        Console.WriteLine("null");
        return;
      }

      try
      {
        string json = JsonSerializer.Serialize(obj, PrettySettings);
        Console.WriteLine(json);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error serializing object: {ex.Message}");
        Console.WriteLine(ex);
        Console.WriteLine(obj.ToString());
      }
    }

    /// <summary>
    /// Prints an error message with consistent formatting.
    /// </summary>
    /// <param name="message">Error message to display.</param>
    /// <param name="ex">Optional exception to include details from.</param>
    public static void PrintError(string message, Exception? ex = null)
    {
      Console.WriteLine();
      Console.WriteLine("=== ERROR ===");
      Console.WriteLine(message);

      if (ex != null)
      {
        Console.WriteLine($"Details: {ex.Message}");
        if (ex.InnerException != null)
        {
          Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
        }
      }

      Console.WriteLine();
    }

    /// <summary>
    /// Prints usage information with consistent formatting.
    /// </summary>
    /// <param name="usage">Usage string to display.</param>
    /// <param name="example">Optional example to show.</param>
    public static void PrintUsage(string usage, string? example = null)
    {
      Console.WriteLine();
      Console.WriteLine("=== USAGE ===");
      Console.WriteLine(usage);

      if (!string.IsNullOrEmpty(example))
      {
        Console.WriteLine($"Example: {example}");
      }

      Console.WriteLine();
    }

    private static JsonSerializerOptions CreatePrettySettings()
    {
      JsonSerializerOptions options = PrimeJsonSerializerOptionsFactory.Clone();
      options.WriteIndented = true;
      options.DefaultIgnoreCondition = JsonIgnoreCondition.Never;

      // Ensure enums render as strings even if downstream overrides remove the converter.
      bool hasStringEnumConverter = options.Converters.Any(converter => converter is JsonStringEnumConverter);
      if (!hasStringEnumConverter)
      {
        options.Converters.Add(new JsonStringEnumConverter());
      }

      return options;
    }
  }
}
