using System.Text;

namespace Core.SourceGenerators.ComponentFilters
{
    public class ComponentsTuplesContentBuilder
    {
        private readonly StringBuilder _builder;

        public ComponentsTuplesContentBuilder()
        {
            _builder = new StringBuilder(@"
using Core.Ecs.Components;

namespace Core.Ecs.ComponentFilters;
");
        }

        public void Add(string[] templatesParams)
        {
            _builder.AppendFormat(@"
public ref struct ComponentsTuple<{0}>", string.Join(", ", templatesParams));

            foreach (var template in templatesParams)
            {
                _builder.AppendFormat(@"
    where {0} : struct, IComponent<{0}>", template);
            }

            _builder.Append(@"
{
    public int EntityId;");

            for (var index = 0; index < templatesParams.Length; index++)
            {
                var template = templatesParams[index];

                _builder.AppendFormat(@"
    public Span<{0}> Component{1};", template, index);
            }

            _builder.AppendLine();

            _builder.Append(@"
    public ComponentsTuple(
        int entityId,
");

            for (var index = 0; index < templatesParams.Length; index++)
            {
                var template = templatesParams[index];

                _builder.AppendFormat("        Span<{0}> component{1}", template, index);
                
                if (index != templatesParams.Length - 1)
                    _builder.AppendLine(",");
                else
                    _builder.AppendLine(")");
            }

            _builder.Append(@"
    {
        EntityId = entityId;
");
            
            for (var index = 0; index < templatesParams.Length; index++)
            {
                _builder.AppendFormat(@"
        Component{0} = component{0};", index);
            }

            _builder.Append(@"
    }");
            
            _builder.AppendLine();
            
            _builder.Append(@"
    public void Deconstruct(
        out int entityId,
");

            for (var index = 0; index < templatesParams.Length; index++)
            {
                var template = templatesParams[index];

                _builder.AppendFormat("        out Span<{0}> component{1}", template, index);
                
                if (index != templatesParams.Length - 1)
                    _builder.AppendLine(",");
                else
                    _builder.AppendLine(")");
            }

            _builder.Append(@"
    {
        entityId = EntityId;
");
            
            for (var index = 0; index < templatesParams.Length; index++)
            {
                _builder.AppendFormat(@"
        component{0} = Component{0};", index);
            }

            _builder.Append(@"
    }");
            _builder.Append(@"
}
");
        }
        
        public string GetContent()
        {
            return _builder.ToString();
        }
    }
}