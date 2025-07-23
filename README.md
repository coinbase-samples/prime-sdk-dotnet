# Coinbase Prime .NET SDK

## Overview

The _Coinbase Prime .NET SDK_ is a sample library that demonstrates the structure of a [Coinbase Prime](https://prime.coinbase.com/) driver for
the [REST APIs](https://docs.cdp.coinbase.com/prime/reference).

## License

The _Coinbase Prime .NET SDK_ sample library is free and open source and released under the [Apache License, Version 2.0](LICENSE).

The application and code are only available for demonstration purposes.

## Installation

The _Coinbase Prime .NET SDK_ is vended through [NuGet](https://www.nuget.org/packages/CoinbaseSdk.Prime/) and available for installation via the `dotnet` CLI.

```bash
dotnet add package CoinbaseSdk.Prime --version x.y.z
```

or if using [paket](https://fsprojects.github.io/Paket/):

```bash
paket add CoinbaseSdk.Prime --version x.y.z
```

## Usage

To use the _Coinbase Prime .NET SDK_, initialize the Credentials class and create a new client. The Credentials struct is JSON
enabled. Ensure that Prime API credentials are stored in a secure manner.

```c#
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

var request = new GetActivityRequest.GetActivityRequestBuilder()
    .WithActivityId("sample-activity-id")
    .Build();

try
{
    var response = activitiesService.GetActivity(request);
    Console.WriteLine($"Retrieved activity: {response.Activity?.Id}");
    Console.WriteLine($"Activity type: {response.Activity?.Type}");
    Console.WriteLine($"Activity status: {response.Activity?.Status}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error retrieving activity: {ex.Message}");
}
```

The JSON format expected for `COINBASE_PRIME_CREDENTIALS` is:

```json
{
  "accessKey": "",
  "passphrase": "",
  "signingKey": ""
}
```

## Running Examples

For examples of how to use the client, see the files in the [`examples`](src/CoinbaseSdk/PrimeExample/examples/) directory.

### Prerequisites

1. Install the `dotnet-script` global tool:

```bash
dotnet tool install -g dotnet-script
```

2. Set required environment variables:
   - `COINBASE_PRIME_CREDENTIALS`: JSON credentials (see format above)
   - `COINBASE_PRIME_PORTFOLIO_ID`: Your portfolio ID (for portfolio-scoped examples)
   - `COINBASE_PRIME_ENTITY_ID`: Your entity ID (for entity-scoped examples)

### Running Individual Examples

Each example is a standalone C# script file (`.csx`) that can be executed directly:

```bash
# Activities examples
dotnet script src/CoinbaseSdk/PrimeExample/examples/activities/GetActivity.csx

# Futures examples
dotnet script src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmMarginCallDetails.csx
dotnet script src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmRiskLimits.csx

# Orders examples
dotnet script src/CoinbaseSdk/PrimeExample/examples/orders/GetOrderEditHistory.csx

# Wallets examples
dotnet script src/CoinbaseSdk/PrimeExample/examples/wallets/CreateWalletDepositAddress.csx
dotnet script src/CoinbaseSdk/PrimeExample/examples/wallets/ListWalletAddresses.csx
```

