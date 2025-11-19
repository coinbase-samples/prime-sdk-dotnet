# Coinbase Prime .NET SDK

## Overview

The _Coinbase Prime .NET SDK_ is a sample library that demonstrates the structure of a [Coinbase Prime](https://prime.coinbase.com/) driver for the [REST APIs](https://docs.cdp.coinbase.com/prime/reference).

## License

The _Coinbase Prime .NET SDK_ sample library is free and open source and released under the [Apache License, Version 2.0](LICENSE).

The application and code are only available for demonstration purposes.

## Installation

```bash
dotnet add package CoinbaseSdk.Prime --version x.y.z
```

## Configuration

Create a `.env` file with your API credentials:

```bash
cp .env.example .env
```

Required environment variables:

```bash
PRIME_ACCESS_KEY=your-access-key
PRIME_PASSPHRASE=your-passphrase
PRIME_SIGNING_KEY=your-signing-key
```

## Usage

Initialize the client using environment variables:

```csharp
var client = CoinbasePrimeClient.FromEnv();
var activitiesService = new ActivitiesService(client);

var request = new GetActivityRequest(activityId);
var response = activitiesService.GetActivity(request);
```

## Examples

Run examples from the PrimeExample project:

```bash
# List available examples
dotnet run --project src/CoinbaseSdk/PrimeExample list

# Run examples via Program.cs
dotnet run --project src/CoinbaseSdk/PrimeExample ListPortfolios
dotnet run --project src/CoinbaseSdk/PrimeExample GetPortfolio --portfolioId <portfolio-id>
dotnet run --project src/CoinbaseSdk/PrimeExample ListAssets --entityId <entity-id>

# Run standalone file-based examples (.NET 10+)
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/activities/GetActivity.cs -- --activityId <activity-id>

# Or with executable permissions on Unix:
./src/CoinbaseSdk/PrimeExample/examples/activities/GetActivity.cs --activityId <activity-id>
```

Set optional environment variables for convenience:

```bash
PRIME_ENTITY_ID=your-entity-id
PRIME_PORTFOLIO_ID=your-portfolio-id
```

## JSON Serialization

The SDK now relies on the shared `CoinbaseSdk.Core.Serialization.JsonUtility` defaults for all request/response payloads. The defaults include camelCase property names, tolerant enum handling, and ISO-8601 timestamps powered by `PrimeJsonSerializerOptionsFactory`.

Most developers do not need to touch the serializer options. If you do need to customize serialization, register an override **before** constructing any Prime clients:

```csharp
JsonUtility.ConfigureDefaults(options =>
{
    options.PropertyNamingPolicy = null; // use PascalCase
    options.Converters.Add(new MyCustomConverter());
});
```

The Prime SDK will automatically pick up the updated defaults the next time you instantiate `CoinbasePrimeClient` (or any other Coinbase SDK built on `CoinbaseSdk.Core`).

