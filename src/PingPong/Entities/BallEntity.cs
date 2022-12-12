using Core;
using Core.Common;
using PingPong.Components;

namespace PingPong.Entities;

public static class BallEntity
{
    public static void Create(in GameContext gameContext)
    {
        var gameWorld = gameContext.GameWorld;
        var ball = gameWorld.AddEntity();

        ref var positionComponent = ref ball.AddComponent<PositionComponent>();
        positionComponent.Point = new Vector2<int>(gameContext.Renderer.Width / 2, gameContext.Renderer.Height / 2);
        
        ref var moveComponent = ref ball.AddComponent<MoveComponent>();
        moveComponent.Dir = (MoveComponent.Direction)new Random().Next(0, 8);

        ref var boxColliderComponent = ref ball.AddComponent<BoxColliderComponent>();
        boxColliderComponent.Rectangle = new Rectangle<int>(positionComponent.Point, new Vector2<int>(1, 1));

        ref var drawComponent = ref ball.AddComponent<DrawableComponent>();
        drawComponent.Symbols = new[] { '@' };
    }
}