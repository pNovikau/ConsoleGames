using Core.Common;
using Core.Ecs.Components;

namespace PingPong.Components;

public struct DrawableComponent : IComponent<DrawableComponent>
{
    public string Symbols;
    public Vector2<int> Size;

    public int Id { get; init; }
}