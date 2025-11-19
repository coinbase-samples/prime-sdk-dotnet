/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Tests.Serialization
{
  using System;
  using System.Collections.Generic;
  using System.Text.Json;
  using CoinbaseSdk.Prime.Activities;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model;
  using CoinbaseSdk.Prime.Model.Enums;
  using CoinbaseSdk.Prime.Orders;
  using CoinbaseSdk.Prime.Serialization;
  using CoinbaseSdk.Prime.Wallets;
  using Xunit;

  public class PrimeSerializationSmokeTests
  {
    public static IEnumerable<object[]> RoundTripSamples => new List<object[]>
    {
      new object[] { typeof(GetActivityResponse), Samples.CreateActivityResponse() },
      new object[] { typeof(ListWalletsResponse), Samples.CreateListWalletsResponse() },
      new object[] { typeof(CreateOrderRequest), Samples.CreateOrderRequestSample() }
    };

    [Theory]
    [MemberData(nameof(RoundTripSamples))]
    public void Serializer_ShouldRoundTripRepresentativeDtos(Type dtoType, object instance)
    {
      JsonSerializerOptions options = PrimeJsonSerializerOptionsFactory.Default;

      string json = JsonSerializer.Serialize(instance, dtoType, options);
      object? result = JsonSerializer.Deserialize(json, dtoType, options);

      Assert.NotNull(result);

      string roundTrip = JsonSerializer.Serialize(result, dtoType, options);
      AssertJsonEquivalent(json, roundTrip);
    }

    [Fact]
    public void Deserialize_ShouldFallbackToNullForUnknownEnums()
    {
      const string payload = """{ "side": "UNSUPPORTED_SIDE" }""";
      JsonSerializerOptions options = PrimeJsonSerializerOptionsFactory.Default;

      EnumProbe? probe = JsonSerializer.Deserialize<EnumProbe>(payload, options);

      Assert.NotNull(probe);
      Assert.Null(probe.Side);
    }

    private static void AssertJsonEquivalent(string expected, string actual)
    {
      using JsonDocument expectedDoc = JsonDocument.Parse(expected);
      using JsonDocument actualDoc = JsonDocument.Parse(actual);

      string expectedRaw = expectedDoc.RootElement.GetRawText();
      string actualRaw = actualDoc.RootElement.GetRawText();

      Assert.True(string.Equals(expectedRaw, actualRaw, StringComparison.Ordinal), $"JSON mismatch.\nExpected: {expected}\nActual:   {actual}");
    }

    private sealed class EnumProbe
    {
      public OrderSide? Side { get; set; }
    }

    private static class Samples
    {
      internal static GetActivityResponse CreateActivityResponse()
      {
        return new GetActivityResponse
        {
          Activity = new Activity
          {
            Id = "acct-activity-1",
            ReferenceId = "order-123",
            Category = ActivityCategory.ACTIVITY_CATEGORY_ORDER,
            Type = PrimeActivityType.ACTIVITY_TYPE_MARKET_ORDER,
            Status = ActivityStatus.ACTIVITY_STATUS_PROCESSING,
            Title = "BUY BTC/USD - Market",
            Description = "Sample order pending approval",
            CreatedAt = "2025-01-01T00:00:00Z",
            UpdatedAt = "2025-01-01T00:05:00Z",
            UserActions =
            [
              new UserAction
              {
                Action = CoinbaseSdk.Prime.Model.Enums.Action.ACTION_INITIATE,
                UserId = "user-1",
                Timestamp = "2025-01-01T00:00:00Z"
              }
            ],
            Symbols = ["BTC", "USD"]
          }
        };
      }

      internal static ListWalletsResponse CreateListWalletsResponse()
      {
        return new ListWalletsResponse
        {
          Wallets =
          [
            new Wallet
            {
              Id = "wallet-1",
              Name = "BTC Hot Wallet",
              Symbol = "BTC",
              Type = WalletType.TRADING,
              Visibility = WalletVisibility.WALLET_VISIBILITY_VISIBLE,
              CreatedAt = DateTime.UtcNow,
              Network = new Network
              {
                Id = "bitcoin",
                Type = "mainnet"
              }
            }
          ],
          Pagination = new Pagination
          {
            NextCursor = "cursor-2",
            SortDirection = SortDirection.ASC,
            HasNext = true
          }
        };
      }

      internal static CreateOrderRequest CreateOrderRequestSample()
      {
        return new CreateOrderRequest("portfolio-1")
        {
          ProductId = "BTC-USD",
          Side = OrderSide.BUY,
          Type = OrderType.LIMIT,
          ClientOrderId = "client-order-1",
          BaseQuantity = "1.5",
          LimitPrice = "65000.00",
          TimeInForce = TimeInForceType.GOOD_UNTIL_CANCELLED,
          PostOnly = true,
          HistoricalPov = "0.3"
        };
      }
    }
  }
}

