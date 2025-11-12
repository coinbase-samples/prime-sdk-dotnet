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

using CoinbaseSdk.Prime.Activities;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.PrimeExample.Common;

namespace CoinbaseSdk.PrimeExample.Examples.Activities;

/// <summary>
/// Example demonstrating how to get a portfolio activity
/// </summary>
public static class GetPortfolioActivity
{
    /// <summary>
    /// Gets a specific portfolio activity
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>True if successful, false otherwise</returns>
    public static bool Execute(string[] args)
    {
        // Parse named arguments
        string? portfolioId = null;
        string? activityId = null;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--portfolioId" && i + 1 < args.Length)
            {
                portfolioId = args[++i];
            }
            else if (args[i] == "--activityId" && i + 1 < args.Length)
            {
                activityId = args[++i];
            }
        }

        if (string.IsNullOrEmpty(portfolioId) || string.IsNullOrEmpty(activityId))
        {
            PrettyPrinter.PrintError("Error", new ArgumentException("PortfolioId and ActivityId are required. Provide via --portfolioId and --activityId parameters."));
            return false;
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
            PrettyPrinter.PrintResponse("GetActivityResponse", response);
            return true;
        }
        catch (Exception ex)
        {
            PrettyPrinter.PrintError("Error retrieving activity", ex);
            return false;
        }
    }
}
