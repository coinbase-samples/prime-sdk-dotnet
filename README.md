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

The SDK relies entirely on the shared `CoinbaseSdk.Core.Serialization.JsonUtility` defaults for all request/response payloads. Those defaults already include camelCase property names, tolerant enum handling (via `NullOnUnknownEnumConverter`), and ISO-8601 timestamps backed by `UtcIso8601DateTimeOffsetConverter`.

Global overrides have been removed to keep the serialization surface deterministic across packages. If you need custom behavior, create your own `JsonSerializerOptions` instance and pass it through a custom `IJsonUtility` implementation (or a derived `CoinbaseClient`) for your application-specific calls instead of mutating global state.

