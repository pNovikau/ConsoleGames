using Core.Ecs;
using PingPong.Components;

namespace PingPong.Entities;

public static class FpsEntity
{
    public static void Create(IGameWorld gameWorld)
    {
        var fps = gameWorld.AddEntity();

        fps.AddComponent<PositionComponent>();
        fps.AddComponent<FpsComponent>();

        ref var drawableComponent = ref fps.AddComponent<DrawableComponent>();
        drawableComponent.Symbols = new char[125];
        "FPS: ".ToArray().CopyTo(drawableComponent.Symbols.AsMemory());
    }
}