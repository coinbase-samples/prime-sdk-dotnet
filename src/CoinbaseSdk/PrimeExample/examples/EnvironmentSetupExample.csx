#!/usr/bin/env dotnet-script
#r "../../Prime/bin/Debug/net8.0/CoinbaseSdk.Prime.dll"
#r "nuget: CoinbaseSdk.Core, 0.0.1"
#load "../PrettyPrinter.csx"
#load "../DotEnvLoader.csx"
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
using CoinbaseSdk.Core.Credentials;
using CoinbaseSdk.Core.Serialization;
using CoinbaseSdk.Prime.Client;

Console.WriteLine("=== Coinbase Prime SDK Environment Setup Example ===");
Console.WriteLine("This example demonstrates how to configure environment variables for the SDK.");
Console.WriteLine();

// Load environment variables from .env file
Console.WriteLine("Step 1: Loading environment variables...");
bool credentialsLoaded = DotEnvLoader.LoadEnvironmentVariables();

if (!credentialsLoaded)
{
    Console.WriteLine("❌ No credentials found in environment variables or .env file.");
    DotEnvLoader.PrintSetupInstructions();
    return;
}

Console.WriteLine("✅ Environment variables loaded successfully.");
Console.WriteLine();

// Check what credential format we're using
var existingJson = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS");
var individualAccessKey = Environment.GetEnvironmentVariable("COINBASE_PRIME_ACCESS_KEY");

if (!string.IsNullOrEmpty(individualAccessKey))
{
    Console.WriteLine("📋 Using individual credential fields format:");
    Console.WriteLine($"   ACCESS_KEY: {MaskCredential(individualAccessKey)}");
    Console.WriteLine($"   PASSPHRASE: {MaskCredential(Environment.GetEnvironmentVariable("COINBASE_PRIME_PASSPHRASE"))}");
    Console.WriteLine($"   SIGNING_KEY: {MaskCredential(Environment.GetEnvironmentVariable("COINBASE_PRIME_SIGNING_KEY"))}");
    Console.WriteLine("   ✅ Automatically converted to JSON format for SDK compatibility");
}
else if (!string.IsNullOrEmpty(existingJson))
{
    Console.WriteLine("📋 Using JSON credentials format:");
    Console.WriteLine($"   CREDENTIALS: {MaskJsonCredentials(existingJson)}");
}

Console.WriteLine();

// Validate all required variables
Console.WriteLine("Step 2: Validating required environment variables...");

var requiredVars = new[]
{
    "COINBASE_PRIME_CREDENTIALS",
    "COINBASE_PRIME_PORTFOLIO_ID",
    "COINBASE_PRIME_ENTITY_ID"
};

foreach (var varName in requiredVars)
{
    var value = Environment.GetEnvironmentVariable(varName);
    if (!string.IsNullOrEmpty(value))
    {
        string displayValue = varName == "COINBASE_PRIME_CREDENTIALS" 
            ? MaskJsonCredentials(value) 
            : MaskCredential(value);
        Console.WriteLine($"   ✅ {varName}: {displayValue}");
    }
    else
    {
        Console.WriteLine($"   ❌ {varName}: Not set");
    }
}

Console.WriteLine();

// Test SDK initialization
Console.WriteLine("Step 3: Testing SDK initialization...");

try
{
    var credentialsBlob = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS");
    if (string.IsNullOrEmpty(credentialsBlob))
    {
        throw new InvalidOperationException("COINBASE_PRIME_CREDENTIALS not found after loading");
    }

    var serializer = new JsonUtility();
    var credentials = serializer.Deserialize<CoinbaseCredentials>(credentialsBlob);

    if (credentials == null)
    {
        throw new InvalidOperationException("Failed to parse credentials JSON");
    }

    var client = new CoinbasePrimeClient(credentials);
    Console.WriteLine("   ✅ SDK client initialized successfully");
    
    // Display some client info without making actual API calls
    Console.WriteLine("   📡 API Base URL: https://api.prime.coinbase.com/v1 (default)");
    Console.WriteLine("   🔐 Authentication configured");
}
catch (Exception ex)
{
    Console.WriteLine($"   ❌ SDK initialization failed: {ex.Message}");
    return;
}

Console.WriteLine();

// Optional configuration check
Console.WriteLine("Step 4: Checking optional configuration...");

var optionalVars = new[]
{
    ("COINBASE_PRIME_API_URL", "API base URL"),
    ("COINBASE_PRIME_TIMEOUT_SECONDS", "Request timeout")
};

foreach (var (varName, description) in optionalVars)
{
    var value = Environment.GetEnvironmentVariable(varName);
    if (!string.IsNullOrEmpty(value))
    {
        Console.WriteLine($"   ✅ {description}: {value}");
    }
    else
    {
        Console.WriteLine($"   ⚪ {description}: Using default");
    }
}

Console.WriteLine();
Console.WriteLine("🎉 Environment setup validation complete!");
Console.WriteLine("You're ready to run other example scripts.");

Console.WriteLine();
Console.WriteLine("=== Next Steps ===");
Console.WriteLine("Try running these examples:");
Console.WriteLine("  dotnet script examples/activities/GetActivity.csx <activity-id>");
Console.WriteLine("  dotnet script examples/futures/GetPositions.csx");
Console.WriteLine("  dotnet script examples/portfolios/GetPortfolios.csx");

// Helper methods
static string MaskCredential(string? value)
{
    if (string.IsNullOrEmpty(value))
        return "Not set";
    
    if (value.Length <= 8)
        return new string('*', value.Length);
    
    return $"{value.Substring(0, 4)}...{value.Substring(value.Length - 4)}";
}

static string MaskJsonCredentials(string json)
{
    if (string.IsNullOrEmpty(json))
        return "Not set";
    
    // Simple regex-like replacement for credential values in JSON
    var masked = json;
    
    // This is a simple approach - in production you might use proper JSON parsing
    if (json.Contains("accessKey"))
    {
        masked = System.Text.RegularExpressions.Regex.Replace(masked, 
            @"""accessKey""\s*:\s*""([^""]+)""", 
            m => $"\"accessKey\":\"***{m.Groups[1].Value.Substring(Math.Max(0, m.Groups[1].Value.Length - 4))}\"");
    }
    
    if (json.Contains("passphrase"))
    {
        masked = System.Text.RegularExpressions.Regex.Replace(masked, 
            @"""passphrase""\s*:\s*""([^""]+)""", 
            "\"passphrase\":\"****\"");
    }
    
    if (json.Contains("signingKey"))
    {
        masked = System.Text.RegularExpressions.Regex.Replace(masked, 
            @"""signingKey""\s*:\s*""([^""]+)""", 
            "\"signingKey\":\"****\"");
    }
    
    return masked;
}