using Core.Ecs.Managers;
using Core.Ecs.Systems;

namespace Core.Ecs;

public interface IGameWorld
{
    internal IEntityManager EntityManager { get; }
    internal ISystemManager SystemManager { get; }
    internal IComponentManager ComponentManager { get; }

    void Initialize(GameContext context);

    EntityBuilder AddEntity();
    EntityBuilder GetEntity(int id);

    void Update(GameContext context);

    void RegisterSystem<TSystem>(GameContext context)
        where TSystem : class, ISystem, new();
}