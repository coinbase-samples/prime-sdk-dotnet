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

namespace Coinbase.Prime.Portfolios
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Http;
  using Coinbase.Core.Service;

  public class PortfoliosService(ICoinbaseClient client) :
   CoinbaseService(client)
  {
    public ListPortfoliosResponse ListPortfolios(CallOptions? options = null)
    {
      return this.Request<ListPortfoliosResponse>(
        HttpMethod.Get,
        "/portfolios",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<ListPortfoliosResponse> ListPortfoliosAsync(
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListPortfoliosResponse>(
        HttpMethod.Get,
        "/portfolios",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetPortfolioByIdResponse GetPortfolioById(
      string portfolioId,
      CallOptions? options = null)
    {
      return this.Request<GetPortfolioByIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetPortfolioByIdResponse> GetPortfolioByIdAsync(
      string portfolioId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetPortfolioByIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }

    public GetPortfolioCreditInformationResponse GetPortfolioCreditInformation(
      string portfolioId,
      CallOptions? options = null)
    {
      return this.Request<GetPortfolioCreditInformationResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/credit",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetPortfolioCreditInformationResponse> GetPortfolioCreditInformationAsync(
      string portfolioId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetPortfolioCreditInformationResponse>(
        HttpMethod.Post,
        $"/portfolios/{portfolioId}/credit",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }
  }
}