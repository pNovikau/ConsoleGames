using Core.Common.Collections;
using Core.Ecs.Managers.Events;

namespace Core.Ecs.Managers;

public class EntityManager : IEntityManager
{
    private readonly IUnorderedList<Entity> _list;

    public EntityManager()
    {
        _list = new UnorderedList<Entity>();
    }

    public EntityManagerEvents Events { get; } = new();

    public ref Entity Create()
    {
        ref var entity = ref _list.Get();

        var eventArgs = new EntityEventArgs(entity.Id);
        Events.OnEntityAdded(eventArgs);

        return ref entity;
    }

    public ref Entity Get(int id) => ref _list.Items[id];

    public void Remove(int id)
    {
        _list.Remove(id);

        var eventArgs = new EntityEventArgs(id);
        Events.OnEntityRemoved(eventArgs);
    }
}