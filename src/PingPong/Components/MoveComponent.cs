using Core.Common;
using Core.Ecs.Components;

namespace PingPong.Components;

public struct MoveComponent : IComponent<MoveComponent>
{
    public Vector2<float> Direction;
    public float Velocity;

    public int Id { get; init; }
}