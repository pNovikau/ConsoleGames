using Core.Ecs.Components;

namespace PingPong.Components;

public struct FpsComponent : IComponent<FpsComponent>
{
    public int LastTotalFrames;
    public int Fps;

    public int Id { get; init; }
}