# Changelog

## [0.4.0] - 2025-SEP-29

### Added

- **New API Endpoints**
  - `FuturesService.GetFcmMarginCallDetails` - FCM margin call information
  - `FuturesService.GetFcmRiskLimits` - FCM risk limit management
  - `OrdersService.EditOrder` - Modify existing order parameters
  - `OrdersService.GetOrderEditHistory` - Order modification audit trail
  - `PositionsService.ListAggregatePositions` - Aggregate position summaries
  - `PortfoliosService.GetPortfolioCounterparty` - Portfolio counterparty details
  - `StakingService.ClaimStakingRewards` - Claim accumulated staking rewards
  - `StakingService.CreatePortfolioStake` - Create new staking position
  - `StakingService.CreatePortfolioUnstake` - Create unstaking request
  - `StakingService.ListPortfolioStakingBalances` - Portfolio staking balance details
  - `WalletsService.CreateWalletDepositAddress` - Generate new deposit addresses
  - `WalletsService.ListWalletAddresses` - List all wallet addresses

- **New Domain Models**
  - `Action`, `Activity`, `ActivityCreationResponse`, `ActivityLevel` - Activity tracking models
  - `Commission`, `CounterpartyDestination` - Commission and destination models
  - `ExistingLocate`, `FcmMarginCall`, `FcmMarginCallDetails`, `FcmRiskLimit` - FCM trading models
  - `FcmMarginCallState`, `FcmMarginCallType`, `HierarchyType` - FCM enums
  - `MarginAddOnType`, `Network`, `NetworkFamily` - Network and margin models
  - `OnchainTransactionDetails`, `OrderEdit`, `OrderFill`, `OrderStatus` - Order models
  - `PaginatedRequest`, `PaginatedRequestBuilder`, `PaginatedResponse` - Pagination infrastructure
  - `PaymentMethodDestination`, `PmAssetInfo`, `PortfolioBalanceType` - Asset and payment models
  - `PortfolioStakingMetadata`, `PortfolioUser`, `QuoteResponse` - Portfolio models
  - `RfqProductDetails`, `RiskAssessment`, `SigningStatus`, `SortDirection` - Product and risk models
  - `StakingInitiateResponse`, `StakingUnstakeResponse`, `Transaction` - Staking and transaction models
  - `UserRole`, `VisibilityStatus`, `WalletAddress` - User and wallet models
  - `WalletCryptoDepositInstructions`, `WalletDepositInstructionType` - Wallet deposit models
  - `WalletFiatDepositInstructions`, `WalletVisibility` - Wallet configuration models
  - `Web3Asset`, `Web3Balance`, `Web3TransactionMetadata` - Web3 integration models

### Fixed

- **WalletsService** - Fixed `CreateWalletDepositAddress` method to properly pass request body to API

### Changed

- **Method Renaming** - Simplified SDK method names for consistency:
  - `GetActivityByActivityId` → `GetActivity`
  - `GetEntityActivityByActivityId` → `GetPortfolioActivity`
  - `GetPortfolioAddressBook` → `ListAddressBookEntries`
  - `GetOrderByOrderId` → `GetOrder`
  - `GetPortfolioById` → `GetPortfolio`
  - `GetTransactionByTransactionId` → `GetTransaction`
  - `GetWalletById` → `GetWallet`

- **Model Renaming** - Consolidated and renamed request/response models:
  - `GetActivityByActivityIdRequest/Response` → `GetActivityRequest/Response`
  - `GetEntityActivityByActivityIdRequest` → `GetActivityRequest`
  - `GetActivityByActivityIdRequest` → `GetPortfolioActivityRequest`
  - `GetPortfolioAddressBookRequest/Response` → `ListAddressBookEntriesRequest/Response`
  - `GetOrderByOrderIdRequest/Response` → `GetOrderRequest/Response`
  - `GetPortfolioByIdRequest/Response` → `GetPortfolioRequest/Response`
  - `GetTransactionByTransactionIdRequest/Response` → `GetTransactionRequest/Response`
  - `GetWalletByIdRequest/Response` → `GetWalletRequest/Response`
  - `PMAssetInfo` → `PmAssetInfo`

- **Enhanced pagination** with typed `SortDirection` enum and standardized request patterns

- **Model Updates**:
  - `OrderFill` - Added `ClientProductId`, `VenueFees`, and `CesCommission` properties
  - `Transaction` - Added `NetworkFamily` property for blockchain network identification
  - `CreateOrderRequest` - Added `NetworkFamily` support for cross-chain orders
  - `CreateWithdrawalRequest` - Enhanced with additional withdrawal options

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
