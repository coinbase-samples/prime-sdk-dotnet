# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Development Commands

### Building
```bash
dotnet build prime-sdk-dotnet.sln
```

### Running Examples

#### Prerequisites
First, build the SDK to generate the local DLL:
```bash
dotnet build src/CoinbaseSdk/Prime/CoinbaseSdk.Prime.csproj
```

#### Running Individual Example Scripts
Each example is a standalone dotnet-script (.csx) file that references the locally built DLL:
```bash
# Navigate to the example directory
cd src/CoinbaseSdk/PrimeExample/examples/activities

# Run a specific example
dotnet script GetActivity.csx <activity-id>
```

#### Available Examples
- `activities/GetActivity.csx` - Retrieve activity by ID
- `futures/GetFcmMarginCallDetails.csx` - Get FCM margin call details  
- `futures/GetFcmRiskLimits.csx` - Get FCM risk limits
- `orders/GetOrderEditHistory.csx` - Get order edit history
- `transactions/GetTransactionByTransactionId.csx` - Get transaction by ID
- `transactions/ListPortfolioTransactions.csx` - List portfolio transactions
- `wallets/CreateWalletDepositAddress.csx` - Create wallet deposit address
- `wallets/ListWalletAddresses.csx` - List wallet addresses

### Testing
Note: Tests require environment variables COINBASE_PRIME_CREDENTIALS (JSON with accessKey, passphrase, signingKey) and COINBASE_PRIME_PORTFOLIO_ID.

### Code Analysis
The project uses StyleCop analyzers with TreatWarningsAsErrors=true in the main SDK project. StyleCop rules are configured in `src/StyleCopRules.ruleset` with many documentation-related rules disabled.

## Architecture Overview

### Project Structure
- **CoinbaseSdk.Prime**: Main SDK library targeting .NET 8.0
- **PrimeExample/examples/**: Standalone dotnet-script examples demonstrating SDK usage
- **prime-sdk-dotnet.sln**: Solution file

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

### AI Agent Code Generation
- **Runtime Analysis**: AI agents analyze the OpenAPI specification directly at runtime
- **Tag-Based Generation**: Endpoints are processed by OpenAPI tags to generate corresponding service methods and models
- **Dynamic Approach**: No static analysis tools needed - AI agents work directly with the specification
- **Guidelines**: All AI agent workflows and code generation patterns are documented in this file

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

#### SDK Naming Conventions vs OpenAPI Spec
The SDK uses intentional naming differences from the OpenAPI specification for better .NET conventions:

**Collection Operations - "List" vs "Get"**:
- OpenAPI: `PrimeRESTAPI_GetPortfolios` → SDK: `ListPortfolios`
- OpenAPI: `PrimeRESTAPI_GetEntityActivities` → SDK: `ListEntityActivities`  
- OpenAPI: `PrimeRESTAPI_GetPortfolioActivities` → SDK: `ListActivities`
- OpenAPI: `PrimeRESTAPI_GetEntityAssets` → SDK: `ListAssets`
- OpenAPI: `PrimeRESTAPI_GetEntityBalances` → SDK: `ListEntityBalances`
- OpenAPI: `PrimeRESTAPI_GetPortfolioProducts` → SDK: `ListPortfolioProducts`

**Individual Resource Operations - Simplified Names**:
- OpenAPI: `PrimeRESTAPI_GetOrderByOrderId` → SDK: `GetOrder`
- OpenAPI: `PrimeRESTAPI_GetWalletById` → SDK: `GetWallet`
- OpenAPI: `PrimeRESTAPI_GetPortfolioByPortfolioId` → SDK: `GetPortfolio`

**Rationale**:
- **"List" prefix**: Clearly indicates collection/array returns, following .NET conventions
- **Simplified individual resource names**: Removes redundant "ById" suffixes since the parameter context makes it clear
- **Consistency**: All collection operations use "List", all individual operations use "Get"
- **Backwards compatibility**: Old method names remain as deprecated alternatives

### Code Generation Guidelines

#### Speed Optimization for Object Generation
**PRIORITY: Execute quickly, validate afterwards**

**Parallelized Fast Generation Strategy:**
1. **Analyze entire OpenAPI spec** first to identify all missing components
2. **Plan batches by domain** - group related models/services together (e.g., FCM, Staking, Orders)
3. **Execute multiple Task agents in parallel** - generate 5-10 models simultaneously across domains
4. **NO premature validation** - generate ALL files first, validate once at the end
5. **Template-based rapid creation** - copy existing files and modify rather than create from scratch
6. **Single build validation** - run `dotnet build` only after all generation is complete

**Parallelization Implementation:**
```
// Execute multiple Task agents simultaneously in a single message
Task 1: Generate FCM models (3 files)
Task 2: Generate Staking models (5 files) 
Task 3: Generate Invoice models (5 files)
Task 4: Generate Margin models (7 files)
Task 5: Generate Misc models (7 files)
Task 6: Generate Wallet models (3 files)
Task 7: Generate Order models (3 files)
Task 8: Generate Service A
Task 9: Generate Service B
```

**Fast Template-Based Approach:**
1. **Use existing files as templates** - copy/modify instead of creating from scratch
2. **Batch creation** - generate 5-10 files rapidly per task
3. **Focus on compilation first** - get objects created and building, refine schema compliance later
4. **Template pattern**: Copy existing request/response pair → Search/replace service names → Adjust properties

**Template Locations:**
- Request template: `activities/ListActivitiesRequest.cs` 
- Response template: `activities/ListActivitiesResponse.cs`
- Single object response: `activities/GetPortfolioActivityResponse.cs`

**Rapid Generation Process:**
1. Identify ALL missing objects from OpenAPI analysis (not build errors)
2. Plan parallelized batches by domain (FCM, Staking, Orders, etc.)
3. Use template files for structure (copyright, namespace, builder pattern)
4. Launch multiple generation tasks simultaneously 
5. Run `dotnet build` ONCE after all generation completes
6. Fix any compilation errors rapidly in batch

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
- **Enums**: Use standard OpenAPI names directly without JsonPropertyName (e.g., `FCM_MARGIN_CALL_STATE_CLOSED`) - JsonStringEnumConverter handles serialization automatically
- **Methods**: PascalCase matching operation name

#### File Organization
- **Services**: `src/CoinbaseSdk/Prime/{domain}/I{Domain}Service.cs` and `{Domain}Service.cs`
- **Models**: `src/CoinbaseSdk/Prime/{domain}/{ModelName}.cs` or `src/CoinbaseSdk/Prime/model/{ModelName}.cs`
- **Requests/Responses**: Co-located with service or in dedicated folders

### AI Agent Development Workflow
For adding new endpoints or updating existing ones using AI agents:

1. **Analyze OpenAPI Specification**: AI agent reads `apiSpec/prime-public-spec.yaml` directly
2. **Process by Tags**: Group endpoints by OpenAPI tags (e.g., Activities, Wallets, Orders)
3. **Generate Service Methods**: Create methods following SDK naming conventions (List vs Get)
4. **Generate Models**: Extract and create request/response models from OpenAPI schemas
5. **Apply Patterns**: Use established SDK patterns for consistency
6. **Validate**: Ensure `dotnet build prime-sdk-dotnet.sln` passes after generation

### AI Agent Code Generation Guidelines

#### Full Coverage Requirement
**IMPORTANT**: Any code generation request intends for **100% coverage** of the OpenAPI specification. This includes:

- **All endpoints**: Every operation defined in the OpenAPI spec must be implemented
- **All models**: Every schema in `components.schemas` must have corresponding C# classes
- **All enums**: Every enum type must be properly implemented with string converters
- **All request/response types**: Complete coverage of all API input/output models
- **All service domains**: Every OpenAPI tag must have a corresponding service

The SDK should achieve complete feature parity with the OpenAPI specification. When a generation request is made, ALL missing models, endpoints, and changes must be generated - no prioritization or partial implementation.

#### Coverage Validation
When generating or updating code:
1. **Endpoint Coverage**: Verify all `operationId` entries from OpenAPI spec are implemented
2. **Model Coverage**: Verify all `components.schemas` entries have corresponding C# classes
3. **Service Coverage**: Verify all OpenAPI tags have corresponding service classes
4. **Missing Features**: Identify and implement any missing Web3, NFT, staking, or advanced trading features

#### Tag-Based Endpoint Processing
- **Read OpenAPI tags**: Each tag represents a service domain
- **Extract operations**: Get all operations for each tag
- **Apply naming conventions**: Transform OpenAPI operation names to SDK method names
- **Generate service interface**: Create I{Service}Service interface
- **Generate service implementation**: Create {Service}Service class

#### Model Generation from Schemas
- **Extract schemas**: Process `components.schemas` from OpenAPI spec
- **Generate model classes**: Create C# classes with proper JSON property mapping
- **Handle enums**: Convert OpenAPI enums to C# enums with JsonStringEnumConverter
- **Builder patterns**: Add builder classes for complex models
- **Validation**: Include required field validation and proper nullable types