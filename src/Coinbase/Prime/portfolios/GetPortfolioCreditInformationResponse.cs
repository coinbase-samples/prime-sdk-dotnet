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

using Newtonsoft.Json;
using System;

namespace Coinbase.Prime.Portfolios
{
  public class GetPortfolioCreditInformationResponse
  {
    public PostTradeCredit PostTradeCredit { get; set; }

    public GetPortfolioCreditInformationResponse() { }

    public GetPortfolioCreditInformationResponse(Builder builder)
    {
      PostTradeCredit = builder.PostTradeCredit;
    }

    public class Builder
    {
      public PostTradeCredit PostTradeCredit { get; private set; }

      public Builder() { }

      public Builder SetPostTradeCredit(PostTradeCredit postTradeCredit)
      {
        PostTradeCredit = postTradeCredit;
        return this;
      }

      public GetPortfolioCreditInformationResponse Build()
      {
        return new GetPortfolioCreditInformationResponse(this);
      }
    }
  }
}