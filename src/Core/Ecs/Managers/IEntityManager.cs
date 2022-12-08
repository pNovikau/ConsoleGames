using Core.Ecs.Managers.Events;

namespace Core.Ecs.Managers;

public interface IEntityManager
{
    EntityManagerEvents Events { get; }

    ref Entity Create();
    ref Entity Get(int id);
    void Remove(int id);
}