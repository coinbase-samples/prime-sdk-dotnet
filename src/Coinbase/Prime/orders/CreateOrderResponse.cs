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
  using System.Text.Json.Serialization;
  public class CreateOrderResponse
  {
    [JsonPropertyName("order_id")]
    public string? OrderId { get; set; }

    public CreateOrderResponse() { }

    public CreateOrderResponse(string orderId)
    {
      OrderId = orderId;
    }

    public class CreateOrderResponseBuilder
    {
      private string? _orderId;

      public CreateOrderResponseBuilder WithOrderId(string? orderId)
      {
        this._orderId = orderId;
        return this;
      }

      public CreateOrderResponse Build()
      {
        return new CreateOrderResponse
        {
          OrderId = this._orderId
        };
      }
    }
  }
}