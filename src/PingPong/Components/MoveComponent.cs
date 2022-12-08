using Core.Ecs.Components;

namespace PingPong.Components;

public struct MoveComponent : IComponent<MoveComponent>
{
    public enum Direction
    {
        None,
        Up,
        Left,
        Right,
        Down
    }

    public Direction Dir;

    public int Id { get; init; }
}