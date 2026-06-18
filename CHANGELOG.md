# Changelog

## [0.6.0] - 2026-JUN-17

### Added

#### New API Endpoints

**Financing Service**
- **`GetCrossMarginRiskParameters()`**: Cross-margin risk parameters for an entity (`GET /v1/entities/{entity_id}/cross_margin/risk_parameters`)
- **`GetMarketData()`**: Entity market data (`GET /v1/entities/{entity_id}/market_data`)
- **`GetCrossMarginPrimeOverview()`**: Prime cross-margin overview (`GET /v2/entities/{entity_id}/cross_margin/prime`)
- **`UpdateFundingSettings()`**: Update FCM funding settings (`POST /v1/entities/{entity_id}/funding_settings`)

**Advanced Transfer Service**
- **`GetPortfolioCounterparty()`**: Portfolio counterparty ID (`GET /v1/portfolios/{portfolio_id}/counterparty`)

#### New & Updated Models
- **`CrossMarginRiskParameters`**, **`TierPairRateEntry`**, **`CrossMarginPrimeMarginSummary`**, **`MarketData`**, **`CrossMarginPrimeDerivativesEquityBreakdown`**, **`CrossMarginPrimeRiskNettingInfo`**, **`CrossMarginPrimeSpotEquityBreakdown`**, **`CrossMarginPrimeXMPosition`**, **`PrimeXMMarginCallThresholds`**, **`PrimeXMMarginRequirementBreakdown`**, **`PrimeXMMarginThreshold`**, **`PrimeXMOffsetCreditBreakdown`**
- **`WalletStakingMetadata`**, **`ValidatorProvider`**, **`ValidatorUnstakePreview`**: Staking metadata, ETH validator provider, and per-validator unstake preview
- **`CreatePortfolioUnstakeRequest`**: Added `validator_provider`; `amount` is optional
- **`CreateStakeRequest`**, **`CreateUnstakeRequest`**: Added optional `metadata`
- **`Order`**, **`CreateQuoteRequest`**: `quote_duration_ms`
- **`PreviewUnstakeResponse`**: Added `wallet_id`, `wallet_address`, and `validators`

#### New Enums
- **`PrimeXMHealthStatus`**, **`PrimeXMMarginRequirementType`**, **`PrimeXMMarginThresholdType`**

#### New Examples
- `financing/GetCrossMarginRiskParameters.cs`, `GetMarketData.cs`, `GetCrossMarginPrimeOverview.cs`, `UpdateFundingSettings.cs`
- `advancedtransfer/GetPortfolioCounterparty.cs`

### Changed

- **`GetCrossMarginOverview()`**: Response summary fields aligned with the Prime API cross-margin overview shape
- **`GetPortfolioCounterparty()`**: Moved from **`PortfoliosService`** to **`AdvancedTransferService`** (same HTTP route; update service accessor and namespaces)
- **`CoinbaseSdk.Core`**: Minimum version `0.2.1`
- **`CoinbasePrimeClient`**: Optional `IJsonUtility` injection; `WithApiBasePath()` and `WithApiVersion()` for versioned base paths
- **`Pagination.Builder`**: Builder methods renamed to `WithNextCursor`, `WithSortDirection`, `WithHasNext`

### Notes

- This repository is deprecated; development continues at [coinbase/prime-sdk-dotnet](https://github.com/coinbase/prime-sdk-dotnet). NuGet releases **0.7.0+** are published from the canonical repository.

### Removed

- **`PortfoliosService.GetPortfolioCounterparty`**: Use **`AdvancedTransferService.GetPortfolioCounterparty`** instead
- **`ActivityCreationResponse`**: Activity fields are on concrete onchain address-book response types (`CreateOnchainAddressBookEntryResponse`, etc.)

## [0.5.0] - 2026-APR-7
### Added

- **New API Endpoints**
  - `FuturesService.GetFcmEquity`
  - `ProductsService.GetCandles`
  - `StakingService.GetStakingStatus`
  - `StakingService.GetUnstakingStatus`
  - `StakingService.PreviewUnstake`
  - `TransactionsService.ListAdvancedTransfers`
  - `TransactionsService.CreateAdvancedTransfer`
  - `TransactionsService.CancelAdvancedTransfer`
  - `TransactionsService.ListAdvancedTransferTransactions`
  - `TransactionsService.GetTransactionTravelRuleData`
  - `TransactionsService.SubmitDepositTravelRuleData`

- **New Examples** (run with `dotnet run --file <path>`)

  ```bash
  # Allocations
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/allocations/ListAllocationsByClientNettingId.cs --portfolioId <portfolio_id> --nettingId <netting_id>

  # Futures
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmEquity.cs --entityId <entity_id>

  # Onchain Address Book
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/onchainaddressbook/CreateOnchainAddressBookEntry.cs --portfolioId <portfolio_id> --addressGroup <address_group>
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/onchainaddressbook/UpdateOnchainAddressBookEntry.cs --portfolioId <portfolio_id> --addressGroup <address_group>

  # Products
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/products/GetCandles.cs --portfolioId <portfolio_id> --productId BTC-USD --granularity ONE_HOUR

  # Staking
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/GetStakingStatus.cs --portfolioId <portfolio_id> --walletId <wallet_id>
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/GetUnstakingStatus.cs --portfolioId <portfolio_id> --walletId <wallet_id>
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/PreviewUnstake.cs --portfolioId <portfolio_id> --walletId <wallet_id> --amount 1.0

  # Advanced Transfers
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/ListAdvancedTransfers.cs --portfolioId <portfolio_id>
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/CreateAdvancedTransfer.cs --portfolioId <portfolio_id> --transferType BLIND_MATCH
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/CancelAdvancedTransfer.cs --portfolioId <portfolio_id> --advancedTransferId <transfer_id>
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/ListAdvancedTransferTransactions.cs --portfolioId <portfolio_id> --advancedTransferId <transfer_id>

  # Onchain Transactions
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/CreateOnchainTransaction.cs --portfolioId <portfolio_id> --walletId <wallet_id> --rawUnsignedTxn <raw_txn>

  # Travel Rule
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/GetTransactionTravelRuleData.cs --portfolioId <portfolio_id> --transactionId <transaction_id>
  dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/SubmitDepositTravelRuleData.cs --portfolioId <portfolio_id> --transactionId <transaction_id>
  ```

- **New Domain Models**
  - `AdvancedTransfer`, `BlindMatchMetadata`, `CommissionDetailTotal`
  - `FcmScheduledMaintenance`, `FcmTradingSessionDetails`, `FundMovement`
  - `FutureProductDetails`, `PerpetualProductDetails`
  - `RequestToSubmitTravelRuleDataForAnExistingDepositTransaction`
  - `StakingStatus`, `TravelRuleData`, `ValidatorAllocation`, `ValidatorStakingInfo`

- **New Enums**
  - `AdvancedTransferState`, `AdvancedTransferType`, `ContractExpiryType`
  - `ExpiringContractStatus`, `FcmMarginHealthState`, `FcmTradingSessionClosedReason`
  - `FcmTradingSessionState`, `ProductType`, `RiskManagementType`
  - `SecondaryPermission`, `StakeType`

### Changed

- **SDK Generator overhaul** — `tools/model-generator` has been replaced by `tools/generator`, a
  fully OpenAPI-spec-driven multi-phase code generator. It now generates models, enums, request and
  response types, service interfaces, service implementations, and runnable example scripts directly
  from the Prime OpenAPI spec. Run with `dotnet run --project tools/generator`; use `--dry-run` or
  `--diff` to preview changes without writing files. See `tools/generator/README.md` for full usage.

### Removed

- **`tools/model-generator`** — superseded by `tools/generator` (see above).

## [0.4.0] - 2025-DEC-23

### Added

- **New API Endpoints**
  - `FinancingService.GetCrossMarginOverview`
  - `FinancingService.ListFinancingEligibleAssets`
  - `FinancingService.ListTradeFinanceObligations`
  - `FuturesService.GetFcmMarginCallDetails`
  - `FuturesService.GetFcmRiskLimits`
  - `FuturesService.GetFcmSettings`
  - `FuturesService.SetFcmSettings`
  - `OrdersService.EditOrder`
  - `OrdersService.ListOrderEditHistory`
  - `OrdersService.ListPortfolioFills`
  - `PortfoliosService.GetPortfolioCounterparty`
  - `StakingService.ClaimStakingRewards`
  - `StakingService.CreatePortfolioStake`
  - `StakingService.CreatePortfolioUnstake`
  - `StakingService.ListTransactionValidators`
  - `WalletsService.CreateWalletDepositAddress`
  - `WalletsService.ListWalletAddresses`

- **Comprehensive Examples**
  - Added examples for all SDK service methods across 19 categories:
    activities, addressbook, allocations, assets, balances, commission,
    financing, futures, invoice, onchainaddressbook, orders, paymentmethods,
    portfolios, positions, products, staking, transactions, users, wallets

- **Unit Tests**
  - `CoinbasePrimeClientTests`
  - `PaginatedRequestBuilderTests`
  - `PaginatedRequestTests`
  - `PrimeJsonSerializerOptionsFactoryTests`
  - `PrimeSerializationSmokeTests`

- **New Domain Models**
  - `Action`, `Activity`, `ActivityCreationResponse`, `ActivityLevel`
  - `Commission`, `CounterpartyDestination`
  - `ExistingLocate`, `FcmMarginCall`, `FcmMarginCallDetails`, `FcmRiskLimit`
  - `FcmMarginCallState`, `FcmMarginCallType`, `HierarchyType`
  - `MarginAddOnType`, `Network`, `NetworkFamily`
  - `OnchainTransactionDetails`, `OrderEdit`, `OrderFill`, `OrderStatus`
  - `PaginatedRequest`, `PaginatedRequestBuilder`, `PaginatedResponse`
  - `PaymentMethodDestination`, `PmAssetInfo`, `PortfolioBalanceType`
  - `PortfolioStakingMetadata`, `PortfolioUser`
  - `RfqProductDetails`, `RiskAssessment`, `SigningStatus`, `SortDirection`
  - `Transaction`
  - `UserRole`, `VisibilityStatus`, `WalletAddress`
  - `WalletCryptoDepositInstructions`, `WalletDepositInstructionType`
  - `WalletFiatDepositInstructions`, `WalletVisibility`
  - `Web3Asset`, `Web3Balance`, `Web3TransactionMetadata`

### Removed

- **Unused Classes**
  - `ListPortfolioStakingBalancesRequest` / `ListPortfolioStakingBalancesResponse`
  - `ListAggregatePositionsRequest` / `ListAggregatePositionsResponse`
  - `CreateOnchainAddressBookEntryResponse`
  - `UpdateOnchainAddressBookEntryResponse`
  - `DeleteOnchainAddressGroupResponse`
  - `QuoteResponse`
  - `StakingInitiateResponse`
  - `StakingUnstakeResponse`

### Fixed

- **WalletsService** - `CreateWalletDepositAddress`

### Changed

- **Method Renaming**
  - `GetActivityByActivityId` → `GetActivity`
  - `GetEntityActivityByActivityId` → `GetPortfolioActivity`
  - `GetPortfolioAddressBook` → `ListAddressBookEntries`
  - `GetPortfolioAllocations` → `ListPortfolioAllocations`
  - `GetAllocationsByClientNettingId` → `ListAllocationsByClientNettingId`
  - `GetOrderByOrderId` → `GetOrder`
  - `GetPortfolioById` → `GetPortfolio`
  - `GetTransactionByTransactionId` → `GetTransaction`
  - `GetWalletById` → `GetWallet`

- **Model Renaming**
  - `GetActivityByActivityIdRequest/Response` → `GetActivityRequest/Response`
  - `GetEntityActivityByActivityIdRequest` → `GetActivityRequest`
  - `GetActivityByActivityIdRequest` → `GetPortfolioActivityRequest`
  - `GetPortfolioAddressBookRequest/Response` → `ListAddressBookEntriesRequest/Response`
  - `GetOrderByOrderIdRequest/Response` → `GetOrderRequest/Response`
  - `GetPortfolioByIdRequest/Response` → `GetPortfolioRequest/Response`
  - `GetTransactionByTransactionIdRequest/Response` → `GetTransactionRequest/Response`
  - `GetWalletByIdRequest/Response` → `GetWalletRequest/Response`
  - `PMAssetInfo` → `PmAssetInfo`

- **Pagination**
  - Typed `SortDirection` enum
  - Standardized request patterns

- **Model Updates**
  - `OrderFill` - `ClientProductId`, `VenueFees`, `CesCommission`
  - `Transaction` - `NetworkFamily`
  - `CreateOrderRequest` - `NetworkFamily`
  - `CreateWithdrawalRequest`

## [0.3.0] - 2025-MAY-15

### Removed

- Common abstract classes BasePrimeRequest and BaseListRequest

### Added

- Entity Endpoints
  - ListEntityBalances
  - ListEntityPositions
  - ListAggregateEntityPositions
- Futures Endpoints
  - CancelEntityFuturesSweep
  - GetEntityFcmBalance
  - GetEntityPositions
  - ListEntityFuturesSweeps
  - ScheduleEntityFuturesSweeps
  - SetAutoSweep
- RFQ Endpoints
  - CreateQuoteRequest
  - AcceptQuote
- Prime Financing Endpoints
  - ListExistingLocations
  - ListInterestAccruals
  - ListPortfolioInterestAccruals
  - ListMarginCallSummaries
  - ListMarginConversions
  - GetEntityLocateAvailabilities
  - GetMarginInformation
  - GetPortfolioBuyingPower
  - GetPortfolioCreditInformation
  - GetPortfolioWithdrawalPower
  - GetTieredPricingFees
  - CreateNewLocates
- Prime Staking Endpoints
  - CreateStake
  - CreateUnstake
- Moved all models to one communal package for easier export
- Moved all Request/Response object to service specific package
