/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Financing
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// Create New Locates.
  /// </summary>
  public class CreateNewLocatesRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? Symbol { get; set; }

    public string? Amount { get; set; }

    public string? ConversionDate { get; set; }

    public string? LocateDate { get; set; }

    public class CreateNewLocatesRequestBuilder
    {
      private string? _portfolioId;
      private string? _symbol;
      private string? _amount;
      private string? _conversionDate;
      private string? _locateDate;

      public CreateNewLocatesRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public CreateNewLocatesRequestBuilder WithSymbol(string? symbol)
      {
        _symbol = symbol;
        return this;
      }

      public CreateNewLocatesRequestBuilder WithAmount(string? amount)
      {
        _amount = amount;
        return this;
      }

      public CreateNewLocatesRequestBuilder WithConversionDate(string? conversionDate)
      {
        _conversionDate = conversionDate;
        return this;
      }

      public CreateNewLocatesRequestBuilder WithLocateDate(string? locateDate)
      {
        _locateDate = locateDate;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public CreateNewLocatesRequest Build()
      {
        Validate();
        return new CreateNewLocatesRequest(_portfolioId!)
        {
          Symbol = _symbol,
          Amount = _amount,
          ConversionDate = _conversionDate,
          LocateDate = _locateDate,
        };
      }
    }
  }
}
