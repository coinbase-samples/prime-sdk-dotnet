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

var designatedFundingPortfolioIdOption = new Option<string?>(
    name: "--designatedFundingPortfolioId",
    description: "Designated Funding Portfolio Id");

var automaticConversionEnabledOption = new Option<string?>(
    name: "--automaticConversionEnabled",
    description: "Automatic Conversion Enabled");

var automaticLoanEnabledOption = new Option<string?>(
    name: "--automaticLoanEnabled",
    description: "Automatic Loan Enabled");

var automaticExcessReturnEnabledOption = new Option<string?>(
    name: "--automaticExcessReturnEnabled",
    description: "Automatic Excess Return Enabled");

var excessFundsTargetAmountOption = new Option<string?>(
    name: "--excessFundsTargetAmount",
    description: "Excess Funds Target Amount");

var rootCommand = new RootCommand("Update Funding Settings")
{
    entityIdOption,
    designatedFundingPortfolioIdOption,
    automaticConversionEnabledOption,
    automaticLoanEnabledOption,
    automaticExcessReturnEnabledOption,
    excessFundsTargetAmountOption,
};

rootCommand.SetHandler((context) =>
{
    var entityId = context.ParseResult.GetValueForOption(entityIdOption);
    var designatedFundingPortfolioId = context.ParseResult.GetValueForOption(designatedFundingPortfolioIdOption);
    var automaticConversionEnabled = context.ParseResult.GetValueForOption(automaticConversionEnabledOption);
    var automaticLoanEnabled = context.ParseResult.GetValueForOption(automaticLoanEnabledOption);
    var automaticExcessReturnEnabled = context.ParseResult.GetValueForOption(automaticExcessReturnEnabledOption);
    var excessFundsTargetAmount = context.ParseResult.GetValueForOption(excessFundsTargetAmountOption);

    entityId ??= Environment.GetEnvironmentVariable("PRIME_ENTITY_ID");

    if (string.IsNullOrEmpty(entityId))
    {
        Console.Error.WriteLine("Error: --entityId is required (or set PRIME_ENTITY_ID env var).");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(designatedFundingPortfolioId))
    {
        Console.Error.WriteLine("Error: --designatedFundingPortfolioId is required.");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(automaticConversionEnabled))
    {
        Console.Error.WriteLine("Error: --automaticConversionEnabled is required.");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(automaticLoanEnabled))
    {
        Console.Error.WriteLine("Error: --automaticLoanEnabled is required.");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(automaticExcessReturnEnabled))
    {
        Console.Error.WriteLine("Error: --automaticExcessReturnEnabled is required.");
        Environment.ExitCode = 1;
        return;
    }

    if (string.IsNullOrEmpty(excessFundsTargetAmount))
    {
        Console.Error.WriteLine("Error: --excessFundsTargetAmount is required.");
        Environment.ExitCode = 1;
        return;
    }

    try
    {
        Console.WriteLine($"Using EntityId: {entityId}");

        var client = CoinbasePrimeClient.FromEnv();
        var financingService = new FinancingService(client);

        var request = new UpdateFundingSettingsRequest.UpdateFundingSettingsRequestBuilder()
            .WithEntityId(entityId)
            .WithDesignatedFundingPortfolioId(designatedFundingPortfolioId)
            .WithAutomaticConversionEnabled(automaticConversionEnabled)
            .WithAutomaticLoanEnabled(automaticLoanEnabled)
            .WithAutomaticExcessReturnEnabled(automaticExcessReturnEnabled)
            .WithExcessFundsTargetAmount(excessFundsTargetAmount)
            .Build();

        PrettyPrinter.PrintResponse("UpdateFundingSettingsRequest", request);

        var response = financingService.UpdateFundingSettings(request);

        PrettyPrinter.PrintResponse("UpdateFundingSettingsResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error calling UpdateFundingSettings", ex);
        Environment.ExitCode = 1;
    }
});

return rootCommand.Invoke(args);
