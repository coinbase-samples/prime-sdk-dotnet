# Coinbase Prime .NET — integration tests (GET)

Live API tests in `Get/` exercise **GET** endpoints (read-only) against `api.prime.coinbase.com`. They are in a separate project so local/CI can run the library and unit tests without credentials.

**Environment variable reference (required vs optional, CI examples):** see **[`ENVIRONMENT_VARIABLES.md`](ENVIRONMENT_VARIABLES.md)** in this project.

**Not in scope (future work):** POST/PUT/DELETE, and GET endpoints on **Financing**, **Futures**, **Staking**, and **Invoice** services (entitlement / side-effect risk).

## Run

```bash
# From repo root — requires network + credentials, or all tests are skipped
dotnet test src/CoinbaseSdk/Prime.IntegrationTests/CoinbaseSdk.Prime.IntegrationTests.csproj --filter "Category=Integration"
```

- **No credentials:** all cases **skip** (exit 0). Safe for `dotnet test` in sandboxes.
- **Strict mode (e.g. future CI):** set `PRIME_INTEGRATION_REQUIRED=true`. If credentials are missing, fixture init **fails** the run (no silent green).

## Environment variables (summary)

| When | You need |
|------|----------|
| **Live API (tests execute)** | `PRIME_ACCESS_KEY`, `PRIME_PASSPHRASE`, `PRIME_SIGNING_KEY` — all three |
| **CI must fail if secrets are missing** | Also set `PRIME_INTEGRATION_REQUIRED=true` (or `1` / `yes`) |
| **Stable / specific resources** | Optional: `PRIME_PORTFOLIO_ID`, `PRIME_ENTITY_ID`, and the `PRIME_*_ID` overrides below |

`CoinbasePrimeClient.FromEnv()` loads a **`.env`** file from the working directory tree (DotNetEnv), so you can keep the three auth variables in a **gitignored** `.env` for local runs.

| Variable | Required for live run? | Description |
|----------|------------------------|-------------|
| `PRIME_ACCESS_KEY` | **Yes** (with passphrase + key) | API access key |
| `PRIME_PASSPHRASE` | **Yes** | API passphrase |
| `PRIME_SIGNING_KEY` | **Yes** | API signing key (secret) |
| `PRIME_INTEGRATION_REQUIRED` | No | `true` / `1` / `yes` — fail the run if auth vars missing |
| `PRIME_PORTFOLIO_ID` | No | Target portfolio; else first from `ListPortfolios` |
| `PRIME_ENTITY_ID` | No | From env or from `GetPortfolio` |
| `PRIME_WALLET_ID` | No | Else first VAULT from `ListWallets` |
| `PRIME_ORDER_ID` | No | Else first from `ListPortfolioOrders` |
| `PRIME_ACTIVITY_ID` | No | Else first from `ListActivities` |
| `PRIME_TRANSACTION_ID` | No | Else first from `ListPortfolioTransactions` |
| `PRIME_ALLOCATION_ID` | No | Else allocation `root_id` from `ListPortfolioAllocations` |
| `PRIME_ADDRESS_BOOK_ID` | No | Else first from `ListAddressBookEntries` |
| `PRIME_PAYMENT_METHOD_ID` | No | Else first from `ListEntityPaymentMethods` |
| `PRIME_ONCHAIN_ADDRESS_GROUP_ID` | No | Else first from `ListOnchainAddressGroups` |
| `PRIME_CLIENT_NETTING_ID` | No | Else `netting_id` on first allocation when present |
| `PRIME_ASSET_SYMBOL` | No | Default `BTC` in fixture |
| `PRIME_PRODUCT_ID` | No | Default `BTC-USD` in fixture |

Full tables, copy-paste `export` examples, and CI notes: **[`ENVIRONMENT_VARIABLES.md`](ENVIRONMENT_VARIABLES.md)**.

## Design

- **Category:** all tests are `[Trait("Category", "Integration")]`.
- **Serial:** `[Collection("PrimeIntegration")]` + `DisableParallelization` to reduce rate-limit bursts.
- **Bootstrap:** `PrimeIntegrationFixture` resolves shared ids once; optional id-specific tests **skip** if an id is missing.
- **429:** bootstrap uses limited retries (see `IntegrationRetry`).

## Future GitHub Actions (after repo migration)

`coinbase-samples` has no Actions in this tree; add workflows when the repo is under an org with Actions. Suggested **unit** job: `dotnet test` excluding integration:

```bash
dotnet test --filter "Category!=Integration"
```

Suggested **integration** job (e.g. on `main`, `workflow_dispatch`, or nightly schedule): set `PRIME_INTEGRATION_REQUIRED: true` and map repo secrets to the env vars above; use a **read-only** Prime API key. Example TRX log:

```bash
dotnet test src/CoinbaseSdk/Prime.IntegrationTests/CoinbaseSdk.Prime.IntegrationTests.csproj \
  --filter "Category=Integration" \
  --logger "trx;LogFileName=integration.trx" --logger "console;verbosity=normal"
```

Use `actions/upload-artifact` for `**/*.trx`, job `timeout-minutes: 20`, and `concurrency: integration-${{ github.ref }}` with `cancel-in-progress: true` for long suites.

## Filtering for CI

- Integration only: `--filter "Category=Integration"`
- Unit only (when unit tests are present): `--filter "Category!=Integration"`
