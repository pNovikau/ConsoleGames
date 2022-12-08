using Core.Ecs.Components;

namespace PingPong.Components;

public struct PositionComponent : IComponent<PositionComponent>
{
    public int X;
    public int Y;

    public int Id { get; init; }
}