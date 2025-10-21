# Changelog

## [0.4.0] - 2025-SEP-29

### Added

- **New API Endpoints**
  - `FuturesService.GetFcmMarginCallDetails`
  - `FuturesService.GetFcmRiskLimits`
  - `OrdersService.EditOrder`
  - `OrdersService.ListOrderEditHistory`
  - `PortfoliosService.GetPortfolioCounterparty`
  - `StakingService.ClaimStakingRewards`
  - `StakingService.CreatePortfolioStake`
  - `StakingService.CreatePortfolioUnstake`
  - `WalletsService.CreateWalletDepositAddress`
  - `WalletsService.ListWalletAddresses`

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
