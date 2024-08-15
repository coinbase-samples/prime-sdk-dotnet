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
  using CoinbaseSdk.Prime.Model;

  public class Activity
  {
    public string? Id { get; set; }

    [JsonPropertyName("reference_id")]
    public string? ReferenceId { get; set; }

    public ActivityCategory Category { get; set; }
    public ActivityType Type { get; set; }

    [JsonPropertyName("secondary_type")]
    public ActivitySecondaryType SecondaryType { get; set; }

    public ActivityStatus Status { get; set; }

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    [JsonPropertyName("user_actions")]
    public UserAction[] UserActions { get; set; } = [];

    [JsonPropertyName("transactions_metadata")]
    public TransactionsMetadata? TransactionsMetadata { get; set; }

    [JsonPropertyName("account_metadata")]
    public AccountMetadata? AccountMetadata { get; set; }

    [JsonPropertyName("orders_metadata")]
    public OrdersMetadata? OrdersMetadata { get; set; }

    public string[] Symbols { get; set; } = [];

    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; set; }

    public Activity()
    {
    }
    public class ActivityBuilder
    {
      private string? _id;
      private string? _referenceId;
      private ActivityCategory _category;
      private ActivityType _type;
      private ActivitySecondaryType _secondaryType;
      private ActivityStatus _status;
      private string? _createdBy;
      private string? _title;
      private string? _description;
      private UserAction[] _userActions = [];
      private TransactionsMetadata? _transactionsMetadata;
      private AccountMetadata? _accountMetadata;
      private OrdersMetadata? _ordersMetadata;
      private string[] _symbols = [];
      private string? _createdAt;
      private string? _updatedAt;

      public ActivityBuilder WithId(string? id)
      {
        _id = id;
        return this;
      }

      public ActivityBuilder WithReferenceId(string? referenceId)
      {
        _referenceId = referenceId;
        return this;
      }

      public ActivityBuilder WithCategory(ActivityCategory category)
      {
        _category = category;
        return this;
      }

      public ActivityBuilder WithType(ActivityType type)
      {
        _type = type;
        return this;
      }

      public ActivityBuilder WithSecondaryType(ActivitySecondaryType secondaryType)
      {
        _secondaryType = secondaryType;
        return this;
      }

      public ActivityBuilder WithStatus(ActivityStatus status)
      {
        _status = status;
        return this;
      }

      public ActivityBuilder WithCreatedBy(string? createdBy)
      {
        _createdBy = createdBy;
        return this;
      }

      public ActivityBuilder WithTitle(string? title)
      {
        _title = title;
        return this;
      }

      public ActivityBuilder WithDescription(string? description)
      {
        _description = description;
        return this;
      }

      public ActivityBuilder WithUserActions(UserAction[] userActions)
      {
        _userActions = userActions;
        return this;
      }

      public ActivityBuilder WithTransactionsMetadata(TransactionsMetadata? transactionsMetadata)
      {
        _transactionsMetadata = transactionsMetadata;
        return this;
      }

      public ActivityBuilder WithAccountMetadata(AccountMetadata? accountMetadata)
      {
        _accountMetadata = accountMetadata;
        return this;
      }

      public ActivityBuilder WithOrdersMetadata(OrdersMetadata? ordersMetadata)
      {
        _ordersMetadata = ordersMetadata;
        return this;
      }

      public ActivityBuilder WithSymbols(string[] symbols)
      {
        _symbols = symbols;
        return this;
      }

      public ActivityBuilder WithCreatedAt(string? createdAt)
      {
        _createdAt = createdAt;
        return this;
      }

      public ActivityBuilder WithUpdatedAt(string? updatedAt)
      {
        _updatedAt = updatedAt;
        return this;
      }

      public Activity Build()
      {
        return new Activity
        {
          Id = _id,
          ReferenceId = _referenceId,
          Category = _category,
          Type = _type,
          SecondaryType = _secondaryType,
          Status = _status,
          CreatedBy = _createdBy,
          Title = _title,
          Description = _description,
          UserActions = _userActions,
          TransactionsMetadata = _transactionsMetadata,
          AccountMetadata = _accountMetadata,
          OrdersMetadata = _ordersMetadata,
          Symbols = _symbols,
          CreatedAt = _createdAt,
          UpdatedAt = _updatedAt
        };
      }
    }
  }
}
