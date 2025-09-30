# Coinbase Prime .NET SDK Model Generator

This tool automatically generates C# model classes from the OpenAPI specification for the Coinbase Prime REST API.

## Overview

The model generator follows a two-phase approach:

1. **Code Generation**: Uses OpenAPI Generator to create base C# models from the OpenAPI specification
2. **Post-Processing**: Applies .NET-specific transformations to match existing SDK patterns

## Generated Patterns

The tool generates models that match the existing SDK patterns exactly:

### Builder Pattern
- Generated for classes with 4+ properties or complex types
- Fluent `With{PropertyName}()` methods
- `Build()` method returning configured instance
- Nested public class within the main model

### JSON Serialization
- `[JsonPropertyName("snake_case")]` attributes for property mapping
- PascalCase C# properties mapping to snake_case JSON
- Proper nullable reference types (`string?`)

### Inheritance
- Paginated response classes inherit from `PaginatedResponse`
- Maintains flat `/model/` directory structure

### Documentation
- XML documentation for classes and properties
- Standard Apache 2.0 license headers

## Usage

### Command Line
```bash
# From the ModelGenerator directory
dotnet run --project tools/ModelGenerator

# Or use the convenience script
./tools/ModelGenerator/generate-models.sh
```

### Requirements
- .NET 8.0 SDK
- Node.js and npm (for OpenAPI Generator CLI)
- OpenAPI Generator CLI will be installed automatically if not present

## Output

Generated models are placed in:
```
src/CoinbaseSdk/Prime/model/
```

## Configuration

The tool is configured to:
- Target .NET 8.0
- Use `System.Text.Json` for serialization
- Generate only models (no APIs or clients)
- Apply PascalCase naming for properties
- Enable nullable reference types

## Architecture

### Files
- `Program.cs` - Main entry point and orchestration
- `OpenApiGenerator.cs` - Handles OpenAPI Generator CLI integration
- `PostProcessor.cs` - Applies .NET-specific transformations using Roslyn
- `ModelGenerator.csproj` - Project file with dependencies

### Dependencies
- `Microsoft.CodeAnalysis.CSharp` - Roslyn APIs for syntax transformation
- `Newtonsoft.Json` - JSON handling for configuration
- `YamlDotNet` - YAML parsing for OpenAPI spec analysis

## Transformations Applied

1. **Namespace Standardization**: All models in `CoinbaseSdk.Prime.Model`
2. **Builder Pattern Generation**: For appropriate model classes
3. **JSON Attribute Mapping**: Snake case to PascalCase conversion
4. **Array Initialization**: Empty array defaults (`= []`)
5. **Pagination Inheritance**: For list response classes
6. **Enum Formatting**: ALL_CAPS_WITH_UNDERSCORES format
7. **Documentation Generation**: XML docs from OpenAPI descriptions
8. **License Headers**: Standard Coinbase Apache 2.0 headers

## Quality Assurance

The generator is designed to produce models with minimal differences from manually created ones:

- Preserves existing naming conventions
- Maintains architectural patterns
- Ensures compilation without errors
- Supports existing test suite without modification

## Troubleshooting

### OpenAPI Generator Not Found
The tool will attempt to install OpenAPI Generator CLI via npm if not found. Ensure Node.js and npm are available.

### Build Errors
Generated models should compile without errors. If issues occur:
1. Check the OpenAPI specification for validity
2. Review the post-processor transformations
3. Ensure all dependencies are available

### Missing Models
If expected models are not generated:
1. Verify the OpenAPI spec contains the schema definitions
2. Check the generator configuration excludes the schemas
3. Review the file filtering logic in the post-processor

## Extending the Generator

To add new transformations:

1. Modify the `ModelTransformer` class in `PostProcessor.cs`
2. Add new visitor methods for specific syntax patterns
3. Update the `ApplyFinalFormatting` method for text-level changes
4. Test with a small subset of models first

## Integration with Build Process

The generator can be integrated into the build process by adding an MSBuild target:

```xml
<Target Name="GenerateModels" BeforeTargets="BeforeBuild">
  <Exec Command="dotnet run --project $(MSBuildThisFileDirectory)../tools/ModelGenerator/ModelGenerator.csproj" />
</Target>
```