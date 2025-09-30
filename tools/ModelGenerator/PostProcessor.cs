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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using YamlDotNet.Serialization;

namespace ModelGenerator;

public class PostProcessor
{
    private readonly string _inputDir;
    private readonly string _outputDir;
    private readonly Dictionary<string, SchemaInfo> _schemas;

    private const string LicenseHeader = @"/*
 * Copyright 2024-present Coinbase Global, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the ""License"");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an ""AS IS"" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */";

    public PostProcessor(string inputDir, string outputDir)
    {
        _inputDir = inputDir;
        _outputDir = outputDir;
        _schemas = new Dictionary<string, SchemaInfo>();
    }

    public async Task ProcessModelsAsync()
    {
        Console.WriteLine("Loading OpenAPI schema information...");
        await LoadSchemaInfoAsync();

        Console.WriteLine("Finding generated model files...");
        var modelFiles = FindGeneratedModelFiles();
        Console.WriteLine($"Found {modelFiles.Count} model files to process");

        // Create output directory
        Directory.CreateDirectory(_outputDir);

        foreach (var file in modelFiles)
        {
            Console.WriteLine($"Processing: {Path.GetFileName(file)}");
            await ProcessModelFileAsync(file);
        }

        Console.WriteLine($"Processed {modelFiles.Count} model files");

        // Clean up unwanted generated files
        CleanupUnwantedFiles();
    }

    private async Task LoadSchemaInfoAsync()
    {
        var specPath = Path.Combine(GetProjectRoot(), "apiSpec", "prime-public-spec.yaml");
        if (!File.Exists(specPath))
        {
            Console.WriteLine("Warning: OpenAPI spec not found, proceeding without schema info");
            return;
        }

        try
        {
            var yaml = await File.ReadAllTextAsync(specPath);
            var deserializer = new DeserializerBuilder().Build();
            var spec = deserializer.Deserialize<OpenApiSpec>(yaml);

            if (spec?.Components?.Schemas != null)
            {
                foreach (var schema in spec.Components.Schemas)
                {
                    _schemas[schema.Key] = schema.Value;
                }
            }
            Console.WriteLine($"Loaded {_schemas.Count} schema definitions from OpenAPI spec.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Could not parse OpenAPI spec for schema info: {ex.Message}");
            Console.WriteLine("Proceeding without schema info - basic transformations will still be applied.");
        }
    }

    private List<string> FindGeneratedModelFiles()
    {
        var csharpDir = Path.Combine(_inputDir, "raw", "src", "main", "csharp");
        if (!Directory.Exists(csharpDir))
        {
            // Try alternative structure
            csharpDir = Path.Combine(_inputDir, "raw");
        }

        var files = new List<string>();
        if (Directory.Exists(csharpDir))
        {
            files.AddRange(Directory.GetFiles(csharpDir, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.Contains("Test") && !f.Contains("Client") && !f.Contains("Api")));
        }

        return files;
    }

    private async Task ProcessModelFileAsync(string filePath)
    {
        var content = await File.ReadAllTextAsync(filePath);
        var tree = CSharpSyntaxTree.ParseText(content);
        var root = tree.GetCompilationUnitRoot();

        // Transform the syntax tree
        var transformer = new ModelTransformer(_schemas);
        var newRoot = transformer.Visit(root);

        // Generate the output
        var result = newRoot.ToFullString();
        result = ApplyFinalFormatting(result);

        // Determine output file path
        var fileName = Path.GetFileName(filePath);
        var outputPath = Path.Combine(_outputDir, fileName);

        await File.WriteAllTextAsync(outputPath, result);
    }

    private string ApplyFinalFormatting(string content)
    {
        // Add license header
        var result = new StringBuilder();
        result.AppendLine(LicenseHeader);
        result.AppendLine();

        // Skip any existing headers in the content
        var lines = content.Split('\n');
        var startIndex = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim().StartsWith("namespace"))
            {
                startIndex = i;
                break;
            }
        }

        for (int i = startIndex; i < lines.Length; i++)
        {
            result.AppendLine(lines[i].TrimEnd());
        }

        return result.ToString();
    }

    private void CleanupUnwantedFiles()
    {
        Console.WriteLine("Cleaning up unwanted generated files...");

        var cleanupRules = new List<string>
        {
            // PrimeRESTAPI request files (handled manually in services)
            "PrimeRESTAPI*Request.cs",

            // Google/infrastructure files
            "Google*.cs",
            "IHostBuilderExtensions.cs",
            "IServiceCollectionExtensions.cs",

            // Specific problematic model files
            "ChangeOnchainAddressGroupRequestIsARequestToCreateOrUpdateANewOnchainAddressGroup.cs",
            "CreateATransferBetweenTwoWallets.cs",

            // RFQ is also a request-type class
            "RFQ.cs"
        };

        var deletedCount = 0;
        var projectRoot = GetProjectRoot();
        var generatedDir = Path.Combine(projectRoot, "generated");

        // Clean up files from the output directory
        foreach (var pattern in cleanupRules)
        {
            var files = Directory.GetFiles(_outputDir, pattern, SearchOption.AllDirectories);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"Deleted unwanted file: {Path.GetFileName(file)}");
                    deletedCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Could not delete {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }

        // Clean up the generated/ directory if it exists
        if (Directory.Exists(generatedDir))
        {
            try
            {
                Directory.Delete(generatedDir, true);
                Console.WriteLine("Deleted generated/ directory");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not delete generated/ directory: {ex.Message}");
            }
        }

        Console.WriteLine($"Cleanup completed. Removed {deletedCount} unwanted files.");
    }

    private string GetProjectRoot()
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
        throw new InvalidOperationException("Could not find project root");
    }
}

public class ModelTransformer : CSharpSyntaxRewriter
{
    private readonly Dictionary<string, SchemaInfo> _schemas;

    public ModelTransformer(Dictionary<string, SchemaInfo> schemas)
    {
        _schemas = schemas;
    }

    public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
    {
        // Update namespace and using statements
        var newUsings = SyntaxFactory.List(new[]
        {
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Text.Json.Serialization"))
        });

        var newNamespace = SyntaxFactory.NamespaceDeclaration(
            SyntaxFactory.ParseName("CoinbaseSdk.Prime.Model"));

        // Move all members to the new namespace
        var classMembers = node.Members.OfType<ClassDeclarationSyntax>().Cast<MemberDeclarationSyntax>();
        var enumMembers = node.Members.OfType<EnumDeclarationSyntax>().Cast<MemberDeclarationSyntax>();
        var members = classMembers.Concat(enumMembers).ToList();

        var transformedMembers = new List<MemberDeclarationSyntax>();
        foreach (var member in members)
        {
            var transformed = Visit(member);
            if (transformed is MemberDeclarationSyntax transformedMember)
            {
                transformedMembers.Add(transformedMember);
            }
        }

        newNamespace = newNamespace.WithMembers(SyntaxFactory.List(transformedMembers));

        return SyntaxFactory.CompilationUnit()
            .WithUsings(newUsings)
            .WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>(new[] { newNamespace }))
            .NormalizeWhitespace();
    }

    public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        // Transform the class
        var className = node.Identifier.ValueText;
        var transformedClass = (ClassDeclarationSyntax)base.VisitClassDeclaration(node)!;

        // Add XML documentation if not present
        if (transformedClass != null && !HasXmlDocumentation(transformedClass))
        {
            var xmlDoc = GenerateClassDocumentation(className);
            transformedClass = transformedClass.WithLeadingTrivia(xmlDoc);
        }

        // Add builder pattern if this class should have one
        if (transformedClass != null && ShouldHaveBuilder(transformedClass))
        {
            transformedClass = AddBuilderPattern(transformedClass);
        }

        // Handle pagination inheritance
        if (transformedClass != null && IsPaginatedResponse(className))
        {
            transformedClass = AddPaginationInheritance(transformedClass);
        }

        // Add default constructor
        if (transformedClass != null && !HasDefaultConstructor(transformedClass))
        {
            transformedClass = AddDefaultConstructor(transformedClass);
        }

        return transformedClass ?? node;
    }

    public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        var property = (PropertyDeclarationSyntax)base.VisitPropertyDeclaration(node)!;
        var propertyName = property.Identifier.ValueText;

        // Add JsonPropertyName attribute if needed
        var jsonName = ConvertToSnakeCase(propertyName);
        if (jsonName != propertyName.ToLower() && !HasJsonPropertyNameAttribute(property))
        {
            property = AddJsonPropertyNameAttribute(property, jsonName);
        }

        // Add XML documentation if not present
        if (!HasXmlDocumentation(property))
        {
            var xmlDoc = GeneratePropertyDocumentation(propertyName);
            property = property.WithLeadingTrivia(xmlDoc);
        }

        // Fix array initialization syntax
        property = FixArrayInitialization(property);

        return property;
    }

    public override SyntaxNode VisitEnumDeclaration(EnumDeclarationSyntax node)
    {
        var enumDecl = (EnumDeclarationSyntax)base.VisitEnumDeclaration(node)!;
        var enumName = enumDecl.Identifier.ValueText;

        // Add XML documentation if not present
        if (!HasXmlDocumentation(enumDecl))
        {
            var xmlDoc = GenerateEnumDocumentation(enumName);
            enumDecl = enumDecl.WithLeadingTrivia(xmlDoc);
        }

        // Ensure enum values are in ALL_CAPS format
        var newMembers = new List<EnumMemberDeclarationSyntax>();
        foreach (var member in enumDecl.Members)
        {
            var newName = ConvertToAllCaps(member.Identifier.ValueText);
            var newMember = member.WithIdentifier(SyntaxFactory.Identifier(newName));
            newMembers.Add(newMember);
        }

        return enumDecl.WithMembers(SyntaxFactory.SeparatedList(newMembers));
    }

    private bool HasXmlDocumentation(SyntaxNode node)
    {
        return node.GetLeadingTrivia()
            .Any(t => t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
                     t.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia));
    }

    private SyntaxTriviaList GenerateClassDocumentation(string className)
    {
        return SyntaxFactory.TriviaList(
            SyntaxFactory.Trivia(
                SyntaxFactory.DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia)
                    .WithContent(SyntaxFactory.List(new XmlNodeSyntax[]
                    {
                        SyntaxFactory.XmlText("    /// "),
                        SyntaxFactory.XmlElement("summary",
                            SyntaxFactory.List(new XmlNodeSyntax[]
                            {
                                SyntaxFactory.XmlText($"\n    /// Represents a {className} in the Coinbase Prime system.\n    /// ")
                            })),
                        SyntaxFactory.XmlText("\n")
                    }))));
    }

    private SyntaxTriviaList GeneratePropertyDocumentation(string propertyName)
    {
        return SyntaxFactory.TriviaList(
            SyntaxFactory.Trivia(
                SyntaxFactory.DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia)
                    .WithContent(SyntaxFactory.List(new XmlNodeSyntax[]
                    {
                        SyntaxFactory.XmlText("    /// "),
                        SyntaxFactory.XmlElement("summary",
                            SyntaxFactory.List(new XmlNodeSyntax[]
                            {
                                SyntaxFactory.XmlText($"\n    /// The {propertyName} property.\n    /// ")
                            })),
                        SyntaxFactory.XmlText("\n")
                    }))));
    }

    private SyntaxTriviaList GenerateEnumDocumentation(string enumName)
    {
        return SyntaxFactory.TriviaList(
            SyntaxFactory.Trivia(
                SyntaxFactory.DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia)
                    .WithContent(SyntaxFactory.List(new XmlNodeSyntax[]
                    {
                        SyntaxFactory.XmlText("    /// "),
                        SyntaxFactory.XmlElement("summary",
                            SyntaxFactory.List(new XmlNodeSyntax[]
                            {
                                SyntaxFactory.XmlText($"\n    /// Represents the {enumName} enumeration.\n    /// ")
                            })),
                        SyntaxFactory.XmlText("\n")
                    }))));
    }

    private bool ShouldHaveBuilder(ClassDeclarationSyntax classDecl)
    {
        // Skip enums and small classes
        var properties = classDecl.Members.OfType<PropertyDeclarationSyntax>().ToList();
        return properties.Count >= 4 || HasComplexProperties(properties);
    }

    private bool HasComplexProperties(List<PropertyDeclarationSyntax> properties)
    {
        return properties.Any(p => IsComplexType(p.Type));
    }

    private bool IsComplexType(TypeSyntax type)
    {
        var typeName = type.ToString();
        return !typeName.Contains("string") && !typeName.Contains("int") &&
               !typeName.Contains("bool") && !typeName.Contains("decimal") &&
               !typeName.Contains("DateTime");
    }

    private ClassDeclarationSyntax AddBuilderPattern(ClassDeclarationSyntax classDecl)
    {
        var className = classDecl.Identifier.ValueText;
        var builderClassName = $"{className}Builder";
        var properties = classDecl.Members.OfType<PropertyDeclarationSyntax>().ToList();

        // Create builder class
        var builderClass = CreateBuilderClass(builderClassName, className, properties);

        // Add builder as nested class
        var newMembers = classDecl.Members.Add(builderClass);
        return classDecl.WithMembers(newMembers);
    }

    private ClassDeclarationSyntax CreateBuilderClass(string builderClassName, string targetClassName,
        List<PropertyDeclarationSyntax> properties)
    {
        var builderClass = SyntaxFactory.ClassDeclaration(builderClassName)
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));

        // Add XML documentation for builder
        var xmlDoc = SyntaxFactory.TriviaList(
            SyntaxFactory.Trivia(
                SyntaxFactory.DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia)
                    .WithContent(SyntaxFactory.List(new XmlNodeSyntax[]
                    {
                        SyntaxFactory.XmlText("    /// "),
                        SyntaxFactory.XmlElement("summary",
                            SyntaxFactory.List(new XmlNodeSyntax[]
                            {
                                SyntaxFactory.XmlText($"\n    /// Builder class for creating {targetClassName} instances.\n    /// ")
                            })),
                        SyntaxFactory.XmlText("\n")
                    }))));

        builderClass = builderClass.WithLeadingTrivia(xmlDoc);

        // Add private fields
        var members = new List<MemberDeclarationSyntax>();
        foreach (var prop in properties)
        {
            var fieldName = "_" + prop.Identifier.ValueText.ToLowerFirstChar();
            var field = SyntaxFactory.FieldDeclaration(
                SyntaxFactory.VariableDeclaration(prop.Type)
                    .WithVariables(SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(fieldName))
                            .WithInitializer(GetDefaultInitializer(prop.Type)))))
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PrivateKeyword)));
            members.Add(field);
        }

        // Add With methods
        foreach (var prop in properties)
        {
            var withMethod = CreateWithMethod(builderClassName, prop);
            members.Add(withMethod);
        }

        // Add Build method
        var buildMethod = CreateBuildMethod(targetClassName, properties);
        members.Add(buildMethod);

        return builderClass.WithMembers(SyntaxFactory.List(members));
    }

    private MethodDeclarationSyntax CreateWithMethod(string builderClassName, PropertyDeclarationSyntax property)
    {
        var methodName = $"With{property.Identifier.ValueText}";
        var paramName = property.Identifier.ValueText.ToLowerFirstChar();
        var fieldName = "_" + paramName;

        return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.IdentifierName(builderClassName),
                SyntaxFactory.Identifier(methodName))
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(SyntaxFactory.ParameterList(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Parameter(SyntaxFactory.Identifier(paramName))
                        .WithType(property.Type))))
            .WithBody(SyntaxFactory.Block(
                SyntaxFactory.ExpressionStatement(
                    SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                        SyntaxFactory.IdentifierName(fieldName),
                        SyntaxFactory.IdentifierName(paramName))),
                SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression())));
    }

    private MethodDeclarationSyntax CreateBuildMethod(string targetClassName, List<PropertyDeclarationSyntax> properties)
    {
        var assignments = properties.Select(prop =>
            SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                SyntaxFactory.IdentifierName(prop.Identifier.ValueText),
                SyntaxFactory.IdentifierName("_" + prop.Identifier.ValueText.ToLowerFirstChar())));

        var objectCreation = SyntaxFactory.ObjectCreationExpression(
                SyntaxFactory.IdentifierName(targetClassName))
            .WithInitializer(SyntaxFactory.InitializerExpression(SyntaxKind.ObjectInitializerExpression,
                SyntaxFactory.SeparatedList<ExpressionSyntax>(assignments)));

        return SyntaxFactory.MethodDeclaration(
                SyntaxFactory.IdentifierName(targetClassName),
                SyntaxFactory.Identifier("Build"))
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithBody(SyntaxFactory.Block(
                SyntaxFactory.ReturnStatement(objectCreation)));
    }

    private EqualsValueClauseSyntax? GetDefaultInitializer(TypeSyntax type)
    {
        var typeName = type.ToString();
        if (typeName.EndsWith("[]"))
        {
            return SyntaxFactory.EqualsValueClause(
                SyntaxFactory.ImplicitArrayCreationExpression(
                    SyntaxFactory.InitializerExpression(SyntaxKind.ArrayInitializerExpression)));
        }
        return null;
    }

    private bool IsPaginatedResponse(string className)
    {
        return className.Contains("List") && className.Contains("Response");
    }

    private ClassDeclarationSyntax AddPaginationInheritance(ClassDeclarationSyntax classDecl)
    {
        var baseList = classDecl.BaseList ?? SyntaxFactory.BaseList();
        var paginatedType = SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName("PaginatedResponse"));

        if (!baseList.Types.Any(t => t.Type.ToString().Contains("PaginatedResponse")))
        {
            baseList = baseList.AddTypes(paginatedType);
            return classDecl.WithBaseList(baseList);
        }

        return classDecl;
    }

    private bool HasDefaultConstructor(ClassDeclarationSyntax classDecl)
    {
        return classDecl.Members.OfType<ConstructorDeclarationSyntax>()
            .Any(c => c.ParameterList.Parameters.Count == 0);
    }

    private ClassDeclarationSyntax AddDefaultConstructor(ClassDeclarationSyntax classDecl)
    {
        var constructor = SyntaxFactory.ConstructorDeclaration(classDecl.Identifier)
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(SyntaxFactory.ParameterList())
            .WithBody(SyntaxFactory.Block());

        return classDecl.WithMembers(classDecl.Members.Add(constructor));
    }

    private bool HasJsonPropertyNameAttribute(PropertyDeclarationSyntax property)
    {
        return property.AttributeLists.Any(list =>
            list.Attributes.Any(attr => attr.Name.ToString().Contains("JsonPropertyName")));
    }

    private PropertyDeclarationSyntax AddJsonPropertyNameAttribute(PropertyDeclarationSyntax property, string jsonName)
    {
        var attribute = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("JsonPropertyName"))
            .WithArgumentList(SyntaxFactory.AttributeArgumentList(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.AttributeArgument(
                        SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                            SyntaxFactory.Literal(jsonName))))));

        var attributeList = SyntaxFactory.AttributeList(
            SyntaxFactory.SingletonSeparatedList(attribute));

        return property.WithAttributeLists(property.AttributeLists.Add(attributeList));
    }

    private PropertyDeclarationSyntax FixArrayInitialization(PropertyDeclarationSyntax property)
    {
        var typeName = property.Type.ToString();
        if (typeName.EndsWith("[]") && property.Initializer == null)
        {
            var initializer = SyntaxFactory.EqualsValueClause(
                SyntaxFactory.ImplicitArrayCreationExpression(
                    SyntaxFactory.InitializerExpression(SyntaxKind.ArrayInitializerExpression)));

            return property.WithInitializer(initializer);
        }
        return property;
    }

    private string ConvertToSnakeCase(string pascalCase)
    {
        return Regex.Replace(pascalCase, "([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }

    private string ConvertToAllCaps(string value)
    {
        return Regex.Replace(value, "([a-z0-9])([A-Z])", "$1_$2").ToUpper();
    }
}

// Extension methods
public static class StringExtensions
{
    public static string ToLowerFirstChar(this string input)
    {
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }
}

// OpenAPI spec models for YAML parsing
public class OpenApiSpec
{
    public Components? Components { get; set; }
}

public class Components
{
    public Dictionary<string, SchemaInfo>? Schemas { get; set; }
}

public class SchemaInfo
{
    public string? Type { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, PropertyInfo>? Properties { get; set; }
    public List<string>? Enum { get; set; }
}

public class PropertyInfo
{
    public string? Type { get; set; }
    public string? Description { get; set; }
}