//-----------------------------------------------------------------------
// <copyright file="ICodeGenerator.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SoulCenterProject.Modules.Ai.Interfaces
{
    public interface ICodeGenerator
    {
        void AddMethod(string classPath, string className, string methodName, string methodContent);

        void AddMethodByRoslyn(
            string solutionPath,
            string projectName,
            string className,
            string classPath,
            SyntaxNode methodNode);

        void AddProperty(string classPath, string className, string propertyName, string propertyType);

        void AddPropertyByRoslyn(
            string solutionPath,
            string projectName,
            string className,
            string classPath,
            PropertyDeclarationSyntax propertyNode);

        // Class and Method Manipulation (Simple)
        void CreateClass(string className, string classPath, string initialContent = "");

        // Class and Method Manipulation (Roslyn)
        void CreateClassByRoslyn(string solutionPath, string projectName, string className, string classPath);

        // File I/O Helpers
        void CreateDirectory(string directoryPath);

        void CreateFile(string filePath);

        void DeleteFile(string filePath);

        void ExtractTemplate(string templateName, string destinationPath);

        // Potentially More Advanced:
        void InstallPackage(string packageName, string projectPath);

        // Template-Related (If you need these soon)
        void LoadTemplate(string templatePath);

        // Additional Helpers (Optional)
        string ReadFile(string filePath);

        string SubstitutePlaceholders(string templateString, Dictionary<string, string> placeholders);

        void UpdateClass(string classPath, string className, string updatedContent);

        void UpdateClassByRoslyn(
            string solutionPath,
            string projectName,
            string className,
            string classPath,
            SyntaxNode updatedClass);

        void UpdateFile(string filePath, string newContent);
    }
}