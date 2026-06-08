/*
 * Copyright 2026-present Coinbase Global, Inc.
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

using CoinbaseSdk.Tools.Generator.Processing;
using Xunit;

namespace CoinbaseSdk.Tools.Generator.Tests;

public class SpecAnalyzerTests
{
  [Theory]
  [InlineData("Payment Methods", "paymentmethods")]
  [InlineData("Onchain Address Book", "onchainaddressbook")]
  [InlineData("Travel Rule", "travelrule")]
  [InlineData("Orders", "orders")]
  public void DefaultFolderFromTag_NormalizesToServiceKey(string tag, string expectedFolder)
  {
    Assert.Equal(expectedFolder, SpecAnalyzer.DefaultFolderFromTag(tag));
  }
}
