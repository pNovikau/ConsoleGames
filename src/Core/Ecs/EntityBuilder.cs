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

    public ref TComponent GetComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        ref var entity = ref _entityManager.Get(_entityId);
        var componentId = entity.Components[IComponent<TComponent>.ComponentType];
        
        return ref _componentManager.GetComponent<TComponent>(componentId);
    }

    public void RemoveComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        ref var entity = ref _entityManager.Get(_entityId);
        var componentId = entity.Components[IComponent<TComponent>.ComponentType];
        entity.ComponentsTypes.Remove(IComponent<TComponent>.ComponentType);

        _componentManager.Delete<TComponent>(componentId);

        var args = new EntityComponentEventArgs(_entityId, componentId, IComponent<TComponent>.ComponentType);
        _entityManager.Events.OnEntityComponentRemoved(args);
    }
    
    public void RemoveComponent<TComponent>(in TComponent component)
        where TComponent : struct, IComponent<TComponent>
    {
        ref var entity = ref _entityManager.Get(_entityId);
        entity.ComponentsTypes.Remove(IComponent<TComponent>.ComponentType);

        _componentManager.Delete<TComponent>(component.Id);

        var args = new EntityComponentEventArgs(_entityId, component.Id, IComponent<TComponent>.ComponentType);
        _entityManager.Events.OnEntityComponentRemoved(args);
    }

    public bool HasComponent<TComponent>()
        where TComponent : struct, IComponent<TComponent>
    {
        ref var entity = ref _entityManager.Get(_entityId);

        return entity.ComponentsTypes.Contains(IComponent<TComponent>.ComponentType);
    }
}