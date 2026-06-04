# Coinbase Prime .NET SDK

## Overview

The _Coinbase Prime .NET SDK_ is a sample library that demonstrates the structure of a [Coinbase Prime](https://prime.coinbase.com/) driver for the [REST APIs](https://docs.cdp.coinbase.com/prime/reference).

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) or later
- Coinbase Prime API credentials (access key, passphrase, and signing key), which can be created in the Prime web console under **Settings > APIs**

## Installation

```bash
dotnet add package CoinbaseSdk.Prime --version x.y.z
```

## Configuration

Coinbase Prime API credentials can be provided via environment variables. Copy the example file and populate your credentials:

```bash
cp .env.example .env
```

Required environment variables:

```bash
PRIME_ACCESS_KEY=your-access-key
PRIME_PASSPHRASE=your-passphrase
PRIME_SIGNING_KEY=your-signing-key
```

Optional environment variables used by examples:

```bash
PRIME_ENTITY_ID=your-entity-id
PRIME_PORTFOLIO_ID=your-portfolio-id
```

Entity ID can be retrieved by calling [Get Portfolio](https://docs.cdp.coinbase.com/prime/reference/primerestapi_getportfolio).

## Usage

Initialize the client using environment variables (automatically loads a `.env` file if present):

```csharp
var client = CoinbasePrimeClient.FromEnv();
```

Or construct credentials manually:

```csharp
var credentials = new CoinbaseCredentials(accessKey, passphrase, signingKey);
var client = new CoinbasePrimeClient(credentials);
```

Once the client is initialized, instantiate a service to make the desired call. For example, to list portfolios:

```csharp
var portfoliosService = new PortfoliosService(client);
var response = portfoliosService.ListPortfolios(new ListPortfoliosRequest());
```

Or to list activities for a portfolio using the request builder:

```csharp
var activitiesService = new ActivitiesService(client);

var request = new ListActivitiesRequest.ListActivitiesRequestBuilder()
    .WithPortfolioId(portfolioId)
    .Build();

var response = activitiesService.ListActivities(request);
```

## OpenAPI spec

The Prime public OpenAPI specification is vendored at `apiSpec/prime-public-api-spec.yaml`. To refresh it:

```bash
make fetch-spec
```

## Build

To build the sample library, ensure that [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) or later is installed and then run:

```bash
dotnet build prime-sdk-dotnet.sln
```

## Examples

Each example under `src/CoinbaseSdk/PrimeExample/examples/` is a standalone `.cs` file executable directly with .NET 10+:

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/portfolios/ListPortfolios.cs
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/portfolios/GetPortfolio.cs -- --portfolioId <portfolio-id>
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/activities/ListActivities.cs -- --portfolioId <portfolio-id>
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/balances/ListEntityBalances.cs -- --entityId <entity-id>
```

On Unix systems with executable permissions set, examples can also be invoked directly:

```bash
./src/CoinbaseSdk/PrimeExample/examples/portfolios/ListPortfolios.cs
```

Available example categories: `activities`, `addressbook`, `advancedtransfer`, `allocations`, `assets`, `balances`, `commission`, `financing`, `futures`, `invoice`, `onchainaddressbook`, `orders`, `paymentmethods`, `portfolios`, `positions`, `products`, `staking`, `transactions`, `users`, `wallets`.

## 🚨 Security and Bug Reports

If you discover a security vulnerability within this SDK, please see our [Security Policy](SECURITY.md) for disclosure information.

## 📧 Contact

- [GitHub Issues](https://github.com/coinbase-samples/prime-sdk-dotnet/issues)

## License

The _Coinbase Prime .NET SDK_ sample library is free and open source and released under the [Apache License, Version 2.0](LICENSE).

The application and code are only available for demonstration purposes.
