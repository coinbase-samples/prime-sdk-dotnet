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
  public class CreateTransferResponse
  {
    [JsonPropertyName("activity_id")]
    public string? ActivityId { get; set; }

    [JsonPropertyName("approval_url")]
    public string? ApprovalUrl { get; set; }

    public string? Symbol { get; set; }
    public string? Amount { get; set; }
    public string? Fee { get; set; }

    [JsonPropertyName("destination_address")]
    public string? DestinationAddress { get; set; }

    [JsonPropertyName("destination_type")]
    public string? DestinationType { get; set; }

    [JsonPropertyName("source_address")]
    public string? SourceAddress { get; set; }

    [JsonPropertyName("transaction_id")]
    public string? TransactionId { get; set; }

    public CreateTransferResponse() { }

    public class CreateTransferResponseBuilder
    {
      private string? _activityId;
      private string? _approvalUrl;
      private string? _symbol;
      private string? _amount;
      private string? _fee;
      private string? _destinationAddress;
      private string? _destinationType;
      private string? _sourceAddress;
      private string? _transactionId;

      public CreateTransferResponseBuilder WithActivityId(string? activityId)
      {
        this._activityId = activityId;
        return this;
      }

      public CreateTransferResponseBuilder WithApprovalUrl(string? approvalUrl)
      {
        this._approvalUrl = approvalUrl;
        return this;
      }

      public CreateTransferResponseBuilder WithSymbol(string? symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public CreateTransferResponseBuilder WithAmount(string? amount)
      {
        this._amount = amount;
        return this;
      }

      public CreateTransferResponseBuilder WithFee(string? fee)
      {
        this._fee = fee;
        return this;
      }

      public CreateTransferResponseBuilder WithDestinationAddress(string? destinationAddress)
      {
        this._destinationAddress = destinationAddress;
        return this;
      }

      public CreateTransferResponseBuilder WithDestinationType(string? destinationType)
      {
        this._destinationType = destinationType;
        return this;
      }

      public CreateTransferResponseBuilder WithSourceAddress(string? sourceAddress)
      {
        this._sourceAddress = sourceAddress;
        return this;
      }

      public CreateTransferResponseBuilder WithTransactionId(string? transactionId)
      {
        this._transactionId = transactionId;
        return this;
      }

      public CreateTransferResponse Build()
      {
        return new CreateTransferResponse
        {
          ActivityId = this._activityId,
          ApprovalUrl = this._approvalUrl,
          Symbol = this._symbol,
          Amount = this._amount,
          Fee = this._fee,
          DestinationAddress = this._destinationAddress,
          DestinationType = this._destinationType,
          SourceAddress = this._sourceAddress,
          TransactionId = this._transactionId
        };
      }
    }
  }
}
