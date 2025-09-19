#!/usr/bin/env dotnet-script
#r "../../../Prime/bin/Debug/net8.0/CoinbaseSdk.Prime.dll"
#r "nuget: CoinbaseSdk.Core, 0.0.1"
#load "../../PrettyPrinter.csx"
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
using CoinbaseSdk.Prime.Portfolios;
using CoinbaseSdk.Prime.Client;

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
var portfoliosService = new PortfoliosService(client);

var request = new GetPortfolioFillsRequest.GetPortfolioFillsRequestBuilder()
    .WithPortfolioId(portfolioId)
    .WithLimit(100) // Optional: limit number of results
    .Build();

try
{
    var response = portfoliosService.GetPortfolioFills(request);
    PrettyPrinter.PrintResponse("GetPortfolioFillsResponse", response);
}
catch (Exception ex)
{
    PrettyPrinter.PrintError("Error retrieving portfolio fills", ex);
}