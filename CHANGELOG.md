# Changelog

## [Unreleased]

### Added

- New Model Classes
  - `Commission` - Fee calculation and commission details
  - `OrderEdit` - Order modification history tracking
  - `PortfolioUser` - Enhanced user model with portfolio context
  - `UserRole` - Comprehensive user permission roles enum
  - `SortDirection` - Sorting specification enum
  - `ListPortfolioFillsResponse` - Response wrapper for portfolio fills

### Enhanced

- **OrderFill Model** - Added missing fields from OpenAPI specification:
  - `ClientProductId` - Settlement currency indicator
  - `VenueFees` - Venue-specific fees
  - `CesCommission` - CES commission details
  - Updated builder pattern to support new fields

### Fixed

- **WalletsService** - Fixed `CreateWalletDepositAddress` method to properly pass request body to API
- **Network Model** - Added missing `Network.cs` model for blockchain network identification

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
