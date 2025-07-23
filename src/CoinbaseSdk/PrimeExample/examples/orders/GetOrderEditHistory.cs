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
using CoinbaseSdk.Core.Serialization;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Orders;

string? credentialsBlob = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS");
if (credentialsBlob == null)
{
    Console.WriteLine("COINBASE_PRIME_CREDENTIALS environment variable not set");
    return;
}

string? portfolioId = Environment.GetEnvironmentVariable("COINBASE_PRIME_PORTFOLIO_ID");
if (portfolioId == null)
{
    Console.WriteLine("COINBASE_PRIME_PORTFOLIO_ID environment variable not set");
    return;
}

var serializer = new JsonUtility();
var credentials = serializer.Deserialize<CoinbaseCredentials>(credentialsBlob);

if (credentials == null)
{
    Console.WriteLine("Failed to parse COINBASE_PRIME_CREDENTIALS environment variable");
    return;
}

var client = new CoinbasePrimeClient(credentials);
var ordersService = new OrdersService(client);

var request = new GetOrderEditHistoryRequest.GetOrderEditHistoryRequestBuilder()
    .WithPortfolioId(portfolioId)
    .WithOrderId("sample-order-id")
    .Build();

try
{
    var response = ordersService.GetOrderEditHistory(request);
    Console.WriteLine($"Order edit history retrieved: {response.Edits?.Length ?? 0} edits");
    
    if (response.Edits != null)
    {
        foreach (var edit in response.Edits)
        {
            Console.WriteLine($"Edit ID: {edit.EditId}, Type: {edit.EditType}, Timestamp: {edit.EditTimestamp}");
            
            if (edit.PreviousValues != null && edit.NewValues != null)
            {
                Console.WriteLine($"  Previous: Size={edit.PreviousValues.Size}, Price={edit.PreviousValues.Price}");
                Console.WriteLine($"  New: Size={edit.NewValues.Size}, Price={edit.NewValues.Price}");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error retrieving order edit history: {ex.Message}");
}