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

namespace CoinbaseSdk.Prime.Products
{
  using CoinbaseSdk.Prime.Common;
  public class ListPortfolioProductsResponse
  {
    public Product[] Products { get; set; } = [];
    public Pagination? Pagination { get; set; }

    public ListPortfolioProductsResponse() { }

    public class ListPortfolioProductsResponseBuilder
    {
      private Product[] _products = [];
      private Pagination? _pagination;

      public ListPortfolioProductsResponseBuilder WithProducts(Product[] products)
      {
        this._products = products;
        return this;
      }

      public ListPortfolioProductsResponseBuilder WithPagination(Pagination? pagination)
      {
        this._pagination = pagination;
        return this;
      }

      public ListPortfolioProductsResponse Build()
      {
        return new ListPortfolioProductsResponse
        {
          Products = this._products,
          Pagination = this._pagination
        };
      }
    }
  }
}
