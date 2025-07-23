# SDK Development Tools

This directory contains tools and utilities for maintaining and extending the Coinbase Prime .NET SDK.

## Code Generation

### `code-generation/extract_endpoints_v2.py`
**Purpose**: Manual endpoint catalog for SDK maintenance and gap analysis.

**Usage**:
```bash
python3 tools/code-generation/extract_endpoints_v2.py
```

**Output**: Comprehensive markdown documentation of all 77 OpenAPI endpoints organized by domain.

**When to Use**:
- Comparing OpenAPI spec against current SDK implementation
- Identifying missing endpoints after spec updates
- Generating documentation for new API coverage
- Planning SDK feature development

**Maintenance**: Update the hardcoded endpoint list when the OpenAPI spec changes.

### `code-generation/complete_endpoint_analysis.py`
**Purpose**: Additional analysis tool for endpoint coverage and implementation status.

## Directory Structure

```
tools/
├── README.md                          # This file
└── code-generation/
    ├── extract_endpoints_v2.py        # Primary endpoint extraction tool
    └── complete_endpoint_analysis.py  # Additional analysis utilities
```

## For AI Agents and Contributors

When working on SDK updates or extensions:

1. **Check OpenAPI Spec**: Always start with `apiSpec/prime-public-spec.yaml`
2. **Run Endpoint Analysis**: Use `extract_endpoints_v2.py` to catalog current spec endpoints
3. **Compare Implementation**: Check against existing service files in `src/CoinbaseSdk/Prime/`
4. **Follow Patterns**: Use `CLAUDE.md` and `.cursor/rules/` for code generation guidelines
5. **Validate Changes**: Ensure `dotnet build prime-sdk-dotnet.sln` passes after modifications

## Updating These Tools

When the OpenAPI specification is updated:

1. Review changes in `apiSpec/prime-public-spec.yaml`
2. Update endpoint definitions in `extract_endpoints_v2.py`
3. Run the tool to verify output accuracy
4. Use output to identify new endpoints for SDK implementation
5. Follow the code generation patterns in `CLAUDE.md` for implementation

## Integration with AI Development

These tools are specifically designed to assist AI agents in:
- Understanding the current API surface area
- Identifying implementation gaps
- Generating consistent code following SDK patterns
- Maintaining OpenAPI specification compliance