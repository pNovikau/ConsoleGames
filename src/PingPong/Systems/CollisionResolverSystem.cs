using System.Diagnostics;
using Core;
using Core.Ecs.ComponentFilters;
using Core.Ecs.Extensions;
using PingPong.Components;

namespace PingPong.Systems;

public class CollisionResolverSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<HasCollisionComponent, BoxColliderComponent, MoveComponent> _componentsFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = context.GameWorld.CreateComponentFilter<HasCollisionComponent, BoxColliderComponent, MoveComponent>();
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, _, boxColliderComponentSpan, moveComponentSpan) in _componentsFilter)
        {
            ref var boxColliderComponent = ref boxColliderComponentSpan[0];
            ref var moveComponent = ref moveComponentSpan[0];

            var direction = moveComponent.Direction * -1;
            moveComponent.Direction.X = 0;
            moveComponent.Direction.Y = 0;

            boxColliderComponent.Rectangle.Position.X += direction.X * moveComponent.Velocity;
            boxColliderComponent.Rectangle.Position.Y += direction.Y * moveComponent.Velocity;
            
            if (direction != moveComponent.Direction)
                Debug.WriteLine("[CollisionResolverSystem]: {0}", boxColliderComponent.Rectangle.Position);
        }
    }
}