# Prime SDK Generator

Holistic code generation for the Coinbase Prime .NET SDK from the [Prime OpenAPI specification](https://api.prime.coinbase.com/v1/openapi.yaml).

## What it generates

Every run produces, together:

1. **Models & enums** (`CoinbaseSdk.Prime.Model` / `Model.Enums`) via OpenAPI Generator CLI + post-processing (same pipeline as the former model-only tool).
2. **Request / Response DTOs**, **service interfaces**, and **service implementations** under each feature folder (e.g. `orders/`, `wallets/`), driven by `SpecAnalyzer` (tag routing, services, optional schema-prefix discovery), `OperationBindingGenerator`, `config/operations-overrides.json`, and `config/generator-config.json`.

There is no mode to generate only one category; output is always kept in sync.

## Architecture

```
tools/generator/
  Program.cs                 # Entry: download spec, run all phases
  config/
    generator-config.json    # Transforms, tag overrides, method-order overrides, specUrl
    operations-overrides.json # Sparse patches per operationId (overrides win)
    .openapi-generator-ignore
    openapitools.json
  phases/
    ModelEnumPhase.cs        # OpenAPI Generator CLI + ModelPostProcessor
    OpenApiGenerator.cs
    ModelPostProcessor.cs
    ClientSurfacePhase.cs    # Orchestrates request/response/service emission
    ResponsePhase.cs
    RequestPhase.cs
    ServicePhase.cs
  processing/
    GeneratorConfiguration.cs
    GeneratorXmlDoc.cs
    SharedTransforms.cs
    NamingResolver.cs
    OpenApiSchemaCodegen.cs
    OperationBindingGenerator.cs
    OperationBindingValidator.cs
    SpecAnalyzer.cs           # Derives tag→folder, services, schema prefixes from spec
  spec/
    SpecParser.cs
    SpecModels.cs
    SpecResponseSchema.cs
  templates/                 # Model templates for CLI; placeholder .mustache for client surface
  sync-copyright-years-from-git.sh  # Aligns file-header Copyright years with Git first-commit dates
```

## Copyright years (generator sources)

- **File headers** under `tools/generator/` should use the year the file was **first added in Git**, not a hardcoded guess. After adding or renaming generator sources, run from the repo root:

```bash
bash tools/generator/sync-copyright-years-from-git.sh
```

- **Emitted C#** under `src/CoinbaseSdk/Prime/` (requests, responses, services, new examples) uses the year from the first commit that added `tools/generator/phases/RequestPhase.cs`, resolved at generator startup via `CopyrightHelper.InitializeSdkEmittedCopyrightYear`. **New** output files (no prior version on disk) get that year in `ApplyCopyrightYear`; existing files keep their on-disk year.

## Prerequisites

- .NET 9.0+
- OpenAPI Generator CLI:
  - `npm install -g @openapitools/openapi-generator-cli`
  - or `brew install openapi-generator`
- Committed OpenAPI spec at `apiSpec/prime-public-api-spec.yaml` (refresh with `make fetch-spec` from the repo root).
- Optional network access when using `--live` / `--fetch-spec` to download from `specUrl`.

## Usage

From the repository root:

```bash
make generate
# or:
dotnet run --project tools/generator
```

By default the generator reads the **committed** spec at `committedSpecPath` in `generator-config.json` (`apiSpec/prime-public-api-spec.yaml`). To download the live spec instead:

```bash
dotnet run --project tools/generator -- --live
```

Diagnostic flags (still runs all phases; only affects whether files are written):

```bash
dotnet run --project tools/generator -- --dry-run   # Log paths; do not write
dotnet run --project tools/generator -- --diff      # Compare generated text to files on disk
```

Live downloads are cached under `generated/openapi.yaml` (gitignored).

Bindings are derived for every spec operation (see `OperationBindingGenerator`). `operations-overrides.json` may patch any field; stale `operationId` rows fail the run. Overrides that exactly match derived defaults log a warning and can be removed.

Or use the helper script from this directory:

```bash
cd tools/generator
./generate.sh
```

## Configuration

- **`config/generator-config.json`** — `specUrl`, `committedSpecPath`, `emitRequestBuilders` (default `true`; set `false` to omit nested request builders — breaking for callers), `filePathReplacements` (semantic renames; common schema prefixes are also merged from the spec), `contentReplacements`, `acronymMappings`, `enumNameMappings`, `tagToFolderOverrides` (only when the default tag→folder rule is wrong, e.g. routing a tag to an existing folder), `serviceMethodOrderOverrides` (optional per-service method order), `statusCodeOverrides` (optional permissive create-status lists).
- **`config/operations-overrides.json`** — Optional array of sparse patches: `operationId` plus any of `sdkMethod`, `service`, `omitRequest`, `forcePaginated`, `paramTypeOverrides` (merged onto derived values).

Default **tag → folder** is lowercase with spaces removed (`Payment Methods` → `paymentmethods`). **Services** (`folder`, `namespace`, `I*Service` / `*Service` names) are derived from the canonical OpenAPI tag for that folder.

### `serviceMethodOrderOverrides`

When present for a service key, fixes `I*Service` / `*Service` method order. When omitted, methods sort by HTTP verb (GET, POST, PUT, PATCH, DELETE), path depth, path, then `sdkMethod`.

### Success HTTP status codes

By default, success codes come from the OpenAPI `responses` map (200, 201, 202 with JSON body, 204). When both **200** and **201** are documented, **Created** is emitted before **OK**. Optional `statusCodeOverrides` maps `sdkMethod` to status names (e.g. `Created`, `OK`) when the spec omits **201** but the API may return it.

Optional `x-sdk-method-name` on an operation overrides the derived `SdkMethod` name before `operations-overrides.json` is applied.

## Workflow

1. Run the generator.
2. Review diff (`--diff` or `git diff`).
3. `dotnet build prime-sdk-dotnet.sln`
4. `dotnet test src/CoinbaseSdk/Prime.Tests/CoinbaseSdk.Prime.Tests.csproj`
5. `dotnet test tools/generator/Generator.Tests/Generator.Tests.csproj` (generator unit tests)
6. Commit.

## Technical notes

- Raw OpenAPI YAML from `--live` is cached under `generated/openapi.yaml` (gitignored). CLI output uses `generated/model-cli/` and is removed after model post-processing.
- Client DTOs and services are emitted as C# strings (see phase classes); additional `.mustache` files under `templates/` are placeholders for future template-driven tweaks.
- **JSON naming:** Prime HTTP uses `PrimeJsonDefaults` (snake_case policy). Emit `[JsonPropertyName]` for wire names the policy cannot infer. **Web3 → Onchain** renames apply to CLR identifiers only (hand-maintained since Jul 2025); wire names like `web3_transaction_metadata` are preserved via `[JsonPropertyName]` when needed.
- **Boilerplate toggles:** `emitRequestBuilders` controls nested fluent builders on request DTOs. Further reductions (records, primary constructors only) are intentionally not enabled by default — they change the public API.

### Hand-maintained exceptions

- `src/CoinbaseSdk/Prime/common/PaginatedRequest.cs` — abstract base for paginated list requests (not an OpenAPI schema).
- `client/`, `configuration/`, `serialization/` — SDK wiring, not OpenAPI models.

All `model/` types and `common/Pagination.cs` are generated from the spec (`commonModels` in `generator-config.json` routes `PaginatedResponse` → `Pagination`).
