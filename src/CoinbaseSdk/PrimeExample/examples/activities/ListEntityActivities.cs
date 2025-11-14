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
using CoinbaseSdk.PrimeExample.Common;

// Load environment variables
DotNetEnv.Env.TraversePath().Load();

// Parse command line arguments
string? entityId = null;
string? symbolsStr = null;
string? categoriesStr = null;
string? startTime = null;
string? endTime = null;
string? cursor = null;
string? sortDirectionStr = null;
int limit = 100;

for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--entityId" && i + 1 < args.Length)
    {
        entityId = args[i + 1];
    }
    else if (args[i] == "--symbols" && i + 1 < args.Length)
    {
        symbolsStr = args[i + 1];
    }
    else if (args[i] == "--categories" && i + 1 < args.Length)
    {
        categoriesStr = args[i + 1];
    }
    else if (args[i] == "--startTime" && i + 1 < args.Length)
    {
        startTime = args[i + 1];
    }
    else if (args[i] == "--endTime" && i + 1 < args.Length)
    {
        endTime = args[i + 1];
    }
    else if (args[i] == "--cursor" && i + 1 < args.Length)
    {
        cursor = args[i + 1];
    }
    else if (args[i] == "--sortDirection" && i + 1 < args.Length)
    {
        sortDirectionStr = args[i + 1];
    }
    else if (args[i] == "--limit" && i + 1 < args.Length)
    {
        int.TryParse(args[i + 1], out limit);
    }
}

// Fallback to environment variable for entityId
if (string.IsNullOrEmpty(entityId))
{
    entityId = Environment.GetEnvironmentVariable("PRIME_ENTITY_ID");
}

if (string.IsNullOrEmpty(entityId))
{
    PrettyPrinter.PrintUsage(
        "Usage: dotnet run --file ListEntityActivities.cs -- --entityId <entity-id> [options]",
        "dotnet run --file ListEntityActivities.cs -- --entityId 89765432-1012-3456-7890-123456789012 --symbols BTC,ETH --categories PAYMENT,TRANSFER --limit 10 --sortDirection DESC"
    );
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
