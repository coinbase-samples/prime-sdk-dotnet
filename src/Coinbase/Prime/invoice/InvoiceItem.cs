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
  using Newtonsoft.Json;
  public class InvoiceItem
  {
    public string? Description { get; set; }

    [JsonProperty("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    [JsonProperty("invoice_type")]
    public InvoiceType InvoiceType { get; set; }

    public double? Rate { get; set; }
    public double? Quantity { get; set; }
    public double? Price { get; set; }

    [JsonProperty("average_auc")]
    public double? AverageAuc { get; set; }
    public double? Total { get; set; }

    public InvoiceItem() { }
  }
}
