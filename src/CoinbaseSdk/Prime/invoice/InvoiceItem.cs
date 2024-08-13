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

namespace CoinbaseSdk.Prime.Invoice
{
  using System.Text.Json.Serialization;
  public class InvoiceItem
  {
    public string? Description { get; set; }

    [JsonPropertyName("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    [JsonPropertyName("invoice_type")]
    public InvoiceType InvoiceType { get; set; }

    public double? Rate { get; set; }
    public double? Quantity { get; set; }
    public double? Price { get; set; }

    [JsonPropertyName("average_auc")]
    public double? AverageAuc { get; set; }
    public double? Total { get; set; }

    public InvoiceItem() { }

    public class InvoiceItemBuilder
    {
      private string? _description;
      private string? _currencySymbol;
      private InvoiceType _invoiceType;
      private double? _rate;
      private double? _quantity;
      private double? _price;
      private double? _averageAuc;
      private double? _total;

      public InvoiceItemBuilder WithDescription(string? description)
      {
        this._description = description;
        return this;
      }

      public InvoiceItemBuilder WithCurrencySymbol(string? currencySymbol)
      {
        this._currencySymbol = currencySymbol;
        return this;
      }

      public InvoiceItemBuilder WithInvoiceType(InvoiceType invoiceType)
      {
        this._invoiceType = invoiceType;
        return this;
      }

      public InvoiceItemBuilder WithRate(double? rate)
      {
        this._rate = rate;
        return this;
      }

      public InvoiceItemBuilder WithQuantity(double? quantity)
      {
        this._quantity = quantity;
        return this;
      }

      public InvoiceItemBuilder WithPrice(double? price)
      {
        this._price = price;
        return this;
      }

      public InvoiceItemBuilder WithAverageAuc(double? averageAuc)
      {
        this._averageAuc = averageAuc;
        return this;
      }

      public InvoiceItemBuilder WithTotal(double? total)
      {
        this._total = total;
        return this;
      }

      public InvoiceItem Build()
      {
        return new InvoiceItem
        {
          Description = this._description,
          CurrencySymbol = this._currencySymbol,
          InvoiceType = this._invoiceType,
          Rate = this._rate,
          Quantity = this._quantity,
          Price = this._price,
          AverageAuc = this._averageAuc,
          Total = this._total
        };
      }
    }
  }
}
