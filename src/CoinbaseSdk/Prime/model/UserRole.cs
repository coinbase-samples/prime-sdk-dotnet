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
    /// <summary>
    /// Indicates the user's role.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// An auditor.
        /// </summary>
        AUDITOR,

        /// <summary>
        /// A signatory.
        /// </summary>
        SIGNATORY,

        /// <summary>
        /// An admin.
        /// </summary>
        ADMIN,

        /// <summary>
        /// An initiator.
        /// </summary>
        INITIATOR,

        /// <summary>
        /// A reviewer.
        /// </summary>
        REVIEWER,

        /// <summary>
        /// A trader.
        /// </summary>
        TRADER,

        /// <summary>
        /// A trader with full permissions.
        /// </summary>
        FULL_TRADER,

        /// <summary>
        /// A team manager.
        /// </summary>
        TEAM_MANAGER,

        /// <summary>
        /// An approver.
        /// </summary>
        APPROVER
    }
}