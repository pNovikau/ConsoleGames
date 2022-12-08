using Core.Ecs.Components;

namespace Core.Ecs.ComponentFilters;

public ref struct ComponentsTuple<TComponent, TComponent1>
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
{
    public int EntityId;
    public Span<TComponent> Component;
    public Span<TComponent1> Component1;

    public ComponentsTuple(int entityId, Span<TComponent> component, Span<TComponent1> component1)
    {
        EntityId = entityId;
        Component = component;
        Component1 = component1;
    }

    public void Deconstruct(out int entityId, out Span<TComponent> component, out Span<TComponent1> component1)
    {
        entityId = EntityId;
        component = Component;
        component1 = Component1;
    }
}

public ref struct ComponentsTuple<TComponent, TComponent1, TComponent2>
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
{
    public int EntityId;
    public Span<TComponent> Component;
    public Span<TComponent1> Component1;
    public Span<TComponent2> Component2;

    public ComponentsTuple(
        int entityId,
        Span<TComponent> component, 
        Span<TComponent1> component1,
        Span<TComponent2> component2)
    {
        EntityId = entityId;
        Component = component;
        Component1 = component1;
        Component2 = component2;
    }

    public void Deconstruct(
        out int entityId,
        out Span<TComponent> component, 
        out Span<TComponent1> component1,
        out Span<TComponent2> component2)
    {
        entityId = EntityId;
        component = Component;
        component1 = Component1;
        component2 = Component2;
    }
}

public ref struct ComponentsTuple<TComponent, TComponent1, TComponent2, TComponent3>
    where TComponent : struct, IComponent<TComponent>
    where TComponent1 : struct, IComponent<TComponent1>
    where TComponent2 : struct, IComponent<TComponent2>
    where TComponent3 : struct, IComponent<TComponent3>
{
    public int EntityId;
    public Span<TComponent> Component;
    public Span<TComponent1> Component1;
    public Span<TComponent2> Component2;
    public Span<TComponent3> Component3;

    public ComponentsTuple(
        int entityId,
        Span<TComponent> component, 
        Span<TComponent1> component1,
        Span<TComponent2> component2,
        Span<TComponent3> component3)
    {
        EntityId = entityId;
        Component = component;
        Component1 = component1;
        Component2 = component2;
        Component3 = component3;
    }

    public void Deconstruct(
        out int entityId,
        out Span<TComponent> component, 
        out Span<TComponent1> component1,
        out Span<TComponent2> component2,
        out Span<TComponent3> component3)
    {
        entityId = EntityId; 
        component = Component;
        component1 = Component1;
        component2 = Component2;
        component3 = Component3;
    }
}