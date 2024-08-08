using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System.Collections.Generic;
using System.Linq;

public class ProjectStructureAnalyzer
{
    public ProjectStructure AnalyzeProject(string projectPath)
    {
        // 1. Create a workspace and open the project
        var workspace = MSBuildWorkspace.Create();
        var project = workspace.OpenProjectAsync(projectPath).Result;

        // 2. Get the compilation for the project
        var compilation = project.GetCompilationAsync().Result;

        // 3. Extract namespaces, classes, and methods
        var namespaces = new List<NamespaceStructure>();
        foreach (var syntaxTree in compilation.SyntaxTrees)
        {
            // Correctly cast the root to CompilationUnitSyntax
            var root = syntaxTree.GetRoot() as CompilationUnitSyntax;

            if (root != null) // Ensure the cast was successful
            {
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                namespaces.AddRange(GetNamespaces(root, semanticModel));
            }
        }

        // 4. Create and return the ProjectStructure object
        return new ProjectStructure { Name = project.Name, Namespaces = namespaces };
    }

    // Helper method to extract namespaces, classes, and methods from syntax tree
    private List<NamespaceStructure> GetNamespaces(CompilationUnitSyntax root, SemanticModel semanticModel)
    {
        var namespaces = new List<NamespaceStructure>();

        foreach (var namespaceDeclaration in root.DescendantNodes().OfType<NamespaceDeclarationSyntax>())
        {
            var namespaceName = namespaceDeclaration.Name.ToString();
            var namespaceStructure = new NamespaceStructure { Name = namespaceName, Classes = new List<ClassStructure>() };

            foreach (var classDeclaration in namespaceDeclaration.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                var className = classDeclaration.Identifier.ToString();
                var classStructure = new ClassStructure { Name = className, Methods = new List<MethodStructure>() };

                foreach (var methodDeclaration in classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>())
                {
                    var methodName = methodDeclaration.Identifier.ToString();
                    var methodStructure = new MethodStructure { Name = methodName };

                    // Add additional method details if needed (e.g., return type, parameters)
                    // using the SemanticModel
                    // ...

                    classStructure.Methods.Add(methodStructure);
                }

                namespaceStructure.Classes.Add(classStructure);
            }

            namespaces.Add(namespaceStructure);
        }

        return namespaces;
    }
}

// Data structures for namespace, class, and method information
public class ProjectStructure
{
    public string Name { get; set; }
    public List<NamespaceStructure> Namespaces { get; set; }
}

public class NamespaceStructure
{
    public string Name { get; set; }
    public List<ClassStructure> Classes { get; set; }
}

public class ClassStructure
{
    public string Name { get; set; }
    public List<MethodStructure> Methods { get; set; }
}

public class MethodStructure
{
    public string Name { get; set; }
    // Add additional properties as needed (return type, parameters, etc.)
}