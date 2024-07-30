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

namespace Coinbase.Prime.Invoice
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
  }
}
