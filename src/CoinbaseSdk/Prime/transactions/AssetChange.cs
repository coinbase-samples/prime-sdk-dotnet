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
  public class AssetChange
  {
    public AssetChangeType Type { get; set; }
    public string? Symbol { get; set; }
    public string? Amount { get; set; }
    public NftCollection? Collection { get; set; }
    public NftItem? Item { get; set; }

    public AssetChange() { }

    public class AssetChangeBuilder
    {
      private AssetChangeType _type;
      private string? _symbol;
      private string? _amount;
      private NftCollection? _collection;
      private NftItem? _item;

      public AssetChangeBuilder WithType(AssetChangeType type)
      {
        this._type = type;
        return this;
      }

      public AssetChangeBuilder WithSymbol(string symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public AssetChangeBuilder WithAmount(string amount)
      {
        this._amount = amount;
        return this;
      }

      public AssetChangeBuilder WithCollection(NftCollection collection)
      {
        this._collection = collection;
        return this;
      }

      public AssetChangeBuilder WithItem(NftItem item)
      {
        this._item = item;
        return this;
      }

      public AssetChange Build()
      {
        return new AssetChange
        {
          Type = this._type,
          Symbol = this._symbol,
          Amount = this._amount,
          Collection = this._collection,
          Item = this._item
        };
      }
    }
  }
}
