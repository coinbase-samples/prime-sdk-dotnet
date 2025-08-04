# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is the **Coinbase Prime .NET SDK**, a C# library for interacting with the Coinbase Prime REST APIs. The SDK is structured as a single .NET 8.0 project that provides comprehensive access to all Prime API endpoints.

## Build and Development Commands

### Build
```bash
dotnet build prime-sdk-dotnet.sln
```

### Run Examples
Install dotnet-script tool first:
```bash
dotnet tool install -g dotnet-script
```

Run individual examples:
```bash
dotnet script src/CoinbaseSdk/PrimeExample/examples/activities/GetActivity.csx <activity-id>
dotnet script src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmMarginCallDetails.csx
dotnet script src/CoinbaseSdk/PrimeExample/examples/orders/GetOrderEditHistory.csx
dotnet script src/CoinbaseSdk/PrimeExample/examples/wallets/CreateWalletDepositAddress.csx
```

### Code Analysis
The project uses StyleCop analyzers with custom rules defined in `src/StyleCopRules.ruleset`. Many documentation and formatting rules are disabled to reduce noise.

## Architecture

### Core Structure
- **Main SDK**: `src/CoinbaseSdk/Prime/` - Contains all service implementations and models
- **Examples**: `src/CoinbaseSdk/PrimeExample/examples/` - Standalone .csx scripts demonstrating API usage
- **API Spec**: `apiSpec/prime-public-spec.yaml` - OpenAPI specification for reference

### Service Architecture
Each Prime API domain has its own service with a consistent pattern:
- **Interface**: `I{Domain}Service.cs` - Service contract
- **Implementation**: `{Domain}Service.cs` - Service implementation
- **Models**: Request/Response classes following the pattern `{Action}{Domain}Request.cs` and `{Action}{Domain}Response.cs`

### Key Services
- **ActivitiesService**: Portfolio and entity activity tracking
- **OrdersService**: Order management, quotes, and fills
- **TransactionsService**: Transfers, withdrawals, conversions
- **WalletsService**: Wallet management and deposit addresses
- **BalancesService**: Portfolio and wallet balance queries
- **PortfoliosService**: Portfolio information and management
- **FuturesService**: FCM operations and margin management

### Client Infrastructure
- **CoinbasePrimeClient**: Main client class extending CoinbaseClient from Core SDK
- **Authentication**: Uses CoinbaseCredentials with access key, passphrase, and signing key
- **Base URL**: `api.prime.coinbase.com/v1`

### Model Organization
All data models are in `src/CoinbaseSdk/Prime/model/` directory with clear naming:
- Enums use PascalCase (e.g., `ActivityType`, `OrderStatus`)
- Request/Response classes follow consistent naming patterns
- Models support JSON serialization for API communication

### Dependencies
- **CoinbaseSdk.Core**: Provides base client, credentials, and HTTP utilities
- **StyleCop.Analyzers**: Code style enforcement
- **Microsoft.SourceLink.GitHub**: Source debugging support

## Environment Variables for Examples
- `COINBASE_PRIME_CREDENTIALS`: JSON credentials object
- `COINBASE_PRIME_PORTFOLIO_ID`: Portfolio ID for portfolio-scoped examples  
- `COINBASE_PRIME_ENTITY_ID`: Entity ID for entity-scoped examples

## Development Notes
- Target framework: .NET 8.0
- Nullable reference types enabled
- Treats warnings as errors
- Uses embedded debug symbols
- Package validation enabled