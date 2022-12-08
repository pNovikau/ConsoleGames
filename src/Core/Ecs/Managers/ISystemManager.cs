using Core.Ecs.Systems;

namespace Core.Ecs.Managers;

public interface ISystemManager
{
    IReadOnlyList<ISystem> Systems { get; }

    void RegisterSystem<TSystem>(GameContext gameContext) where TSystem : class, ISystem, new();
}