using Core.Ecs.Components;

namespace PingPong.Components;

public struct MoveComponent : IComponent<MoveComponent>
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }

    public Direction Dir;

    public int Id { get; init; }
}