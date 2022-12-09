using Core.Ecs.ComponentFilters;
using Core.Ecs.Components;

namespace Core.Ecs.Extensions;

public static class GameWorldExtensions
{
    public static ComponentsFilter<TComponent> CreateComponentFilter<TComponent>(this IGameWorld world)
        where TComponent : struct, IComponent<TComponent>
    {
        return new ComponentsFilter<TComponent>(world.EntityManager, world.ComponentManager);
    }

    public static ComponentsFilter<TComponent, TComponent1> CreateComponentFilter<TComponent, TComponent1>(this IGameWorld world)
        where TComponent : struct, IComponent<TComponent>
        where TComponent1 : struct, IComponent<TComponent1>
    {
        return new ComponentsFilter<TComponent, TComponent1>(world.EntityManager, world.ComponentManager);
    }

    public static ComponentsFilter<TComponent, TComponent1, TComponent2> CreateComponentFilter<TComponent, TComponent1, TComponent2>(this IGameWorld world)
        where TComponent : struct, IComponent<TComponent>
        where TComponent1 : struct, IComponent<TComponent1>
        where TComponent2 : struct, IComponent<TComponent2>
    {
        return new ComponentsFilter<TComponent, TComponent1, TComponent2>(world.EntityManager, world.ComponentManager);
    }

    public static ComponentsFilter<TComponent, TComponent1, TComponent2, TComponent3> CreateComponentFilter<TComponent, TComponent1, TComponent2, TComponent3>(this IGameWorld world)
        where TComponent : struct, IComponent<TComponent>
        where TComponent1 : struct, IComponent<TComponent1>
        where TComponent2 : struct, IComponent<TComponent2>
        where TComponent3 : struct, IComponent<TComponent3>
    {
        return new ComponentsFilter<TComponent, TComponent1, TComponent2, TComponent3>(world.EntityManager, world.ComponentManager);
    }
}