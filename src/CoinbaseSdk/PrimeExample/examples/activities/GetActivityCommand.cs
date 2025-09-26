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

using System.CommandLine;

namespace CoinbaseSdk.PrimeExample.Examples.Activities;

/// <summary>
/// Command line interface for the GetActivity example
/// </summary>
public static class GetActivityCommand
{
    /// <summary>
    /// Creates the GetActivity command
    /// </summary>
    /// <returns>Configured command</returns>
    public static Command CreateCommand()
    {
        var command = new Command("GetActivity", "Retrieve a specific activity by ID");

        var activityIdOption = new Option<string>(
            "--activityId",
            "The ID of the activity to retrieve")
        {
            IsRequired = true
        };

        var portfolioIdOption = new Option<string?>(
            "--portfolioId",
            "The portfolio ID (optional, can be set via environment variables)");

        command.AddOption(activityIdOption);
        command.AddOption(portfolioIdOption);

        command.SetHandler(async (activityId, portfolioId) =>
        {
            var success = await GetActivity.RunAsync(activityId, portfolioId);
            Environment.ExitCode = success ? 0 : 1;
        }, activityIdOption, portfolioIdOption);

        return command;
    }
}