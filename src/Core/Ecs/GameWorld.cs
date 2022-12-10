using System.Diagnostics;
using Core.Ecs.Managers;
using Core.Ecs.Systems;

namespace Core.Ecs;

public class GameWorld : IGameWorld
{
    public GameWorld()
    {
        EntityManager = new EntityManager();
        SystemManager = new SystemManager();
        ComponentManager = new ComponentManager();
    }

    public IEntityManager EntityManager { get; }
    public ISystemManager SystemManager { get; }
    public IComponentManager ComponentManager { get; }

    public void Initialize(GameContext context)
    {
        foreach (var system in SystemManager.Systems) 
            system.Initialize(context);
    }

    public EntityBuilder AddEntity()
    {
        ref var entity = ref EntityManager.Create();

        return new EntityBuilder(entity.Id, EntityManager, ComponentManager);
    }

    public EntityBuilder GetEntity(int id)
    {
        return new EntityBuilder(id, EntityManager, ComponentManager);
    }

    public void Update(GameContext context)
    {
        foreach (var system in SystemManager.Systems)
            system.Update(context);
    }

    public void RegisterSystem<TSystem>(GameContext context)
        where TSystem : class, ISystem, new()
    {
        SystemManager.RegisterSystem<TSystem>(context);
    }
}