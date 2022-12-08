using System.Diagnostics;
using Core.Ecs.Components;
using Core.Ecs.Managers;
using Core.Ecs.Managers.Events;

namespace Core.Ecs;

public ref struct EntityBuilder
{
    private readonly IEntityManager _entityManager;
    private readonly IComponentManager _componentManager;

    private readonly int _entityId;

    public EntityBuilder(int entityId, IEntityManager entityManager, IComponentManager componentManager)
    {
        Debug.Assert(entityId >= 0);
        Debug.Assert(entityManager != null);
        Debug.Assert(componentManager != null);

        _entityManager = entityManager;
        _componentManager = componentManager;

        _entityId = entityId;
    }

    public ref TComponent AddComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        ref var component = ref _componentManager.CreateComponent<TComponent>();

        ref var entity = ref _entityManager.Get(_entityId);
        entity.Components[IComponent<TComponent>.ComponentType] = component.Id;
        entity.ComponentsTypes.Add(IComponent<TComponent>.ComponentType);

        var args = new EntityComponentEventArgs(_entityId, component.Id, IComponent<TComponent>.ComponentType);
        _entityManager.Events.OnEntityComponentAdded(args);

        return ref component;
    }
}