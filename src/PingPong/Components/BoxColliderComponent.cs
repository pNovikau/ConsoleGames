using Core.Common;
using Core.Ecs.Components;

namespace PingPong.Components;

public struct BoxColliderComponent : IComponent<BoxColliderComponent>
{
    public Rectangle<float> Rectangle;

    public int Id { get; init; }
}