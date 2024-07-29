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

namespace Coinbase.Prime.Activities
{
  using Newtonsoft.Json;
  public class Activity
  {
    public string? Id { get; set; }

    [JsonProperty("reference_id")]
    public string? ReferenceId { get; set; }

    public ActivityCategory Category { get; set; }
    public ActivityType Type { get; set; }

    [JsonProperty("secondary_type")]
    public ActivitySecondaryType SecondaryType { get; set; }

    public ActivityStatus Status { get; set; }

    [JsonProperty("created_by")]
    public string? CreatedBy { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    [JsonProperty("user_actions")]
    public UserAction[] UserActions { get; set; } = [];

    [JsonProperty("transactions_metadata")]
    public TransactionsMetadata? TransactionsMetadata { get; set; }

    [JsonProperty("account_metadata")]
    public AccountMetadata? AccountMetadata { get; set; }

    [JsonProperty("orders_metadata")]
    public OrdersMetadata? OrdersMetadata { get; set; }

    public string[] Symbols { get; set; } = [];

    [JsonProperty("created_at")]
    public string? CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public string? UpdatedAt { get; set; }

    public Activity()
    {
    }

    public Activity(
        string id,
        string referenceId,
        ActivityCategory category,
        ActivityType type,
        ActivitySecondaryType secondaryType,
        ActivityStatus status,
        string createdBy,
        string title,
        string description,
        UserAction[] userActions,
        TransactionsMetadata transactionsMetadata,
        AccountMetadata accountMetadata,
        OrdersMetadata ordersMetadata,
        string[] symbols,
        string createdAt,
        string updatedAt)
    {
      Id = id;
      ReferenceId = referenceId;
      Category = category;
      Type = type;
      SecondaryType = secondaryType;
      Status = status;
      CreatedBy = createdBy;
      Title = title;
      Description = description;
      UserActions = userActions;
      TransactionsMetadata = transactionsMetadata;
      AccountMetadata = accountMetadata;
      OrdersMetadata = ordersMetadata;
      Symbols = symbols;
      CreatedAt = createdAt;
      UpdatedAt = updatedAt;
    }
  }
}
