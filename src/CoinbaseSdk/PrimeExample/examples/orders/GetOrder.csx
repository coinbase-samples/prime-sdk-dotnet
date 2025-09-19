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
using CoinbaseSdk.Prime.Orders;
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
var ordersService = new OrdersService(client);

string orderId;
if (Args.Count > 0)
{
    orderId = Args[0];
}
else
{
    PrettyPrinter.PrintUsage(
        "dotnet script GetOrder.csx <order-id>", 
        "dotnet script GetOrder.csx sample-order-id-12345");
    return;
}

var request = new GetOrderRequest.GetOrderRequestBuilder()
    .WithPortfolioId(portfolioId)
    .WithOrderId(orderId)
    .Build();

try
{
    var response = ordersService.GetOrder(request);
    PrettyPrinter.PrintResponse("GetOrderResponse", response);
}
catch (Exception ex)
{
    PrettyPrinter.PrintError("Error retrieving order", ex);
    Console.WriteLine("Note: Use GetOrders to find valid order IDs");
}