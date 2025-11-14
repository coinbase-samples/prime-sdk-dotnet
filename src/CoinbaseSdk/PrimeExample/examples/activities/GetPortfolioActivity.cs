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
using CoinbaseSdk.PrimeExample.Common;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

// Parse command line arguments
string? portfolioId = null;
string? activityId = null;

for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--portfolioId" && i + 1 < args.Length)
    {
        portfolioId = args[i + 1];
    }
    else if (args[i] == "--activityId" && i + 1 < args.Length)
    {
        activityId = args[i + 1];
    }
}

// Fallback to environment variable for portfolioId
if (string.IsNullOrEmpty(portfolioId))
{
    portfolioId = Environment.GetEnvironmentVariable("PRIME_PORTFOLIO_ID");
}

if (string.IsNullOrEmpty(portfolioId) || string.IsNullOrEmpty(activityId))
{
    PrettyPrinter.PrintUsage(
        "Usage: dotnet run --file GetPortfolioActivity.cs -- --portfolioId <portfolio-id> --activityId <activity-id>",
        "dotnet run --file GetPortfolioActivity.cs -- --portfolioId 89765432-1012-3456-7890-123456789012 --activityId a4df04eb-9d7a-4583-971c-290c935771d6"
    );
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