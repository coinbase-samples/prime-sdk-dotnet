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
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Portfolios;
using CoinbaseSdk.Prime.Common;
using Microsoft.Extensions.Configuration;

namespace CoinbaseSdk.PrimeExample.Examples.Portfolios;

/// <summary>
/// Example demonstrating how to retrieve portfolio information
/// </summary>
public static class GetPortfolio
{
  /// <summary>
  /// Run the example.
  /// </summary>
  public static bool Run(string? portfolioId)
  {
    try
    {
      if (portfolioId == null)
      {
        PrettyPrinter.Print("portfolioId required input parameter");
        return false;
      }
      var client = CoinbasePrimeClient.FromEnv();
      var portfoliosService = new PortfoliosService(client);

      // Build request
      var request = new GetPortfolioRequest(portfolioId);

      // Execute request
      var response = portfoliosService.GetPortfolio(request);

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
