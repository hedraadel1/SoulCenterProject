//-----------------------------------------------------------------------
// <copyright file="BasicCodeGenerator.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
using Microsoft.CodeAnalysis.MSBuild;
using SoulCenterProject.Modules.Ai.Interfaces;
using SoulCenterProject.Modules.Ai.Model;
using SoulCenterProject.Modules.Ai.Services;

namespace SoulCenterProject.Modules.Ai.CodeGeneration
{
    public class BasicCodeGenerator : ICodeGenerator
    {
        public string StatusofExcute;

        public string GetProjectStructureString(
    string projectName,
    List<NamespaceStructure> namespaces)
        {
            StringBuilder projectStructure = new StringBuilder();

            projectStructure.AppendLine($"Project Name: {projectName}");
            foreach (var ns in namespaces)
            {
                projectStructure.AppendLine($"Namespace: {ns.Name}");
                foreach (var cls in ns.Classes)
                {
                    projectStructure.AppendLine($"  Class: {cls.Name}");
                    // Limit the number of methods printed to avoid token limit issues
                    int methodCount = 0;
                    foreach (var method in cls.Methods)
                    {
                        // Print only the first 5 methods
                        if (methodCount < 5)
                        {
                            projectStructure.AppendLine($"    Method: {method.Name}");
                            methodCount++;
                        }
                        else
                        {
                            projectStructure.AppendLine("    ..."); // Indicate more methods exist
                            break;
                        }
                    }

                    // Indicate if there are more methods not printed
                    if (cls.Methods.Count > 5)
                    {
                        projectStructure.AppendLine($"    [Total {cls.Methods.Count} methods]");
                    }

                    // Limit the number of classes printed to avoid token limit issues
                    if (ns.Classes.Count > 5)
                    {
                        projectStructure.AppendLine($"  ... [Total {ns.Classes.Count} classes]");
                    }
                }

                // Indicate if there are more namespaces not printed
                if (namespaces.Count > 5)
                {
                    projectStructure.AppendLine($"... [Total {namespaces.Count} namespaces]");
                }
            }

            return projectStructure.ToString();
        }

        private void WriteProjectStructureToFile(
            string outputPath,
            string projectName,
            List<NamespaceStructure> namespaces)
        {
            using (StreamWriter writer = File.CreateText(outputPath))
            {
                writer.WriteLine($"Project Name: {projectName}");
                foreach (var ns in namespaces)
                {
                    writer.WriteLine($"Namespace: {ns.Name}");
                    foreach (var cls in ns.Classes)
                    {
                        writer.WriteLine($"  Class: {cls.Name}");
                        foreach (var method in cls.Methods)
                        {
                            writer.WriteLine($"    Method: {method.Name}");
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Writes the project structure details to a file.
        /// </summary>
        /// <param name="projectName">The name of the project.</param>
        /// <param name="outputPath">The path to write the project structure details to.</param>
        /// <param name="namespaces">The list of namespaces in the project.</param>
        private async Task WriteProjectStructureToFileDetails(
            string outputPath,
            string projectName,
            List<NamespaceStructure> namespaces)
        {
            // HowToWork
            // This method writes the project structure details to a file.
            // It first initializes a StringBuilder to store the structure, then iterates through namespaces, classes, and methods
            // to append them to the StringBuilder.
            StringBuilder projectStructure = new StringBuilder();

            projectStructure.AppendLine($"Project Name: {projectName}");
            foreach (var ns in namespaces)
            {
                projectStructure.AppendLine($"Namespace: {ns.Name}");
                foreach (var cls in ns.Classes)
                {
                    projectStructure.AppendLine($"  Class: {cls.Name}");
                    foreach (var method in cls.Methods)
                    {
                        projectStructure.AppendLine($"    Method: {method.Name}");
                    }
                }
            }

            using (StreamWriter writer = File.CreateText(outputPath))
            {
                await writer.WriteAsync(projectStructure.ToString());
            }
        }

        public void WriteProjectStructureToFileToai(
            string outputPath,
            string projectName,
            List<NamespaceStructure> namespaces)
        {
            using (StreamWriter writer = File.CreateText(outputPath))
            {
                writer.WriteLine($"Project Name: {projectName}");
                foreach (var ns in namespaces)
                {
                    writer.WriteLine($"Namespace: {ns.Name}");
                    foreach (var cls in ns.Classes)
                    {
                        writer.WriteLine($"  Class: {cls.Name}");
                        // Limit the number of methods printed to avoid token limit issues
                        int methodCount = 0;
                        foreach (var method in cls.Methods)
                        {
                            // Print only the first 5 methods
                            if (methodCount < 5)
                            {
                                writer.WriteLine($"    Method: {method.Name}");
                                methodCount++;
                            }
                            else
                            {
                                writer.WriteLine("    ..."); // Indicate more methods exist
                                break;
                            }
                        }

                        // Indicate if there are more methods not printed
                        if (cls.Methods.Count > 5)
                        {
                            writer.WriteLine($"    [Total {cls.Methods.Count} methods]");
                        }
                    }

                    // Limit the number of classes printed to avoid token limit issues
                    if (ns.Classes.Count > 5)
                    {
                        writer.WriteLine($"  ... [Total {ns.Classes.Count} classes]");
                    }
                }

                // Indicate if there are more namespaces not printed
                if (namespaces.Count > 5)
                {
                    writer.WriteLine($"... [Total {namespaces.Count} namespaces]");
                }
            }
        }



        /// <summary>
        /// Adds a new method to an existing class file.
        /// </summary>
        /// <param name="classPath">The path where the class file is located.</param>
        /// <param name="className">The name of the class to add the method to.</param>
        /// <param name="methodName">The name of the method to add.</param>
        /// <param name="methodContent">The content of the method to add.</param>
        public void AddMethod(string classPath, string className, string methodName, string methodContent)
        {
            // HowToWork
            // This method adds a new method to an existing class file at the specified path.
            // It reads the existing file content, appends the new method content, and then writes the updated content back to the file.
            string fullFilePath = Path.Combine(classPath, className + ".cs");
            string fileContent = File.ReadAllText(fullFilePath);
            fileContent += "\n" + methodContent; // Simple append
            File.WriteAllText(fullFilePath, fileContent);
        }


        /// <summary>
        /// Adds a new method to a class file using Roslyn.
        /// </summary>
        /// <param name="solutionPath">The path to the solution file.</param>
        /// <param name="projectName">The name of the target project in the solution.</param>
        /// <param name="className">The name of the class to which the method will be added.</param>
        /// <param name="classPath">The path within the project where the class file is located.</param>
        /// <param name="methodNode">The syntax node representing the new method.</param>
        public void AddMethodByRoslyn(
            string solutionPath,
            string projectName,
            string className,
            string classPath,
            SyntaxNode methodNode)
        {
            // Load the solution
            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(solutionPath).Result;
            var project = solution.Projects.FirstOrDefault(p => p.Name == projectName);

            if (project == null)
            {
                return;
            }

            // Find the class syntax
            var syntaxRoot = project.Documents.First().GetSyntaxRootAsync().Result;
            var classSyntax = syntaxRoot.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault(c => c.Identifier.Text == className);

            if (classSyntax == null)
            {
                return;
            }

            // Add the new method to the class
            var newClassSyntax = classSyntax.AddMembers((MethodDeclarationSyntax)methodNode);
            var newSyntaxRoot = syntaxRoot.ReplaceNode(classSyntax, newClassSyntax);

            // Update the project with the modified syntax tree
            var newDocument = project.Documents.First().WithSyntaxRoot(newSyntaxRoot);
            var newProject = newDocument.Project;

            // Apply the changes to the solution
            var newSolution = newProject.Solution;
            workspace.TryApplyChanges(newSolution);
        }

        public void AddProperty(string classPath, string className, string propertyName, string propertyType)
        {
            string propertyCode = $"public {propertyType} {propertyName} {{ get; set; }}";
            AddMethod(classPath, className, propertyName + "Property", propertyCode); // Reusing AddMethod       
        }


        /// <summary>
        /// Adds a new property to a class file using Roslyn.
        /// </summary>
        /// <param name="solutionPath">The path to the solution file.</param>
        /// <param name="projectName">The name of the target project in the solution.</param>
        /// <param name="className">The name of the class to which the property will be added.</param>
        /// <param name="classPath">The path within the project where the class file is located.</param>
        /// <param name="propertyNode">The syntax node representing the new property.</param>
        public void AddPropertyByRoslyn(
            string solutionPath,
            string projectName,
            string className,
            string classPath,
            PropertyDeclarationSyntax propertyNode)
        {
            // Load the solution
            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(solutionPath).Result;
            var project = solution.Projects.FirstOrDefault(p => p.Name == projectName);

            if (project == null)
            {
                return;
            }

            // Find the class syntax
            var syntaxRoot = project.Documents.First().GetSyntaxRootAsync().Result;
            var classSyntax = syntaxRoot.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault(c => c.Identifier.Text == className);

            if (classSyntax == null)
            {
                return;
            }

            // Add the new property to the class
            var newClassSyntax = classSyntax.AddMembers(propertyNode);
            var newSyntaxRoot = syntaxRoot.ReplaceNode(classSyntax, newClassSyntax);

            // Update the project with the modified syntax tree
            var newDocument = project.Documents.First().WithSyntaxRoot(newSyntaxRoot);
            var newProject = newDocument.Project;

            // Apply the changes to the solution
            var newSolution = newProject.Solution;
            workspace.TryApplyChanges(newSolution);
        }

        /// <summary>
        /// Creates a new class file with the specified name, path, and optional initial content.
        /// </summary>
        /// <param name="className">The name of the class to create.</param>
        /// <param name="classPath">The path where the class file should be created.</param>
        /// <param name="initialContent">Optional initial content of the class file.</param>
        public void CreateClass(string className, string classPath, string initialContent = "")
        {
            // HowToWork
            // This method creates a new class file with the specified name, path, and optional initial content.
            // It uses a StreamWriter to write the class definition and initial content to the file.
            string fullFilePath = Path.Combine(classPath, className + ".cs");

            using (StreamWriter writer = File.CreateText(fullFilePath))
            {
                writer.WriteLine($"public class {className}");
                writer.WriteLine("{");
                writer.WriteLine(initialContent);
                writer.WriteLine("}");
            }
        }

        /// <summary>
        /// Creates a new class file using Roslyn.
        /// </summary>
        /// <param name="solutionPath">The path to the solution file.</param>
        /// <param name="projectName">The name of the target project in the solution.</param>
        /// <param name="className">The name of the new class to create.</param>
        /// <param name="classPath">The path within the project to place the new class file.</param>
        public void CreateClassByRoslyn(string solutionPath, string projectName, string className, string classPath)
        {
            // Load the solution
            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(solutionPath).Result;
            var project = solution.Projects.FirstOrDefault(p => p.Name == projectName);

            if (project == null)
            {
                return;
            }

            // Define the class syntax
            var classSyntax = SyntaxFactory.ClassDeclaration(className)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .NormalizeWhitespace();

            // Add the class to the namespace
            var syntaxRoot = project.Documents.First().GetSyntaxRootAsync().Result;
            var namespaceSyntax = (NamespaceDeclarationSyntax)syntaxRoot.ChildNodes()
                .FirstOrDefault(node => node is NamespaceDeclarationSyntax);
            var newNamespaceSyntax = namespaceSyntax.AddMembers(classSyntax);
            var newSyntaxRoot = syntaxRoot.ReplaceNode(namespaceSyntax, newNamespaceSyntax);

            // Update the project with the modified syntax tree
            var newDocument = project.Documents.First().WithSyntaxRoot(newSyntaxRoot);
            var newProject = newDocument.Project;

            // Apply the changes to the solution
            var newSolution = newProject.Solution;
            workspace.TryApplyChanges(newSolution);
        }

        /// <summary>
        /// Creates a directory at the specified path.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to create.</param>
        public void CreateDirectory(string directoryPath)
        {
            // HowToWork
            // This method attempts to create a directory at the specified path.
            // If successful, the directory is created; otherwise, an exception is caught and the error message is stored in StatusofExcute.
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception ex)
            {
                StatusofExcute = $"Error creating directory: {ex.Message}";
            }
        }


        /// <summary>
        /// Creates a new file at the specified path.
        /// </summary>
        /// <param name="filePath">The path of the file to create.</param>
        public void CreateFile(string filePath)
        {
            // HowToWork
            // This method attempts to create a new file at the specified path.
            // If successful, an empty file is created; otherwise, an exception is caught and the error message is stored in StatusofExcute.
            try
            {
                File.WriteAllText(filePath, string.Empty);
            }
            catch (Exception ex)
            {
                StatusofExcute = $"Error creating file: {ex.Message}";
            }
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="filePath">The path of the file to delete.</param>
        public void DeleteFile(string filePath)
        {
            // HowToWork
            // This method attempts to delete the file at the specified path.
            // If successful, the file is deleted; otherwise, an exception is caught and the error message is stored in StatusofExcute.
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                StatusofExcute = $"Error deleting file: {ex.Message}";
            }
        }


        /// <summary>
        /// Extracts the text between {startmethod} and {endmethod} tags from the input string.
        /// </summary>
        /// <param name="input">The input string containing the method text.</param>
        /// <returns>The extracted method text.</returns>
        public string ExtractMethodText(string input)
        {
            // HowToWork
            // This method extracts the text between {startmethod} and {endmethod} tags from the input string using a regex pattern.
            // It then concatenates all matched text and returns the extracted method text.
            string pattern = @"\{startmethod\}\s*(.*?)\s*\{endmethod\}";
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(input);

            string extractedText = string.Empty;
            foreach (Match match in matches)
            {
                extractedText += match.Groups[1].Value.Trim() + "\n";
            }

            return extractedText;
        }

        public void ExtractTemplate(string templateName, string destinationPath)
        { throw new NotImplementedException(); }


        /// <summary>
        /// Generates the project structure and writes it to a file.
        /// </summary>
        /// // HowToWork
        /// // This method generates the project structure by analyzing the project using a ProjectAnalyzer.
        /// // It then writes the structure to a file by calling WriteProjectStructureToFileToai method.
        /// 
        /// <param name="solutionPath">The path to the solution file.</param>
        /// <param name="projectName">The name of the target project in the solution.</param>
        public void GenerateProjectStructure(string solutionPath, string projectName)
        {
            var analyzer = new ProjectAnalyzer();
            var projectStructure = analyzer.AnalyzeProject(solutionPath, projectName);

            if (projectStructure != null)
            {
                var projectDirectory = Path.GetDirectoryName(solutionPath);
                var outputPath = Path.Combine(projectDirectory, $"{projectName}_structure.txt");

                WriteProjectStructureToFileToai(outputPath, projectName, projectStructure.Namespaces);
            }
            else
            {
                Console.WriteLine($@"Project '{projectName}' not found in solution.");
            }
        }

        /// <summary>
        /// Generates the project structure details and writes them to a file.
        /// </summary>
        ///   // HowToWork
        /// // This method generates the project structure details by analyzing the project using a ProjectAnalyzer.
        /// // It then writes the details to a file by calling WriteProjectStructureToFileDetails method.
        /// 
        /// <param name="solutionPath">The path to the solution file.</param>
        /// <param name="projectName">The name of the target project in the solution.</param>
        /// <param name="outputPath">The path to write the project structure details to.</param>
        public async void GenerateProjectStructureDetails(string solutionPath, string projectName, string outputPath)
        {
            var analyzer = new ProjectAnalyzer();
            var projectStructure = analyzer.AnalyzeProject(solutionPath, projectName);

            if (projectStructure != null)
            {
                var projectDirectory = Path.GetDirectoryName(solutionPath);

                await WriteProjectStructureToFileDetails(projectName, outputPath, projectStructure.Namespaces);
            }
            else
            {
                Console.WriteLine($@"Project '{projectName}' not found in solution.");
            }
        }

        /// <summary>
        /// Gets the name of the current project.
        /// </summary>
        ///  HowToWork
        ///  This method gets the name of the current project by getting the file name without extension of the solution path.

        /// <returns>The name of the current project.</returns>
        public string GetProjectName()
        {
            return Path.GetFileNameWithoutExtension(GetSolutionPath());
        }


        /// <summary>
        /// Gets the path of the current solution.
        /// </summary>
        /// HowToWork
        /// This method gets the path of the current solution by navigating up the directory tree from the current directory.        
        /// <returns>The path of the current solution.</returns>
        public string GetSolutionPath()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }


        /// <summary>
        /// Installs a NuGet package into the specified project.
        /// </summary>
        /// <param name="packageName">The name of the NuGet package to install.</param>
        /// <param name="projectPath">The path to the project file (.csproj) where the package should be installed.</param>
        public void InstallPackage(string packageName, string projectPath)
        {
            // HowToWork
            // This method installs a NuGet package into the specified project using the NuGet package manager console.
            // It starts a new process to run the 'nuget install' command with the specified package name and project path.
            try
            {
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;

                process.Start();

                process.StandardInput
                    .WriteLine(
                        $"nuget install {packageName} -OutputDirectory {Path.GetDirectoryName(projectPath)} -NoCache");
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();

                StatusofExcute = $"Successfully installed package '{packageName}'";
            }
            catch (Exception ex)
            {
                StatusofExcute = $"Error installing package '{packageName}': {ex.Message}";
            }
        }

        public void LoadTemplate(string templatePath) { throw new NotImplementedException(); }

        /// <summary>
        /// Reads the contents of a file.
        /// </summary>
        /// <param name="filePath">The path of the file to read.</param>
        /// <returns>The contents of the file as a string.</returns>
        public string ReadFile(string filePath)
        {
            // HowToWork
            // This method reads the contents of a file at the specified path and returns the contents as a string.
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                StatusofExcute = $"Error reading file: {ex.Message}";
                return null;
            }
        }

        public string SubstitutePlaceholders(string templateString, Dictionary<string, string> placeholders)
        { throw new NotImplementedException(); }


        /// <summary>
        /// Updates the content of an existing class file.
        /// </summary>
        /// <param name="classPath">The path where the class file is located.</param>
        /// <param name="className">The name of the class to update.</param>
        /// <param name="updatedContent">The updated content of the class file.</param>
        public void UpdateClass(string classPath, string className, string updatedContent)
        {
            // HowToWork
            // This method updates the content of an existing class file at the specified path.
            // It overwrites the existing file content with the updated content.
            string fullFilePath = Path.Combine(classPath, className + ".cs");
            File.WriteAllText(fullFilePath, updatedContent);
        }


        /// <summary>
        /// Updates an existing class file using Roslyn.
        /// </summary>
        /// <param name="solutionPath">The path to the solution file.</param>
        /// <param name="projectName">The name of the target project in the solution.</param>
        /// <param name="className">The name of the class to update.</param>
        /// <param name="classPath">The path within the project where the class file is located.</param>
        /// <param name="updatedClass">The updated syntax node representing the class.</param>
        public void UpdateClassByRoslyn(
            string solutionPath,
            string projectName,
            string className,
            string classPath,
            SyntaxNode updatedClass)
        {
            // Load the solution
            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(solutionPath).Result;
            var project = solution.Projects.FirstOrDefault(p => p.Name == projectName);

            if (project == null)
            {
                return;
            }

            // Find the class syntax
            var syntaxRoot = project.Documents.First().GetSyntaxRootAsync().Result;
            var classSyntax = syntaxRoot.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault(c => c.Identifier.Text == className);

            if (classSyntax == null)
            {
                return;
            }

            // Replace the old class syntax with the updated one
            var newSyntaxRoot = syntaxRoot.ReplaceNode(classSyntax, updatedClass);

            // Update the project with the modified syntax tree
            var newDocument = project.Documents.First().WithSyntaxRoot(newSyntaxRoot);
            var newProject = newDocument.Project;

            // Apply the changes to the solution
            var newSolution = newProject.Solution;
            workspace.TryApplyChanges(newSolution);
        }


        /// <summary>
        /// Updates the content of a file at the specified path.
        /// </summary>
        /// <param name="filePath">The path of the file to update.</param>
        /// <param name="newContent">The new content to write to the file.</param>
        public void UpdateFile(string filePath, string newContent)
        {
            // HowToWork
            // This method attempts to update the content of a file at the specified path.
            // If successful, the file is updated with the new content; otherwise, an exception is caught and the error message is stored in StatusofExcute.
            try
            {
                File.WriteAllText(filePath, newContent);
            }
            catch (Exception ex)
            {
                StatusofExcute = $"Error updating file: {ex.Message}";
            }
        }
    }
}