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
using CoinbaseSdk.Prime.Financing;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Common;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var entityIdOption = new Option<string?>(
    name: "--entityId",
    description: "The Entity ID");

var rootCommand = new RootCommand("Get Cross Margin Risk Parameters")
{
    entityIdOption,
};

rootCommand.SetHandler((entityId) =>
{
    entityId ??= Environment.GetEnvironmentVariable("PRIME_ENTITY_ID");

    if (string.IsNullOrEmpty(entityId))
    {
        Console.Error.WriteLine("Error: --entityId is required (or set PRIME_ENTITY_ID env var).");
        Environment.ExitCode = 1;
        return;
    }

    try
    {
        Console.WriteLine($"Using EntityId: {entityId}");

        var client = CoinbasePrimeClient.FromEnv();
        var financingService = new FinancingService(client);

        var request = new GetCrossMarginRiskParametersRequest(entityId);

        PrettyPrinter.PrintResponse("GetCrossMarginRiskParametersRequest", request);

        var response = financingService.GetCrossMarginRiskParameters(request);

        PrettyPrinter.PrintResponse("GetCrossMarginRiskParametersResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error calling GetCrossMarginRiskParameters", ex);
        Environment.ExitCode = 1;
    }
}, entityIdOption);

return rootCommand.Invoke(args);
