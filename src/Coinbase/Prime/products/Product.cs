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

    public class ProductBuilder
    {
      private string? _id;
      private string? _baseIncrement;
      private string? _quoteIncrement;
      private string? _priceIncrement;
      private string? _baseMinSize;
      private string? _quoteMinSize;
      private string? _baseMaxSize;
      private string? _quoteMaxSize;
      private string[] _permissions = [];

      public ProductBuilder WithId(string? id)
      {
        this._id = id;
        return this;
      }

      public ProductBuilder WithBaseIncrement(string? baseIncrement)
      {
        this._baseIncrement = baseIncrement;
        return this;
      }

      public ProductBuilder WithQuoteIncrement(string? quoteIncrement)
      {
        this._quoteIncrement = quoteIncrement;
        return this;
      }

      public ProductBuilder WithPriceIncrement(string? priceIncrement)
      {
        this._priceIncrement = priceIncrement;
        return this;
      }

      public ProductBuilder WithBaseMinSize(string? baseMinSize)
      {
        this._baseMinSize = baseMinSize;
        return this;
      }

      public ProductBuilder WithQuoteMinSize(string? quoteMinSize)
      {
        this._quoteMinSize = quoteMinSize;
        return this;
      }

      public ProductBuilder WithBaseMaxSize(string? baseMaxSize)
      {
        this._baseMaxSize = baseMaxSize;
        return this;
      }

      public ProductBuilder WithQuoteMaxSize(string? quoteMaxSize)
      {
        this._quoteMaxSize = quoteMaxSize;
        return this;
      }

      public ProductBuilder WithPermissions(string[] permissions)
      {
        this._permissions = permissions;
        return this;
      }

      public Product Build()
      {
        return new Product
        {
          Id = this._id,
          BaseIncrement = this._baseIncrement,
          QuoteIncrement = this._quoteIncrement,
          PriceIncrement = this._priceIncrement,
          BaseMinSize = this._baseMinSize,
          QuoteMinSize = this._quoteMinSize,
          BaseMaxSize = this._baseMaxSize,
          QuoteMaxSize = this._quoteMaxSize,
          Permissions = this._permissions
        };
      }
    }
  }
}
