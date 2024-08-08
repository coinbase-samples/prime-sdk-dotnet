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
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;

  public class GetTransactionByTransactionIdRequest(string portfolioId, string transactionId)
  : BasePrimeRequest(portfolioId, null)
  {
    [JsonIgnore]
    public string TransactionId { get; set; } = transactionId;

    public class GetTransactionByTransactionIdRequestBuilder
    {
      private string? _portfolioId;
      private string? _transactionId;

      public GetTransactionByTransactionIdRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public GetTransactionByTransactionIdRequestBuilder WithTransactionId(string transactionId)
      {
        this._transactionId = transactionId;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> or <see cref="_transactionId"/> are null, empty
      /// or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(this._transactionId))
        {
          throw new CoinbaseClientException("TransactionId is required");
        }
      }

      /// <summary>
      /// Build the <see cref="GetTransactionByTransactionIdRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="GetTransactionByTransactionIdRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public GetTransactionByTransactionIdRequest Build()
      {
        this.Validate();
        return new GetTransactionByTransactionIdRequest(_portfolioId!, _transactionId!);
      }
    }
  }
}
