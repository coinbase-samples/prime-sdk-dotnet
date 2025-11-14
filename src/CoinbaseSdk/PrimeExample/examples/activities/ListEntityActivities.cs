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
using CoinbaseSdk.Prime.Model.Enums;
using CoinbaseSdk.Prime.Common;
using System.CommandLine;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

var entityIdOption = new Option<string?>(
    name: "--entityId",
    description: "The Entity ID");

var symbolsOption = new Option<string?>(
    name: "--symbols",
    description: "Comma-separated list of symbols");

var categoriesOption = new Option<string?>(
    name: "--categories",
    description: "Comma-separated list of categories");

var startTimeOption = new Option<string?>(
    name: "--startTime",
    description: "Start time (UTC)");

var endTimeOption = new Option<string?>(
    name: "--endTime",
    description: "End time (UTC)");

var cursorOption = new Option<string?>(
    name: "--cursor",
    description: "Cursor for pagination");

var sortDirectionOption = new Option<string?>(
    name: "--sortDirection",
    description: "Sort direction (ASC/DESC)");

var limitOption = new Option<int>(
    name: "--limit",
    getDefaultValue: () => 100,
    description: "Number of results to return");

var rootCommand = new RootCommand("List activities for an entity")
{
    entityIdOption,
    symbolsOption,
    categoriesOption,
    startTimeOption,
    endTimeOption,
    cursorOption,
    sortDirectionOption,
    limitOption
};

rootCommand.SetHandler((entityId, symbolsStr, categoriesStr, startTime, endTime, cursor, sortDirectionStr, limit) =>
{
    // Fallback to environment variable for entityId
    if (string.IsNullOrEmpty(entityId))
    {
        entityId = Environment.GetEnvironmentVariable("PRIME_ENTITY_ID");
    }

    if (string.IsNullOrEmpty(entityId))
    {
        Console.Error.WriteLine("Error: --entityId is required (or set PRIME_ENTITY_ID env var).");
        Environment.ExitCode = 1;
        return;
    }

    try
    {
        Console.WriteLine($"Using Entity ID: {entityId}");

        // Create client and service
        var client = CoinbasePrimeClient.FromEnv();
        var activitiesService = new ActivitiesService(client);

        // Build request
        var requestBuilder = new ListEntityActivitiesRequest.Builder()
            .WithEntityId(entityId)
            .WithLimit(limit);

        if (!string.IsNullOrEmpty(symbolsStr))
        {
            var symbols = symbolsStr.Split(',').Select(s => s.Trim()).ToArray();
            requestBuilder.WithSymbols(symbols);
            Console.WriteLine($"Filtering by symbols: {string.Join(", ", symbols)}");
        }

        if (!string.IsNullOrEmpty(categoriesStr))
        {
            var categoryStrings = categoriesStr.Split(',').Select(s => s.Trim()).ToArray();
            var categories = new List<ActivityCategory?>();
            foreach (var catStr in categoryStrings)
            {
                if (Enum.TryParse<ActivityCategory>(catStr, true, out var category))
                {
                    categories.Add(category);
                }
                else
                {
                    Console.Error.WriteLine($"Invalid category: {catStr}");
                    Environment.ExitCode = 1;
                    return;
                }
            }
            requestBuilder.WithCategories(categories.ToArray());
            Console.WriteLine($"Filtering by categories: {string.Join(", ", categoryStrings)}");
        }

        if (!string.IsNullOrEmpty(startTime))
        {
            requestBuilder.WithStartTime(startTime);
            Console.WriteLine($"Start Time: {startTime}");
        }

        if (!string.IsNullOrEmpty(endTime))
        {
            requestBuilder.WithEndTime(endTime);
            Console.WriteLine($"End Time: {endTime}");
        }

        if (!string.IsNullOrEmpty(cursor))
        {
            requestBuilder.WithCursor(cursor);
            Console.WriteLine($"Cursor: {cursor}");
        }

        if (!string.IsNullOrEmpty(sortDirectionStr))
        {
            if (Enum.TryParse<SortDirection>(sortDirectionStr, true, out var sortDirection))
            {
                requestBuilder.WithSortDirection(sortDirection);
                Console.WriteLine($"Sort Direction: {sortDirection}");
            }
            else
            {
                Console.Error.WriteLine($"Invalid sort direction: {sortDirectionStr}");
                Environment.ExitCode = 1;
                return;
            }
        }

        var request = requestBuilder.Build();

        // Execute request
        var response = activitiesService.ListEntityActivities(request);

        // Print response
        PrettyPrinter.PrintResponse("ListEntityActivitiesResponse", response);

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        PrettyPrinter.PrintError("Error listing entity activities", ex);
        Environment.ExitCode = 1;
    }
}, entityIdOption, symbolsOption, categoriesOption, startTimeOption, endTimeOption, cursorOption, sortDirectionOption, limitOption);

return rootCommand.Invoke(args);
