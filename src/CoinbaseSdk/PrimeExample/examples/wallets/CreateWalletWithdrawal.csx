#!/usr/bin/env dotnet-script
#r "../../../Prime/bin/Debug/net8.0/CoinbaseSdk.Prime.dll"
#r "nuget: CoinbaseSdk.Core, 0.0.1"
#load "../../PrettyPrinter.csx"
#load "../../DotEnvLoader.csx"
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

using CoinbaseSdk.Core.Credentials;
using CoinbaseSdk.Core.Serialization;
using CoinbaseSdk.Prime.Wallets;
using CoinbaseSdk.Prime.Client;

// Load environment variables from .env file
if (!DotEnvLoader.LoadEnvironmentVariables())
{
    Console.WriteLine("Error: No credentials found in environment variables or .env file.");
    DotEnvLoader.PrintSetupInstructions();
    return;
}

// Validate required environment variables
if (!DotEnvLoader.ValidateRequiredVariables("COINBASE_PRIME_CREDENTIALS", "COINBASE_PRIME_PORTFOLIO_ID"))
{
    DotEnvLoader.PrintSetupInstructions();
    return;
}

// Parse credentials (now guaranteed to be available)
string credentialsBlob = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS")!;
string portfolioId = Environment.GetEnvironmentVariable("COINBASE_PRIME_PORTFOLIO_ID")!;

var serializer = new JsonUtility();
var credentials = serializer.Deserialize<CoinbaseCredentials>(credentialsBlob);

if (credentials == null)
{
    Console.WriteLine("Failed to parse COINBASE_PRIME_CREDENTIALS environment variable");
    return;
}

var client = new CoinbasePrimeClient(credentials);
var walletsService = client.WalletsService;

string walletId = "sample-wallet-id"; // Replace with actual wallet ID
string destinationAddress = "sample-destination-address"; // Replace with actual destination
string amount = "0.001"; // Replace with actual amount

if (Args.Count >= 3)
{
    walletId = Args[0];
    destinationAddress = Args[1];
    amount = Args[2];
}
else
{
    PrettyPrinter.PrintUsage(
        "dotnet script CreateWalletWithdrawal.csx <wallet-id> <destination-address> <amount>", 
        "dotnet script CreateWalletWithdrawal.csx sample-wallet-id bc1qxy2kgdygjrsqtzq2n0yrf2493p83kkfjhx0wlh 0.001");
    return;
}

var request = new CreateWalletWithdrawalRequest.CreateWalletWithdrawalRequestBuilder()
    .WithPortfolioId(portfolioId)
    .WithWalletId(walletId)
    .WithAmount(amount)
    .WithDestinationAddress(destinationAddress)
    .Build();

try
{
    var response = walletsService.CreateWalletWithdrawal(request);
    PrettyPrinter.PrintResponse("CreateWalletWithdrawalResponse", response);
}
catch (Exception ex)
{
    PrettyPrinter.PrintError("Error creating wallet withdrawal", ex);
    Console.WriteLine("Note: Use GetWallets to find valid wallet IDs and ensure the destination address is valid");
}