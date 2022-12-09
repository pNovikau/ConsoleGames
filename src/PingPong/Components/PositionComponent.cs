using Core.Common;
using Core.Ecs.Components;

namespace PingPong.Components;

public struct PositionComponent : IComponent<PositionComponent>
{
    public Vector2<int> Point;

    public int Id { get; init; }
}