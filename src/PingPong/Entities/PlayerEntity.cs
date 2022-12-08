using Core;
using PingPong.Components;

namespace PingPong.Entities;

public static class PlayerEntity
{
    public static void Create(in GameContext gameContext)
    {
        var gameWorld = gameContext.GameWorld;
        var player = gameWorld.AddEntity();

        player.AddComponent<PlayerComponent>();

        ref var positionComponent = ref player.AddComponent<PositionComponent>();
        positionComponent.Y = gameContext.Renderer.Height - 2;
        positionComponent.X = gameContext.Renderer.Width / 2;

        ref var drawableComponent = ref player.AddComponent<DrawableComponent>();
        drawableComponent.Symbols = "|============|".ToArray();
        
        ref var sizeComponent = ref player.AddComponent<SizeComponent>();
        sizeComponent.Height = 1;
        sizeComponent.Width = drawableComponent.Symbols.Length;

        ref var moveComponent = ref player.AddComponent<MoveComponent>();
        moveComponent.Dir = MoveComponent.Direction.None;
    }
}