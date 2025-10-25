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

namespace CoinbaseSdk.Prime.Financing
{
  using System.Text.Json.Serialization;

  public class GetEntityLocateAvailabilitiesRequest(string entityId)
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    [JsonPropertyName("conversion_date")]
    public string? ConversionDate { get; set; }

    [JsonPropertyName("locate_date")]
    public string? LocateDate { get; set; }

    public class Builder
    {
      private string? _conversionDate;
      private string? _locateDate;

      public Builder WithConversionDate(string? conversionDate)
      {
        _conversionDate = conversionDate;
        return this;
      }

      public Builder WithLocateDate(string? locateDate)
      {
        _locateDate = locateDate;
        return this;
      }

      public GetEntityLocateAvailabilitiesRequest Build(string entityId)
      {
        return new GetEntityLocateAvailabilitiesRequest(entityId)
        {
          ConversionDate = _conversionDate,
          LocateDate = _locateDate
        };
      }
    }
  }
}
