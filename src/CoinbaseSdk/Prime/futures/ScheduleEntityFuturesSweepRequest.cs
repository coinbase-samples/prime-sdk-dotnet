namespace CoinbaseSdk.Prime.Futures
{
  using System.Text.Json.Serialization;

  /// <summary>
  /// Request to schedule an entity futures sweep.
  /// </summary>
  public class ScheduleEntityFuturesSweepRequest(string entityId)
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    /// <summary>
    /// The amount to sweep (optional).
    /// </summary>
    [JsonPropertyName("amount")]
    public string? Amount { get; set; }

    /// <summary>
    /// The currency to sweep (required).
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    public class ScheduleEntityFuturesSweepRequestBuilder
    {
      public ScheduleEntityFuturesSweepRequestBuilder(string entityId)
      {
        EntityId = entityId;
      }

      public string EntityId { get; }
      public string? Amount { get; set; }
      public string? Currency { get; set; }

      public ScheduleEntityFuturesSweepRequest Build()
      {
        return new ScheduleEntityFuturesSweepRequest(EntityId)
        {
          Amount = Amount,
          Currency = Currency,
        };
      }
    }
  }
}

