/*
 * Copyright 2024-present Coinbase Global, Inc.
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

using System;
using System.IO;
using System.Threading.Tasks;

namespace ModelGenerator;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            Console.WriteLine("Coinbase Prime .NET SDK Model Generator");
            Console.WriteLine("======================================");

            var projectRoot = GetProjectRoot();
            var specPath = Path.Combine(projectRoot, "apiSpec", "prime-public-spec.yaml");
            var outputDir = Path.Combine(projectRoot, "src", "CoinbaseSdk", "Prime", "model");
            var tempDir = Path.Combine(projectRoot, "generated");

            Console.WriteLine($"Project Root: {projectRoot}");
            Console.WriteLine($"OpenAPI Spec: {specPath}");
            Console.WriteLine($"Output Directory: {outputDir}");
            Console.WriteLine($"Temp Directory: {tempDir}");

            if (!File.Exists(specPath))
            {
                Console.Error.WriteLine($"ERROR: OpenAPI spec not found at {specPath}");
                return 1;
            }

            // Phase 1: Generate raw models using OpenAPI Generator
            Console.WriteLine("\nPhase 1: Generating raw models with OpenAPI Generator...");
            var generator = new OpenApiGenerator(specPath, tempDir);
            await generator.GenerateModelsAsync();

            // Phase 2: Post-process models to match existing patterns
            Console.WriteLine("\nPhase 2: Post-processing models...");
            var postProcessor = new PostProcessor(tempDir, outputDir);
            await postProcessor.ProcessModelsAsync();

            Console.WriteLine("\nModel generation completed successfully!");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ERROR: {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);
            return 1;
        }
    }

    private static string GetProjectRoot()
    {
        var current = Directory.GetCurrentDirectory();
        while (current != null)
        {
            if (File.Exists(Path.Combine(current, "prime-sdk-dotnet.sln")))
            {
                return current;
            }
            current = Directory.GetParent(current)?.FullName;
        }
        throw new InvalidOperationException("Could not find project root (looking for prime-sdk-dotnet.sln)");
    }
}