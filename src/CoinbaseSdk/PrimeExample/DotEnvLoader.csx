#!/usr/bin/env dotnet-script
#r "nuget: Newtonsoft.Json, 13.0.3"
#nullable enable

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

using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Lightweight .env file loader that supports both individual credential fields
/// and backward compatibility with JSON credentials format
/// </summary>
public static class DotEnvLoader
{
    /// <summary>
    /// Loads environment variables from .env file(s) and ensures credentials are properly configured
    /// </summary>
    /// <param name="basePath">Base directory to search for .env files (defaults to current directory)</param>
    /// <returns>True if credentials are available after loading, false otherwise</returns>
    public static bool LoadEnvironmentVariables(string? basePath = null)
    {
        if (string.IsNullOrEmpty(basePath))
        {
            basePath = Directory.GetCurrentDirectory();
        }

        // Load .env file first, then .env.local to allow for overrides
        LoadEnvFile(Path.Combine(basePath, ".env"));
        LoadEnvFile(Path.Combine(basePath, ".env.local"));

        // If individual credential fields are set, construct the JSON format for backward compatibility
        return EnsureCredentialsFormat();
    }

    /// <summary>
    /// Validates that all required environment variables are set
    /// </summary>
    /// <param name="requiredVars">List of required environment variable names</param>
    /// <returns>True if all variables are set, false otherwise</returns>
    public static bool ValidateRequiredVariables(params string[] requiredVars)
    {
        var missing = new List<string>();
        
        foreach (var varName in requiredVars)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(varName)))
            {
                missing.Add(varName);
            }
        }

        if (missing.Count > 0)
        {
            Console.WriteLine("Error: The following required environment variables are not set:");
            foreach (var varName in missing)
            {
                Console.WriteLine($"  - {varName}");
            }
            Console.WriteLine();
            Console.WriteLine("Please check your .env file and ensure all required variables are configured.");
            Console.WriteLine("See .env.example for the expected format.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Prints helpful setup information when environment is not properly configured
    /// </summary>
    public static void PrintSetupInstructions()
    {
        Console.WriteLine();
        Console.WriteLine("=== ENVIRONMENT SETUP ===");
        Console.WriteLine("To run this example, you need to configure your environment variables.");
        Console.WriteLine();
        Console.WriteLine("Option 1: Create a .env file (recommended for development)");
        Console.WriteLine("  1. Copy .env.example to .env");
        Console.WriteLine("  2. Fill in your actual credential values");
        Console.WriteLine();
        Console.WriteLine("Option 2: Set environment variables directly");
        Console.WriteLine("  export COINBASE_PRIME_ACCESS_KEY=\"your_key\"");
        Console.WriteLine("  export COINBASE_PRIME_PASSPHRASE=\"your_passphrase\"");
        Console.WriteLine("  export COINBASE_PRIME_SIGNING_KEY=\"your_signing_key\"");
        Console.WriteLine("  export COINBASE_PRIME_PORTFOLIO_ID=\"your_portfolio_id\"");
        Console.WriteLine();
        Console.WriteLine("For more information, see the README.md file.");
        Console.WriteLine();
    }

    /// <summary>
    /// Loads a single .env file if it exists
    /// </summary>
    /// <param name="filePath">Path to the .env file to load</param>
    public static void LoadEnvFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        try
        {
            foreach (var line in File.ReadAllLines(filePath))
            {
                ProcessEnvLine(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Failed to load {filePath}: {ex.Message}");
        }
    }

    /// <summary>
    /// Processes a single line from an .env file
    /// </summary>
    private static void ProcessEnvLine(string line)
    {
        // Remove leading/trailing whitespace
        line = line.Trim();

        // Skip empty lines and comments
        if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
        {
            return;
        }

        // Find the first = sign to split key and value
        int equalIndex = line.IndexOf('=');
        if (equalIndex <= 0)
        {
            return;
        }

        string key = line.Substring(0, equalIndex).Trim();
        string value = line.Substring(equalIndex + 1).Trim();

        // Remove surrounding quotes if present
        if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
            (value.StartsWith("'") && value.EndsWith("'")))
        {
            value = value.Substring(1, value.Length - 2);
        }

        // Only set if not already set (allows environment variables to override .env file)
        if (Environment.GetEnvironmentVariable(key) == null)
        {
            Environment.SetEnvironmentVariable(key, value);
        }
    }

    /// <summary>
    /// Ensures credentials are available in the expected JSON format for backward compatibility
    /// </summary>
    /// <returns>True if credentials are available in JSON format, false otherwise</returns>
    public static bool EnsureCredentialsFormat()
    {
        // Check if JSON credentials already exist
        var existingCredentials = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS");
        if (!string.IsNullOrEmpty(existingCredentials))
        {
            return true; // Already have JSON credentials
        }

        // Try to build credentials from individual fields
        var accessKey = Environment.GetEnvironmentVariable("COINBASE_PRIME_ACCESS_KEY");
        var passphrase = Environment.GetEnvironmentVariable("COINBASE_PRIME_PASSPHRASE");
        var signingKey = Environment.GetEnvironmentVariable("COINBASE_PRIME_SIGNING_KEY");

        // If we have individual fields, construct the JSON format
        if (!string.IsNullOrEmpty(accessKey) && 
            !string.IsNullOrEmpty(passphrase) && 
            !string.IsNullOrEmpty(signingKey))
        {
            var credentialsObject = new
            {
                accessKey = accessKey,
                passphrase = passphrase,
                signingKey = signingKey
            };

            var credentialsJson = JsonConvert.SerializeObject(credentialsObject);
            Environment.SetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS", credentialsJson);
            return true;
        }

        // No credentials found in either format
        return false;
    }
}