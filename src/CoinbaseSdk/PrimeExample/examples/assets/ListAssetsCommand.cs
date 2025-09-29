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

using System.CommandLine;

namespace CoinbaseSdk.PrimeExample.Examples.Assets;

/// <summary>
/// Command line interface for the ListAssets example
/// </summary>
public static class ListAssetsCommand
{
    /// <summary>
    /// Creates the ListAssets command
    /// </summary>
    /// <returns>Configured command</returns>
    public static Command CreateCommand()
    {
        var command = new Command("ListAssets", "List assets for an entity");

        var entityIdOption = new Option<string?>(
            "--entityId",
            "The entity ID (optional, can be set via environment variables)");

        command.AddOption(entityIdOption);

        command.SetHandler((entityId) =>
        {
            var success = ListAssets.Run(entityId);
            Environment.ExitCode = success ? 0 : 1;
        }, entityIdOption);

        return command;
    }
}
