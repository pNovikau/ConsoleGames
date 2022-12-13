using Core;
using Core.Common;
using PingPong.Components;
using PingPong.Resources;
using Math = Core.Common.Math;

namespace PingPong.Entities;

public static class BoxEntities
{
    private static void CreateVerticalWall(in GameContext gameContext, Vector2<float> position, Vector2<float> size)
    {
        var gameWorld = gameContext.GameWorld;
        var wall = gameWorld.AddEntity();
        
        ref var positionComponent = ref wall.AddComponent<PositionComponent>();
        positionComponent.Point = position;

        ref var boxColliderComponent = ref wall.AddComponent<BoxColliderComponent>();
        boxColliderComponent.Rectangle = new Rectangle<float>(positionComponent.Point, size);

        ref var drawableComponent = ref wall.AddComponent<DrawableComponent>();

        var rowLength = Sprites.Brick.IndexOf('\n');
        var requiredLength = (int)(gameContext.Renderer.Height - positionComponent.Point.Y) * rowLength;

        drawableComponent.Symbols = string.Concat(Enumerable.Repeat(Sprites.Brick, Math.DivideRoundingUp(requiredLength, Sprites.Brick.Length)))
            .Substring(0, requiredLength);
    }

    private static void CreateHorizontalWall(in GameContext gameContext, Vector2<float> position, Vector2<float> size)
    {
        var gameWorld = gameContext.GameWorld;
        var wall = gameWorld.AddEntity();
        
        ref var positionComponent = ref wall.AddComponent<PositionComponent>();
        positionComponent.Point = position;

        ref var boxColliderComponent = ref wall.AddComponent<BoxColliderComponent>();
        boxColliderComponent.Rectangle = new Rectangle<float>(position, size);

        ref var drawableComponent = ref wall.AddComponent<DrawableComponent>();
        drawableComponent.Symbols = Sprites.Brick;
    }
    
    public static void Create(in GameContext gameContext)
    {
        var rowLength = Sprites.Brick.IndexOf('\n');

        var leftWallPosition = new Vector2<float>(0, 2);
        var leftWallSize = new Vector2<float>(rowLength, gameContext.Renderer.Height - leftWallPosition.Y);
        CreateVerticalWall(gameContext, leftWallPosition, leftWallSize);

        var rightWallPosition = new Vector2<float>(gameContext.Renderer.Width - rowLength - 1, 2);
        var rightWallSize = new Vector2<float>(rowLength, gameContext.Renderer.Height - rightWallPosition.Y);
        CreateVerticalWall(gameContext, rightWallPosition, rightWallSize);

        //var topWallPosition = new Vector2<float>(1, 2);
        //var topWallSize = new Vector2<float>(gameContext.Renderer.Width - 3, 1);
        //CreateHorizontalWall(gameContext, topWallPosition, topWallSize);
        //
        //var bottomWallPosition = new Vector2<float>(1, gameContext.Renderer.Height - 1);
        //var bottomWallSize = new Vector2<float>(gameContext.Renderer.Width - 3, 1);
        //CreateHorizontalWall(gameContext, bottomWallPosition, bottomWallSize);
    }
}