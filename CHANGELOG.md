# Changelog

## [0.4.0] - 2025-JUL-23

### Added

- **New API Endpoints**
  - Activities: `GetActivity` - Retrieve individual activity details
  - Futures: `GetFcmMarginCallDetails` - FCM margin call information
  - Futures: `GetFcmRiskLimits` - FCM risk limit management
  - Orders: `GetOrderEditHistory` - Order modification audit trail
  - Orders: `GetOrder` - Retrieve individual order details
  - Wallets: `CreateWalletDepositAddress` - Generate new deposit addresses
  - Wallets: `ListWalletAddresses` - List all wallet addresses
  - Wallets: `GetWallet` - Retrieve individual wallet details

- **New Model Classes**
  - `Activity` - Activity details and metadata
  - `Commission` - Fee calculation and commission details
  - `FcmMarginCallDetails` - FCM margin call information
  - `FcmRiskLimit` - FCM risk limit details
  - `HierarchyType` - Organizational hierarchy types enum
  - `Network` - Blockchain network identification
  - `OrderEdit` - Order modification history tracking (model)
  - `OrderEditValues` - Order edit value details
  - `PortfolioUser` - Enhanced user model with portfolio context
  - `SortDirection` - Sorting specification enum
  - `UserRole` - Comprehensive user permission roles enum
  - `WalletAddress` - Comprehensive wallet address details

- **New Request/Response Models**
  - `GetActivityRequest` / Activities service
  - `GetFcmMarginCallDetailsRequest` / `GetFcmMarginCallDetailsResponse`
  - `GetFcmRiskLimitsRequest` / `GetFcmRiskLimitsResponse`
  - `GetOrderEditHistoryRequest` / `GetOrderEditHistoryResponse`
  - `GetOrderRequest` / `GetOrderResponse`
  - `GetWalletRequest` / `GetWalletResponse`
  - `CreateWalletDepositAddressRequest` / `CreateWalletDepositAddressResponse`
  - `ListWalletAddressesRequest` / `ListWalletAddressesResponse`
  - `ListPortfolioFillsResponse` - Response wrapper for portfolio fills

- **Top-Level Program Examples**
  - `examples/activities/GetActivity.cs`
  - `examples/futures/GetFcmMarginCallDetails.cs`
  - `examples/futures/GetFcmRiskLimits.cs`
  - `examples/orders/GetOrderEditHistory.cs`
  - `examples/wallets/CreateWalletDepositAddress.cs`
  - `examples/wallets/ListWalletAddresses.cs`

### Enhanced

- **Service Interfaces** - Added new methods to existing service interfaces:
  - `IActivitiesService` - Added `GetActivity` methods
  - `IFuturesService` - Added `GetFcmMarginCallDetails` and `GetFcmRiskLimits` methods
  - `IOrdersService` - Added `GetOrder` and `GetOrderEditHistory` methods
  - `IWalletsService` - Added `GetWallet`, `CreateWalletDepositAddress`, and `ListWalletAddresses` methods

- **OrderFill Model** - Added missing fields from OpenAPI specification:
  - `ClientProductId` - Settlement currency indicator
  - `VenueFees` - Venue-specific fees
  - `CesCommission` - CES commission details
  - Updated builder pattern to support new fields

- **Example Project Structure** - Reorganized examples with top-level program pattern for direct execution

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
