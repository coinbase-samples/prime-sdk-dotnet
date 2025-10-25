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
  public class CreateNewLocatesRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? Symbol { get; set; }
    public string? Amount { get; set; }
    [JsonPropertyName("locate_date")]
    public string? LocateDate { get; set; }

    public class Builder
    {
      private string? _symbol;
      private string? _amount;
      private string? _locateDate;

      public Builder WithSymbol(string? symbol)
      {
        _symbol = symbol;
        return this;
      }

      public Builder WithAmount(string? amount)
      {
        _amount = amount;
        return this;
      }

      public Builder WithLocateDate(string? locateDate)
      {
        _locateDate = locateDate;
        return this;
      }

      public CreateNewLocatesRequest Build(string portfolioId)
      {
        return new CreateNewLocatesRequest(portfolioId)
        {
          Symbol = _symbol,
          Amount = _amount,
          LocateDate = _locateDate
        };
      }
    }
  }
}
