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

using CoinbaseSdk.Prime.Activities;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Common;
using System.CommandLine;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var portfolioIdOption = new Option<string?>(
    name: "--portfolioId",
    description: "The Portfolio ID");

var activityIdOption = new Option<string?>(
    name: "--activityId",
    description: "The Activity ID");

var rootCommand = new RootCommand("Get portfolio activity by ID")
{
    portfolioIdOption,
    activityIdOption
};

rootCommand.SetHandler((portfolioId, activityId) =>
{
    // Fallback to environment variable for portfolioId
    if (string.IsNullOrEmpty(portfolioId))
    {
        portfolioId = Environment.GetEnvironmentVariable("PRIME_PORTFOLIO_ID");
    }

    if (string.IsNullOrEmpty(portfolioId))
    {
        Console.Error.WriteLine("Error: --portfolioId is required (or set PRIME_PORTFOLIO_ID env var).");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(activityId))
    {
        Console.Error.WriteLine("Error: --activityId is required.");
        Environment.ExitCode = 1;
        return;
    }

    try
    {
        // Create client and service
        var client = CoinbasePrimeClient.FromEnv();
        var activitiesService = new ActivitiesService(client);

        // Build request
        var request = new GetPortfolioActivityRequest.Builder()
            .WithPortfolioId(portfolioId)
            .WithActivityId(activityId)
            .Build();

        // Execute request
        var response = activitiesService.GetPortfolioActivity(request);

        // Print response
        PrettyPrinter.PrintResponse("GetPortfolioActivityResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error retrieving portfolio activity", ex);
        Environment.ExitCode = 1;
    }
}, portfolioIdOption, activityIdOption);

return rootCommand.Invoke(args);
