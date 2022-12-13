using Core.Common;
using Core.Ecs.Components;

namespace PingPong.Components;

public struct HasCollisionComponent : IComponent<HasCollisionComponent>
{
    public Rectangle<float> Overlap;
    public int TargetEntityId;

    public int Id { get; init; }
}