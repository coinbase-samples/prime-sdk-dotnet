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

using Newtonsoft.Json;

namespace Coinbase.Prime.Transactions
{
  public class CreateWithdrawalRequest
  {
    public string? Amount { get; set; }

    [JsonProperty("destination_type")]
    public DestinationType? DestinationType { get; set; }

    [JsonProperty("idempotency_key")]
    public string? IdempotencyKey { get; set; }

    [JsonProperty("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    [JsonProperty("payment_method")]
    public PaymentMethod? PaymentMethod { get; set; }

    [JsonProperty("blockchain_address")]
    public BlockchainAddress? BlockchainAddress { get; set; }

    public CreateWithdrawalRequest() { }
  }
}
