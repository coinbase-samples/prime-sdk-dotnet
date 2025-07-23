#\!/usr/bin/env python3

# Complete list of all endpoints from the OpenAPI spec
all_endpoints = {
    "Activities": [
        ("GET", "/v1/activities/{activity_id}", "PrimeRESTAPI_GetActivity", "Get Activity by Activity ID"),
        ("GET", "/v1/entities/{entity_id}/activities", "PrimeRESTAPI_GetEntityActivities", "List Entity Activities"),
        ("GET", "/v1/portfolios/{portfolio_id}/activities", "PrimeRESTAPI_GetPortfolioActivities", "List Activities"),
        ("GET", "/v1/portfolios/{portfolio_id}/activities/{activity_id}", "PrimeRESTAPI_GetPortfolioActivity", "Get Portfolio Activity by Activity ID")
    ],
    
    "Allocations": [
        ("POST", "/v1/allocations", "PrimeRESTAPI_CreateAllocation", "Create Portfolio Allocations"),
        ("POST", "/v1/allocations/net", "PrimeRESTAPI_CreateNetAllocation", "Create Portfolio Net Allocations"),
        ("GET", "/v1/portfolios/{portfolio_id}/allocations", "PrimeRESTAPI_GetPortfolioAllocations", "Get Portfolio Allocations"),
        ("GET", "/v1/portfolios/{portfolio_id}/allocations/net/{netting_id}", "PrimeRESTAPI_GetAllocationsByClientNettingId", "Get Net Allocations by Netting ID"),
        ("GET", "/v1/portfolios/{portfolio_id}/allocations/{allocation_id}", "PrimeRESTAPI_GetAllocation", "Get Allocation by ID")
    ],
    
    "Assets": [
        ("GET", "/v1/entities/{entity_id}/assets", "PrimeRESTAPI_GetEntityAssets", "List Assets")
    ],
    
    "Balances": [
        ("GET", "/v1/entities/{entity_id}/balances", "PrimeRESTAPI_ListEntityBalances", "List Entity Balances"),
        ("GET", "/v1/portfolios/{portfolio_id}/balances", "PrimeRESTAPI_GetPortfolioBalances", "List Portfolio Balances"),
        ("GET", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/balance", "PrimeRESTAPI_GetWalletBalance", "Get Wallet Balance"),
        ("GET", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/web3_balances", "PrimeRESTAPI_ListWeb3WalletBalances", "List Onchain Wallet Balances")
    ],
    
    "Commission": [
        ("GET", "/v1/portfolios/{portfolio_id}/commission", "PrimeRESTAPI_GetPortfolioCommission", "Get Portfolio Commission")
    ],
    
    "Financing": [
        ("GET", "/v1/entities/{entity_id}/accruals", "PrimeRESTAPI_GetInterestAccruals", "List Interest Accruals"),
        ("GET", "/v1/portfolios/{portfolio_id}/accruals", "PrimeRESTAPI_GetPortfolioInterestAccruals", "List Interest Accruals For Portfolio"),
        ("GET", "/v1/entities/{entity_id}/locates_availability", "PrimeRESTAPI_GetLocateAvailabilities", "Get Entity Locate Availabilities"),
        ("GET", "/v1/entities/{entity_id}/margin", "PrimeRESTAPI_GetMarginInformation", "Get Margin Information"),
        ("GET", "/v1/entities/{entity_id}/margin_summaries", "PrimeRESTAPI_GetMarginSummaries", "List Margin Call Summaries"),
        ("GET", "/v1/entities/{entity_id}/tf_tiered_fees", "PrimeRESTAPI_GetTFTieredPricingFees", "Get Trade Finance Tiered Pricing Fees"),
        ("GET", "/v1/portfolios/{portfolio_id}/buying_power", "PrimeRESTAPI_GetBuyingPower", "Get Portfolio Buying Power"),
        ("GET", "/v1/portfolios/{portfolio_id}/credit", "PrimeRESTAPI_GetPostTradeCredit", "Get Portfolio Credit Information"),
        ("GET", "/v1/portfolios/{portfolio_id}/locates", "PrimeRESTAPI_GetExistingLocates", "List Existing Locates"),
        ("POST", "/v1/portfolios/{portfolio_id}/locates", "PrimeRESTAPI_CreateNewLocates", "Create New Locates"),
        ("GET", "/v1/portfolios/{portfolio_id}/margin_conversions", "PrimeRESTAPI_GetMarginConversions", "List Margin Conversions"),
        ("GET", "/v1/portfolios/{portfolio_id}/withdrawal_power", "PrimeRESTAPI_GetWithdrawalPower", "Get Portfolio Withdrawal Power")
    ],
    
    "Futures": [
        ("POST", "/v1/entities/{entity_id}/futures/auto_sweep", "PrimeRESTAPI_SetAutoSweep", "Set Auto Sweep"),
        ("GET", "/v1/entities/{entity_id}/futures/balance_summary", "PrimeRESTAPI_GetFcmBalance", "Get Entity FCM Balance"),
        ("GET", "/v1/entities/{entity_id}/futures/margin_call_details", "PrimeRESTAPI_GetFcmMarginCallDetails", "Get FCM Margin Call Details"),
        ("GET", "/v1/entities/{entity_id}/futures/positions", "PrimeRESTAPI_GetPositions", "Get Entity Positions"),
        ("GET", "/v1/entities/{entity_id}/futures/risk_limits", "PrimeRESTAPI_GetFcmRiskLimits", "Get FCM Risk Limits"),
        ("GET", "/v1/entities/{entity_id}/futures/sweeps", "PrimeRESTAPI_GetFuturesSweeps", "List Entity Futures Sweeps"),
        ("POST", "/v1/entities/{entity_id}/futures/sweeps", "PrimeRESTAPI_ScheduleFuturesSweep", "Schedule Entity Futures Sweep"),
        ("DELETE", "/v1/entities/{entity_id}/futures/sweeps", "PrimeRESTAPI_CancelFuturesSweep", "Cancel Entity Futures Sweep")
    ],
    
    "Invoice": [
        ("GET", "/v1/entities/{entity_id}/invoices", "PrimeRESTAPI_GetInvoices", "List Invoices")
    ],
    
    "Orders": [
        ("POST", "/v1/portfolios/{portfolio_id}/accept_quote", "PrimeRESTAPI_AcceptQuote", "Accept Quote"),
        ("GET", "/v1/portfolios/{portfolio_id}/fills", "PrimeRESTAPI_GetPortfolioFills", "List Portfolio Fills"),
        ("GET", "/v1/portfolios/{portfolio_id}/open_orders", "PrimeRESTAPI_GetOpenOrders", "List Open Orders"),
        ("POST", "/v1/portfolios/{portfolio_id}/order", "PrimeRESTAPI_CreateOrder", "Create Order"),
        ("POST", "/v1/portfolios/{portfolio_id}/order_preview", "PrimeRESTAPI_OrderPreview", "Get Order Preview"),
        ("GET", "/v1/portfolios/{portfolio_id}/orders", "PrimeRESTAPI_GetOrders", "List Portfolio Orders"),
        ("GET", "/v1/portfolios/{portfolio_id}/orders/{order_id}", "PrimeRESTAPI_GetOrder", "Get Order by Order ID"),
        ("POST", "/v1/portfolios/{portfolio_id}/orders/{order_id}/cancel", "PrimeRESTAPI_CancelOrder", "Cancel Order"),
        ("GET", "/v1/portfolios/{portfolio_id}/orders/{order_id}/edit_history", "PrimeRESTAPI_GetOrderEditHistory", "List Order Edit History"),
        ("GET", "/v1/portfolios/{portfolio_id}/orders/{order_id}/fills", "PrimeRESTAPI_GetOrderFills", "List Order Fills"),
        ("POST", "/v1/portfolios/{portfolio_id}/rfq", "PrimeRESTAPI_CreateQuoteRequest", "Create Quote Request")
    ],
    
    "Payment Methods": [
        ("GET", "/v1/entities/{entity_id}/payment-methods", "PrimeRESTAPI_GetEntityPaymentMethods", "List Entity Payment Methods"),
        ("GET", "/v1/entities/{entity_id}/payment-methods/{payment_method_id}", "PrimeRESTAPI_GetEntityPaymentMethodDetails", "Get Entity Payment Method")
    ],
    
    "Portfolios": [
        ("GET", "/v1/portfolios", "PrimeRESTAPI_GetPortfolios", "List Portfolios"),
        ("GET", "/v1/portfolios/{portfolio_id}", "PrimeRESTAPI_GetPortfolio", "Get Portfolio by Portfolio ID"),
        ("GET", "/v1/portfolios/{portfolio_id}/users", "PrimeRESTAPI_GetPortfolioUsers", "List Portfolio Users")
    ],
    
    "Positions": [
        ("GET", "/v1/entities/{entity_id}/aggregate_positions", "PrimeRESTAPI_ListAggregateEntityPositions", "List Aggregate Entity Positions"),
        ("GET", "/v1/entities/{entity_id}/positions", "PrimeRESTAPI_ListEntityPositions", "List Entity Positions")
    ],
    
    "Products": [
        ("GET", "/v1/portfolios/{portfolio_id}/products", "PrimeRESTAPI_GetPortfolioProducts", "List Portfolio Products")
    ],
    
    "Staking": [
        ("POST", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/staking/initiate", "PrimeRESTAPI_StakingInitiate", "Request to stake or delegate a wallet"),
        ("POST", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/staking/unstake", "PrimeRESTAPI_StakingUnstake", "Request to unstake a wallet")
    ],
    
    "Transactions": [
        ("GET", "/v1/portfolios/{portfolio_id}/transactions", "PrimeRESTAPI_GetPortfolioTransactions", "List Portfolio Transactions"),
        ("GET", "/v1/portfolios/{portfolio_id}/transactions/{transaction_id}", "PrimeRESTAPI_GetTransaction", "Get Transaction by Transaction ID"),
        ("POST", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/conversion", "PrimeRESTAPI_CreateConversion", "Create Conversion"),
        ("POST", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/onchain_transaction", "PrimeRESTAPI_CreateOnchainTransaction", "Create Onchain Transaction"),
        ("GET", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/transactions", "PrimeRESTAPI_GetWalletTransactions", "List Wallet Transactions"),
        ("POST", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/transfers", "PrimeRESTAPI_CreateWalletTransfer", "Create Transfer"),
        ("POST", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/withdrawals", "PrimeRESTAPI_CreateWalletWithdrawal", "Create Withdrawal")
    ],
    
    "Users": [
        ("GET", "/v1/entities/{entity_id}/users", "PrimeRESTAPI_GetEntityUsers", "List Users")
    ],
    
    "Wallets": [
        ("GET", "/v1/portfolios/{portfolio_id}/wallets", "PrimeRESTAPI_GetWallets", "List Portfolio Wallets"),
        ("POST", "/v1/portfolios/{portfolio_id}/wallets", "PrimeRESTAPI_CreateWallet", "Create Wallet"),
        ("GET", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}", "PrimeRESTAPI_GetWallet", "Get Wallet by Wallet ID"),
        ("GET", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/addresses", "PrimeRESTAPI_ListWalletAddresses", "List Wallet Addresses"),
        ("POST", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/addresses", "PrimeRESTAPI_CreateWalletDepositAddress", "Create Wallet Deposit Address"),
        ("GET", "/v1/portfolios/{portfolio_id}/wallets/{wallet_id}/deposit_instructions", "PrimeRESTAPI_GetWalletDepositInstructions", "Get Wallet Deposit Instructions")
    ],
    
    "Address Book": [
        ("GET", "/v1/portfolios/{portfolio_id}/address_book", "PrimeRESTAPI_GetPortfolioAddressBook", "Get Address Book"),
        ("POST", "/v1/portfolios/{portfolio_id}/address_book", "PrimeRESTAPI_CreatePortfolioAddressBookEntry", "Create Address Book Entry")
    ],
    
    "Onchain Address Book": [
        ("PUT", "/v1/portfolios/{portfolio_id}/onchain_address_group", "PrimeRESTAPI_UpdateOnchainAddressGroup", "Update Onchain Address Book Entry"),
        ("POST", "/v1/portfolios/{portfolio_id}/onchain_address_group", "PrimeRESTAPI_CreateOnchainAddressGroup", "Create Onchain Address Book Entry"),
        ("DELETE", "/v1/portfolios/{portfolio_id}/onchain_address_group/{address_group_id}", "PrimeRESTAPI_DeleteOnchainAddressGroup", "Delete Onchain Address Group")
    ],
    
    "Onchain Address Groups": [
        ("GET", "/v1/portfolios/{portfolio_id}/onchain_address_groups", "PrimeRESTAPI_ListOnchainAddressGroups", "List Onchain Address Groups")
    ]
}

def print_summary():
    total_endpoints = sum(len(endpoints) for endpoints in all_endpoints.values())
    
    print("# COMPLETE COINBASE PRIME API ENDPOINT ANALYSIS")
    print("=" * 60)
    print(f"\n**Total Endpoints:** {total_endpoints}")
    print(f"**Total Domains:** {len(all_endpoints)}")
    print("\n## DOMAIN SUMMARY")
    print("-" * 30)
    
    for domain, endpoints in sorted(all_endpoints.items()):
        print(f"- **{domain}**: {len(endpoints)} endpoints")
    
    print("\n## DETAILED ENDPOINT BREAKDOWN")
    print("-" * 40)
    
    for domain, endpoints in sorted(all_endpoints.items()):
        print(f"\n### {domain.upper()} ({len(endpoints)} endpoints)")
        
        for method, path, operation_id, summary in endpoints:
            print(f"- `{method} {path}` - {summary}")
            print(f"  - Operation ID: `{operation_id}`")

def print_sdk_comparison_template():
    print("\n\n# SDK IMPLEMENTATION COMPARISON TEMPLATE")
    print("=" * 50)
    print("\nTo compare with the current SDK implementation, check these service files:")
    print("\n## Service Mapping")
    print("- **Activities** → `ActivitiesService.cs`")
    print("- **Allocations** → `AllocationsService.cs`") 
    print("- **Assets** → `AssetsService.cs`")
    print("- **Balances** → `BalancesService.cs`")
    print("- **Commission** → `CommissionService.cs`")
    print("- **Financing** → `FinancingService.cs`")
    print("- **Futures** → `FuturesService.cs`")
    print("- **Invoice** → `InvoiceService.cs`")
    print("- **Orders** → `OrdersService.cs`")
    print("- **Payment Methods** → `PaymentMethodsService.cs`")
    print("- **Portfolios** → `PortfoliosService.cs`")
    print("- **Positions** → `PositionsService.cs`")
    print("- **Products** → `ProductsService.cs`")
    print("- **Staking** → `StakingService.cs`")
    print("- **Transactions** → `TransactionsService.cs`")
    print("- **Users** → `UsersService.cs`")
    print("- **Wallets** → `WalletsService.cs`")
    print("- **Address Book** → `AddressBookService.cs`")
    print("- **Onchain Address Book** → `OnchainAddressBookService.cs`")

if __name__ == "__main__":
    print_summary()
    print_sdk_comparison_template()
