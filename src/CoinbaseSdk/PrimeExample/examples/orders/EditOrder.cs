#!/usr/bin/env -S dotnet run --file
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

#:project ../../../Prime
#:project ../../
#:package Newtonsoft.Json@13.0.3

using CoinbaseSdk.Prime.Orders;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Common;
using System.CommandLine;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var portfolioIdOption = new Option<string?>(
    name: "--portfolioId",
    description: "The Portfolio ID");

var orderIdOption = new Option<string?>(
    name: "--orderId",
    description: "The Order ID to edit");

var productIdOption = new Option<string?>(
    name: "--productId",
    description: "The product ID (e.g., BTC-USD)");

var origClientOrderIdOption = new Option<string?>(
    name: "--origClientOrderId",
    description: "The original client order ID from when the order was created");

var clientOrderIdOption = new Option<string?>(
    name: "--clientOrderId",
    description: "New client order ID for the edited order");

var baseQuantityOption = new Option<string?>(
    name: "--baseQuantity",
    description: "New order size in base asset units (e.g., 0.001 for BTC)");

var quoteValueOption = new Option<string?>(
    name: "--quoteValue",
    description: "New order size in quote asset units (e.g., 100 for USD)");

var limitPriceOption = new Option<string?>(
    name: "--limitPrice",
    description: "New limit price");

var stopPriceOption = new Option<string?>(
    name: "--stopPrice",
    description: "New stop price (for STOP_LIMIT orders)");

var expiryTimeOption = new Option<string?>(
    name: "--expiryTime",
    description: "New expiry time for orders (UTC)");

var displayQuoteSizeOption = new Option<string?>(
    name: "--displayQuoteSize",
    description: "Display quote size for iceberg orders");

var displayBaseSizeOption = new Option<string?>(
    name: "--displayBaseSize",
    description: "Display base size for iceberg orders");

var rootCommand = new RootCommand("Edit an existing trading order")
{
    portfolioIdOption,
    orderIdOption,
    productIdOption,
    origClientOrderIdOption,
    clientOrderIdOption,
    baseQuantityOption,
    quoteValueOption,
    limitPriceOption,
    stopPriceOption,
    expiryTimeOption,
    displayQuoteSizeOption,
    displayBaseSizeOption,
};

rootCommand.SetHandler((context) =>
{
    // Get option values from context
    var portfolioId = context.ParseResult.GetValueForOption(portfolioIdOption);
    var orderId = context.ParseResult.GetValueForOption(orderIdOption);
    var productId = context.ParseResult.GetValueForOption(productIdOption);
    var origClientOrderId = context.ParseResult.GetValueForOption(origClientOrderIdOption);
    var clientOrderId = context.ParseResult.GetValueForOption(clientOrderIdOption);
    var baseQuantity = context.ParseResult.GetValueForOption(baseQuantityOption);
    var quoteValue = context.ParseResult.GetValueForOption(quoteValueOption);
    var limitPrice = context.ParseResult.GetValueForOption(limitPriceOption);
    var stopPrice = context.ParseResult.GetValueForOption(stopPriceOption);
    var expiryTime = context.ParseResult.GetValueForOption(expiryTimeOption);
    var displayQuoteSize = context.ParseResult.GetValueForOption(displayQuoteSizeOption);
    var displayBaseSize = context.ParseResult.GetValueForOption(displayBaseSizeOption);

    portfolioId ??= Environment.GetEnvironmentVariable("PRIME_PORTFOLIO_ID");

    if (string.IsNullOrEmpty(portfolioId))
    {
        Console.Error.WriteLine("Error: --portfolioId is required (or set PRIME_PORTFOLIO_ID env var).");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(orderId))
    {
        Console.Error.WriteLine("Error: --orderId is required.");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(origClientOrderId))
    {
        Console.Error.WriteLine("Error: --origClientOrderId is required.");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(clientOrderId))
    {
        Console.Error.WriteLine("Error: --clientOrderId is required. Generate a new unique ID for the edited order.");
        Environment.ExitCode = 1;
        return;
    }

    try
    {
        Console.WriteLine($"Using Portfolio ID: {portfolioId}");
        Console.WriteLine($"Editing Order ID: {orderId}");

        // Create client and service
        var client = CoinbasePrimeClient.FromEnv();
        var ordersService = new OrdersService(client);

        // Build request
        var requestBuilder = new EditOrderRequest.EditOrderRequestBuilder()
            .WithPortfolioId(portfolioId)
            .WithOrderId(orderId)
            .WithOrigClientOrderId(origClientOrderId)
            .WithClientOrderId(clientOrderId);

        if (!string.IsNullOrEmpty(productId))
        {
            requestBuilder.WithProductId(productId);
        }

        if (!string.IsNullOrEmpty(baseQuantity))
        {
            requestBuilder.WithBaseQuantity(baseQuantity);
        }

        if (!string.IsNullOrEmpty(quoteValue))
        {
            requestBuilder.WithQuoteValue(quoteValue);
        }

        if (!string.IsNullOrEmpty(limitPrice))
        {
            requestBuilder.WithLimitPrice(limitPrice);
        }

        if (!string.IsNullOrEmpty(stopPrice))
        {
            requestBuilder.WithStopPrice(stopPrice);
        }

        if (!string.IsNullOrEmpty(expiryTime))
        {
            requestBuilder.WithExpiryTime(expiryTime);
        }

        if (!string.IsNullOrEmpty(displayQuoteSize))
        {
            requestBuilder.WithDisplayQuoteSize(displayQuoteSize);
        }

        if (!string.IsNullOrEmpty(displayBaseSize))
        {
            requestBuilder.WithDisplayBaseSize(displayBaseSize);
        }

        var request = requestBuilder.Build();

        PrettyPrinter.PrintResponse("EditOrderRequest", request);

        // Execute request
        var response = ordersService.EditOrder(request);

        // Print response
        PrettyPrinter.PrintResponse("EditOrderResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error editing order", ex);
        Environment.ExitCode = 1;
    }
});

return rootCommand.Invoke(args);

