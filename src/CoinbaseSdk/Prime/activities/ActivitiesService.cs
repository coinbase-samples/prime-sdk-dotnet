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

namespace CoinbaseSdk.Prime.Activities
{
  using System.Net;
  using CoinbaseSdk.Core.Client;
  using CoinbaseSdk.Core.Http;
  using CoinbaseSdk.Core.Service;

  public class ActivitiesService(ICoinbaseClient client) : CoinbaseService(client), IActivitiesService
  {
    public ListActivitiesResponse ListActivities(
      ListActivitiesRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListActivitiesResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/activities",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListActivitiesResponse> ListActivitiesAsync(
      ListActivitiesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListActivitiesResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/activities",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetActivityByActivityIdResponse GetActivityByActivityId(
      GetActivityByActivityIdRequest request,
      CallOptions? options = null)
    {
      return this.Request<GetActivityByActivityIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/activities/{request.ActivityId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetActivityByActivityIdResponse> GetActivityByActivityIdAsync(
      GetActivityByActivityIdRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetActivityByActivityIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{request.PortfolioId}/activities/{request.ActivityId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }
  }
}
