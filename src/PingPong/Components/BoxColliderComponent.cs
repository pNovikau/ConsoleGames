using System.Drawing;
using Core.Ecs.Components;

namespace PingPong.Components;

public struct BoxColliderComponent : IComponent<BoxColliderComponent>
{
    public Point Point1;
    public Point Point2;

    public int Id { get; init; }
}