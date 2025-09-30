#!/bin/bash

# Coinbase Prime .NET SDK Model Generator
# This script generates C# models from the OpenAPI specification

set -e

# Get the script directory
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"

echo "Coinbase Prime .NET SDK Model Generator"
echo "======================================"
echo "Project Root: $PROJECT_ROOT"
echo "Script Directory: $SCRIPT_DIR"

# Change to the project root
cd "$PROJECT_ROOT"

# Ensure we have the OpenAPI Generator CLI installed
if ! command -v openapi-generator-cli &> /dev/null; then
    echo "OpenAPI Generator CLI not found. Installing via npm..."
    npm install -g @openapitools/openapi-generator-cli@2.7.0
fi

# Run the model generator
echo "Running model generator..."
cd "$SCRIPT_DIR"
dotnet run

echo "Model generation completed successfully!"
echo ""
echo "Generated models are in: $PROJECT_ROOT/src/CoinbaseSdk/Prime/model/"
echo ""
echo "Next steps:"
echo "1. Review the generated models"
echo "2. Run tests to ensure everything works"
echo "3. Build the project: dotnet build"