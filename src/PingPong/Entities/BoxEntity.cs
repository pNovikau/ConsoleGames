using System.Drawing;
using Core;
using PingPong.Components;

namespace PingPong.Entities;

public static class BoxEntity
{
    public static void Create(in GameContext gameContext)
    {
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