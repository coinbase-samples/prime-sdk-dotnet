# Changelog

## [0.4.0] - 2025-JUL-23

### Added

- **New API Endpoints**
  - Activities: `GetActivity` - Retrieve individual activity details (renamed from `GetActivityByActivityId`)
  - Activities: `GetPortfolioActivity` - Retrieve specific portfolio activity
  - Activities: `ListEntityActivities` - List activities for entity
  - Assets: `ListEntityAssets` - Retrieve entity asset information
  - Balances: `ListWeb3WalletBalances` - Web3 wallet balance queries
  - Futures: `GetFcmMarginCallDetails` - FCM margin call information
  - Futures: `GetFcmRiskLimits` - FCM risk limit management
  - OnchainAddressGroups: Complete service implementation - Onchain address group management
  - Orders: `GetOrderEditHistory` - Order modification audit trail
  - Orders: `GetOrder` - Retrieve individual order details
  - Positions: `ListAggregatePositions` - Aggregate position summaries
  - Staking: `ListPortfolioStakingBalances` - Portfolio staking balance details
  - Wallets: `CreateWalletDepositAddress` - Generate new deposit addresses
  - Wallets: `ListWalletAddresses` - List all wallet addresses
  - Wallets: `GetWallet` - Retrieve individual wallet details

- **New Model Classes** (45+ new models added)
  - `Activity` - Activity details and metadata
  - `Action` - Transaction action types
  - `ActivityLevel` - Activity level classifications
  - `ActivityCategory` - Activity category classifications enum
  - `ActivityStatus` - Activity status types enum
  - `Commission` - Fee calculation and commission details
  - `CounterpartyDestination` - Counterparty destination details
  - `ExistingLocate` - Existing locate information
  - `FcmMarginCall` - FCM margin call details
  - `FcmMarginCallDetails` - FCM margin call information
  - `FcmMarginCallState` - FCM margin call state enum
  - `FcmMarginCallType` - FCM margin call type enum
  - `FcmRiskLimit` - FCM risk limit details
  - `HierarchyType` - Organizational hierarchy types enum
  - `MarginAddOnType` - Margin add-on type classifications
  - `Network` - Blockchain network identification
  - `NetworkFamily` - Blockchain network family classifications
  - `OnchainTransactionDetails` - Onchain transaction metadata
  - `OrderEdit` - Order modification history tracking
  - `OrderEditValues` - Order edit value details
  - `PaginatedResponse` - Generic pagination wrapper
  - `PaymentMethodDestination` - Payment method destination details
  - `PortfolioBalanceType` - Portfolio balance type classifications
  - `PortfolioStakingMetadata` - Comprehensive portfolio staking information
  - `PortfolioUser` - Enhanced user model with portfolio context
  - `QuoteResponse` - Quote response details
  - `RfqProductDetails` - RFQ product information
  - `RiskAssessment` - Risk assessment details
  - `SigningStatus` - Transaction signing status
  - `SortDirection` - Sorting specification enum
  - `StakingInitiateResponse` - Staking initiation response
  - `StakingUnstakeResponse` - Staking unstake response
  - `TransactionType` - Transaction type classifications enum
  - `UserRole` - Comprehensive user permission roles enum
  - `WalletAddress` - Comprehensive wallet address details
  - `WalletCryptoDepositInstructions` - Crypto deposit instruction details
  - `WalletDepositInstructionType` - Deposit instruction type enum
  - `WalletFiatDepositInstructions` - Fiat deposit instruction details
  - `WalletVisibility` - Wallet visibility settings
  - `Web3Asset` - Web3 asset information
  - `Web3Balance` - Web3 balance details
  - `Web3TransactionMetadata` - Web3 transaction metadata

- **New Request/Response Models**
  - `GetActivityRequest` / Activities service
  - `GetPortfolioActivityRequest` / `GetPortfolioActivityResponse` - Portfolio activity operations
  - `ListEntityActivitiesResponse` - Entity activity listing
  - `ListEntityAssetsRequest` / `ListEntityAssetsResponse` - Entity asset operations
  - `ListWeb3WalletBalancesRequest` / `ListWeb3WalletBalancesResponse` - Web3 wallet balance operations
  - `ListAggregatePositionsRequest` / `ListAggregatePositionsResponse` - Aggregate position operations
  - `ListPortfolioStakingBalancesRequest` / `ListPortfolioStakingBalancesResponse` - Portfolio staking operations
  - `CreatePortfolioStakeRequest` / `CreatePortfolioStakeResponse` - Portfolio staking initiation operations
  - `CreatePortfolioUnstakeRequest` / `CreatePortfolioUnstakeResponse` - Portfolio unstaking operations
  - `ListAddressBookEntriesRequest` / `ListAddressBookEntriesResponse` - Address book entry operations (renamed from GetPortfolioAddressBook)
  - `GetFcmMarginCallDetailsRequest` / `GetFcmMarginCallDetailsResponse`
  - `GetFcmRiskLimitsRequest` / `GetFcmRiskLimitsResponse`
  - `GetOrderEditHistoryRequest` / `GetOrderEditHistoryResponse`
  - `GetOrderRequest` / `GetOrderResponse`
  - `GetWalletRequest` / `GetWalletResponse`
  - `CreateWalletDepositAddressRequest` / `CreateWalletDepositAddressResponse`
  - `ListWalletAddressesRequest` / `ListWalletAddressesResponse`
  - `ListPortfolioFillsResponse` - Response wrapper for portfolio fills

- **Standalone Script Examples**
  - `examples/activities/GetActivity.csx` - C# script using dotnet-script
  - `examples/futures/GetFcmMarginCallDetails.csx` - C# script using dotnet-script
  - `examples/futures/GetFcmRiskLimits.csx` - C# script using dotnet-script
  - `examples/orders/GetOrderEditHistory.csx` - C# script using dotnet-script
  - `examples/transactions/GetTransactionByTransactionId.csx` - Transaction lookup script
  - `examples/transactions/ListPortfolioTransactions.csx` - Portfolio transaction listing script
  - `examples/wallets/CreateWalletDepositAddress.csx` - C# script using dotnet-script
  - `examples/wallets/ListWalletAddresses.csx` - C# script using dotnet-script

### Enhanced

- **Service Interfaces** - Added new methods to existing service interfaces:
  - `IActivitiesService` - Added `GetActivity`, `GetPortfolioActivity`, and `ListEntityActivities` methods
  - `IFuturesService` - Added `GetFcmMarginCallDetails` and `GetFcmRiskLimits` methods
  - `IOrdersService` - Added `GetOrder` and `GetOrderEditHistory` methods
  - `IWalletsService` - Added `GetWallet`, `CreateWalletDepositAddress`, and `ListWalletAddresses` methods
  - `IAddressBookService` - Renamed `GetPortfolioAddressBook` to `ListAddressBookEntries` for consistency
  - `IStakingService` - Added new staking operations including `CreatePortfolioStake` and `CreatePortfolioUnstake`

- **OpenAPI Specification Synchronization**
  - Updated to latest Prime API specification with 211 lines of additions/changes
  - Synchronized all model properties with current API schema
  - Enhanced enum values across multiple model classes
  - Improved type safety by replacing string parameters with proper enum types

- **Type Safety Improvements**
  - **Enum Integration**: Replaced generic string parameters with strongly-typed enums across all request models:
    - `ActivityCategory` enum for activity filtering instead of string arrays
    - `ActivityStatus` enum for status filtering instead of string arrays  
    - `ActivityLevel` enum for entity activity level specification
    - `SortDirection` enum for pagination sorting instead of string values
    - `TransactionType` enum for transaction type filtering
  - **Request Model Standardization**: Updated 25+ request classes to use proper enum types
  - **Pagination Enhancement**: Improved `Pagination` model with typed `SortDirection` enum

- **Model Enhancements**
  - `Transaction` - Added `NetworkFamily` property for blockchain network identification
  - `TransactionMetadata` - Enhanced with additional metadata fields
  - `CreateOrderRequest` - Added `NetworkFamily` support for cross-chain orders
  - `CreateWithdrawalRequest` - Enhanced with additional withdrawal options and `NetworkFamily`
  - `GetOrderPreviewResponse` - Expanded with comprehensive preview details
  - `HierarchyType` - Added new hierarchy type options
  - `VisibilityStatus` - Enhanced visibility control options
  - `InvoiceItem`, `PmAssetInfo`, `TieredPricingFee` - Streamlined model structures
  - `OrderFill Model` - Added missing fields from OpenAPI specification:
    - `ClientProductId` - Settlement currency indicator
    - `VenueFees` - Venue-specific fees
    - `CesCommission` - CES commission details
    - Updated builder pattern to support new fields
  - `PortfolioStakingMetadata` - New model for comprehensive staking information

- **Staking Operations**
  - Enhanced staking service with new request/response models:
    - `CreatePortfolioStakeRequest` / `CreatePortfolioStakeResponse` - Portfolio staking initiation
    - `CreatePortfolioUnstakeRequest` / `CreatePortfolioUnstakeResponse` - Portfolio unstaking operations
  - Updated service interface to support new staking workflows

- **Transaction & Conversion Responses**
  - Enhanced `CreateConversionResponse` and `CreateTransferResponse` with additional metadata
  - Improved `CreateWithdrawalResponse` with comprehensive withdrawal details

- **Example Project Structure** - Converted examples to standalone C# scripts (.csx) using dotnet-script for easier execution
- **Documentation** - Enhanced README with dotnet-script installation instructions and improved example usage guidance

### Fixed

- **WalletsService** - Fixed `CreateWalletDepositAddress` method to properly pass request body to API

### Changed

- **Method Naming Standardization** - Simplified SDK method names to follow consistent .NET conventions:
  - Activities: `GetActivityByActivityId` → `GetActivity` (consolidated portfolio and entity scoped methods)
  - Activities: Removed redundant `GetActivityByActivityId` and `GetEntityActivityByActivityId` methods in favor of unified `GetActivity`
  - AddressBook: `GetPortfolioAddressBook` → `ListAddressBookEntries` for better semantic clarity
  - Portfolios: `GetPortfolioById` → `GetPortfolio`
  - Transactions: `GetTransactionByTransactionId` → `GetTransaction`

- **Request/Response Model Consolidation** - Streamlined duplicate model classes:
  - Activities: Consolidated `GetActivityByActivityIdRequest/Response` into `GetActivityRequest/Response`
  - AddressBook: Renamed `GetPortfolioAddressBookRequest/Response` to `ListAddressBookEntriesRequest/Response`
  - Portfolios: Consolidated `GetPortfolioByIdRequest/Response` into `GetPortfolioRequest/Response`
  - Transactions: Consolidated `GetTransactionByTransactionIdRequest/Response` into `GetTransactionRequest/Response`
  - Removed redundant `GetEntityActivityByActivityIdRequest` class

- **Service Interface Updates** - Updated method signatures to match simplified naming:
  - `IActivitiesService`: Removed deprecated methods, standardized on `GetActivity`
  - `IAddressBookService`: `GetPortfolioAddressBook` → `ListAddressBookEntries`
  - `IPortfoliosService`: `GetPortfolioById` → `GetPortfolio`
  - `ITransactionsService`: `GetTransactionByTransactionId` → `GetTransaction`

- **Type Safety Migration** - Enhanced type safety across the SDK:
  - Migrated from string-based parameters to strongly-typed enum parameters in all request models
  - Updated 25+ request classes to use enum types instead of string arrays or string values
  - Improved compile-time validation and IntelliSense support for developers

- **Example Scripts** - Updated example scripts to use new method names and request types

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
