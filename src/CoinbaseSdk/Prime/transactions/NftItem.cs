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

namespace CoinbaseSdk.Prime.Transactions
{
  public class NftItem
  {
    public string? Name { get; set; }

    public NftItem() { }

    public class NftItemBuilder
    {
      private string? Name;

      public NftItemBuilder() { }

      public NftItemBuilder WithName(string name)
      {
        this.Name = name;
        return this;
      }

      public NftItem Build()
      {
        return new NftItem
        {
          Name = this.Name
        };
      }
    }
  }
}
