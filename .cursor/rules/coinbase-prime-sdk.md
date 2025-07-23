# Coinbase Prime .NET SDK Development Guidelines

## Project Overview

This is a .NET 8.0 SDK for Coinbase Prime REST APIs following a service-oriented architecture pattern. The SDK provides both synchronous and asynchronous methods for all API operations.

## Development Commands

### Building
```bash
dotnet build prime-sdk-dotnet.sln
```

### Running Examples
```bash
dotnet run --project src/CoinbaseSdk/PrimeExample/CoinbaseSdk.PrimeExample.csproj
```

### Code Analysis
The project enforces StyleCop rules with `TreatWarningsAsErrors=true`. Ensure all code passes analysis before committing.

## Coding Conventions

### Service Implementation Pattern
- Use primary constructor syntax: `public class WalletsService(ICoinbaseClient client) : CoinbaseService(client), IWalletsService`
- All services must inherit from `CoinbaseService` and implement a domain-specific interface
- Implement both sync and async versions of all methods:
  ```csharp
  public CreateWalletResponse CreateWallet(CreateWalletRequest request, CallOptions? options = null)
  public Task<CreateWalletResponse> CreateWalletAsync(CreateWalletRequest request, CallOptions? options = null, CancellationToken cancellationToken = default)
  ```

### Request/Response Pattern
- All API operations use strongly-typed request and response objects
- Request objects contain path parameters and query/body data
- Response objects wrap the API response data
- Use nullable reference types consistently

### HTTP Method Mapping
- Follow RESTful conventions in service methods
- Use appropriate HTTP status codes in expected status arrays:
  ```csharp
  [HttpStatusCode.Created, HttpStatusCode.OK]  // For POST operations
  [HttpStatusCode.OK]                          // For GET operations
  ```

### File Organization
- Each API domain has its own folder under `src/CoinbaseSdk/Prime/`
- Services go in domain folders: `{domain}/{Domain}Service.cs`
- Models go in shared `model/` folder
- Request/Response classes in domain folders with descriptive names

## Architecture Patterns

### Dependency Injection
- Services use constructor injection for ICoinbaseClient dependency
- No service locator or static dependencies
- Follow single responsibility principle per service

### Error Handling
- Client handles HTTP errors and converts to typed exceptions
- Services should not catch exceptions unless adding domain-specific context
- Use nullable return types where API may return null

### Async/Await
- Always provide both sync and async versions
- Use `CancellationToken` parameter in async methods
- Follow TAP (Task-based Asynchronous Pattern) guidelines

## API Domain Structure

The SDK is organized into these domains:
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

## Security Guidelines

- Never log or expose credentials, API keys, or sensitive data
- Use environment variables for configuration in examples
- Validate input parameters in request objects
- Follow secure coding practices for financial APIs

## Testing Guidelines

- Examples require `COINBASE_PRIME_CREDENTIALS` (JSON) and `COINBASE_PRIME_PORTFOLIO_ID` environment variables
- Test methods should follow domain-specific patterns
- Use descriptive test names that explain the scenario

## Dependencies

- **CoinbaseSdk.Core**: Provides base client, credentials, HTTP handling, serialization
- **StyleCop.Analyzers**: Code style enforcement
- **Microsoft.SourceLink.GitHub**: Source linking for debugging

## OpenAPI Specification & Code Generation

### OpenAPI Source
- **Location**: `apiSpec/prime-public-spec.yaml`
- **Format**: OpenAPI 3.0.1 specification (7,353 lines)
- **Base URL**: `https://api.prime.coinbase.com/`

### Development Tools
- **Endpoint Analysis Tool**: `tools/code-generation/extract_endpoints_v2.py`
  - Run before making changes to understand current OpenAPI spec coverage
  - Outputs comprehensive endpoint catalog for gap analysis
  - Use: `python3 tools/code-generation/extract_endpoints_v2.py`
- **Tool Documentation**: `tools/README.md` contains usage instructions and maintenance guidelines

### Code Generation from OpenAPI

#### Operation ID to Method Mapping
- OpenAPI operationId format: `PrimeRESTAPI_{MethodName}`
- Strip prefix to get method name: `PrimeRESTAPI_GetActivity` → `GetActivity`
- Generate both sync and async versions

#### Service Generation Pattern
When generating from OpenAPI spec:

1. **Interface Generation**:
```csharp
public interface I{Domain}Service
{
    public {ResponseType} {MethodName}(
        {RequestType} request,
        CallOptions? options = null);

    public Task<{ResponseType}> {MethodName}Async(
        {RequestType} request,
        CallOptions? options = null,
        CancellationToken cancellationToken = default);
}
```

2. **Implementation Generation**:
```csharp
public class {Domain}Service(ICoinbaseClient client) : CoinbaseService(client), I{Domain}Service
{
    public {ResponseType} {MethodName}({RequestType} request, CallOptions? options = null)
    {
        return {HttpMethod}<{RequestType}, {ResponseType}>(request, "{endpoint}", options);
    }

    public Task<{ResponseType}> {MethodName}Async({RequestType} request, CallOptions? options = null, CancellationToken cancellationToken = default)
    {
        return {HttpMethod}Async<{RequestType}, {ResponseType}>(request, "{endpoint}", options, cancellationToken);
    }
}
```

#### Model Generation Patterns

1. **Request Models**:
```csharp
public class {OperationName}Request
{
    [JsonPropertyName("portfolio_id")]
    public required string PortfolioId { get; set; }

    [JsonPropertyName("optional_param")]
    public string? OptionalParam { get; set; }
}
```

2. **Response Models**:
```csharp
public class {OperationName}Response
{
    [JsonPropertyName("data")]
    public {DataType}? Data { get; set; }

    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }
}
```

3. **Enums**:
```csharp
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum {EnumName}
{
    [JsonPropertyName("ENUM_VALUE")]
    EnumValue,
}
```

#### HTTP Method Mapping
- `GET` → `Get<T>()` / `GetAsync<T>()`
- `POST` → `Post<TRequest, TResponse>()` / `PostAsync<TRequest, TResponse>()`
- `PUT` → `Put<TRequest, TResponse>()` / `PutAsync<TRequest, TResponse>()`
- `DELETE` → `Delete<T>()` / `DeleteAsync<T>()`

#### Endpoint URL Patterns
- Entity-scoped: `/v1/entities/{entity_id}/{resource}`
- Portfolio-scoped: `/v1/portfolios/{portfolio_id}/{resource}`
- General: `/v1/{resource}` or `/v1/{resource}/{id}`

#### Domain Tag Mapping
OpenAPI tags map to service domains:
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

#### File Organization for Generated Code
- **Services**: `src/CoinbaseSdk/Prime/{domain}/I{Domain}Service.cs` and `{Domain}Service.cs`
- **Models**: `src/CoinbaseSdk/Prime/model/{ModelName}.cs` or co-located with service
- **Required Imports**:
```csharp
using System.Text.Json.Serialization;
using CoinbaseSdk.Core.Http;
using CoinbaseSdk.Core.Service;
```

## Key Implementation Notes

- Client base path: `api.prime.coinbase.com/v1`
- All services extend `CoinbaseService` base class
- JSON serialization handled by Core SDK utilities
- HTTP client management handled by base `CoinbaseClient`
- Path construction follows pattern: `/portfolios/{portfolioId}/...`

## AI Agent Workflow for SDK Updates

When updating or extending this SDK:

1. **Pre-Analysis**: 
   - Run `python3 tools/code-generation/extract_endpoints_v2.py` to understand current OpenAPI coverage
   - Compare output against existing services in `src/CoinbaseSdk/Prime/`

2. **Implementation**:
   - Follow the service and model generation patterns above
   - Use consistent naming conventions and file organization
   - Ensure StyleCop compliance with single-type-per-file rule

3. **Validation**:
   - Build solution: `dotnet build prime-sdk-dotnet.sln`
   - All new files should use Copyright 2025-present for new work
   - Verify all endpoints follow the established patterns

4. **Tool Maintenance**:
   - Update `tools/code-generation/extract_endpoints_v2.py` when OpenAPI spec changes
   - Keep endpoint definitions synchronized with actual API specification
   - Update tool documentation in `tools/README.md` as needed