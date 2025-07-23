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
using CoinbaseSdk.Prime.Futures;

string? credentialsBlob = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS");
if (credentialsBlob == null)
{
    Console.WriteLine("COINBASE_PRIME_CREDENTIALS environment variable not set");
    return;
}

string? entityId = Environment.GetEnvironmentVariable("COINBASE_PRIME_ENTITY_ID");
if (entityId == null)
{
    Console.WriteLine("COINBASE_PRIME_ENTITY_ID environment variable not set");
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
var futuresService = new FuturesService(client);

var request = new GetFcmRiskLimitsRequest.GetFcmRiskLimitsRequestBuilder()
    .WithEntityId(entityId)
    .Build();

try
{
    var response = futuresService.GetFcmRiskLimits(request);
    Console.WriteLine($"Risk limits retrieved: {response.RiskLimits?.Length ?? 0} items");
    
    if (response.RiskLimits != null)
    {
        foreach (var riskLimit in response.RiskLimits)
        {
            Console.WriteLine($"Product ID: {riskLimit.ProductId}, Limit: {riskLimit.RiskLimitValue}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error retrieving FCM risk limits: {ex.Message}");
}