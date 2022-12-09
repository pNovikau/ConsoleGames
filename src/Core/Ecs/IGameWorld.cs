using Core.Ecs.Managers;

namespace Core.Ecs;

public interface IGameWorld
{
    IEntityManager EntityManager { get; }
    ISystemManager SystemManager { get; }
    IComponentManager ComponentManager { get; }

    void Initialize(GameContext context);
    EntityBuilder AddEntity();
    EntityBuilder GetEntity(int id);
}