# Changelog

## [0.4.0] - 2025-SEP-18

### Added

- **New API Endpoints**
  - `ActivitiesService.GetActivity` - Retrieve individual activity details
  - `ActivitiesService.GetPortfolioActivity` - Retrieve specific portfolio activity
  - `ActivitiesService.ListEntityActivities` - List activities for entity
  - `AssetsService.ListEntityAssets` - Retrieve entity asset information
  - `BalancesService.ListWeb3WalletBalances` - Web3 wallet balance queries
  - `FuturesService.GetFcmMarginCallDetails` - FCM margin call information
  - `FuturesService.GetFcmRiskLimits` - FCM risk limit management
  - `OnchainAddressGroupsService` - Complete onchain address group management
  - `OrdersService.EditOrder` - Modify existing order parameters
  - `OrdersService.GetOrderEditHistory` - Order modification audit trail
  - `OrdersService.GetOrder` - Retrieve individual order details
  - `PositionsService.ListAggregatePositions` - Aggregate position summaries
  - `StakingService.ListPortfolioStakingBalances` - Portfolio staking balance details
  - `WalletsService.CreateWalletDepositAddress` - Generate new deposit addresses
  - `WalletsService.ListWalletAddresses` - List all wallet addresses
  - `WalletsService.GetWallet` - Retrieve individual wallet details

- **New Models and Types**
  - `EditOrderRequest` / `EditOrderResponse` - Order modification support
  - `ActivityCategory`, `ActivityStatus`, `ActivityLevel` - Activity filtering enums
  - `FcmMarginCall`, `FcmRiskLimit` - FCM trading support models
  - `Web3Asset`, `Web3Balance` - Web3 wallet integration models
  - `WalletAddress`, `WalletDepositInstructionType` - Enhanced wallet models
  - `PortfolioStakingMetadata` - Staking information models
  - `NetworkFamily`, `TransactionType`, `SortDirection` - Enhanced type safety enums

### Enhanced

- **Type Safety Improvements**
  - Replaced string parameters with strongly-typed enums across request models
  - Enhanced pagination with typed `SortDirection` enum
  - Improved compile-time validation and IntelliSense support

- **Model Enhancements**
  - `OrderFill` - Added `ClientProductId`, `VenueFees`, and `CesCommission` properties
  - `Transaction` - Added `NetworkFamily` property for blockchain network identification
  - `CreateOrderRequest` - Added `NetworkFamily` support for cross-chain orders
  - `CreateWithdrawalRequest` - Enhanced with additional withdrawal options

### Fixed

- **WalletsService** - Fixed `CreateWalletDepositAddress` method to properly pass request body to API

### Changed

- **Method Renaming** - Simplified SDK method names for consistency:
  - `GetActivityByActivityId` → `GetActivity`
  - `GetPortfolioAddressBook` → `ListAddressBookEntries` 
  - `GetPortfolioById` → `GetPortfolio`
  - `GetTransactionByTransactionId` → `GetTransaction`

- **Model Renaming** - Consolidated duplicate request/response models:
  - `GetActivityByActivityIdRequest/Response` → `GetActivityRequest/Response`
  - `GetPortfolioAddressBookRequest/Response` → `ListAddressBookEntriesRequest/Response`
  - `GetPortfolioByIdRequest/Response` → `GetPortfolioRequest/Response`
  - `GetTransactionByTransactionIdRequest/Response` → `GetTransactionRequest/Response`

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
