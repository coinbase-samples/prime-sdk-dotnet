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

namespace CoinbaseSdk.Prime.Model
{
  /// <summary>
  /// Indicates the order status.
  /// </summary>
  public enum OrderStatus
  {
    /// <summary>
    /// The order is open but unfilled.
    /// </summary>
    OPEN,

    /// <summary>
    /// The order was filled.
    /// </summary>
    FILLED,

    /// <summary>
    /// The order was cancelled.
    /// </summary>
    CANCELLED,

    /// <summary>
    /// The order has expired.
    /// </summary>
    EXPIRED,

    /// <summary>
    /// Order submission failed.
    /// </summary>
    FAILED,

    /// <summary>
    /// The order has been sent but is not yet confirmed.
    /// </summary>
    PENDING,
  }
}
