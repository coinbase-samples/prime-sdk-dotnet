# Environment variables — `Prime.IntegrationTests`

Set these in your shell, CI job `env:`, or a **`.env`** file in the repo (loaded by `CoinbasePrimeClient.FromEnv()` via DotNetEnv).

## Required to hit the live API (not skipped)

| Variable | Purpose |
|----------|---------|
| `PRIME_ACCESS_KEY` | Prime REST API key id |
| `PRIME_PASSPHRASE` | API passphrase |
| `PRIME_SIGNING_KEY` | Base64-encoded private signing key (treat as secret) |

**Without all three above**, every integration test **skips** and the run succeeds (exit 0), unless you use strict mode.

## Optional — control strictness

| Variable | Purpose |
|----------|---------|
| `PRIME_INTEGRATION_REQUIRED` | Set to `1`, `true`, or `yes` to **fail the test run** if the three auth variables are missing (use in CI so an unconfigured job does not “pass” with only skips). |

## Optional — fix or override discovered IDs

Bootstrap in `PrimeIntegrationFixture` calls list endpoints and uses the first sensible row. Override with env if you need a specific resource.

| Variable | If unset |
|----------|----------|
| `PRIME_PORTFOLIO_ID` | First portfolio from `ListPortfolios` |
| `PRIME_ENTITY_ID` | `entity_id` from `GetPortfolio` for the chosen portfolio |
| `PRIME_WALLET_ID` | First **VAULT** wallet from `ListWallets` |
| `PRIME_ORDER_ID` | First order from `ListPortfolioOrders` |
| `PRIME_ACTIVITY_ID` | First activity from `ListActivities` |
| `PRIME_TRANSACTION_ID` | First transaction from `ListPortfolioTransactions` |
| `PRIME_ALLOCATION_ID` | `root_id` of first row from `ListPortfolioAllocations` |
| `PRIME_ADDRESS_BOOK_ID` | First entry from `ListAddressBookEntries` |
| `PRIME_PAYMENT_METHOD_ID` | First id from `ListEntityPaymentMethods` (entity must resolve) |
| `PRIME_ONCHAIN_ADDRESS_GROUP_ID` | First group from `ListOnchainAddressGroups` |
| `PRIME_CLIENT_NETTING_ID` | `netting_id` on first allocation when present, else set manually |

## Optional — defaults for product / asset strings in tests

| Variable | Default when unset |
|----------|----------------------|
| `PRIME_ASSET_SYMBOL` | `BTC` |
| `PRIME_PRODUCT_ID` | `BTC-USD` |

## Example: local shell (do not commit real values)

```bash
export PRIME_ACCESS_KEY="your-access-key"
export PRIME_PASSPHRASE="your-passphrase"
export PRIME_SIGNING_KEY="your-base64-signing-key"
# Optional:
# export PRIME_PORTFOLIO_ID="..."
# export PRIME_ENTITY_ID="..."
```

## Example: CI (map GitHub `secrets` to these names)

`PRIME_INTEGRATION_REQUIRED: "true"` and set `PRIME_ACCESS_KEY`, `PRIME_PASSPHRASE`, `PRIME_SIGNING_KEY` (and often `PRIME_PORTFOLIO_ID` / `PRIME_ENTITY_ID` for stable coverage).

## Security

- Never log or commit these values.
- Prefer a **read-only** API key for GET-only tests.

---

See [README.md](README.md) for how to run the suite and design notes.
