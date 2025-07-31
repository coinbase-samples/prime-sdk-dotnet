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

- **New Model Classes** (40+ new models added)
  - `Activity` - Activity details and metadata
  - `Action` - Transaction action types
  - `ActivityLevel` - Activity level classifications
  - `Commission` - Fee calculation and commission details
  - `CounterpartyDestination` - Counterparty destination details
  - `ExistingLocate` - Existing locate information
  - `FCMMarginCall` - FCM margin call details
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
  - `PortfolioUser` - Enhanced user model with portfolio context
  - `QuoteResponse` - Quote response details
  - `RFQProductDetails` - RFQ product information
  - `RiskAssessment` - Risk assessment details
  - `SigningStatus` - Transaction signing status
  - `SortDirection` - Sorting specification enum
  - `StakingInitiateResponse` - Staking initiation response
  - `StakingUnstakeResponse` - Staking unstake response
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

- **OpenAPI Specification Synchronization**
  - Updated to latest Prime API specification with 211 lines of additions/changes
  - Synchronized all model properties with current API schema
  - Enhanced enum values across multiple model classes

- **Model Enhancements**
  - `Transaction` - Added `NetworkFamily` property for blockchain network identification
  - `TransactionMetadata` - Enhanced with additional metadata fields
  - `CreateOrderRequest` - Added `NetworkFamily` support for cross-chain orders
  - `CreateWithdrawalRequest` - Enhanced with additional withdrawal options and `NetworkFamily`
  - `GetOrderPreviewResponse` - Expanded with comprehensive preview details
  - `HierarchyType` - Added new hierarchy type options
  - `VisibilityStatus` - Enhanced visibility control options
  - `InvoiceItem`, `PMAssetInfo`, `TieredPricingFee` - Streamlined model structures
  - `OrderFill Model` - Added missing fields from OpenAPI specification:
    - `ClientProductId` - Settlement currency indicator
    - `VenueFees` - Venue-specific fees
    - `CesCommission` - CES commission details
    - Updated builder pattern to support new fields

- **Transaction & Conversion Responses**
  - Enhanced `CreateConversionResponse` and `CreateTransferResponse` with additional metadata
  - Improved `CreateWithdrawalResponse` with comprehensive withdrawal details

- **Example Project Structure** - Converted examples to standalone C# scripts (.csx) using dotnet-script for easier execution
- **Documentation** - Enhanced README with dotnet-script installation instructions and improved example usage guidance

### Fixed

- **WalletsService** - Fixed `CreateWalletDepositAddress` method to properly pass request body to API

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
