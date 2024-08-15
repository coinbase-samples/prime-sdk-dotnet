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

namespace CoinbaseSdk.Prime.PaymentMethods
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  public class EntityPaymentMethod
  {
    public string? Id { get; set; }

    public string? Symbol { get; set; }

    [JsonPropertyName("payment_method_type")]
    public PaymentMethodType PaymentMethodType { get; set; }

    [JsonPropertyName("bank_name")]
    public string? BankName { get; set; }

    [JsonPropertyName("account_number")]
    public string? AccountNumber { get; set; }

    public EntityPaymentMethod() { }

    public class EntityPaymentMethodBuilder
    {
      private string? _id;
      private string? _symbol;
      private PaymentMethodType _paymentMethodType;
      private string? _bankName;
      private string? _accountNumber;

      public EntityPaymentMethodBuilder WithId(string? id)
      {
        this._id = id;
        return this;
      }

      public EntityPaymentMethodBuilder WithSymbol(string? symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public EntityPaymentMethodBuilder WithPaymentMethodType(PaymentMethodType paymentMethodType)
      {
        this._paymentMethodType = paymentMethodType;
        return this;
      }

      public EntityPaymentMethodBuilder WithBankName(string? bankName)
      {
        this._bankName = bankName;
        return this;
      }

      public EntityPaymentMethodBuilder WithAccountNumber(string? accountNumber)
      {
        this._accountNumber = accountNumber;
        return this;
      }

      public EntityPaymentMethod Build()
      {
        return new EntityPaymentMethod
        {
          Id = this._id,
          Symbol = this._symbol,
          PaymentMethodType = this._paymentMethodType,
          BankName = this._bankName,
          AccountNumber = this._accountNumber
        };
      }
    }
  }
}
