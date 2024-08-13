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
  public class GetTransactionByTransactionIdResponse
  {
    [JsonPropertyName("transaction")]
    public Transaction? Transaction { get; set; }

    public GetTransactionByTransactionIdResponse() { }

    public class GetTransactionByTransactionIdResponseBuilder
    {
      private Transaction? _transaction;

      public GetTransactionByTransactionIdResponseBuilder WithTransaction(Transaction? transaction)
      {
        this._transaction = transaction;
        return this;
      }

      public GetTransactionByTransactionIdResponse Build()
      {
        return new GetTransactionByTransactionIdResponse
        {
          Transaction = this._transaction
        };
      }
    }
  }
}
