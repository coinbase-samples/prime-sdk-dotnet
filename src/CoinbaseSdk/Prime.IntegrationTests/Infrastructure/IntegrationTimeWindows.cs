/*
 * Copyright 2026-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.IntegrationTests.Infrastructure
{
  public static class IntegrationTimeWindows
  {
    public static (string Start, string End) Last7Days()
    {
      var end = DateTimeOffset.UtcNow;
      var start = end.AddDays(-7);
      return (start.ToString("O"), end.ToString("O"));
    }

    public static (string Start, string End) Last30Days()
    {
      var end = DateTimeOffset.UtcNow;
      var start = end.AddDays(-30);
      return (start.ToString("O"), end.ToString("O"));
    }

    /// <summary>
    /// Returns a window small enough for ONE_MINUTE candles (max 350 candles).
    /// </summary>
    public static (string Start, string End) LastFiveHours()
    {
      var end = DateTimeOffset.UtcNow;
      var start = end.AddHours(-5);
      return (start.ToString("O"), end.ToString("O"));
    }
  }
}
