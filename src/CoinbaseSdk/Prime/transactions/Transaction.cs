/*
 * Copyright 2024-present Coinbase Global, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  public class Transaction
  {
    public string? Id { get; set; }

    [JsonPropertyName("wallet_id")]
    public string? WalletId { get; set; }

    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }

    public TransactionType Type { get; set; }

    public TransactionStatus Status { get; set; }

    public string? Symbol { get; set; }

    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    [JsonPropertyName("completed_at")]
    public string? CompletedAt { get; set; }

    public string? Amount { get; set; }

    [JsonPropertyName("transfer_from")]
    public TransferLocation? TransferFrom { get; set; }

    [JsonPropertyName("transfer_to")]
    public TransferLocation? TransferTo { get; set; }

    [JsonPropertyName("network_fees")]
    public string? NetworkFees { get; set; }

    public string? Fees { get; set; }

    [JsonPropertyName("fee_symbol")]
    public string? FeeSymbol { get; set; }

    [JsonPropertyName("blockchain_ids")]
    public string[] BlockchainIds { get; set; } = [];

    [JsonPropertyName("transaction_id")]
    public string? TransactionId { get; set; }

    [JsonPropertyName("destination_symbol")]
    public string? DestinationSymbol { get; set; }

    [JsonPropertyName("estimated_network_fees")]
    public EstimatedNetworkFees? EstimatedNetworkFees { get; set; }

    public string? Network { get; set; }

    [JsonPropertyName("estimated_asset_changes")]
    public AssetChange[] EstimatedAssetChanges { get; set; } = [];

    public TransactionMetadata? Metadata { get; set; }

    public Transaction() { }

    public class TransactionBuilder
    {
      private string? _id;
      private string? _walletId;
      private string? _portfolioId;
      private TransactionType _type;
      private TransactionStatus _status;
      private string? _symbol;
      private string? _createdAt;
      private string? _completedAt;
      private string? _amount;
      private TransferLocation? _transferFrom;
      private TransferLocation? _transferTo;
      private string? _networkFees;
      private string? _fees;
      private string? _feeSymbol;
      private string[] _blockchainIds = [];
      private string? _transactionId;
      private string? _destinationSymbol;
      private EstimatedNetworkFees? _estimatedNetworkFees;
      private string? _network;
      private AssetChange[] _estimatedAssetChanges = [];
      private TransactionMetadata? _metadata;

      public TransactionBuilder WithId(string id)
      {
        this._id = id;
        return this;
      }

      public TransactionBuilder WithWalletId(string walletId)
      {
        this._walletId = walletId;
        return this;
      }

      public TransactionBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public TransactionBuilder WithType(TransactionType type)
      {
        this._type = type;
        return this;
      }

      public TransactionBuilder WithStatus(TransactionStatus status)
      {
        this._status = status;
        return this;
      }

      public TransactionBuilder WithSymbol(string symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public TransactionBuilder WithCreatedAt(string createdAt)
      {
        this._createdAt = createdAt;
        return this;
      }

      public TransactionBuilder WithCompletedAt(string completedAt)
      {
        this._completedAt = completedAt;
        return this;
      }

      public TransactionBuilder WithAmount(string amount)
      {
        this._amount = amount;
        return this;
      }

      public TransactionBuilder WithTransferFrom(TransferLocation transferFrom)
      {
        this._transferFrom = transferFrom;
        return this;
      }

      public TransactionBuilder WithTransferTo(TransferLocation transferTo)
      {
        this._transferTo = transferTo;
        return this;
      }

      public TransactionBuilder WithNetworkFees(string networkFees)
      {
        this._networkFees = networkFees;
        return this;
      }

      public TransactionBuilder WithFees(string fees)
      {
        this._fees = fees;
        return this;
      }

      public TransactionBuilder WithFeeSymbol(string feeSymbol)
      {
        this._feeSymbol = feeSymbol;
        return this;
      }

      public TransactionBuilder WithBlockchainIds(string[] blockchainIds)
      {
        this._blockchainIds = blockchainIds;
        return this;
      }

      public TransactionBuilder WithTransactionId(string transactionId)
      {
        this._transactionId = transactionId;
        return this;
      }

      public TransactionBuilder WithDestinationSymbol(string destinationSymbol)
      {
        this._destinationSymbol = destinationSymbol;
        return this;
      }

      public TransactionBuilder WithEstimatedNetworkFees(EstimatedNetworkFees estimatedNetworkFees)
      {
        this._estimatedNetworkFees = estimatedNetworkFees;
        return this;
      }

      public TransactionBuilder WithNetwork(string network)
      {
        this._network = network;
        return this;
      }

      public TransactionBuilder WithEstimatedAssetChanges(AssetChange[] estimatedAssetChanges)
      {
        this._estimatedAssetChanges = estimatedAssetChanges;
        return this;
      }

      public TransactionBuilder WithMetadata(TransactionMetadata metadata)
      {
        this._metadata = metadata;
        return this;
      }

      public Transaction Build()
      {
        return new Transaction
        {
          Id = this._id,
          WalletId = this._walletId,
          PortfolioId = this._portfolioId,
          Type = this._type,
          Status = this._status,
          Symbol = this._symbol,
          CreatedAt = this._createdAt,
          CompletedAt = this._completedAt,
          Amount = this._amount,
          TransferFrom = this._transferFrom,
          TransferTo = this._transferTo,
          NetworkFees = this._networkFees,
          Fees = this._fees,
          FeeSymbol = this._feeSymbol,
          BlockchainIds = this._blockchainIds,
          TransactionId = this._transactionId,
          DestinationSymbol = this._destinationSymbol,
          EstimatedNetworkFees = this._estimatedNetworkFees,
          Network = this._network,
          EstimatedAssetChanges = this._estimatedAssetChanges,
          Metadata = this._metadata
        };
      }
    }
  }
}
