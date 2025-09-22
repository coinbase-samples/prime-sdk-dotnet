# CLAUDE.md

@./claude/AI_GENERATION_GUIDELINES.md
@./claude/SDK_GENERATION_TDD.md

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

### All Services (Equal Priority)
All services in this SDK are equally important and follow the same architectural patterns. Each service provides comprehensive access to its respective API domain:

- **ActivitiesService**: Portfolio and entity activity tracking
- **AddressBookService**: Address book management
- **AllocationsService**: Portfolio allocation management
- **AssetsService**: Asset information and management
- **BalancesService**: Portfolio and wallet balance queries
- **CommissionService**: Commission and fee information
- **FinancingService**: Margin, credit, and financing operations
- **FuturesService**: FCM operations and margin management
- **InvoiceService**: Invoice management
- **OnchainAddressBookService**: Onchain address group management
- **OrdersService**: Order management, quotes, and fills
- **PaymentMethodsService**: Payment method management
- **PortfoliosService**: Portfolio information and management
- **PositionsService**: Position tracking and management
- **ProductsService**: Product information
- **StakingService**: Staking and delegation operations
- **TransactionsService**: Transfers, withdrawals, conversions
- **UsersService**: User management
- **WalletsService**: Wallet management and deposit addresses

**Note**: When performing validation or analysis tasks, ALL services must be treated with equal importance. There are no "critical" or "key" services - every service requires the same level of attention and validation.

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
