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

namespace Coinbase.Prime.AddressBook
{
  using System.Text.Json.Serialization;
  public class CreateAddressBookEntryResponse
  {
    [JsonPropertyName("activity_type")]
    public AddressBookActivityType ActivityType { get; set; }
    [JsonPropertyName("num_approvals_remaining")]
    public int? NumApprovalsRemaining { get; set; }
    [JsonPropertyName("activity_id")]
    public string? ActivityId { get; set; }

    public class CreateAddressBookEntryResponseBuilder
    {
      private AddressBookActivityType _activityType;
      private int? _numApprovalsRemaining;
      private string? _activityId;

      public CreateAddressBookEntryResponseBuilder WithActivityType(AddressBookActivityType activityType)
      {
        this._activityType = activityType;
        return this;
      }

      public CreateAddressBookEntryResponseBuilder WithNumApprovalsRemaining(int? numApprovalsRemaining)
      {
        this._numApprovalsRemaining = numApprovalsRemaining;
        return this;
      }

      public CreateAddressBookEntryResponseBuilder WithActivityId(string? activityId)
      {
        this._activityId = activityId;
        return this;
      }

      public CreateAddressBookEntryResponse Build()
      {
        return new CreateAddressBookEntryResponse
        {
          ActivityType = this._activityType,
          NumApprovalsRemaining = this._numApprovalsRemaining,
          ActivityId = this._activityId
        };
      }
    }
  }
}