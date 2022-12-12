using Core;
using Core.Common;
using PingPong.Components;

namespace PingPong.Entities;

public static class BoxEntities
{
    private static void CreateVerticalWall(in GameContext gameContext, Vector2<int> position, Vector2<int> size)
    {
        var gameWorld = gameContext.GameWorld;
        var wall = gameWorld.AddEntity();
        
        ref var positionComponent = ref wall.AddComponent<PositionComponent>();
        positionComponent.Point = position;

        ref var boxColliderComponent = ref wall.AddComponent<BoxColliderComponent>();
        boxColliderComponent.Rectangle = new Rectangle<int>(positionComponent.Point, size);

        ref var drawableComponent = ref wall.AddComponent<DrawableComponent>();
        var array = new char[size.Y * 2];

        for (var i = 0; i < array.Length; i++)
        {
            if (i % 2 == 0)
                array[i] = '*';
            else
                array[i] = '\n';
        }

        drawableComponent.Symbols = array;
    }

    private static void CreateHorizontalWall(in GameContext gameContext, Vector2<int> position, Vector2<int> size)
    {
        var gameWorld = gameContext.GameWorld;
        var wall = gameWorld.AddEntity();
        
        ref var positionComponent = ref wall.AddComponent<PositionComponent>();
        positionComponent.Point = position;

        ref var boxColliderComponent = ref wall.AddComponent<BoxColliderComponent>();
        boxColliderComponent.Rectangle = new Rectangle<int>(position, size);

        ref var drawableComponent = ref wall.AddComponent<DrawableComponent>();
        var array = new char[size.X + 1];
        array.AsSpan().Fill('*');
        array[^1] = '\n';

        drawableComponent.Symbols = array;
    }
    
    public static void Create(in GameContext gameContext)
    {
        var leftWallPosition = new Vector2<int>(0, 2);
        var leftWallSize = new Vector2<int>(1, gameContext.Renderer.Height - leftWallPosition.Y);
        CreateVerticalWall(gameContext, leftWallPosition, leftWallSize);

        var rightWallPosition = new Vector2<int>(gameContext.Renderer.Width - 2, 2);
        var rightWallSize = new Vector2<int>(1, gameContext.Renderer.Height - rightWallPosition.Y);
        CreateVerticalWall(gameContext, rightWallPosition, rightWallSize);

        var topWallPosition = new Vector2<int>(1, 2);
        var topWallSize = new Vector2<int>(gameContext.Renderer.Width - 3, 1);
        CreateHorizontalWall(gameContext, topWallPosition, topWallSize);

        var bottomWallPosition = new Vector2<int>(1, gameContext.Renderer.Height - 1);
        var bottomWallSize = new Vector2<int>(gameContext.Renderer.Width - 3, 1);
        CreateHorizontalWall(gameContext, bottomWallPosition, bottomWallSize);
    }
}