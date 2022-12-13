using System.Text;
using Core.SourceGenerators.ComponentFilters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Core.SourceGenerators
{
    [Generator]
    public class FiltersSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxContextReceiver is FilterClassReceiver receiver))
                return;

            var content = FiltersSourceGeneratorContext.FilterBuilder.GetContent();
            context.AddSource("ComponentsFilters.g.cs", SourceText.From(content, Encoding.UTF8));

            content = FiltersSourceGeneratorContext.TuplesBuilder.GetContent();
            context.AddSource("ComponentsTuples.g.cs", SourceText.From(content, Encoding.UTF8));
            
            FiltersSourceGeneratorContext.Clear();
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            FiltersSourceGeneratorContext.Initialize();

            context.RegisterForSyntaxNotifications(() => new FilterClassReceiver());
        }
    }
}