//-----------------------------------------------------------------------
// <copyright file="ProjectAnalyzer.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using SoulCenterProject.Modules.Ai.Model;

namespace SoulCenterProject.Modules.Ai.CodeGeneration
{
    public class ProjectAnalyzer
    {
        /// <summary>
        /// Retrieves the namespaces, classes, and methods from a given syntax tree.
        /// </summary>
        /// <param name="root">The root syntax node of the syntax tree.</param>
        /// <param name="semanticModel">The semantic model associated with the syntax tree.</param>
        /// <returns>
        /// An enumerable collection of <see cref="NamespaceStructure"/> objects representing the namespaces in the
        /// syntax tree.
        /// </returns>
        private IEnumerable<NamespaceStructure> GetNamespaces(SyntaxNode root, SemanticModel semanticModel)
        {
            /// <summary>
            /// HowToWork
            /// </summary>
            ///  This method opens a solution, retrieves the specified project,
            /// and analyzes its structure by iterating over its syntax trees to extract namespaces, classes, and methods.
            ///  <summary>

            var namespaces = new List<NamespaceStructure>();
            foreach(var namespaceDeclaration in root.DescendantNodes().OfType<NamespaceDeclarationSyntax>())
            {
                var namespaceName = namespaceDeclaration.Name.ToString();
                var classes = new List<ClassStructure>();
                foreach(var classDeclaration in
                         namespaceDeclaration.DescendantNodes().OfType<ClassDeclarationSyntax>())
                {
                    var className = classDeclaration.Identifier.Text;
                    var methods = new List<MethodStructure>();
                    foreach(var methodDeclaration in classDeclaration.DescendantNodes()
                        .OfType<MethodDeclarationSyntax>())
                    {
                        var methodName = methodDeclaration.Identifier.Text;
                        // Add more details if needed (parameters, return type, etc.)
                        methods.Add(new MethodStructure { Name = methodName });
                    }

                    // Add more details if needed (properties, fields, etc.)
                    classes.Add(new ClassStructure { Name = className, Methods = methods });
                }

                namespaces.Add(new NamespaceStructure { Name = namespaceName, Classes = classes });
            }

            return namespaces;
        }

        /// <summary>
        /// Analyzes a project in a solution and retrieves its structure including namespaces, classes, and methods.
        /// </summary>
        /// <param name="solutionPath">The path to the solution file (.sln).</param>
        /// <param name="projectName">The name of the project to analyze.</param>
        /// <returns>A <see cref="ProjectStructure"/> object representing the project's structure.</returns>
        public ProjectStructure AnalyzeProject(string solutionPath, string projectName)
        {
            /// <summary>
            /// HowToWork
            /// </summary>
            ///  This method opens a solution, retrieves the specified project,
            /// and analyzes its structure by iterating over its syntax trees to extract namespaces, classes, and methods.
            ///  <summary>


            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(solutionPath).Result;
            var project = solution.Projects.FirstOrDefault(p => p.Name == projectName);

            if(project == null)
            {
                return null;
            }

            var compilation = project.GetCompilationAsync().Result;

            var namespaces = new List<NamespaceStructure>();
            foreach(var syntaxTree in compilation.SyntaxTrees)
            {
                var root = syntaxTree.GetRoot();
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                namespaces.AddRange(GetNamespaces(root, semanticModel));
            }

            return new ProjectStructure { Name = projectName, Namespaces = namespaces };
        }
    }
}