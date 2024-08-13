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
  using CoinbaseSdk.Prime.Common;
  public class ListWalletTransactionsResponse
  {
    [JsonPropertyName("transactions")]
    public Transaction[] Transactions { get; set; } = [];

    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }

    public ListWalletTransactionsResponse() { }

    public class ListWalletTransactionsResponseBuilder
    {
      private Transaction[] Transactions = [];
      private Pagination? Pagination;

      public ListWalletTransactionsResponseBuilder() { }

      public ListWalletTransactionsResponseBuilder WithTransactions(Transaction[] transactions)
      {
        this.Transactions = transactions;
        return this;
      }

      public ListWalletTransactionsResponseBuilder WithPagination(Pagination? pagination)
      {
        this.Pagination = pagination;
        return this;
      }

      public ListWalletTransactionsResponse Build()
      {
        return new ListWalletTransactionsResponse
        {
          Transactions = this.Transactions,
          Pagination = this.Pagination
        };
      }
    }
  }
}
