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

namespace CoinbaseSdk.Prime.Model
{
  public class Web3TransactionMetadata
  {
    public string? Label { get; set; }
    public AssetChange[]? ConfirmedAssetChanges { get; set; }

    public Web3TransactionMetadata() { }

    public class Web3TransactionMetadataBuilder
    {
      private string? _label;
      private AssetChange[]? _confirmedAssetChanges;

      public Web3TransactionMetadataBuilder WithLabel(string label)
      {
        this._label = label;
        return this;
      }

      public Web3TransactionMetadataBuilder WithConfirmedAssetChanges(AssetChange[] confirmedAssetChanges)
      {
        this._confirmedAssetChanges = confirmedAssetChanges;
        return this;
      }

      public Web3TransactionMetadata Build()
      {
        return new Web3TransactionMetadata
        {
          Label = this._label,
          ConfirmedAssetChanges = this._confirmedAssetChanges,
        };
      }
    }
  }
}