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

using CoinbaseSdk.Prime.Products;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Common;
using System.CommandLine;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var portfolioIdOption = new Option<string?>(
    name: "--portfolioId",
    description: "The Portfolio ID");

var productIdOption = new Option<string?>(
    name: "--productId",
    description: "The Product ID (e.g. BTC-USD)");

var granularityOption = new Option<string?>(
    name: "--granularity",
    description: "Candle granularity (e.g. ONE_MINUTE, FIVE_MINUTE, FIFTEEN_MINUTE, ONE_HOUR, SIX_HOUR, ONE_DAY)");

var rootCommand = new RootCommand("Get public product candles")
{
    portfolioIdOption,
    productIdOption,
    granularityOption,
};

rootCommand.SetHandler((portfolioId, productId, granularity) =>
{
    portfolioId ??= Environment.GetEnvironmentVariable("PRIME_PORTFOLIO_ID");

    if (string.IsNullOrEmpty(portfolioId))
    {
        Console.Error.WriteLine("Error: --portfolioId is required (or set PRIME_PORTFOLIO_ID env var).");
        Environment.ExitCode = 1;
        return;
    }

    try
    {
        Console.WriteLine($"Using Portfolio ID: {portfolioId}");

        var client = CoinbasePrimeClient.FromEnv();
        var productsService = new ProductsService(client);

        var request = new GetCandlesRequest(portfolioId)
        {
            ProductId = productId,
            Granularity = granularity,
        };

        PrettyPrinter.PrintResponse("GetCandlesRequest", request);

        var response = productsService.GetCandles(request);

        PrettyPrinter.PrintResponse("GetCandlesResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error getting candles", ex);
        Environment.ExitCode = 1;
    }
}, portfolioIdOption, productIdOption, granularityOption);

return rootCommand.Invoke(args);
