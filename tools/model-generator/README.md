# Model Generator

Generates C# model classes and enums from the OpenAPI specification.

## Purpose

Generates domain models (`CoinbaseSdk.Prime.Model`) and enums (`CoinbaseSdk.Prime.Model.Enums`) from the OpenAPI spec. Request/Response classes are excluded and maintained separately in service packages.

## Architecture

The generator implements a three-stage pipeline:

1. **OpenAPI Generator** (`OpenApiGenerator.cs`) - Generates raw POJOs from the OpenAPI spec using the OpenAPI Generator CLI tool
2. **Post-Processor** (`PostProcessor.cs`) - Applies transformations (Web3→Onchain renaming, schema filtering, file routing)
3. **Custom Templates** - Mustache templates generate models with builder pattern, license headers, and proper annotations

## Usage

### Prerequisites

- .NET 9.0+
- OpenAPI Generator CLI installed:
  - `npm install -g @openapitools/openapi-generator-cli`
  - or `brew install openapi-generator`
- Network access to fetch the OpenAPI spec from `https://api.prime.coinbase.com/v1/openapi.yaml`

### Build

```bash
cd tools/model-generator
dotnet build
```

### Generate Models

```bash
cd tools/model-generator
dotnet run
```

Or from the project root:

```bash
dotnet run --project tools/model-generator
```

## Generated Code

Models:
- Apache 2.0 license headers
- `System.Text.Json.Serialization` `[JsonPropertyName]` annotations
- Builder pattern
- Standard getters/setters (`Is` prefix for booleans)
- Parameterless and builder constructors

Enums: `UPPERCASE_WITH_UNDERSCORES` naming.

## Configuration

### Input
- OpenAPI spec: Fetched automatically from `https://api.prime.coinbase.com/v1/openapi.yaml` during generation

### Output
- Models: `src/CoinbaseSdk/Prime/model/`
- Enums: `src/CoinbaseSdk/Prime/model/enums/`

### Filtering

The `.openapi-generator-ignore` file excludes:
- `*Request.cs` - service-specific requests
- `*Response.cs` - service-specific responses
- `Google*.cs` - infrastructure types
- `RFQ.cs` - inline schemas
- `*AllOf*.cs` - composition artifacts

## Technical Details

### Dependencies
- OpenAPI Generator CLI (code generation from OpenAPI spec)
- System.Text.Json (JSON annotations)
- YamlDotNet (YAML parsing)

### Transformations

The post-processor applies:
- **Web3→Onchain**: Renames classes/fields containing "Web3" to "Onchain" while preserving `[JsonPropertyName]` mappings
- **Schema filtering**: Skips schemas matching ignore patterns
- **Package routing**: Places enums in `model/enums/`, models in `model/`
- **Full regeneration**: Processes all models from the OpenAPI spec, updating existing files and creating new ones as needed

## Workflow

1. Run generator: `dotnet run --project tools/model-generator`
2. Review generated files
3. Build project: `dotnet build`
4. Run tests: `dotnet test`
5. Commit changes
