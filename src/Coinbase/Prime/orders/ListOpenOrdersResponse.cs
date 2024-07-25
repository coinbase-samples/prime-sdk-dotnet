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

  public class ListOpenOrdersResponse
  {
    public Order[] Orders { get; set; }
    public Pagination Pagination { get; set; }
    public ListOpenOrdersRequest Request { get; set; }

    public ListOpenOrdersResponse() { }

    public ListOpenOrdersResponse(Builder builder)
    {
      Orders = builder.Orders;
      Pagination = builder.Pagination;
      Request = builder.Request;
    }

    public class Builder
    {
      public Order[] Orders { get; private set; }
      public Pagination Pagination { get; private set; }
      public ListOpenOrdersRequest Request { get; private set; }

      public Builder() { }

      public Builder WithOrders(Order[] orders)
      {
        Orders = orders;
        return this;
      }

      public Builder WithPagination(Pagination pagination)
      {
        Pagination = pagination;
        return this;
      }

      public Builder WithRequest(ListOpenOrdersRequest request)
      {
        Request = request;
        return this;
      }

      public ListOpenOrdersResponse Build()
      {
        return new ListOpenOrdersResponse(this);
      }
    }
  }
}