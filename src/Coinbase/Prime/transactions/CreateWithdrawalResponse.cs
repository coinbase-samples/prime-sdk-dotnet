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

namespace Coinbase.Prime.Transactions
{
  using System.Text.Json.Serialization;
  public class CreateWithdrawalResponse
  {
    [JsonPropertyName("activity_id")]
    public string? ActivityId { get; set; }

    [JsonPropertyName("approval_url")]
    public string? ApprovalUrl { get; set; }

    public string? Symbol { get; set; }
    public string? Amount { get; set; }
    public string? Fee { get; set; }

    [JsonPropertyName("destination_type")]
    public DestinationType DestinationType { get; set; }

    [JsonPropertyName("source_type")]
    public string? SourceType { get; set; }

    [JsonPropertyName("blockchain_destination")]
    public BlockchainAddress? BlockchainDestination { get; set; }

    [JsonPropertyName("blockchain_source")]
    public BlockchainAddress? BlockchainSource { get; set; }

    [JsonPropertyName("transaction_id")]
    public string? TransactionId { get; set; }

    public CreateWithdrawalResponse() { }

    public class CreateWithdrawalResponseBuilder
    {
      private string? _activityId;
      private string? _approvalUrl;
      private string? _symbol;
      private string? _amount;
      private string? _fee;
      private DestinationType _destinationType;
      private string? _sourceType;
      private BlockchainAddress? _blockchainDestination;
      private BlockchainAddress? _blockchainSource;
      private string? _transactionId;

      public CreateWithdrawalResponseBuilder WithActivityId(string? activityId)
      {
        this._activityId = activityId;
        return this;
      }

      public CreateWithdrawalResponseBuilder WithApprovalUrl(string? approvalUrl)
      {
        this._approvalUrl = approvalUrl;
        return this;
      }

      public CreateWithdrawalResponseBuilder WithSymbol(string? symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public CreateWithdrawalResponseBuilder WithAmount(string? amount)
      {
        this._amount = amount;
        return this;
      }

      public CreateWithdrawalResponseBuilder WithFee(string? fee)
      {
        this._fee = fee;
        return this;
      }

      public CreateWithdrawalResponseBuilder WithDestinationType(DestinationType destinationType)
      {
        this._destinationType = destinationType;
        return this;
      }

      public CreateWithdrawalResponseBuilder WithSourceType(string? sourceType)
      {
        this._sourceType = sourceType;
        return this;
      }

      public CreateWithdrawalResponseBuilder WithBlockchainDestination(BlockchainAddress? blockchainDestination)
      {
        this._blockchainDestination = blockchainDestination;
        return this;
      }

      public CreateWithdrawalResponseBuilder WithBlockchainSource(BlockchainAddress? blockchainSource)
      {
        this._blockchainSource = blockchainSource;
        return this;
      }

      public CreateWithdrawalResponseBuilder WithTransactionId(string? transactionId)
      {
        this._transactionId = transactionId;
        return this;
      }

      public CreateWithdrawalResponse Build()
      {
        return new CreateWithdrawalResponse
        {
          ActivityId = this._activityId,
          ApprovalUrl = this._approvalUrl,
          Symbol = this._symbol,
          Amount = this._amount,
          Fee = this._fee,
          DestinationType = this._destinationType,
          SourceType = this._sourceType,
          BlockchainDestination = this._blockchainDestination,
          BlockchainSource = this._blockchainSource,
          TransactionId = this._transactionId
        };
      }
    }
  }
}
