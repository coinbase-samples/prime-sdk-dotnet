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
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ModelGenerator;

public class OpenApiGenerator
{
    private readonly string _specPath;
    private readonly string _outputDir;

    public OpenApiGenerator(string specPath, string outputDir)
    {
        _specPath = specPath;
        _outputDir = outputDir;
    }

    public async Task GenerateModelsAsync()
    {
        var rawOutputDir = Path.Combine(_outputDir, "raw");

        // Clean and create output directory
        if (Directory.Exists(rawOutputDir))
        {
            Directory.Delete(rawOutputDir, true);
        }
        Directory.CreateDirectory(rawOutputDir);

        // Create OpenAPI Generator config
        var configPath = Path.Combine(_outputDir, "generator-config.json");
        await CreateGeneratorConfigAsync(configPath, rawOutputDir);

        // Check if OpenAPI Generator is available
        if (!await IsOpenApiGeneratorAvailableAsync())
        {
            Console.WriteLine("OpenAPI Generator not found. Attempting to install via npm...");
            await InstallOpenApiGeneratorAsync();
        }

        // Run OpenAPI Generator
        await RunOpenApiGeneratorAsync(configPath);

        Console.WriteLine($"Raw models generated in: {rawOutputDir}");
    }

    private async Task CreateGeneratorConfigAsync(string configPath, string outputDir)
    {
        var config = new
        {
            generatorName = "csharp",
            inputSpec = _specPath,
            outputDir = outputDir,
            additionalProperties = new
            {
                targetFramework = "net8.0",
                modelPropertyNaming = "PascalCase",
                enumNameSuffix = "",
                useDateTimeOffset = false,
                useCollection = false,
                nullableReferenceTypes = true,
                packageName = "Generated",
                packageTitle = "Generated Models",
                packageDescription = "Generated models from OpenAPI spec",
                packageCompany = "Coinbase",
                packageAuthors = "Coinbase",
                packageCopyright = "Copyright 2024-present Coinbase Global, Inc.",
                packageVersion = "1.0.0",
                excludeTests = true,
                generatePropertyChanged = false,
                hideGenerationTimestamp = true,
                sourceFolder = "src/main/csharp"
            }
        };

        var json = JsonConvert.SerializeObject(config, Formatting.Indented);
        await File.WriteAllTextAsync(configPath, json);
        Console.WriteLine($"Generator config created: {configPath}");
    }

    private async Task<bool> IsOpenApiGeneratorAvailableAsync()
    {
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "openapi-generator-cli",
                    Arguments = "version",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            await process.WaitForExitAsync();
            return process.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }

    private async Task InstallOpenApiGeneratorAsync()
    {
        Console.WriteLine("Installing OpenAPI Generator CLI via npm...");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "npm",
                Arguments = "install -g @openapitools/openapi-generator-cli@2.7.0",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        process.Start();

        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException($"Failed to install OpenAPI Generator CLI. Error: {error}");
        }

        Console.WriteLine("OpenAPI Generator CLI installed successfully.");
    }

    private async Task RunOpenApiGeneratorAsync(string configPath)
    {
        Console.WriteLine("Running OpenAPI Generator...");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "openapi-generator-cli",
                Arguments = $"generate -c \"{configPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        process.Start();

        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            Console.WriteLine($"OpenAPI Generator output: {output}");
            Console.WriteLine($"OpenAPI Generator error: {error}");
            throw new InvalidOperationException($"OpenAPI Generator failed with exit code {process.ExitCode}");
        }

        Console.WriteLine("OpenAPI Generator completed successfully.");
        if (!string.IsNullOrEmpty(output))
        {
            Console.WriteLine($"Output: {output}");
        }
    }
}