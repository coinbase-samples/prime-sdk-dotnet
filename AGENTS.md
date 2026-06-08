# Repository Guidelines

## Project Structure & Module Organization
Keep the solution rooted at `prime-sdk-dotnet.sln`. Library code lives in `src/CoinbaseSdk/Prime` and runnable samples in `src/CoinbaseSdk/PrimeExample`. Shared analyzers are configured through `src/StyleCopRules.ruleset`. The OpenAPI spec is tracked at `apiSpec/prime-public-api-spec.yaml`; refresh it with `make fetch-spec`. Regenerate models, enums, requests, responses, services, and examples with `make generate` (`tools/generator`).

## Build, Test, and Development Commands
- `dotnet restore prime-sdk-dotnet.sln` installs all NuGet dependencies.
- `dotnet build prime-sdk-dotnet.sln` compiles the library and samples.
- `make fetch-spec` downloads the latest public OpenAPI spec into `apiSpec/prime-public-api-spec.yaml`.
- `make generate` runs the holistic OpenAPI generator (`tools/generator`) against the committed spec, then `dotnet format`.
- `dotnet run --project src/CoinbaseSdk/PrimeExample list` enumerates sample scenarios; swap `list` for any example command to execute it.

## Coding Style & Naming Conventions
Adhere to the StyleCop rules baked into the project; analyzer violations will fail the build. Use four-space indentation, expression-bodied members sparingly, and keep braces on new lines. Favor PascalCase for public types and members, camelCase for locals and parameters, and suffix async methods with `Async`. Run `dotnet format` locally to auto-fix whitespace and style issues before committing.

## Testing Guidelines
Validate changes by building the solution and running relevant examples under `src/CoinbaseSdk/PrimeExample/examples/`.

## Commit & Pull Request Guidelines
Follow the existing history: short, imperative commit subjects in lowercase (for example, `add order client`). Link related issues or tickets in the commit body when useful. Pull requests should summarize the change, highlight impacted modules, note any manual test steps, and include screenshots when UI-affecting (rare here). Confirm CI passes and call out any follow-up work before requesting review.

## Security & Configuration Tips
Do not commit `.env` files or credentials. Document new configuration keys in the README and provide sanitized samples. When adding network calls, ensure sensitive headers and payloads are excluded from logs.
