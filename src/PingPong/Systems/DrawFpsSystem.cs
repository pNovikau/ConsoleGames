using Core;
using Core.Common;
using Core.Ecs.ComponentFilters;
using Core.Ecs.Extensions;
using PingPong.Components;

namespace PingPong.Systems;

public class DrawFpsSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<DrawableComponent, PositionComponent, FpsComponent> _componentsFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = context.GameWorld.CreateComponentFilter<DrawableComponent, PositionComponent, FpsComponent>();
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, drawableComponentSpan, positionComponentSpan, fpsComponentSpan) in _componentsFilter)
        {
            ref var drawableComponent = ref drawableComponentSpan[0];
            ref var positionComponent = ref positionComponentSpan[0];
            ref var fpsComponent = ref fpsComponentSpan[0];

            //fpsComponent.Fps.TryFormat(drawableComponent.Symbols.AsSpan()[("FPS: ".Length)..], out _);
            //
            //context.Renderer.Draw(drawableComponent.Symbols, new Vector2<int>((int)positionComponent.Point.X, (int)positionComponent.Point.Y));
        }
    }
}