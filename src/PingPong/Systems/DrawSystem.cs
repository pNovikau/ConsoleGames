using Core;
using Core.Common;
using Core.Ecs.ComponentFilters;
using Core.Ecs.Extensions;
using PingPong.Components;

namespace PingPong.Systems;

public class DrawSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<DrawableComponent, PositionComponent> _componentsFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = context.GameWorld.CreateComponentFilter<DrawableComponent, PositionComponent>();
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, drawableComponentSpan, positionComponentSpan) in _componentsFilter)
        {
            ref var drawableComponent = ref drawableComponentSpan[0];
            ref var positionComponent = ref positionComponentSpan[0];

            context.Renderer.Draw(drawableComponent.Symbols, new Vector2<int>((int)positionComponent.Point.X, (int)positionComponent.Point.Y));
        }
    }
}