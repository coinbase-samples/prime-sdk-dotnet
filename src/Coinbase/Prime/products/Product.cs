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

namespace Coinbase.Prime.Products
{
  using System.Text.Json.Serialization;
  public class Product
  {
    public string? Id { get; set; }

    [JsonPropertyName("base_increment")]
    public string? BaseIncrement { get; set; }

    [JsonPropertyName("quote_increment")]
    public string? QuoteIncrement { get; set; }

    [JsonPropertyName("price_increment")]
    public string? PriceIncrement { get; set; }

    [JsonPropertyName("base_min_size")]
    public string? BaseMinSize { get; set; }

    [JsonPropertyName("quote_min_size")]
    public string? QuoteMinSize { get; set; }

    [JsonPropertyName("base_max_size")]
    public string? BaseMaxSize { get; set; }

    [JsonPropertyName("quote_max_size")]
    public string? QuoteMaxSize { get; set; }

    public string[] Permissions { get; set; } = [];

    public Product() { }
  }
}
