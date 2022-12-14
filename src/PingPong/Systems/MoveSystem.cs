using Core;
using Core.Ecs.ComponentFilters;
using Core.Ecs.Extensions;
using PingPong.Components;

namespace PingPong.Systems;

public class MoveSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<MoveComponent, PositionComponent> _componentsFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = context.GameWorld.CreateComponentFilter<MoveComponent, PositionComponent>();
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, moveComponentSpan, positionComponentSpan) in _componentsFilter)
        {
            ref var moveComponent = ref moveComponentSpan[0];
            ref var positionComponent = ref positionComponentSpan[0];

            positionComponent.Point.X += moveComponent.Direction.X * moveComponent.Velocity;
            positionComponent.Point.Y += moveComponent.Direction.Y * moveComponent.Velocity;
        }
    }
}