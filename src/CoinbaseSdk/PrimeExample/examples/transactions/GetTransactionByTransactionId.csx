#!/usr/bin/env dotnet-script
#r "../../../Prime/bin/Debug/net8.0/CoinbaseSdk.Prime.dll"
#r "nuget: CoinbaseSdk.Core, 0.0.1"

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

string transactionId;
if (Args.Count > 0)
{
    transactionId = Args[0];
}
else
{
    Console.WriteLine("Usage: dotnet script GetTransactionByTransactionId.csx <transaction-id>");
    Console.WriteLine("Example: dotnet script GetTransactionByTransactionId.csx 550e8400-e29b-41d4-a716-446655440000");
    return;
}

var request = new GetTransactionByTransactionIdRequest.GetTransactionByTransactionIdRequestBuilder()
    .WithPortfolioId(portfolioId)
    .WithTransactionId(transactionId)
    .Build();

try
{
    var response = transactionsService.GetTransactionByTransactionId(request);
    Console.WriteLine("GetTransactionByTransactionIdResponse");
    Console.WriteLine(serializer.Serialize(response));
}
catch (Exception ex)
{
    Console.WriteLine($"Error retrieving transaction: {ex.Message}");
}