# Changelog

## [0.4.0] - 2025-JUL-23

### Added

- Activities Endpoints
  - GetActivity (individual activity details)
- Futures Endpoints  
  - GetFcmMarginCallDetails
  - GetFcmRiskLimits
- Orders Endpoints
  - GetOrderEditHistory 
  - GetOrder (replaces GetOrderByOrderId)
- Wallets Endpoints
  - CreateWalletDepositAddress
  - GetWallet (replaces GetWalletById)
  - ListWalletAddresses
- New Model Classes
  - Network (blockchain network information)
  - HierarchyType (organizational hierarchy types)
  - WalletAddress (wallet address details)
  - OrderEdit and OrderEditValues (order modification tracking)
  - FcmMarginCallDetails and FcmRiskLimit (futures margin management)

### Changed

- Updated service interfaces and implementations across Activities, Futures, Orders, and Wallets services
- Reorganized example project structure from `example/` to `examples/` directory

### Removed

- Deprecated request/response classes:
  - GetOrderByOrderIdRequest/Response (replaced by GetOrderRequest/Response)
  - GetWalletByIdRequest/Response (replaced by GetWalletRequest/Response)
- Legacy example structure

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
