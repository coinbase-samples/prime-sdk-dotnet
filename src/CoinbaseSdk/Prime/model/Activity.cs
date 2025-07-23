/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;

  /// <summary>
  /// Represents an account activity in the Coinbase Prime system.
  /// </summary>
  public class Activity
  {
    /// <summary>
    /// A unique id for the account activity.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// A reference for orders and transactions, n/a for other category types.
    /// </summary>
    [JsonPropertyName("reference_id")]
    public string? ReferenceId { get; set; }

    /// <summary>
    /// The category of the activity.
    /// </summary>
    public ActivityCategory Category { get; set; }

    /// <summary>
    /// The type of the activity.
    /// </summary>
    public ActivityType Type { get; set; }

    /// <summary>
    /// The secondary type of the activity.
    /// </summary>
    [JsonPropertyName("secondary_type")]
    public ActivitySecondaryType SecondaryType { get; set; }

    /// <summary>
    /// The status of the activity.
    /// </summary>
    public ActivityStatus Status { get; set; }

    /// <summary>
    /// Id of user who created the activity.
    /// </summary>
    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Title of the activity.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Description detail of the activity.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Actions related to the Activity.
    /// </summary>
    [JsonPropertyName("user_actions")]
    public UserAction[] UserActions { get; set; } = [];

    /// <summary>
    /// Metadata for transactions associated with this activity.
    /// </summary>
    [JsonPropertyName("transactions_metadata")]
    public ActivityMetadataTransactions? TransactionsMetadata { get; set; }

    /// <summary>
    /// Metadata for accounts associated with this activity.
    /// </summary>
    [JsonPropertyName("account_metadata")]
    public ActivityMetadataAccount? AccountMetadata { get; set; }

    /// <summary>
    /// Metadata for orders associated with this activity.
    /// </summary>
    [JsonPropertyName("orders_metadata")]
    public Dictionary<string, string>? OrdersMetadata { get; set; }

    /// <summary>
    /// List of currencies included in an activity.
    /// </summary>
    public string[] Symbols { get; set; } = [];

    /// <summary>
    /// Time activity was created at.
    /// </summary>
    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    /// <summary>
    /// Time for latest status update of account activity.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; set; }

    /// <summary>
    /// The hierarchy type of the activity.
    /// </summary>
    [JsonPropertyName("hierarchy_type")]
    public HierarchyType? HierarchyType { get; set; }

    public Activity() { }

    /// <summary>
    /// Builder class for creating Activity instances.
    /// </summary>
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
      private ActivityMetadataTransactions? _transactionsMetadata;
      private ActivityMetadataAccount? _accountMetadata;
      private Dictionary<string, string>? _ordersMetadata;
      private string[] _symbols = [];
      private string? _createdAt;
      private string? _updatedAt;
      private HierarchyType? _hierarchyType;

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

      public ActivityBuilder WithTransactionsMetadata(
          ActivityMetadataTransactions? transactionsMetadata)
      {
        _transactionsMetadata = transactionsMetadata;
        return this;
      }

      public ActivityBuilder WithAccountMetadata(ActivityMetadataAccount? accountMetadata)
      {
        _accountMetadata = accountMetadata;
        return this;
      }

      public ActivityBuilder WithOrdersMetadata(Dictionary<string, string>? ordersMetadata)
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

      public ActivityBuilder WithHierarchyType(HierarchyType? hierarchyType)
      {
        _hierarchyType = hierarchyType;
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
          UpdatedAt = _updatedAt,
          HierarchyType = _hierarchyType,
        };
      }
    }
  }
}
