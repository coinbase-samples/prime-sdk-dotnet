# Changelog

## [0.5.0] - 2026-MAR-30

### Added

- **New Service Area**
  - `AdvancedTransfersService` with 4 endpoints:
    - `ListAdvancedTransfers`
    - `CreateAdvancedTransfer`
    - `CancelAdvancedTransfer`
    - `ListAdvancedTransferTransactions`

- **New API Endpoints**
  - `FuturesService.GetFcmEquity`
  - `ProductsService.GetCandles`
  - `TransactionsService.GetTransactionTravelRuleData`
  - `TransactionsService.SubmitDepositTravelRuleData`
  - `StakingService.GetStakingStatus`
  - `StakingService.PreviewUnstake`
  - `StakingService.GetUnstakingStatus`

- **New Domain Models**
  - `AdvancedTransfer`, `BlindMatchMetadata`, `CommissionDetailTotal`
  - `FcmScheduledMaintenance`, `FcmTradingSessionDetails`
  - `FundMovement`, `FutureProductDetails`, `PerpetualProductDetails`
  - `RequestToSubmitTravelRuleDataForAnExistingDepositTransaction`
  - `StakingStatus`, `TravelRuleData`
  - `ValidatorAllocation`, `ValidatorStakingInfo`

- **New Enums**
  - `AdvancedTransferState`, `AdvancedTransferType`
  - `ContractExpiryType`, `ExpiringContractStatus`
  - `FcmMarginHealthState`, `FcmTradingSessionClosedReason`, `FcmTradingSessionState`
  - `ProductType`, `RiskManagementType`, `SecondaryPermission`, `StakeType`

- **Examples**
  - Added examples for all 11 new endpoints

### Changed

- **Updated Request Classes**
  - `CreateOrderRequest`: added `peg_offset_type`, `offset`, `wig_level`
  - `GetOrderPreviewRequest`: added `settl_currency`, `postOnly`, `display_quote_size`, `display_base_size`, `peg_offset_type`, `offset`, `wig_level`
  - `CreateWithdrawalRequest`: added `travel_rule_data`
  - `ListActivitiesRequest`: added `get_network_unified_activities`
  - `ListEntityActivitiesRequest`: added `get_network_unified_activities`
  - `ListPortfolioTransactionsRequest`: added `get_network_unified_transactions`, `travel_rule_status`
  - `ListWalletsRequest`: added `get_network_unified_wallets`
  - `ListPortfolioProductsRequest`: added `product_type`, `contract_expiry_type`, `expiring_contract_status`
  - `CreateOnchainTransactionRequest`: added `raw_unsigned_txn`

- **Updated Models** (via model generator)
  - `Balance`, `EntityUser`, `Fill`, `Order`, `PortfolioUser`, `Product`, `TravelRuleParty`, `WalletUnstakeInputs`
  - `RewardSubtype` enum updated with new values

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
