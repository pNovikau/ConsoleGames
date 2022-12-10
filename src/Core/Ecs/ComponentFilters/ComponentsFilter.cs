using System.Diagnostics;
using Core.Common.Collections;
using Core.Ecs.Managers;
using Core.Ecs.Managers.Events;

namespace Core.Ecs.ComponentFilters;

public abstract class ComponentsFilter : IComponentFilter
{
    private readonly int[] _componentTypes;

    private readonly Queue<int> _addedEntityQueue;
    private readonly Queue<int> _deletedEntityQueue;

    protected readonly FastList<int> EntitiesIds = new(255);
    protected readonly IEntityManager EntityManager;
    protected readonly IComponentManager ComponentManager;

    private bool _isBusy;

    protected bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (value == false)
            {
                while (_deletedEntityQueue.TryDequeue(out var entityId))
                    EntitiesIds.Remove(entityId);

                while (_addedEntityQueue.TryDequeue(out var entityId))
                    EntitiesIds.Add(entityId);
            }

            _isBusy = value;
        }
    }

    protected ComponentsFilter(IEntityManager entityManager, IComponentManager componentManager, params int[] componentTypes)
    {
        Debug.Assert(entityManager != null);
        Debug.Assert(componentManager != null);

        entityManager.Events.EntityAdded += OnEntityAdded;
        entityManager.Events.EntityRemoved += OnEntityRemoved;
        entityManager.Events.EntityComponentAdded += OnEntityComponentAdded;
        entityManager.Events.EntityComponentRemoved += OnEntityComponentRemoved;

        _componentTypes = componentTypes;

        _addedEntityQueue = new Queue<int>();
        _deletedEntityQueue = new Queue<int>();

        EntityManager = entityManager;
        ComponentManager = componentManager;
    }

    protected virtual bool OnEntityAdded(ref EntityEventArgs args)
    {
        if (EntitiesIds.Contains(args.EntityId) ||
            IsEntityContainsAllComponents(args.EntityId))
        {
            if (IsBusy)
                _addedEntityQueue.Enqueue(args.EntityId);
            else
                EntitiesIds.Add(args.EntityId);

            return true;
        }

        return false;
    }

    protected virtual bool OnEntityRemoved(ref EntityEventArgs args)
    {
        if (IsEntityContainsAllComponents(args.EntityId))
        {
            if (IsBusy)
                _deletedEntityQueue.Enqueue(args.EntityId);
            else
                EntitiesIds.Remove(args.EntityId);

            return true;
        }

        return false;
    }

    protected virtual bool OnEntityComponentAdded(ref EntityComponentEventArgs args)
    {
        if (EntitiesIds.Contains(args.EntityId) ||
            !IsEntityContainsAllComponents(args.EntityId))
            return false;

        if (IsBusy)
            _addedEntityQueue.Enqueue(args.EntityId);
        else
            EntitiesIds.Add(args.EntityId);

        return true;
    }

    protected virtual bool OnEntityComponentRemoved(ref EntityComponentEventArgs args)
    {
        if (Array.IndexOf(_componentTypes, args.ComponentType) == -1)
            return false;

        if (IsBusy)
            _deletedEntityQueue.Enqueue(args.EntityId);
        else
            EntitiesIds.Remove(args.EntityId);

        return true;
    }

    private bool IsEntityContainsAllComponents(int entityId)
    {
        ref var entity = ref EntityManager.Get(entityId);
        var counter = 0;

        for (var i = 0; i < entity.ComponentsTypes.Count; i++)
        {
            if (Array.IndexOf(_componentTypes, entity.ComponentsTypes[i]) != -1)
                ++counter;
        }

        return counter == _componentTypes.Length;
    }

    public void Dispose()
    {
        EntityManager.Events.EntityAdded -= OnEntityAdded;
        EntityManager.Events.EntityRemoved -= OnEntityRemoved;
        EntityManager.Events.EntityComponentAdded -= OnEntityComponentAdded;
        EntityManager.Events.EntityComponentRemoved -= OnEntityComponentRemoved;
    }
}