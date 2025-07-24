#!/usr/bin/env dotnet-script
#r "nuget: CoinbaseSdk.Prime, *"

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
using CoinbaseSdk.Prime.Activities;
using CoinbaseSdk.Prime.Client;

string? credentialsBlob = Environment.GetEnvironmentVariable("COINBASE_PRIME_CREDENTIALS");
if (credentialsBlob == null)
{
    Console.WriteLine("COINBASE_PRIME_CREDENTIALS environment variable not set");
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
var activitiesService = new ActivitiesService(client);

string activityId;
if (Args.Count > 0)
{
    activityId = Args[0];
}
else
{
    Console.WriteLine("Usage: dotnet script GetActivity.csx <activity-id>");
    Console.WriteLine("Example: dotnet script GetActivity.csx a4df04eb-9d7a-4583-971c-290c935771d6");
    return;
}

var request = new GetActivityRequest.GetActivityRequestBuilder()
    .WithActivityId(activityId)
    .Build();

try
{
    var response = activitiesService.GetActivity(request);
    Console.WriteLine("GetActivityResponse");
    Console.WriteLine(serializer.Serialize(response));
}
catch (Exception ex)
{
    Console.WriteLine($"Error retrieving activity: {ex.Message}");
}
