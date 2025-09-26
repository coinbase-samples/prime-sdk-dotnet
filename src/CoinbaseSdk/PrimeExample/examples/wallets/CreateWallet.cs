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

using CoinbaseSdk.Core.Credentials;
using CoinbaseSdk.Prime.Model;
using CoinbaseSdk.Prime.Wallets;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.PrimeExample.Common;
using Microsoft.Extensions.Configuration;

namespace CoinbaseSdk.PrimeExample.Examples.Wallets;

/// <summary>
/// Example demonstrating how to create a new wallet
/// </summary>
public static class CreateWallet
{
    /// <summary>
    /// Creates a new wallet
    /// </summary>
    /// <param name="symbol">The asset symbol (e.g., BTC, ETH)</param>
    /// <param name="walletStringType">The wallet type (e.g., VAULT, TRADING)</param>
    /// <param name="name">The wallet name</param>
    /// <param name="portfolioId">Optional portfolio ID. If not provided, will use configuration</param>
    /// <returns>Task representing the async operation</returns>
    public static bool Run(string symbol, string walletStringType, string name, string? portfolioId = null)
    {
        try
        {
            // Create client and service
            var client = CoinbasePrimeClient.FromEnv();
            var walletsService = new WalletsService(client);

            var walletType = (WalletType)Enum.Parse(typeof(WalletType), walletStringType);

            // Build request
            var request = new CreateWalletRequest.CreateWalletRequestBuilder()
                .WithSymbol(symbol)
                .WithType(walletType)
                .WithName(name)
                .Build();

            // Execute request
            var response = walletsService.CreateWallet(request);

            // Print response
            PrettyPrinter.PrintResponse("CreateWalletResponse", response);

            return true;
        }
        catch (Exception ex)
        {
            PrettyPrinter.PrintError("Error creating wallet", ex);
            return false;
        }
    }
}

