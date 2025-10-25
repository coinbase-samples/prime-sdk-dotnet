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

namespace CoinbaseSdk.Prime.Wallets
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  public class GetWalletDepositInstructionsResponse
  {
    [JsonPropertyName("crypto_instructions")]
    public WalletCryptoDepositInstructions? CryptoDepositInstructions { get; set; }
    [JsonPropertyName("fiat_instructions")]
    public WalletFiatDepositInstructions? FiatDepositInstructions { get; set; }

    public GetWalletDepositInstructionsResponse() { }
  }
}
