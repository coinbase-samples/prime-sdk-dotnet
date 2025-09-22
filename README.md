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

## Configuration

### Setting up Environment Variables

The SDK supports two methods for configuring credentials:

#### Method 1: Using .env file (Recommended for Development)

1. Copy the example environment file:
```bash
cp .env.example .env
```

2. Edit the `.env` file and add your credentials:
```env
# Individual credential fields (recommended)
COINBASE_PRIME_ACCESS_KEY=your_access_key_here
COINBASE_PRIME_PASSPHRASE=your_passphrase_here
COINBASE_PRIME_SIGNING_KEY=your_signing_key_here

# Account IDs for examples
COINBASE_PRIME_PORTFOLIO_ID=your-portfolio-id
COINBASE_PRIME_ENTITY_ID=your-entity-id
```

**Important:** Never commit the `.env` file to version control. It's already included in `.gitignore` for your safety.

#### Method 2: System Environment Variables

You can also set environment variables directly in your system or CI/CD pipeline:

```bash
# Individual fields (new format)
export COINBASE_PRIME_ACCESS_KEY="your_access_key"
export COINBASE_PRIME_PASSPHRASE="your_passphrase"
export COINBASE_PRIME_SIGNING_KEY="your_signing_key"

# OR using JSON format (backward compatible)
export COINBASE_PRIME_CREDENTIALS='{"accessKey":"...","passphrase":"...","signingKey":"..."}'
```

## Usage

To use the _Coinbase Prime .NET SDK_, initialize the Credentials class and create a new client. The SDK now automatically loads credentials from environment variables or `.env` files.

**Note:** When using the SDK in your applications, the `EnvFileLoader.Load()` method will automatically look for a `.env` file in your project root and load its variables into the environment. This is particularly useful for local development.

### Example: Using Individual Credential Fields (Recommended)

```c#
using CoinbaseSdk.Core.Credentials;
using CoinbaseSdk.Core.Serialization;
using CoinbaseSdk.Prime.Activities;
using CoinbaseSdk.Prime.Client;
using CoinbaseSdk.Prime.Utils;

// Load environment variables from .env file if present
EnvFileLoader.Load();

// Create credentials from individual environment variables
var credentials = new CoinbaseCredentials(
    Environment.GetEnvironmentVariable("COINBASE_PRIME_ACCESS_KEY"),
    Environment.GetEnvironmentVariable("COINBASE_PRIME_PASSPHRASE"),
    Environment.GetEnvironmentVariable("COINBASE_PRIME_SIGNING_KEY")
);

var client = new CoinbasePrimeClient(credentials);
var activitiesService = new ActivitiesService(client);

var request = new GetActivityRequest.GetActivityRequestBuilder()
    .WithActivityId("sample-activity-id")
    .Build();

try
{
    var response = activitiesService.GetActivity(request);
    var serializer = new JsonUtility();
    Console.WriteLine("GetActivityResponse");
    Console.WriteLine(serializer.Serialize(response));
}
catch (Exception ex)
{
    Console.WriteLine($"Error retrieving activity: {ex.Message}");
}
```

### Example: Using JSON Format (Backward Compatible)

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
    Console.WriteLine("GetActivityResponse");
    Console.WriteLine(serializer.Serialize(response));
}
catch (Exception ex)
{
    Console.WriteLine($"Error retrieving activity: {ex.Message}");
}
```

### Credential Formats

#### Individual Fields (Recommended)
Set these environment variables separately:
- `COINBASE_PRIME_ACCESS_KEY`: Your API access key
- `COINBASE_PRIME_PASSPHRASE`: Your API passphrase
- `COINBASE_PRIME_SIGNING_KEY`: Your API signing key (private key)

#### JSON Format (Backward Compatible)
Alternatively, use a single JSON string in `COINBASE_PRIME_CREDENTIALS`:
```json
{
  "accessKey": "your_access_key",
  "passphrase": "your_passphrase",
  "signingKey": "your_signing_key"
}
```

## Running Examples

For examples of how to use the client, see the files in the [`examples`](src/CoinbaseSdk/PrimeExample/examples/) directory.

### Prerequisites

1. **Set up your environment:**
   ```bash
   # Copy and configure the environment file
   cp .env.example .env
   # Edit .env with your credentials and IDs
   ```

2. **Install the `dotnet-script` global tool:**
   ```bash
   dotnet tool install -g dotnet-script
   ```

3. **Configure required values in `.env`:**
   - **Credentials** (choose one format):
     - Individual fields: `COINBASE_PRIME_ACCESS_KEY`, `COINBASE_PRIME_PASSPHRASE`, `COINBASE_PRIME_SIGNING_KEY`
     - OR JSON format: `COINBASE_PRIME_CREDENTIALS`
   - **Account IDs**:
     - `COINBASE_PRIME_PORTFOLIO_ID`: Your portfolio ID (for portfolio-scoped examples)
     - `COINBASE_PRIME_ENTITY_ID`: Your entity ID (for entity-scoped examples)

### Running Individual Examples

Each example is a standalone C# script file (`.csx`) that can be executed directly:

```bash
# The examples automatically load .env files when present
# Activities examples
dotnet script src/CoinbaseSdk/PrimeExample/examples/activities/GetActivity.csx <activity-id>

# Futures examples
dotnet script src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmMarginCallDetails.csx
dotnet script src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmRiskLimits.csx

# Orders examples
dotnet script src/CoinbaseSdk/PrimeExample/examples/orders/GetOrderEditHistory.csx

# Wallets examples
dotnet script src/CoinbaseSdk/PrimeExample/examples/wallets/CreateWalletDepositAddress.csx
dotnet script src/CoinbaseSdk/PrimeExample/examples/wallets/ListWalletAddresses.csx
```

