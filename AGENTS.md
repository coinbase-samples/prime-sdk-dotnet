# Repository Guidelines

## Project Structure & Module Organization
Keep the solution rooted at `prime-sdk-dotnet.sln`. Library code lives in `src/CoinbaseSdk/Prime`, **integration** (live-API) GET tests in `src/CoinbaseSdk/Prime.IntegrationTests` (see that folder’s `README.md` and `ENVIRONMENT_VARIABLES.md` for required env vars), and runnable samples in `src/CoinbaseSdk/PrimeExample`. Shared analyzers are configured through `src/StyleCopRules.ruleset`. Use the `tools` directory for generated assets or scripts already provided by the repository. Run `dotnet run --project tools/generator` to regenerate models, enums, requests, responses, and services from the Prime OpenAPI spec (see `tools/generator/README.md`).

## Build, Test, and Development Commands
- `dotnet restore prime-sdk-dotnet.sln` installs all NuGet dependencies.
- `dotnet build prime-sdk-dotnet.sln` compiles the library, samples, and tests with warnings treated as errors.
- `dotnet test src/CoinbaseSdk/Prime.IntegrationTests/CoinbaseSdk.Prime.IntegrationTests.csproj --filter "Category=Integration"` runs live GET tests (skips if `PRIME_ACCESS_KEY` / `PRIME_PASSPHRASE` / `PRIME_SIGNING_KEY` are unset).
- `dotnet run --project src/CoinbaseSdk/PrimeExample list` enumerates sample scenarios; swap `list` for any example command to execute it.
- `dotnet run --project tools/generator` runs the holistic OpenAPI-driven generator; add `--dry-run` or `--diff` for diagnostics only.

## Coding Style & Naming Conventions
Adhere to the StyleCop rules baked into the project; analyzer violations will fail the build. Use four-space indentation, expression-bodied members sparingly, and keep braces on new lines. Favor PascalCase for public types and members, camelCase for locals and parameters, and suffix async methods with `Async`. Run `dotnet format` locally to auto-fix whitespace and style issues before committing.

## Testing Guidelines
Integration tests: use `[SkippableFact]`, the shared `PrimeIntegrationFixture` (see `Prime.IntegrationTests/Infrastructure`), and do not log credentials. Use `--filter "Category=Integration"` or `Category!=Integration` in CI. For client-only unit tests (mocks, serialization), when present, use xUnit in a dedicated test project, name files `<ClassName>Tests.cs`, and prefer Moq for SDK boundaries.

## Commit & Pull Request Guidelines
Follow the existing history: short, imperative commit subjects in lowercase (for example, `add order client`). Link related issues or tickets in the commit body when useful. Pull requests should summarize the change, highlight impacted modules, note any manual test steps, and include screenshots when UI-affecting (rare here). Confirm CI passes and call out any follow-up work before requesting review.

## Security & Configuration Tips
Do not commit `.env` files or credentials. Document new configuration keys in the README and provide sanitized samples. When adding network calls, ensure sensitive headers and payloads are excluded from logs.
