using Core.Ecs.Components;

namespace PingPong.Components;

public struct SizeComponent : IComponent<SizeComponent>
{
    public int Height;
    public int Width;

    public int Id { get; init; }
}