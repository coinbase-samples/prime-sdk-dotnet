#!/usr/bin/env -S dotnet run --file
/*
 * Copyright 2026-present Coinbase Global, Inc.
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

#:project ../../../Prime
#:project ../../
#:package Newtonsoft.Json@13.0.3

using System.CommandLine;
using CoinbaseSdk.Prime.AdvancedTransfer;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Common;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var portfolioIdOption = new Option<string?>(
    name: "--portfolioId",
    description: "The Portfolio ID");

var rootCommand = new RootCommand("Get Portfolio Counterparty")
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
        Console.WriteLine($"Using PortfolioId: {portfolioId}");

        var client = CoinbasePrimeClient.FromEnv();
        var advancedTransferService = new AdvancedTransferService(client);

        var request = new GetPortfolioCounterpartyRequest(portfolioId);

        PrettyPrinter.PrintResponse("GetPortfolioCounterpartyRequest", request);

        var response = advancedTransferService.GetPortfolioCounterparty(request);

        PrettyPrinter.PrintResponse("GetPortfolioCounterpartyResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error calling GetPortfolioCounterparty", ex);
        Environment.ExitCode = 1;
    }
}, portfolioIdOption);

return rootCommand.Invoke(args);
