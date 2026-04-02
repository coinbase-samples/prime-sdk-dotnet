# Prime example commands

Run these from the **repository root** using [`dotnet run --file`](https://learn.microsoft.com/dotnet/core/tools/dotnet-run).

## Environment (required for most scripts)

- `PRIME_ACCESS_KEY`, `PRIME_PASSPHRASE`, `PRIME_SIGNING_KEY` — API credentials (see `.env.example`).
- `PRIME_PORTFOLIO_ID` — used whenever an example would take `--portfolioId`, and for `--sourcePortfolioId` on allocation creates.
- `PRIME_ENTITY_ID` — used whenever an example would take `--entityId`.

This list **omits** `--portfolioId`, `--entityId`, and `--sourcePortfolioId` from the command line; set the env vars above instead. It also **omits optional** CLI flags (filters, pagination, etc.); only arguments the script treats as required are shown.

Required flags were detected from each script’s `Error: --… is required` checks (and “either/or” quantity rules for a few order examples).

## Activities

### `GetActivity.cs`

Get activity by ID.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/activities/GetActivity.cs -- --activityId <activity-id>
```

### `GetPortfolioActivity.cs`

Get portfolio activity by ID.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/activities/GetPortfolioActivity.cs -- --activityId <activity-id>
```

### `ListActivities.cs`

List activities for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/activities/ListActivities.cs
```

### `ListEntityActivities.cs`

List activities for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/activities/ListEntityActivities.cs
```

## Addressbook

### `CreateAddressBookEntry.cs`

Create a new address book entry.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/addressbook/CreateAddressBookEntry.cs -- --address <address> --currencySymbol <currency-symbol>
```

### `ListAddressBookEntries.cs`

List address book entries for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/addressbook/ListAddressBookEntries.cs
```

## Advanced transfer

### `CancelAdvancedTransfer.cs`

Cancel Advanced Transfer.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/advancedtransfer/CancelAdvancedTransfer.cs -- --advancedTransferId <advanced-transfer-id>
```

### `CreateAdvancedTransfer.cs`

Create Advanced Transfer.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/advancedtransfer/CreateAdvancedTransfer.cs -- --advancedTransfer <advanced-transfer>
```

### `ListAdvancedTransferTransactions.cs`

List Advanced Transfer Transactions.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/advancedtransfer/ListAdvancedTransferTransactions.cs -- --advancedTransferId <advanced-transfer-id>
```

### `ListAdvancedTransfers.cs`

List Advanced Transfers.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/advancedtransfer/ListAdvancedTransfers.cs
```

## Allocations

### `CreateAllocation.cs`

Create an allocation.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/allocations/CreateAllocation.cs -- --productId <product-id> --orderIds <order-ids> --destinationPortfolioId <destination-portfolio-id> --amount <amount>
```

### `CreateNetAllocation.cs`

Create a net allocation.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/allocations/CreateNetAllocation.cs -- --productId <product-id> --orderIds <order-ids> --destinationPortfolioId <destination-portfolio-id> --amount <amount>
```

### `GetAllocation.cs`

Get an allocation by ID.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/allocations/GetAllocation.cs -- --allocationId <allocation-id>
```

### `ListAllocationsByClientNettingId.cs`

List Allocations By Client Netting Id.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/allocations/ListAllocationsByClientNettingId.cs -- --nettingId <netting-id>
```

### `ListAllocationsByNettingId.cs`

Get allocations by client netting ID.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/allocations/ListAllocationsByNettingId.cs -- --nettingId <netting-id>
```

### `ListPortfolioAllocations.cs`

List allocations for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/allocations/ListPortfolioAllocations.cs -- --startDate <start-date>
```

## Assets

### `ListAssets.cs`

List assets for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/assets/ListAssets.cs
```

## Balances

### `GetWalletBalance.cs`

Get balance for a specific wallet.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/balances/GetWalletBalance.cs -- --walletId <wallet-id>
```

### `ListEntityBalances.cs`

List balances for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/balances/ListEntityBalances.cs
```

### `ListOnchainWalletBalances.cs`

List onchain (web3) balances for a wallet.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/balances/ListOnchainWalletBalances.cs -- --walletId <wallet-id>
```

### `ListPortfolioBalances.cs`

List balances for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/balances/ListPortfolioBalances.cs
```

## Commission

### `GetPortfolioCommission.cs`

Get commission for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/commission/GetPortfolioCommission.cs
```

## Financing

### `CreateNewLocates.cs`

Create new locates for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/CreateNewLocates.cs -- --symbol <symbol> --amount <amount>
```

### `GetCrossMarginOverview.cs`

Get cross margin overview for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/GetCrossMarginOverview.cs
```

### `GetEntityLocateAvailabilities.cs`

Get locate availabilities for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/GetEntityLocateAvailabilities.cs
```

### `GetMarginInformation.cs`

Get margin information for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/GetMarginInformation.cs
```

### `GetPortfolioBuyingPower.cs`

Get buying power for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/GetPortfolioBuyingPower.cs -- --baseCurrency <base-currency> --quoteCurrency <quote-currency>
```

### `GetPortfolioCreditInformation.cs`

Get credit information for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/GetPortfolioCreditInformation.cs
```

### `GetPortfolioWithdrawalPower.cs`

Get withdrawal power for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/GetPortfolioWithdrawalPower.cs
```

### `GetTradeFinanceTieredPricingFees.cs`

Get trade finance tiered pricing fees for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/GetTradeFinanceTieredPricingFees.cs
```

### `ListExistingLocates.cs`

List existing locates for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/ListExistingLocates.cs
```

### `ListFinancingEligibleAssets.cs`

List financing eligible assets.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/ListFinancingEligibleAssets.cs
```

### `ListInterestAccruals.cs`

List interest accruals for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/ListInterestAccruals.cs
```

### `ListInterestAccrualsForPortfolio.cs`

List interest accruals for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/ListInterestAccrualsForPortfolio.cs
```

### `ListMarginCallSummaries.cs`

List margin call summaries for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/ListMarginCallSummaries.cs
```

### `ListMarginConversions.cs`

List margin conversions for a portfolio (deprecated).

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/ListMarginConversions.cs
```

### `ListTradeFinanceObligations.cs`

List trade finance obligations for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/financing/ListTradeFinanceObligations.cs
```

## Futures

### `CancelEntityFuturesSweep.cs`

Cancel a scheduled futures sweep for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/CancelEntityFuturesSweep.cs
```

### `GetFcmBalance.cs`

Get FCM balance summary for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmBalance.cs
```

### `GetFcmEquity.cs`

Get Fcm Equity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmEquity.cs
```

### `GetFcmMarginCallDetails.cs`

Get FCM margin call details for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmMarginCallDetails.cs
```

### `GetFcmRiskLimits.cs`

Get FCM risk limits for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmRiskLimits.cs
```

### `GetFcmSettings.cs`

Get FCM settings for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/GetFcmSettings.cs
```

### `GetPositions.cs`

Get futures positions for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/GetPositions.cs
```

### `ListEntityFuturesSweeps.cs`

List futures sweeps for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/ListEntityFuturesSweeps.cs
```

### `ScheduleEntityFuturesSweep.cs`

Schedule a futures sweep for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/ScheduleEntityFuturesSweep.cs -- --currency <currency>
```

### `SetAutoSweep.cs`

Set auto sweep for an entity's futures.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/SetAutoSweep.cs
```

### `SetFcmSettings.cs`

Set FCM settings for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/futures/SetFcmSettings.cs
```

## Invoice

### `ListInvoices.cs`

List invoices for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/invoice/ListInvoices.cs
```

## On-chain address book

### `CreateOnchainAddressBookEntry.cs`

Create Onchain Address Book Entry.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/onchainaddressbook/CreateOnchainAddressBookEntry.cs -- --addressGroup <address-group>
```

### `CreateOnchainAddressGroup.cs`

Create an onchain address group.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/onchainaddressbook/CreateOnchainAddressGroup.cs -- --name <name> --address <address>
```

### `DeleteOnchainAddressGroup.cs`

Delete an onchain address group.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/onchainaddressbook/DeleteOnchainAddressGroup.cs -- --addressGroupId <address-group-id>
```

### `ListOnchainAddressGroups.cs`

List onchain address groups for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/onchainaddressbook/ListOnchainAddressGroups.cs
```

### `UpdateOnchainAddressBookEntry.cs`

Update Onchain Address Book Entry.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/onchainaddressbook/UpdateOnchainAddressBookEntry.cs -- --addressGroup <address-group>
```

### `UpdateOnchainAddressGroup.cs`

Update an onchain address group.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/onchainaddressbook/UpdateOnchainAddressGroup.cs -- --addressGroupId <address-group-id>
```

## Orders

### `AcceptQuote.cs`

Accept a quote (RFQ - Request for Quote).

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/AcceptQuote.cs -- --productId <product-id> --quoteId <quote-id> --clientOrderId <client-order-id>
```

### `CancelOrder.cs`

Cancel an existing order.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/CancelOrder.cs -- --orderId <order-id>
```

### `CreateOrder.cs`

Create a new trading order.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/CreateOrder.cs -- --productId <product-id> --side <side> --type <type> --baseQuantity <base-quantity>
```

*Use `--quoteValue <quote-value>` instead of `--baseQuantity` when sizing by quote value.*

### `CreateQuote.cs`

Create a quote (RFQ - Request for Quote).

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/CreateQuote.cs -- --productId <product-id> --side <side> --baseQuantity <base-quantity>
```

*Use `--quoteValue <quote-value>` instead of `--baseQuantity` when sizing by quote value.*

### `EditOrder.cs`

Edit an existing trading order.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/EditOrder.cs -- --orderId <order-id> --origClientOrderId <orig-client-order-id> --clientOrderId <client-order-id>
```

### `GetOrder.cs`

Get details of a specific order.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/GetOrder.cs -- --orderId <order-id>
```

### `GetOrderPreview.cs`

Preview an order before creating it.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/GetOrderPreview.cs -- --productId <product-id> --side <side> --type <type> --baseQuantity <base-quantity>
```

*Use `--quoteValue <quote-value>` instead of `--baseQuantity` when sizing by quote value.*

### `ListOpenOrders.cs`

List open orders for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/ListOpenOrders.cs
```

### `ListOrderEditHistory.cs`

List edit history for a specific order.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/ListOrderEditHistory.cs -- --orderId <order-id>
```

### `ListOrderFills.cs`

List fills for a specific order.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/ListOrderFills.cs -- --orderId <order-id>
```

### `ListPortfolioFills.cs`

List all fills for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/ListPortfolioFills.cs
```

### `ListPortfolioOrders.cs`

List all orders for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/orders/ListPortfolioOrders.cs
```

## Paymentmethods

### `GetEntityPaymentMethod.cs`

Get payment method details by ID.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/paymentmethods/GetEntityPaymentMethod.cs -- --paymentMethodId <payment-method-id>
```

### `ListEntityPaymentMethods.cs`

List payment methods for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/paymentmethods/ListEntityPaymentMethods.cs
```

## Portfolios

### `GetPortfolio.cs`

Get portfolio by ID.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/portfolios/GetPortfolio.cs
```

### `GetPortfolioCounterparty.cs`

Get portfolio counterparty information.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/portfolios/GetPortfolioCounterparty.cs
```

### `ListPortfolios.cs`

List all portfolios.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/portfolios/ListPortfolios.cs
```

## Positions

### `ListAggregateEntityPositions.cs`

List aggregate positions for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/positions/ListAggregateEntityPositions.cs
```

### `ListEntityPositions.cs`

List positions for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/positions/ListEntityPositions.cs
```

## Products

### `GetCandles.cs`

Get OHLCV candles for a portfolio product.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/products/GetCandles.cs
```

### `ListPortfolioProducts.cs`

List tradable products for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/products/ListPortfolioProducts.cs
```

## Staking

### `ClaimStakingRewards.cs`

Claim staking rewards from a wallet.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/ClaimStakingRewards.cs -- --walletId <wallet-id>
```

### `CreatePortfolioStake.cs`

Create a portfolio-level stake.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/CreatePortfolioStake.cs -- --currencySymbol <currency-symbol> --amount <amount>
```

### `CreatePortfolioUnstake.cs`

Create a portfolio-level unstake.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/CreatePortfolioUnstake.cs -- --currencySymbol <currency-symbol> --amount <amount>
```

### `CreateStake.cs`

Create a wallet-level stake.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/CreateStake.cs -- --walletId <wallet-id>
```

### `CreateUnstake.cs`

Create a wallet-level unstake.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/CreateUnstake.cs -- --walletId <wallet-id>
```

### `GetStakingStatus.cs`

Get Staking Status.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/GetStakingStatus.cs -- --walletId <wallet-id>
```

### `GetUnstakingStatus.cs`

Get Unstaking Status.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/GetUnstakingStatus.cs -- --walletId <wallet-id>
```

### `ListTransactionValidators.cs`

List ETH 0x02 validators for wallet-level stake transactions.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/ListTransactionValidators.cs
```

### `PreviewUnstake.cs`

Preview Unstake.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/staking/PreviewUnstake.cs -- --walletId <wallet-id> --amount <amount>
```

## Transactions

### `CreateConversion.cs`

Create a currency conversion.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/CreateConversion.cs -- --walletId <wallet-id> --sourceSymbol <source-symbol> --destinationSymbol <destination-symbol> --amount <amount>
```

### `CreateOnchainTransaction.cs`

Create Onchain Transaction.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/CreateOnchainTransaction.cs -- --walletId <wallet-id> --rawUnsignedTxn <raw-unsigned-txn>
```

### `CreateTransfer.cs`

Create a transfer between wallets.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/CreateTransfer.cs -- --walletId <wallet-id> --destination <destination> --currencySymbol <currency-symbol> --amount <amount>
```

### `CreateWithdrawal.cs`

Create a withdrawal.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/CreateWithdrawal.cs -- --walletId <wallet-id> --currencySymbol <currency-symbol> --amount <amount> --destinationType <destination-type>
```

### `GetTransaction.cs`

Get transaction by ID.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/GetTransaction.cs -- --transactionId <transaction-id>
```

### `GetTransactionTravelRuleData.cs`

Get Transaction Travel Rule Data.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/GetTransactionTravelRuleData.cs -- --transactionId <transaction-id>
```

### `ListPortfolioTransactions.cs`

List transactions for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/ListPortfolioTransactions.cs
```

### `ListWalletTransactions.cs`

List transactions for a wallet.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/ListWalletTransactions.cs -- --walletId <wallet-id>
```

### `SubmitDepositTravelRuleData.cs`

Submit Deposit Travel Rule Data.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/transactions/SubmitDepositTravelRuleData.cs -- --transactionId <transaction-id>
```

## Users

### `ListPortfolioUsers.cs`

List users for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/users/ListPortfolioUsers.cs
```

### `ListUsers.cs`

List users for an entity.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/users/ListUsers.cs
```

## Wallets

### `CreateWallet.cs`

Create a new wallet.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/wallets/CreateWallet.cs -- --symbol <symbol> --type <type>
```

### `CreateWalletDepositAddress.cs`

Create a deposit address for a wallet.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/wallets/CreateWalletDepositAddress.cs -- --walletId <wallet-id> --networkId <network-id>
```

### `GetWallet.cs`

Get a specific wallet.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/wallets/GetWallet.cs -- --walletId <wallet-id>
```

### `GetWalletDepositInstructions.cs`

Get deposit instructions for a wallet.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/wallets/GetWalletDepositInstructions.cs -- --walletId <wallet-id>
```

### `ListWalletAddresses.cs`

List addresses for a wallet.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/wallets/ListWalletAddresses.cs -- --walletId <wallet-id>
```

### `ListWallets.cs`

List wallets for a portfolio.

```bash
dotnet run --file src/CoinbaseSdk/PrimeExample/examples/wallets/ListWallets.cs
```
