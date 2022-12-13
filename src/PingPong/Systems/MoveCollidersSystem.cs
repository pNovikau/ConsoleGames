using Core;
using Core.Ecs.ComponentFilters;
using Core.Ecs.Extensions;
using PingPong.Components;

namespace PingPong.Systems;

public class MoveCollidersSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<MoveComponent, BoxColliderComponent> _boxColliderFilter = default!;

    public override void Initialize(GameContext context)
    {
        _boxColliderFilter = context.GameWorld.CreateComponentFilter<MoveComponent, BoxColliderComponent>();
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, moveComponentSpan, boxColliderComponentSpan) in _boxColliderFilter)
        {
            ref var moveComponent = ref moveComponentSpan[0];
            ref var boxColliderComponent = ref boxColliderComponentSpan[0];
            
            boxColliderComponent.Rectangle.Position.X += moveComponent.Direction.X * moveComponent.Velocity;
            boxColliderComponent.Rectangle.Position.Y += moveComponent.Direction.Y * moveComponent.Velocity;
        }
    }
}