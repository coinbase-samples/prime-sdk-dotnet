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
  using System.Text.Json.Serialization;
  public class Consensus
  {
    [JsonPropertyName("approval_deadline")]
    public string? ApprovalDeadline { get; set; }

    [JsonPropertyName("has_passed_consensus")]
    public bool? HasPassedConsensus { get; set; }

    public Consensus()
    {
    }

    public Consensus(string approvalDeadline, bool hasPassedConsensus)
    {
      ApprovalDeadline = approvalDeadline;
      HasPassedConsensus = hasPassedConsensus;
    }

    public class ConsensusBuilder
    {
      private string? _approvalDeadline;
      private bool? _hasPassedConsensus;

      public ConsensusBuilder WithApprovalDeadline(string approvalDeadline)
      {
        _approvalDeadline = approvalDeadline;
        return this;
      }

      public ConsensusBuilder WithHasPassedConsensus(bool hasPassedConsensus)
      {
        _hasPassedConsensus = hasPassedConsensus;
        return this;
      }

      public Consensus Build()
      {
        return new Consensus()
        {
          ApprovalDeadline = _approvalDeadline,
          HasPassedConsensus = _hasPassedConsensus
        };
      }
    }
  }
}
