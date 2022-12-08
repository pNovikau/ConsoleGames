using Core.Ecs.Components;

namespace PingPong.Components;

public struct PlayerComponent : IComponent<PlayerComponent>
{
    public int Id { get; init; }
}