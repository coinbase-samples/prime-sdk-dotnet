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
/// Example demonstrating how to retrieve a specific activity by ID
/// </summary>
public static class GetActivity
{
  /// <summary>
  /// Retrieves a specific activity by ID
  /// </summary>
  /// <param name="activityId">The activity ID to retrieve</param>
  /// <returns>True if successful, false otherwise</returns>
  public static bool Run(string activityId)
  {
    try
    {
      // Create client and service
      var client = CoinbasePrimeClient.FromEnv();
      var activitiesService = new ActivitiesService(client);

      // Build request
      var request = new GetActivityRequest(activityId);

      // Execute request
      var response = activitiesService.GetActivity(request);

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
