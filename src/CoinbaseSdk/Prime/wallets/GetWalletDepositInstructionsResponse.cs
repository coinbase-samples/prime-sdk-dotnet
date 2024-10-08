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
  public class GetWalletDepositInstructionsResponse
  {
    [JsonPropertyName("crypto_instructions")]
    public CryptoDepositInstructions? CryptoDepositInstructions { get; set; }
    [JsonPropertyName("fiat_instructions")]
    public FiatDepositInstructions? FiatDepositInstructions { get; set; }

    public GetWalletDepositInstructionsResponse() { }

    public class GetWalletDepositInstructionsResponseBuilder
    {
      private CryptoDepositInstructions? _cryptoDepositInstructions;
      private FiatDepositInstructions? _fiatDepositInstructions;

      public GetWalletDepositInstructionsResponseBuilder() { }

      public GetWalletDepositInstructionsResponseBuilder WithCryptoDepositInstructions(CryptoDepositInstructions cryptoDepositInstructions)
      {
        this._cryptoDepositInstructions = cryptoDepositInstructions;
        return this;
      }

      public GetWalletDepositInstructionsResponseBuilder WithFiatDepositInstructions(FiatDepositInstructions fiatDepositInstructions)
      {
        this._fiatDepositInstructions = fiatDepositInstructions;
        return this;
      }

      public GetWalletDepositInstructionsResponse Build()
      {
        return new GetWalletDepositInstructionsResponse
        {
          CryptoDepositInstructions = this._cryptoDepositInstructions,
          FiatDepositInstructions = this._fiatDepositInstructions
        };
      }
    }
  }
}
