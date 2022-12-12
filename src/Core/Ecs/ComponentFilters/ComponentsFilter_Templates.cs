using System.Collections;
using Core.Ecs.Components;

namespace Core.Ecs.ComponentFilters;

public partial class ComponentsFilter<TComponent> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent> { }

public partial class ComponentsFilter<TComponent, TComponent1> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1> { }

public partial class ComponentsFilter<TComponent, TComponent1, TComponent2> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2> { }

public partial class ComponentsFilter<TComponent, TComponent1, TComponent2, TComponent3> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
    where TComponent3 : struct, IComponent<TComponent3> { }
    
public partial class ComponentsFilter<TComponent, TComponent1, TComponent2, TComponent3, TComponent4> : ComponentsFilter, IEnumerable
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
    where TComponent3 : struct, IComponent<TComponent3>
    where TComponent4 : struct, IComponent<TComponent4> { }