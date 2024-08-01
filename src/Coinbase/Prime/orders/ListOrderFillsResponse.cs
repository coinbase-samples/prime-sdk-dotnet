/*
 * Copyright 2024-present Coinbase Global, Inc.
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

namespace Coinbase.Prime.Orders
{
  using Coinbase.Prime.Common;
  public class ListOrderFillsResponse
  {
    public List<OrderFill> OrderFills { get; set; } = [];

    public Pagination? Pagination { get; set; }

    public class ListOrderFillsResponseBuilder
    {
      private List<OrderFill> _orderFills = [];
      private Pagination? _pagination;

      public ListOrderFillsResponseBuilder WithOrderFills(List<OrderFill> orderFills)
      {
        this._orderFills = orderFills;
        return this;
      }

      public ListOrderFillsResponseBuilder WithPagination(Pagination pagination)
      {
        this._pagination = pagination;
        return this;
      }

      public ListOrderFillsResponse Build()
      {
        return new ListOrderFillsResponse
        {
          OrderFills = this._orderFills,
          Pagination = this._pagination
        };
      }
    }
  }
}