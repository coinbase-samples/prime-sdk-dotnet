# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Development Commands

### Building
```bash
dotnet build prime-sdk-dotnet.sln
```

### Running Examples
```bash
dotnet run --project src/CoinbaseSdk/PrimeExample/CoinbaseSdk.PrimeExample.csproj
```

### Testing
Note: Tests require environment variables COINBASE_PRIME_CREDENTIALS (JSON with accessKey, passphrase, signingKey) and COINBASE_PRIME_PORTFOLIO_ID.

### Code Analysis
The project uses StyleCop analyzers with TreatWarningsAsErrors=true in the main SDK project.

## Architecture Overview

### Project Structure
- **CoinbaseSdk.Prime**: Main SDK library targeting .NET 8.0
- **CoinbaseSdk.PrimeExample**: Console application demonstrating SDK usage
- **prime-sdk-dotnet.sln**: Solution file containing both projects

### Core Architecture
The SDK follows a service-oriented architecture with clear separation of concerns:

1. **Client Layer** (`client/`): `CoinbasePrimeClient` extends `CoinbaseClient` from Core SDK, handles HTTP requests and authentication
2. **Service Layer** (various `/` folders): Each API domain has its own service (e.g., `WalletsService`, `OrdersService`, `PortfoliosService`)
3. **Model Layer** (`model/`): Request/response DTOs and domain models
4. **Base Classes**: Services inherit from `CoinbaseService` and implement domain-specific interfaces

### Service Pattern
All services follow consistent patterns:
- Implement both sync and async methods (e.g., `CreateOrder` and `CreateOrderAsync`)
- Use constructor injection for client dependency: `public class WalletsService(ICoinbaseClient client)`
- Return strongly-typed response objects
- Accept request objects with validation

### API Domains
- **Activities**: Transaction and portfolio activity tracking
- **AddressBook**: Wallet address management
- **Allocations**: Trade allocation and netting
- **Assets**: Asset information and metadata
- **Balances**: Portfolio, wallet, and entity balance queries
- **Commission**: Fee and commission calculations
- **Financing**: Margin, credit, and trade finance operations
- **Futures**: FCM balance and position management
- **Invoice**: Billing and invoice management
- **OnchainAddressBook**: Blockchain address management
- **Orders**: Order creation, management, and fills
- **PaymentMethods**: Payment method configuration
- **Portfolios**: Portfolio information and management
- **Positions**: Position tracking and aggregation
- **Products**: Trading product information
- **Staking**: Cryptocurrency staking operations
- **Transactions**: Transaction processing and history
- **Users**: User and entity management
- **Wallets**: Wallet creation and management

### Dependencies
- **CoinbaseSdk.Core**: Provides base client functionality, credentials, HTTP handling, and serialization
- **StyleCop.Analyzers**: Code style enforcement
- **Microsoft.SourceLink.GitHub**: Source linking for debugging

### Key Files
- `src/CoinbaseSdk/Prime/client/CoinbasePrimeClient.cs:25`: Main client implementation
- `src/CoinbaseSdk/Prime/model/`: All domain models and DTOs
- Service files follow pattern: `src/CoinbaseSdk/Prime/{domain}/{Domain}Service.cs`

## OpenAPI Specification & Code Generation

### OpenAPI Source
- **Location**: `apiSpec/prime-public-spec.yaml`
- **Format**: OpenAPI 3.0.1 specification with 7,353 lines
- **Server**: https://api.prime.coinbase.com/
- **Version**: 0.1

### Code Generation Tools
- **Endpoint Analysis**: `tools/code-generation/extract_endpoints_v2.py`
  - Catalogs all 77 OpenAPI endpoints with complete metadata
  - Use for gap analysis and implementation planning
  - Run: `python3 tools/code-generation/extract_endpoints_v2.py`
- **Tool Documentation**: `tools/README.md`
  - Instructions for using generation tools
  - Maintenance guidelines for spec updates

### API Structure Analysis
The OpenAPI spec defines endpoints across multiple domains with consistent patterns:

#### Endpoint Patterns
1. **Entity-scoped endpoints**: `/v1/entities/{entity_id}/*`
2. **Portfolio-scoped endpoints**: `/v1/portfolios/{portfolio_id}/*` 
3. **General endpoints**: `/v1/{resource}` or `/v1/{resource}/{id}`

#### Operation ID Mapping
All endpoints use consistent `operationId` pattern: `PrimeRESTAPI_{MethodName}`
Examples:
- `PrimeRESTAPI_GetActivity` → `GetActivity`
- `PrimeRESTAPI_CreateAllocation` → `CreateAllocation`  
- `PrimeRESTAPI_ListEntityActivities` → `ListEntityActivities`

#### Service Domain Mapping
Tags in OpenAPI spec map directly to service domains:
- `Activities` → `ActivitiesService`
- `Allocations` → `AllocationsService`
- `Financing` → `FinancingService`
- `Futures` → `FuturesService`
- `Invoice` → `InvoiceService`
- `Orders` → `OrdersService`
- `Portfolios` → `PortfoliosService`
- `Positions` → `PositionsService`
- `Transactions` → `TransactionsService`
- `Wallets` → `WalletsService`

### Code Generation Guidelines

#### Service Method Generation
Each OpenAPI operation should generate:
1. **Sync method**: `{OperationName}({RequestType} request, CallOptions? options = null)`
2. **Async method**: `{OperationName}Async({RequestType} request, CallOptions? options = null, CancellationToken cancellationToken = default)`

#### Request/Response Model Generation
- **Request models**: Path parameters and query parameters → `{OperationName}Request` 
- **Response models**: Response schema → `{OperationName}Response`
- **Nested models**: Component schemas → individual model classes

#### HTTP Method Mapping
- `GET` → Service method returning response object
- `POST` → Service method accepting request body + response object  
- `PUT/PATCH` → Service method accepting request body + response object
- `DELETE` → Service method with optional request/response

#### Naming Conventions
- **Classes**: PascalCase (e.g., `CreateAllocationRequest`)
- **Properties**: PascalCase with JSON attribute mapping (e.g., `[JsonPropertyName("portfolio_id")] public string PortfolioId`)
- **Enums**: PascalCase values with string enum converter
- **Methods**: PascalCase matching operation name

#### File Organization
- **Services**: `src/CoinbaseSdk/Prime/{domain}/I{Domain}Service.cs` and `{Domain}Service.cs`
- **Models**: `src/CoinbaseSdk/Prime/{domain}/{ModelName}.cs` or `src/CoinbaseSdk/Prime/model/{ModelName}.cs`
- **Requests/Responses**: Co-located with service or in dedicated folders

### SDK Development Workflow
For adding new endpoints or updating existing ones:

1. **Analyze Current State**: Run `python3 tools/code-generation/extract_endpoints_v2.py` to catalog all OpenAPI endpoints
2. **Identify Gaps**: Compare tool output against existing service files in `src/CoinbaseSdk/Prime/`
3. **Follow Patterns**: Use the code generation guidelines below for consistent implementation
4. **Validate**: Ensure `dotnet build prime-sdk-dotnet.sln` passes after changes

### Missing Endpoints (Potential Generation Targets)
Use the endpoint analysis tool to identify:
- Missing CRUD operations not yet implemented
- New API endpoints added to the OpenAPI spec
- Parameter variations or new request/response models
- Endpoints with implementation discrepancies