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
using CoinbaseSdk.Prime.Portfolios;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.PrimeExample.Common;
using Microsoft.Extensions.Configuration;

namespace CoinbaseSdk.PrimeExample.Examples.Portfolios;

/// <summary>
/// Example demonstrating how to retrieve portfolio information
/// </summary>
public static class GetPortfolio
{
    /// <summary>
    /// Retrieves portfolio information
    /// </summary>
    /// <param name="portfolioId">Optional portfolio ID. If not provided, will use configuration</param>
    /// <returns>Task representing the async operation</returns>
    public static bool Run(string? portfolioId = null)
    {
        try
        {
            // Create client and service
            var client = CoinbasePrimeClient.FromEnv();
            var portfoliosService = new PortfoliosService(client);

            // Build request
            var request = new GetPortfolioRequest.GetPortfolioRequestBuilder()
                .Build();

            // Execute request
            var response = portfoliosService.GetPortfolioAsync(request);

            // Print response
            PrettyPrinter.PrintResponse("GetPortfolioResponse", response);

            return true;
        }
        catch (Exception ex)
        {
            PrettyPrinter.PrintError("Error retrieving portfolio", ex);
            return false;
        }
    }
}

