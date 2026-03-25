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

using CoinbaseSdk.Prime.Transactions;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Common;
using System.CommandLine;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var portfolioIdOption = new Option<string?>(
    name: "--portfolioId",
    description: "The Portfolio ID");

var transactionIdOption = new Option<string>(
    name: "--transactionId",
    description: "The Transaction ID");

var isSelfOption = new Option<bool>(
    name: "--isSelf",
    description: "Whether the transfer is to a self-owned address",
    getDefaultValue: () => true);

var rootCommand = new RootCommand("Submit deposit travel rule data for a transaction")
{
    portfolioIdOption,
    transactionIdOption,
    isSelfOption,
};

rootCommand.SetHandler((portfolioId, transactionId, isSelf) =>
{
    portfolioId ??= Environment.GetEnvironmentVariable("PRIME_PORTFOLIO_ID");

    if (string.IsNullOrEmpty(portfolioId))
    {
        Console.Error.WriteLine("Error: --portfolioId is required (or set PRIME_PORTFOLIO_ID env var).");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(transactionId))
    {
        Console.Error.WriteLine("Error: --transactionId is required.");
        Environment.ExitCode = 1;
        return;
    }

    try
    {
        Console.WriteLine($"Using Portfolio ID: {portfolioId}");
        Console.WriteLine($"Using Transaction ID: {transactionId}");

        var client = CoinbasePrimeClient.FromEnv();
        var transactionsService = new TransactionsService(client);

        var request = new SubmitDepositTravelRuleDataRequest(portfolioId, transactionId)
        {
            IsSelf = isSelf,
        };

        PrettyPrinter.PrintResponse("SubmitDepositTravelRuleDataRequest", request);

        var response = transactionsService.SubmitDepositTravelRuleData(request);

        PrettyPrinter.PrintResponse("SubmitDepositTravelRuleDataResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error submitting deposit travel rule data", ex);
        Environment.ExitCode = 1;
    }
}, portfolioIdOption, transactionIdOption, isSelfOption);

return rootCommand.Invoke(args);
