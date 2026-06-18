/*
 * Copyright 2024-present Coinbase Global, Inc.
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

namespace CoinbaseSdk.Prime.Transactions
{
  using CoinbaseSdk.Core.Http;

  public interface ITransactionsService
  {
    /// <summary>
    /// List Portfolio Transactions.
    /// </summary>
    public ListPortfolioTransactionsResponse ListPortfolioTransactions(
      ListPortfolioTransactionsRequest request,
      CallOptions? options = null);

    public Task<ListPortfolioTransactionsResponse> ListPortfolioTransactionsAsync(
      ListPortfolioTransactionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Transaction by Transaction ID.
    /// </summary>
    public GetTransactionResponse GetTransaction(
      GetTransactionRequest request,
      CallOptions? options = null);

    public Task<GetTransactionResponse> GetTransactionAsync(
      GetTransactionRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Transaction Travel Rule Data.
    /// </summary>
    public GetTransactionTravelRuleDataResponse GetTransactionTravelRuleData(
      GetTransactionTravelRuleDataRequest request,
      CallOptions? options = null);

    public Task<GetTransactionTravelRuleDataResponse> GetTransactionTravelRuleDataAsync(
      GetTransactionTravelRuleDataRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// List Wallet Transactions.
    /// </summary>
    public ListWalletTransactionsResponse ListWalletTransactions(
      ListWalletTransactionsRequest request,
      CallOptions? options = null);

    public Task<ListWalletTransactionsResponse> ListWalletTransactionsAsync(
      ListWalletTransactionsRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Create Conversion.
    /// </summary>
    public CreateConversionResponse CreateConversion(
      CreateConversionRequest request,
      CallOptions? options = null);

    public Task<CreateConversionResponse> CreateConversionAsync(
      CreateConversionRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Create Onchain Transaction.
    /// </summary>
    public CreateOnchainTransactionResponse CreateOnchainTransaction(
      CreateOnchainTransactionRequest request,
      CallOptions? options = null);

    public Task<CreateOnchainTransactionResponse> CreateOnchainTransactionAsync(
      CreateOnchainTransactionRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Create Transfer.
    /// </summary>
    public CreateTransferResponse CreateTransfer(
      CreateTransferRequest request,
      CallOptions? options = null);

    public Task<CreateTransferResponse> CreateTransferAsync(
      CreateTransferRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Create Withdrawal.
    /// </summary>
    public CreateWithdrawalResponse CreateWithdrawal(
      CreateWithdrawalRequest request,
      CallOptions? options = null);

    public Task<CreateWithdrawalResponse> CreateWithdrawalAsync(
      CreateWithdrawalRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);

    /// <summary>
    /// Submit Deposit Travel Rule Data.
    /// </summary>
    public SubmitDepositTravelRuleDataResponse SubmitDepositTravelRuleData(
      SubmitDepositTravelRuleDataRequest request,
      CallOptions? options = null);

    public Task<SubmitDepositTravelRuleDataResponse> SubmitDepositTravelRuleDataAsync(
      SubmitDepositTravelRuleDataRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default);
  }
}
