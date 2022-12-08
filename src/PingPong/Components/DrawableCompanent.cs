using Core.Ecs.Components;

namespace PingPong.Components;

public struct DrawableComponent : IComponent<DrawableComponent>
{
    public char[] Symbols;

    public int Id { get; init; }
}