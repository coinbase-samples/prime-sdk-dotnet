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
/// Command line interface for the GetPortfolio example
/// </summary>
public static class GetPortfolioCommand
{
    /// <summary>
    /// Creates the GetPortfolio command
    /// </summary>
    /// <returns>Configured command</returns>
    public static Command CreateCommand()
    {
        var command = new Command("GetPortfolio", "Retrieve portfolio information");

        var portfolioIdOption = new Option<string?>(
            "--portfolioId",
            "The portfolio ID (required)")
        {
            IsRequired = true
        };

        command.AddOption(portfolioIdOption);

        command.SetHandler((portfolioId) =>
        {
            var success = GetPortfolio.Run(portfolioId);
            Environment.ExitCode = success ? 0 : 1;
        }, portfolioIdOption);

        return command;
    }
}
