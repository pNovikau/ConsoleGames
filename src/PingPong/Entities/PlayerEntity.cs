using Core;
using Core.Common;
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
        positionComponent.Point = new Vector2<int>(gameContext.Renderer.Width / 2, gameContext.Renderer.Height - 2);
        
        ref var drawableComponent = ref player.AddComponent<DrawableComponent>();
        drawableComponent.Symbols = "|============|".ToArray();

        ref var boxColliderComponent = ref player.AddComponent<BoxColliderComponent>();
        boxColliderComponent.Rectangle = new Rectangle<int>(positionComponent.Point, new Vector2<int>(drawableComponent.Symbols.Length, 1));

        ref var moveComponent = ref player.AddComponent<MoveComponent>();
        moveComponent.Dir = MoveComponent.Direction.None;
    }
}