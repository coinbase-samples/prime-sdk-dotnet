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
using CoinbaseSdk.Prime.Model.Enums;
using CoinbaseSdk.Prime.Common;
using System.CommandLine;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var portfolioIdOption = new Option<string?>(
    name: "--portfolioId",
    description: "The Portfolio ID");

var productIdOption = new Option<string?>(
    name: "--productId",
    description: "The product ID (e.g., BTC-USD)");

var sideOption = new Option<string?>(
    name: "--side",
    description: "Order side (BUY or SELL)");

var typeOption = new Option<string?>(
    name: "--type",
    description: "Order type (MARKET, LIMIT, TWAP, VWAP, STOP_LIMIT, BLOCK, RFQ, PEG)");

var baseQuantityOption = new Option<string?>(
    name: "--baseQuantity",
    description: "Order size in base asset units (e.g., 0.001 for BTC)");

var quoteValueOption = new Option<string?>(
    name: "--quoteValue",
    description: "Order size in quote asset units (e.g., 100 for USD)");

var limitPriceOption = new Option<string?>(
    name: "--limitPrice",
    description: "Limit price (required for LIMIT, TWAP, VWAP, STOP_LIMIT orders)");

var stopPriceOption = new Option<string?>(
    name: "--stopPrice",
    description: "Stop price (for STOP_LIMIT orders)");

var timeInForceOption = new Option<string?>(
    name: "--timeInForce",
    description: "Time in force (GOOD_UNTIL_DATE_TIME, GOOD_UNTIL_CANCELLED, IMMEDIATE_OR_CANCEL, FILL_OR_KILL)");

var clientOrderIdOption = new Option<string?>(
    name: "--clientOrderId",
    description: "Client-generated order ID (auto-generated if not provided)");

var startTimeOption = new Option<string?>(
    name: "--startTime",
    description: "Start time for TWAP/VWAP orders (UTC)");

var expiryTimeOption = new Option<string?>(
    name: "--expiryTime",
    description: "Expiry time for orders (UTC)");

var postOnlyOption = new Option<bool?>(
    name: "--postOnly",
    description: "Post-only flag for LIMIT orders");

var rootCommand = new RootCommand("Create a new trading order")
{
    portfolioIdOption,
    productIdOption,
    sideOption,
    typeOption,
    baseQuantityOption,
    quoteValueOption,
    limitPriceOption,
    stopPriceOption,
    timeInForceOption,
    clientOrderIdOption,
    startTimeOption,
    expiryTimeOption,
    postOnlyOption,
};

rootCommand.SetHandler((context) =>
{
    // Get option values from context (needed because SetHandler only supports up to 8 parameters)
    var portfolioId = context.ParseResult.GetValueForOption(portfolioIdOption);
    var productId = context.ParseResult.GetValueForOption(productIdOption);
    var sideStr = context.ParseResult.GetValueForOption(sideOption);
    var typeStr = context.ParseResult.GetValueForOption(typeOption);
    var baseQuantity = context.ParseResult.GetValueForOption(baseQuantityOption);
    var quoteValue = context.ParseResult.GetValueForOption(quoteValueOption);
    var limitPrice = context.ParseResult.GetValueForOption(limitPriceOption);
    var stopPrice = context.ParseResult.GetValueForOption(stopPriceOption);
    var timeInForceStr = context.ParseResult.GetValueForOption(timeInForceOption);
    var clientOrderId = context.ParseResult.GetValueForOption(clientOrderIdOption);
    var startTime = context.ParseResult.GetValueForOption(startTimeOption);
    var expiryTime = context.ParseResult.GetValueForOption(expiryTimeOption);
    var postOnly = context.ParseResult.GetValueForOption(postOnlyOption);

    portfolioId ??= Environment.GetEnvironmentVariable("PRIME_PORTFOLIO_ID");

    if (string.IsNullOrEmpty(portfolioId))
    {
        Console.Error.WriteLine("Error: --portfolioId is required (or set PRIME_PORTFOLIO_ID env var).");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(productId))
    {
        Console.Error.WriteLine("Error: --productId is required (e.g., BTC-USD).");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(sideStr))
    {
        Console.Error.WriteLine("Error: --side is required (BUY or SELL).");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(typeStr))
    {
        Console.Error.WriteLine("Error: --type is required (MARKET, LIMIT, TWAP, VWAP, STOP_LIMIT, BLOCK, RFQ, PEG).");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(baseQuantity) && string.IsNullOrEmpty(quoteValue))
    {
        Console.Error.WriteLine("Error: Either --baseQuantity or --quoteValue is required.");
        Environment.ExitCode = 1;
        return;
    }

    // Parse side enum
    if (!Enum.TryParse<OrderSide>(sideStr, true, out var side))
    {
        Console.Error.WriteLine($"Error: Invalid side '{sideStr}'. Must be BUY or SELL.");
        Environment.ExitCode = 1;
        return;
    }

    // Parse type enum
    if (!Enum.TryParse<OrderType>(typeStr, true, out var orderType))
    {
        Console.Error.WriteLine($"Error: Invalid type '{typeStr}'. Must be one of: MARKET, LIMIT, TWAP, VWAP, STOP_LIMIT, BLOCK, RFQ, PEG.");
        Environment.ExitCode = 1;
        return;
    }

    // Parse time in force if provided
    TimeInForceType? timeInForce = null;
    if (!string.IsNullOrEmpty(timeInForceStr))
    {
        if (!Enum.TryParse<TimeInForceType>(timeInForceStr, true, out var parsedTimeInForce))
        {
            Console.Error.WriteLine($"Error: Invalid timeInForce '{timeInForceStr}'. Must be one of: GOOD_UNTIL_DATE_TIME, GOOD_UNTIL_CANCELLED, IMMEDIATE_OR_CANCEL, FILL_OR_KILL.");
            Environment.ExitCode = 1;
            return;
        }
        timeInForce = parsedTimeInForce;
    }

    try
    {
        Console.WriteLine($"Using Portfolio ID: {portfolioId}");

        // Create client and service
        var client = CoinbasePrimeClient.FromEnv();
        var ordersService = new OrdersService(client);

        // Generate client order ID if not provided
        clientOrderId ??= Guid.NewGuid().ToString();

        // Build request
        var requestBuilder = new CreateOrderRequest.CreateOrderRequestBuilder()
            .WithPortfolioId(portfolioId)
            .WithProductId(productId)
            .WithSide(side)
            .WithType(orderType)
            .WithClientOrderId(clientOrderId);

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

        if (timeInForce.HasValue)
        {
            requestBuilder.WithTimeInForce(timeInForce.Value);
        }

        if (!string.IsNullOrEmpty(startTime))
        {
            requestBuilder.WithStartTime(startTime);
        }

        if (!string.IsNullOrEmpty(expiryTime))
        {
            requestBuilder.WithExpiryTime(expiryTime);
        }

        if (postOnly.HasValue)
        {
            requestBuilder.WithPostOnly(postOnly.Value);
        }

        var request = requestBuilder.Build();

        PrettyPrinter.PrintResponse("CreateOrderRequest", request);

        // Execute request
        var response = ordersService.CreateOrder(request);

        // Print response
        PrettyPrinter.PrintResponse("CreateOrderResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error creating order", ex);
        Environment.ExitCode = 1;
    }
});

return rootCommand.Invoke(args);
