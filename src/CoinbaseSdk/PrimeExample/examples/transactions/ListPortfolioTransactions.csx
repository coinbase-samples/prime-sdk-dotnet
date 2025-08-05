#!/usr/bin/env dotnet-script
#r "../../../Prime/bin/Debug/net8.0/CoinbaseSdk.Prime.dll"
#r "nuget: CoinbaseSdk.Core, 0.0.1"
#load "../../PrettyPrinter.csx"

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

#nullable enable

using CoinbaseSdk.Core.Credentials;
using CoinbaseSdk.Core.Serialization;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Model;
using CoinbaseSdk.Prime.Transactions;

string? credentialsBlob = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS");
if (credentialsBlob == null)
{
    Console.WriteLine("COINBASE_PRIME_CREDENTIALS environment variable not set");
    return;
}

string? portfolioId = Environment.GetEnvironmentVariable("COINBASE_PRIME_PORTFOLIO_ID");
if (portfolioId == null)
{
    Console.WriteLine("COINBASE_PRIME_PORTFOLIO_ID environment variable not set");
    return;
}

var serializer = new JsonUtility();
var credentials = serializer.Deserialize<CoinbaseCredentials>(credentialsBlob);

if (credentials == null)
{
    Console.WriteLine("Failed to parse COINBASE_PRIME_CREDENTIALS environment variable");
    return;
}

var client = new CoinbasePrimeClient(credentials);
var transactionsService = new TransactionsService(client);

var requestBuilder = new ListPortfolioTransactionsRequest.ListPortfolioTransactionsRequestBuilder()
    .WithPortfolioId(portfolioId);

// Parse CLI arguments for transaction type and symbols
if (Args.Count > 0)
{
    string transactionTypeArg = Args[0].ToUpper();
    
    if (Enum.TryParse<TransactionType>(transactionTypeArg, out var transactionType))
    {
        requestBuilder.WithTypes([transactionType]);
        Console.WriteLine($"Filtering by transaction type: {transactionType}");
    }
    else
    {
        Console.WriteLine($"Invalid transaction type: {Args[0]}");
        Console.WriteLine("Valid transaction types:");
        foreach (var type in Enum.GetValues<TransactionType>())
        {
            Console.WriteLine($"  {type}");
        }
        return;
    }
}

// Parse symbols filter if provided as second argument
if (Args.Count > 1)
{
    string symbolsArg = Args[1];
    string[] symbols = symbolsArg.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    
    if (symbols.Length > 0)
    {
        requestBuilder.WithSymbols(symbols);
        Console.WriteLine($"Filtering by symbols: {string.Join(", ", symbols)}");
    }
}

if (Args.Count == 0)
{
    Console.WriteLine("Usage: dotnet script ListPortfolioTransactions.csx [transaction-type] [symbols]");
    Console.WriteLine("Example: dotnet script ListPortfolioTransactions.csx STAKE");
    Console.WriteLine("Example: dotnet script ListPortfolioTransactions.csx STAKE ETH,SOL");
    Console.WriteLine("Example: dotnet script ListPortfolioTransactions.csx WITHDRAWAL BTC");
    Console.WriteLine();
    Console.WriteLine("Available transaction types:");
    foreach (var type in Enum.GetValues<TransactionType>())
    {
        Console.WriteLine($"  {type}");
    }
    Console.WriteLine();
    Console.WriteLine("Symbols should be comma-separated (e.g., ETH,SOL,BTC)");
    Console.WriteLine();
    Console.WriteLine("Running without filter to show all transactions...");
}

var request = requestBuilder.Build();

try
{
    var response = transactionsService.ListPortfolioTransactions(request);
    PrettyPrinter.PrintResponse("ListPortfolioTransactionsResponse", response);
}
catch (Exception ex)
{
    PrettyPrinter.PrintError("Error retrieving portfolio transactions", ex);
}