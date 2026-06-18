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

using CoinbaseSdk.Tools.Generator.Phases;
using CoinbaseSdk.Tools.Generator.Spec;
using Xunit;

namespace CoinbaseSdk.Tools.Generator.Tests;

public class ServicePhaseStatusArrayTests
{
  [Theory]
  [InlineData("CreateOrder")]
  [InlineData("ClaimStakingRewards")]
  [InlineData("SubmitDepositTravelRuleData")]
  [InlineData("PreviewUnstake")]
  public void StatusArray_CreateStyleWithSpec200Only_AcceptsCreatedAndOk(string sdkMethod)
  {
    var op = new ParsedOperation { SuccessStatusCodes = [200] };

    Assert.Equal(
      "[HttpStatusCode.Created, HttpStatusCode.OK]",
      ServicePhase.StatusArray(sdkMethod, op));
  }

  [Theory]
  [InlineData("GetOrderPreview")]
  [InlineData("CancelOrder")]
  [InlineData("SetAutoSweep")]
  [InlineData("ListPortfolioOrders")]
  public void StatusArray_NonCreateStyleWithSpec200Only_AcceptsOkOnly(string sdkMethod)
  {
    var op = new ParsedOperation { SuccessStatusCodes = [200] };

    Assert.Equal("[HttpStatusCode.OK]", ServicePhase.StatusArray(sdkMethod, op));
  }

  [Fact]
  public void StatusArray_WhenSpecDocuments201And200_EmitsCreatedBeforeOk()
  {
    var op = new ParsedOperation { SuccessStatusCodes = [200, 201] };

    Assert.Equal(
      "[HttpStatusCode.Created, HttpStatusCode.OK]",
      ServicePhase.StatusArray("CreateOrder", op));
  }

  [Fact]
  public void StatusArray_WhenNoSuccessCodes_DefaultsToOk()
  {
    var op = new ParsedOperation();

    Assert.Equal("[HttpStatusCode.OK]", ServicePhase.StatusArray("CreateOrder", op));
  }
}
