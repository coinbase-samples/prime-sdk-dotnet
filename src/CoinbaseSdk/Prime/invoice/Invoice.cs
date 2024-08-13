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
  public class Invoice
  {
    public string? Id { get; set; }

    [JsonPropertyName("billing_month")]
    public int? BillingMonth { get; set; }

    [JsonPropertyName("billing_year")]
    public int? BillingYear { get; set; }

    [JsonPropertyName("due_date")]
    public string? DueDate { get; set; }

    [JsonPropertyName("invoice_number")]
    public string? InvoiceNumber { get; set; }

    public InvoiceState State { get; set; }

    [JsonPropertyName("usd_amount_paid")]
    public double? UsdAmountPaid { get; set; }

    [JsonPropertyName("usd_amount_owed")]
    public double? UsdAmountOwed { get; set; }

    [JsonPropertyName("invoice_items")]
    public InvoiceItem[] InvoiceItems { get; set; } = [];

    public Invoice() { }

    public class InvoiceBuilder
    {
      private string? _id;
      private int? _billingMonth;
      private int? _billingYear;
      private string? _dueDate;
      private string? _invoiceNumber;
      private InvoiceState _state;
      private double? _usdAmountPaid;
      private double? _usdAmountOwed;
      private InvoiceItem[] _invoiceItems = [];

      public InvoiceBuilder WithId(string? id)
      {
        this._id = id;
        return this;
      }

      public InvoiceBuilder WithBillingMonth(int? billingMonth)
      {
        this._billingMonth = billingMonth;
        return this;
      }

      public InvoiceBuilder WithBillingYear(int? billingYear)
      {
        this._billingYear = billingYear;
        return this;
      }

      public InvoiceBuilder WithDueDate(string? dueDate)
      {
        this._dueDate = dueDate;
        return this;
      }

      public InvoiceBuilder WithInvoiceNumber(string? invoiceNumber)
      {
        this._invoiceNumber = invoiceNumber;
        return this;
      }

      public InvoiceBuilder WithState(InvoiceState state)
      {
        this._state = state;
        return this;
      }

      public InvoiceBuilder WithUsdAmountPaid(double? usdAmountPaid)
      {
        this._usdAmountPaid = usdAmountPaid;
        return this;
      }

      public InvoiceBuilder WithUsdAmountOwed(double? usdAmountOwed)
      {
        this._usdAmountOwed = usdAmountOwed;
        return this;
      }

      public InvoiceBuilder WithInvoiceItems(InvoiceItem[] invoiceItems)
      {
        this._invoiceItems = invoiceItems;
        return this;
      }

      public Invoice Build()
      {
        return new Invoice
        {
          Id = this._id,
          BillingMonth = this._billingMonth,
          BillingYear = this._billingYear,
          DueDate = this._dueDate,
          InvoiceNumber = this._invoiceNumber,
          State = this._state,
          UsdAmountPaid = this._usdAmountPaid,
          UsdAmountOwed = this._usdAmountOwed,
          InvoiceItems = this._invoiceItems
        };
      }
    }
  }
}
