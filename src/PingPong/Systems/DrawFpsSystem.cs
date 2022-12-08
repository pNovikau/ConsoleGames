using Core;
using Core.Ecs.ComponentFilters;
using PingPong.Components;

namespace PingPong.Systems;

public class DrawFpsSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<DrawableComponent, PositionComponent, FpsComponent> _componentsFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = new ComponentsFilter<DrawableComponent, PositionComponent, FpsComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, drawableComponentSpan, positionComponentSpan, fpsComponentSpan) in _componentsFilter)
        {
            ref var drawableComponent = ref drawableComponentSpan[0];
            ref var positionComponent = ref positionComponentSpan[0];
            ref var fpsComponent = ref fpsComponentSpan[0];

            fpsComponent.Fps.TryFormat(drawableComponent.Symbols.AsSpan()[("FPS: ".Length)..], out _);

            context.Renderer.Draw(drawableComponent.Symbols, positionComponent.X, positionComponent.Y);
        }
    }
}

public class CollisionDetectionSystem : Core.Ecs.Systems.System
{
    public override void Update(GameContext context)
    {
        throw new NotImplementedException();
    }
}