using Core;
using Core.Common;
using PingPong.Components;
using PingPong.Resources;

namespace PingPong.Entities;

public static class PlayerEntity
{
    public static void Create(in GameContext gameContext)
    {
        var gameWorld = gameContext.GameWorld;
        var player = gameWorld.AddEntity();
        
        player.AddComponent<PlayerComponent>();
        
        ref var positionComponent = ref player.AddComponent<PositionComponent>();
        positionComponent.Point = new Vector2<float>(gameContext.Renderer.Width / 2, gameContext.Renderer.Height - 11);

        ref var drawableComponent = ref player.AddComponent<DrawableComponent>();
        drawableComponent.Symbols = Sprites.Player.Symbols;

        ref var boxColliderComponent = ref player.AddComponent<BoxColliderComponent>();
        boxColliderComponent.Rectangle = new Rectangle<float>(positionComponent.Point, new Vector2<float>(Sprites.Player.Size.X, Sprites.Player.Size.Y));

        ref var moveComponent = ref player.AddComponent<MoveComponent>();
        moveComponent.Velocity = 4;
    }
}