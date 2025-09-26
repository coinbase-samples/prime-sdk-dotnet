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
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Orders;

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

if (Args.Length == 0)
{
    Console.WriteLine("Usage: dotnet script EditOrder.csx <order-id>");
    return;
}

string orderId = Args[0];

var serializer = new JsonUtility();
var credentials = serializer.Deserialize<CoinbaseCredentials>(credentialsBlob);

if (credentials == null)
{
    Console.WriteLine("Failed to parse COINBASE_PRIME_CREDENTIALS environment variable");
    return;
}

var client = new CoinbasePrimeClient(credentials);
var ordersService = client.OrdersService;

var request = new EditOrderRequest.EditOrderRequestBuilder()
    .WithPortfolioId(portfolioId)
    .WithOrderId(orderId)
    .WithOrigClientOrderId("original-client-order-123")
    .WithClientOrderId("updated-client-order-456")
    .WithLimitPrice("50000.00")
    .WithBaseQuantity("0.01")
    .Build();

try
{
    var response = ordersService.EditOrder(request);
    PrettyPrinter.PrintResponse("EditOrderResponse", response);
}
catch (Exception ex)
{
    PrettyPrinter.PrintError("Error editing order", ex);
}