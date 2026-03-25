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

using CoinbaseSdk.Prime.AdvancedTransfers;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Common;
using System.CommandLine;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var portfolioIdOption = new Option<string?>(
    name: "--portfolioId",
    description: "The Portfolio ID");

var advancedTransferIdOption = new Option<string>(
    name: "--advancedTransferId",
    description: "The Advanced Transfer ID");

var rootCommand = new RootCommand("List transactions for an advanced transfer")
{
    portfolioIdOption,
    advancedTransferIdOption,
};

rootCommand.SetHandler((portfolioId, advancedTransferId) =>
{
    portfolioId ??= Environment.GetEnvironmentVariable("PRIME_PORTFOLIO_ID");

    if (string.IsNullOrEmpty(portfolioId))
    {
        Console.Error.WriteLine("Error: --portfolioId is required (or set PRIME_PORTFOLIO_ID env var).");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(advancedTransferId))
    {
        Console.Error.WriteLine("Error: --advancedTransferId is required.");
        Environment.ExitCode = 1;
        return;
    }

    try
    {
        Console.WriteLine($"Using Portfolio ID: {portfolioId}");
        Console.WriteLine($"Using Advanced Transfer ID: {advancedTransferId}");

        var client = CoinbasePrimeClient.FromEnv();
        var service = new AdvancedTransfersService(client);

        var request = new ListAdvancedTransferTransactionsRequest(portfolioId, advancedTransferId);

        PrettyPrinter.PrintResponse("ListAdvancedTransferTransactionsRequest", request);

        var response = service.ListAdvancedTransferTransactions(request);

        PrettyPrinter.PrintResponse("ListAdvancedTransferTransactionsResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error listing advanced transfer transactions", ex);
        Environment.ExitCode = 1;
    }
}, portfolioIdOption, advancedTransferIdOption);

return rootCommand.Invoke(args);
