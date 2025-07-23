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

namespace CoinbaseSdk.PrimeExample.Examples.Portfolios;

/// <summary>
/// Command line interface for the ListPortfolios example
/// </summary>
public static class ListPortfoliosCommand
{
    /// <summary>
    /// Creates the ListPortfolios command
    /// </summary>
    /// <returns>Configured command</returns>
    public static Command CreateCommand()
    {
        var command = new Command("ListPortfolios", "List all portfolios");

        command.SetHandler(() =>
        {
            var success = ListPortfolios.Run();
            Environment.ExitCode = success ? 0 : 1;
        });

        return command;
    }
}

