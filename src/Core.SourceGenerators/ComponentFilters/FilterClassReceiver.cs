using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.SourceGenerators.ComponentFilters
{
    public class FilterClassReceiver : ISyntaxContextReceiver
    {
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is ClassDeclarationSyntax classDeclarationSyntax &&
                context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax) is INamedTypeSymbol classSymbol &&
                classSymbol.Name.Equals("ComponentsFilter") &&
                classSymbol.IsGenericType)
            {
                FiltersSourceGeneratorContext.FilterBuilder.Add(classSymbol.TypeArguments.Select(p => p.ToDisplayString()).ToArray());
                FiltersSourceGeneratorContext.TuplesBuilder.Add(classSymbol.TypeArguments.Select(p => p.ToDisplayString()).ToArray());
            }
        }
    }
}