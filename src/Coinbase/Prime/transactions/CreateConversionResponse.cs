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

namespace Coinbase.Prime.Transactions
{
  using System.Text.Json.Serialization;
  public class CreateConversionResponse
  {
    [JsonPropertyName("activity_id")]
    public string? ActivityId { get; set; }

    [JsonPropertyName("source_symbol")]
    public string? SourceSymbol { get; set; }

    [JsonPropertyName("destination_symbol")]
    public string? DestinationSymbol { get; set; }

    public string? Amount { get; set; }
    public string? Destination { get; set; }
    public string? Source { get; set; }

    public CreateConversionResponse() { }

    public class CreateConversionResponseBuilder
    {
      private string? _activityId;
      private string? _sourceSymbol;
      private string? _destinationSymbol;
      private string? _amount;
      private string? _destination;
      private string? _source;

      public CreateConversionResponseBuilder WithActivityId(string? activityId)
      {
        this._activityId = activityId;
        return this;
      }

      public CreateConversionResponseBuilder WithSourceSymbol(string? sourceSymbol)
      {
        this._sourceSymbol = sourceSymbol;
        return this;
      }

      public CreateConversionResponseBuilder WithDestinationSymbol(string? destinationSymbol)
      {
        this._destinationSymbol = destinationSymbol;
        return this;
      }

      public CreateConversionResponseBuilder WithAmount(string? amount)
      {
        this._amount = amount;
        return this;
      }

      public CreateConversionResponseBuilder WithDestination(string? destination)
      {
        this._destination = destination;
        return this;
      }

      public CreateConversionResponseBuilder WithSource(string? source)
      {
        this._source = source;
        return this;
      }

      public CreateConversionResponse Build()
      {
        return new CreateConversionResponse
        {
          ActivityId = this._activityId,
          SourceSymbol = this._sourceSymbol,
          DestinationSymbol = this._destinationSymbol,
          Amount = this._amount,
          Destination = this._destination,
          Source = this._source
        };
      }
    }
  }
}
