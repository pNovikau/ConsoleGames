using Core;
using Core.Common;
using PingPong.Components;
using PingPong.Resources;

namespace PingPong.Entities;

public static class BallEntity
{
    public static void Create(in GameContext gameContext)
    {
        var random = new Random();
        
        var gameWorld = gameContext.GameWorld;
        var ball = gameWorld.AddEntity();
        
        ref var positionComponent = ref ball.AddComponent<PositionComponent>();
        positionComponent.Point = new Vector2<float>(gameContext.Renderer.Width / 2, gameContext.Renderer.Height / 2);
        
        ref var moveComponent = ref ball.AddComponent<MoveComponent>();
        //moveComponent.Direction = new Vector2<float>(random.NextSingle() * (1f - -1f) + -1f, random.NextSingle() * (1f - -1f) + -1f);
        //moveComponent.Velocity = 5;
        
        ref var boxColliderComponent = ref ball.AddComponent<BoxColliderComponent>();
        boxColliderComponent.Rectangle = new Rectangle<float>(positionComponent.Point, new Vector2<float>(35, 17));
        
        ref var drawComponent = ref ball.AddComponent<DrawableComponent>();
        drawComponent.Symbols = Sprites.Ball.Symbols;
    }
}