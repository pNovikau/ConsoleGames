﻿using System.Text;

namespace Core.SourceGenerators.ComponentFilters
{
    public class ComponentFiltersContentBuilder
    {
        private readonly StringBuilder _builder;

        public ComponentFiltersContentBuilder()
        {
            _builder = new StringBuilder(@"// <auto-generated/>
using System.Collections;
using Core.Ecs.Components;
using Core.Ecs.Managers;

namespace Core.Ecs.ComponentFilters;");
        }

        public void Add(string[] templatesParams)
        {
            _builder.Append(@"
public partial class ComponentsFilter<");

            _builder.Append(string.Join(",", templatesParams));

            _builder.AppendLine(@"> : ComponentsFilter, IEnumerable");

            _builder.Append(@"
{
    public ComponentsFilter(IEntityManager entityManager, IComponentManager componentManager)
        : base(entityManager, componentManager,");

            for (var i = 0; i < templatesParams.Length; i++)
            {
                var template = templatesParams[i];
                
                _builder.AppendFormat(@"
            IComponent<{0}>.ComponentType", template);

                if (i != templatesParams.Length - 1)
                    _builder.Append(", ");
                else
                    _builder.AppendLine(") { }");
            }

            _builder.Append(@"

    public Enumerator GetEnumerator() => new(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator, IDisposable
    {
        private readonly ComponentsFilter<");
            
            _builder.Append(string.Join(", ", templatesParams));

            _builder.Append(@"> _componentFilter;

        private int _index = -1;

        public Enumerator(ComponentsFilter<");
            
            _builder.Append(string.Join(", ", templatesParams));

            _builder.Append(@"> componentFilter) : this()
        {
            _componentFilter = componentFilter;

            _componentFilter.IsBusy = true;
        }

        public bool MoveNext()
        {
            if (_index == _componentFilter.EntitiesIds.Count - 1)
                return false;

            _index++;
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public ComponentsTuple<");
            
            _builder.Append(string.Join(", ", templatesParams));
            _builder.Append(@"> Current
        {
            get
            {
                ref var entity = ref _componentFilter.EntityManager.Get(_componentFilter.EntitiesIds[_index]);
");

            for (var i = 0; i < templatesParams.Length; i++)
            {
                var template = templatesParams[i];

                _builder.AppendFormat(@"
                var componentId{0} = entity.Components[IComponent<{1}>.ComponentType];", i, template);
                _builder.AppendFormat(@"
                var component{0} = _componentFilter.ComponentManager.GetComponentAsSpan<{1}>(componentId{0});", i, template);

                _builder.AppendLine();
            }

            _builder.AppendFormat(@"
                return new ComponentsTuple<{0}>(_componentFilter.EntitiesIds[_index], ", string.Join(",", templatesParams));

            for (var i = 0; i < templatesParams.Length; i++)
            {
                _builder.Append("component");
                _builder.Append(i);
                
                if (i != templatesParams.Length - 1)
                    _builder.Append(", ");
                else
                    _builder.AppendLine(");");
            }

            _builder.Append(@"
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            Reset();
            _componentFilter.IsBusy = false;
        }
    }
}

");
        }
        
        public void Clear()
        {
            _builder.Clear();
        }

        public string GetContent()
        {
            return _builder.ToString();
        }
    }
}