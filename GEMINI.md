# Coinbase Prime .NET SDK Context

## Project Overview
This repository contains the **Coinbase Prime .NET SDK**, a library for interacting with the [Coinbase Prime REST APIs](https://docs.cdp.coinbase.com/prime/reference). It is a standard .NET solution targeting `net8.0`.

## Directory Structure

*   **`src/CoinbaseSdk/Prime/`**: The core SDK library. Contains services, models, and client configuration.
*   **`src/CoinbaseSdk/Prime.Tests/`**: xUnit test suite for the SDK.
*   **`src/CoinbaseSdk/PrimeExample/`**: A console application demonstrating SDK usage with various scenarios.
*   **`tools/model-generator/`**: A custom tool to generate C# models from the Coinbase Prime OpenAPI specification.

## Development Workflow

### Prerequisites
*   .NET 8.0 SDK
*   OpenAPI Generator CLI (for model generation): `npm install -g @openapitools/openapi-generator-cli` or `brew install openapi-generator`

### Common Commands

| Action | Command |
| :--- | :--- |
| **Restore Dependencies** | `dotnet restore prime-sdk-dotnet.sln` |
| **Build Solution** | `dotnet build prime-sdk-dotnet.sln` |
| **Run Tests** | `dotnet test src/CoinbaseSdk/Prime.Tests/CoinbaseSdk.Prime.Tests.csproj` |
| **List Examples** | `dotnet run --project src/CoinbaseSdk/PrimeExample list` |
| **Run Specific Example** | `dotnet run --project src/CoinbaseSdk/PrimeExample <CommandName>` (e.g., `ListPortfolios`) |
| **Generate Models** | `dotnet run --project tools/model-generator` |

### Configuration
*   **Environment Variables:** The SDK and examples use environment variables for credentials.
    *   Copy `.env.example` to `.env`.
    *   Required: `PRIME_ACCESS_KEY`, `PRIME_PASSPHRASE`, `PRIME_SIGNING_KEY`.
    *   Optional (for examples): `PRIME_ENTITY_ID`, `PRIME_PORTFOLIO_ID`.

## Code Conventions

*   **Style:** The project enforces coding style via **StyleCop**. Violations are treated as build errors.
    *   Use 4-space indentation.
    *   PascalCase for public members, camelCase for locals/parameters.
    *   Async methods must end with `Async`.
*   **Models:** Models are generated using the `tools/model-generator`. Do not manually modify generated files in `src/CoinbaseSdk/Prime/model/` unless you are updating the generator logic.
*   **Testing:**
    *   Use xUnit.
    *   Test files should mirror the source structure (e.g., `ActivitiesService` -> `ActivitiesServiceTests`).
    *   Use Moq for mocking dependencies.

## Key Files

*   **`prime-sdk-dotnet.sln`**: The main solution file.
*   **`AGENTS.md`**: Detailed guidelines for AI agents and developers regarding project structure and conventions.
*   **`src/CoinbaseSdk/Prime/CoinbaseSdk.Prime.csproj`**: Main project file defining dependencies (e.g., `CoinbaseSdk.Core`, `DotNetEnv`).
*   **`tools/model-generator/README.md`**: Documentation for the model generation process.
