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

using CoinbaseSdk.Prime.Assets;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Common;

namespace CoinbaseSdk.PrimeExample.Examples.Assets;

/// <summary>
/// Example demonstrating how to list assets
/// </summary>
public static class ListAssets
{
    /// <summary>
    /// Lists assets for an entity
    /// </summary>
    /// <param name="entityId">Optional entity ID. If not provided, will use configuration</param>
    /// <returns>True if successful, false otherwise</returns>
    public static bool Run(string? entityId = null)
    {
        try
        {
            // Create client and service
            var client = CoinbasePrimeClient.FromEnv();
            var assetsService = new AssetsService(client);

            // Get entity ID from parameter or environment
            entityId ??= Environment.GetEnvironmentVariable("PRIME_ENTITY_ID");
            if (string.IsNullOrEmpty(entityId))
            {
                PrettyPrinter.PrintError("Error", new ArgumentException("Entity ID is required. Provide via --entityId parameter or PRIME_ENTITY_ID environment variable."));
                return false;
            }

            // Build request
            var request = new ListAssetsRequest(entityId);

            // Execute request
            var response = assetsService.ListAssets(request);

            // Print response
            PrettyPrinter.PrintResponse("ListAssetsResponse", response);

            return true;
        }
        catch (Exception ex)
        {
            PrettyPrinter.PrintError("Error retrieving assets", ex);
            return false;
        }
    }
}
