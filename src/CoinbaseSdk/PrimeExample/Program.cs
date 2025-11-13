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
using CoinbaseSdk.PrimeExample.Common;
using CoinbaseSdk.PrimeExample.Examples.Assets;
using CoinbaseSdk.PrimeExample.Examples.Portfolios;
using Spectre.Console;

namespace CoinbaseSdk.PrimeExample;

/// <summary>
/// Main program entry point for Coinbase Prime SDK examples
/// </summary>
public class Program
{
    /// <summary>
    /// Main entry point
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>Exit code</returns>
    public static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Coinbase Prime SDK Examples")
        {
            Description = "Interactive examples for the Coinbase Prime .NET SDK"
        };

        // Add list command to show available examples
        var listCommand = new Command("list", "List all available examples");
        listCommand.SetHandler(ListExamples);
        rootCommand.AddCommand(listCommand);

        // Add example commands
        rootCommand.AddCommand(ListAssetsCommand.CreateCommand());
        rootCommand.AddCommand(GetPortfolioCommand.CreateCommand());
        rootCommand.AddCommand(ListPortfoliosCommand.CreateCommand());

        return await rootCommand.InvokeAsync(args);
    }

    /// <summary>
    /// Lists all available examples
    /// </summary>
    private static void ListExamples()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule("[bold blue]Coinbase Prime SDK Examples[/]").RuleStyle("blue"));
        AnsiConsole.WriteLine();

        var table = new Table()
            .AddColumn("Domain")
            .AddColumn("Command")
            .AddColumn("Description");

        table.AddRow("Assets", "ListAssets", "List assets for an entity");
        table.AddRow("Portfolios", "GetPortfolio", "Retrieve portfolio information");
        table.AddRow("Portfolios", "ListPortfolios", "List all portfolios");

        AnsiConsole.Write(table);

        AnsiConsole.WriteLine();
        Console.WriteLine("Usage: dotnet run -- [command] [options]");
        Console.WriteLine("Examples:");
        Console.WriteLine("  dotnet run -- ListPortfolios");
        Console.WriteLine("  dotnet run -- GetPortfolio --portfolioId your-portfolio-id");
        Console.WriteLine("  dotnet run -- ListAssets --entityId your-entity-id");
        AnsiConsole.WriteLine();
    }
}
