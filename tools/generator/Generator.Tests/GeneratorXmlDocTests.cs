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

public class GeneratorXmlDocTests
{
  [Fact]
  public void FormatPropertySummary_EscapesXmlAndAddsPeriod()
  {
    var doc = GeneratorXmlDoc.FormatPropertySummary("Amount < 100 & fee > 0");

    Assert.Contains("/// Amount &lt; 100 &amp; fee &gt; 0.", doc);
    Assert.Contains("/// <summary>", doc);
    Assert.Contains("/// </summary>", doc);
  }

  [Fact]
  public void FormatPropertySummary_SplitsMultiLineText()
  {
    var doc = GeneratorXmlDoc.FormatPropertySummary("Line one\nLine two");

    Assert.Contains("/// Line one.", doc);
    Assert.Contains("/// Line two.", doc);
  }

  [Fact]
  public void DecodeHtmlEntities_DecodesCommonEntities()
  {
    Assert.Equal("driver's license", GeneratorXmlDoc.DecodeHtmlEntities("driver&#39;s license"));
    Assert.Equal("a & b", GeneratorXmlDoc.DecodeHtmlEntities("a &amp; b"));
  }

  [Fact]
  public void FormatTypeSummary_ReturnsEmptyForBlankInput()
  {
    Assert.Equal(string.Empty, GeneratorXmlDoc.FormatTypeSummary(null));
    Assert.Equal(string.Empty, GeneratorXmlDoc.FormatTypeSummary("   "));
  }
}
