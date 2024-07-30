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

namespace Coinbase.Prime.Activities
{
  using System.Net;
  using Coinbase.Core.Client;
  using Coinbase.Core.Http;
  using Coinbase.Core.Service;
  public class ActivtiesService(ICoinbaseClient client) : CoinbaseService(client)
  {
    public ListActivitiesResponse ListActivities(
      string portfolioId,
      ListActivitiesRequest request,
      CallOptions? options = null)
    {
      return this.Request<ListActivitiesResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/activities",
        [HttpStatusCode.OK],
        request,
        options);
    }

    public Task<ListActivitiesResponse> ListActivitiesAsync(
      string portfolioId,
      ListActivitiesRequest request,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<ListActivitiesResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/activities",
        [HttpStatusCode.OK],
        request,
        options,
        cancellationToken);
    }

    public GetActivityByActivityIdResponse GetActivityByActivityId(
      string portfolioId,
      string activityId,
      CallOptions? options = null)
    {
      return this.Request<GetActivityByActivityIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/activities/{activityId}",
        [HttpStatusCode.OK],
        null,
        options);
    }

    public Task<GetActivityByActivityIdResponse> GetActivityByActivityIdAsync(
      string portfolioId,
      string activityId,
      CallOptions? options = null,
      CancellationToken cancellationToken = default)
    {
      return this.RequestAsync<GetActivityByActivityIdResponse>(
        HttpMethod.Get,
        $"/portfolios/{portfolioId}/activities/{activityId}",
        [HttpStatusCode.OK],
        null,
        options,
        cancellationToken);
    }
  }
}