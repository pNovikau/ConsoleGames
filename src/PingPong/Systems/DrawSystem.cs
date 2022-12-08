using Core;
using Core.Ecs.ComponentFilters;
using PingPong.Components;

namespace PingPong.Systems;

public class DrawSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<DrawableComponent, PositionComponent> _componentsFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = new ComponentsFilter<DrawableComponent, PositionComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, drawableComponentSpan, positionComponentSpan) in _componentsFilter)
        {
            ref var drawableComponent = ref drawableComponentSpan[0];
            ref var positionComponent = ref positionComponentSpan[0];

            context.Renderer.Draw(drawableComponent.Symbols, positionComponent.X, positionComponent.Y);
        }
    }
}