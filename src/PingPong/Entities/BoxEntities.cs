using Core;
using Core.Common;
using PingPong.Components;

namespace PingPong.Entities;

public static class BoxEntities
{
    private static void CreateLeftWall(in GameContext gameContext)
    {
        var gameWorld = gameContext.GameWorld;
        var wall = gameWorld.AddEntity();
        
        ref var positionComponent = ref wall.AddComponent<PositionComponent>();
        positionComponent.Point = new Vector2<int>(0, 2);

        ref var boxColliderComponent = ref wall.AddComponent<BoxColliderComponent>();
        var size = new Vector2<int>
        {
            X = 1,
            Y = gameContext.Renderer.Height - positionComponent.Point.Y
        };
        boxColliderComponent.Rectangle = new Rectangle<int>(positionComponent.Point, size);

        ref var drawableComponent = ref wall.AddComponent<DrawableComponent>();
        var array = new char[gameContext.Renderer.Width * size.Y];
        for (var i = 0; i < array.Length; i++)
        {
            if (i % gameContext.Renderer.Width == 0)
                array[i] = '*';
        }

        drawableComponent.Symbols = array;
    }
    
    private static void CreateRightWall(in GameContext gameContext)
    {
        var gameWorld = gameContext.GameWorld;
        var wall = gameWorld.AddEntity();
        
        ref var positionComponent = ref wall.AddComponent<PositionComponent>();
        positionComponent.Point = new Vector2<int>(0, 2);

        ref var boxColliderComponent = ref wall.AddComponent<BoxColliderComponent>();
        var size = new Vector2<int>
        {
            X = 1,
            Y = gameContext.Renderer.Height - positionComponent.Point.Y
        };
        boxColliderComponent.Rectangle = new Rectangle<int>(new Vector2<int>(gameContext.Renderer.Width - 1, 2), size);

        ref var drawableComponent = ref wall.AddComponent<DrawableComponent>();
        var array = new char[gameContext.Renderer.Width * size.Y];
        for (var i = 0; i < array.Length; i++)
        {
            if ((i + 1) % gameContext.Renderer.Width == 0)
                array[i] = '*';
        }

        drawableComponent.Symbols = array;
    }
    
    public static void Create(in GameContext gameContext)
    {
        CreateLeftWall(gameContext);
        CreateRightWall(gameContext);

        //var gameWorld = gameContext.GameWorld;
        //var wall = gameWorld.AddEntity();
        //
        //ref var positionComponent = ref wall.AddComponent<PositionComponent>();
        //positionComponent.X = 0;
        //positionComponent.Y = 2;
        //
        //ref var sizeComponent = ref wall.AddComponent<SizeComponent>();
        //sizeComponent.Height = gameContext.Renderer.Height - positionComponent.Y;
        //sizeComponent.Width = gameContext.Renderer.Width - positionComponent.X;
        //
        //ref var drawableComponent = ref wall.AddComponent<DrawableComponent>();
        //
        //var array = new char[sizeComponent.Height * sizeComponent.Width];
        //
        //array.AsSpan()[..sizeComponent.Width].Fill('*');
        //array.AsSpan()[^sizeComponent.Width..].Fill('*');
        //
        //for (var i = 0; i < array.Length; i++)
        //{
        //    if (i % sizeComponent.Width == 0 ||
        //        (i + 1) % sizeComponent.Width == 0)
        //        array[i] = '*';
        //}
        //
        //drawableComponent.Symbols = array;
        //
        //ref var leftBorder = ref wall.AddComponent<BoxColliderComponent>();
        //leftBorder.Point1 = new Point(positionComponent.X, positionComponent.Y);
        //leftBorder.Point2 = new Point(positionComponent.X, positionComponent.Y + sizeComponent.Height);
        //
        //ref var rightBorder = ref wall.AddComponent<BoxColliderComponent>();
        //rightBorder.Point1 = new Point(positionComponent.X + sizeComponent.Width, positionComponent.Y);
        //rightBorder.Point2 = new Point(positionComponent.X + sizeComponent.Width, positionComponent.Y + sizeComponent.Height);
        //
        //ref var topBorder = ref wall.AddComponent<BoxColliderComponent>();
        //topBorder.Point1 = new Point(positionComponent.X + 1, positionComponent.Y);
        //topBorder.Point2 = new Point(positionComponent.X + sizeComponent.Width - 1, positionComponent.Y);
        //
        //ref var bottomBorder = ref wall.AddComponent<BoxColliderComponent>();
        //bottomBorder.Point1 = new Point(positionComponent.X + 1, positionComponent.Y + sizeComponent.Height);
        //bottomBorder.Point2 = new Point(positionComponent.X + sizeComponent.Width - 1, positionComponent.Y + sizeComponent.Height);
    }
}