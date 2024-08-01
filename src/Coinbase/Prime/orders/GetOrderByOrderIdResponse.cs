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
  public class GetOrderByOrderIdResponse
  {
    public Order? Order { get; set; }

    public class GetOrderByOrderIdResponseBuilder
    {
      private Order? _order;

      public GetOrderByOrderIdResponseBuilder WithOrder(Order order)
      {
        this._order = order;
        return this;
      }

      public GetOrderByOrderIdResponse Build()
      {
        return new GetOrderByOrderIdResponse
        {
          Order = this._order
        };
      }
    }
  }
}