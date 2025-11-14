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

var activityIdOption = new Option<string?>(
    name: "--activityId",
    description: "The Activity ID");

var rootCommand = new RootCommand("Get activity by ID")
{
    activityIdOption
};

rootCommand.SetHandler((activityId) =>
{
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
        var request = new GetActivityRequest(activityId);

        // Execute request
        var response = activitiesService.GetActivity(request);

        // Print response
        PrettyPrinter.PrintResponse("GetActivityResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error retrieving activity", ex);
        Environment.ExitCode = 1;
    }
}, activityIdOption);

return rootCommand.Invoke(args);
