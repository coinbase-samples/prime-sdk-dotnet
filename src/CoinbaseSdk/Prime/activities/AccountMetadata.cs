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
  public class AccountMetadata
  {
    public Consensus? Consensus { get; set; }

    public AccountMetadata()
    {
    }

    public AccountMetadata(Consensus consensus)
    {
      Consensus = consensus;
    }

    public class Builder
    {
      private Consensus? _consensus;
      public Builder WithConsensus(Consensus consensus)
      {
        this._consensus = consensus;
        return this;
      }

      public AccountMetadata Build()
      {
        return new AccountMetadata()
        {
          Consensus = this._consensus
        };
      }
    }
  }
}
