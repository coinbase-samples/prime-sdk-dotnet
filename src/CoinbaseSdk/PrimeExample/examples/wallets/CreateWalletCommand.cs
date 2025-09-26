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

namespace CoinbaseSdk.PrimeExample.Examples.Wallets;

/// <summary>
/// Command line interface for the CreateWallet example
/// </summary>
public static class CreateWalletCommand
{
    /// <summary>
    /// Creates the CreateWallet command
    /// </summary>
    /// <returns>Configured command</returns>
    public static Command CreateCommand()
    {
        var command = new Command("CreateWallet", "Create a new wallet");

        var symbolOption = new Option<string>(
            "--symbol",
            "The asset symbol (e.g., BTC, ETH)")
        {
            IsRequired = true
        };

        var walletTypeOption = new Option<string>(
            "--walletType",
            () => "VAULT",
            "The wallet type (VAULT, TRADING, etc.)");

        var nameOption = new Option<string>(
            "--name",
            "The wallet name")
        {
            IsRequired = true
        };

        var portfolioIdOption = new Option<string?>(
            "--portfolioId",
            "The portfolio ID (optional, can be set via environment variables)");

        command.AddOption(symbolOption);
        command.AddOption(walletTypeOption);
        command.AddOption(nameOption);
        command.AddOption(portfolioIdOption);

        command.SetHandler((symbol, walletType, name, portfolioId) =>
        {
            var success = CreateWallet.Run(symbol, walletType, name, portfolioId);
            Environment.ExitCode = success ? 0 : 1;
        }, symbolOption, walletTypeOption, nameOption, portfolioIdOption);

        return command;
    }
}
