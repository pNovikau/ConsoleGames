using System.Collections;
using Core.Ecs.Components;

namespace Core.Ecs.ComponentFilters;

[AttributeUsage(AttributeTargets.Class)]
file class GenerateComponentsFilterAttribute : Attribute {}

[GenerateComponentsFilter]
public partial class ComponentsFilter<TComponent> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent> { }

[GenerateComponentsFilter]
public partial class ComponentsFilter<TComponent, TComponent1> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1> { }

[GenerateComponentsFilter]
public partial class ComponentsFilter<TComponent, TComponent1, TComponent2> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2> { }

[GenerateComponentsFilter]
public partial class ComponentsFilter<TComponent, TComponent1, TComponent2, TComponent3> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
    where TComponent3 : struct, IComponent<TComponent3> { }

[GenerateComponentsFilter]
public partial class ComponentsFilter<TComponent, TComponent1, TComponent2, TComponent3, TComponent4> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
    where TComponent3 : struct, IComponent<TComponent3>
    where TComponent4 : struct, IComponent<TComponent4> { }