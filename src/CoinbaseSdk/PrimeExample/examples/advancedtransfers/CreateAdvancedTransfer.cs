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
using CoinbaseSdk.Prime.Model;
using System.CommandLine;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var portfolioIdOption = new Option<string?>(
    name: "--portfolioId",
    description: "The Portfolio ID");

var rootCommand = new RootCommand("Create an advanced transfer")
{
    portfolioIdOption,
};

rootCommand.SetHandler((portfolioId) =>
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
        var service = new AdvancedTransfersService(client);

        var advancedTransfer = new AdvancedTransfer();
        var request = new CreateAdvancedTransferRequest(portfolioId)
        {
            AdvancedTransfer = advancedTransfer,
        };

        PrettyPrinter.PrintResponse("CreateAdvancedTransferRequest", request);

        var response = service.CreateAdvancedTransfer(request);

        PrettyPrinter.PrintResponse("CreateAdvancedTransferResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error creating advanced transfer", ex);
        Environment.ExitCode = 1;
    }
}, portfolioIdOption);

return rootCommand.Invoke(args);
