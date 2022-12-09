using Core;
using Core.Ecs.ComponentFilters;
using Core.Ecs.Extensions;
using PingPong.Components;

namespace PingPong.Systems;

public class MoveSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<MoveComponent, PositionComponent> _componentsFilter = default!;
    private ComponentsFilter<MoveComponent, HasCollisionComponent> _collisionFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = context.GameWorld.CreateComponentFilter<MoveComponent, PositionComponent>();
        _collisionFilter = context.GameWorld.CreateComponentFilter<MoveComponent, HasCollisionComponent>();
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, moveComponentSpan, hasCollisionComponentSpan) in _collisionFilter)
        {
            ref var moveComponent = ref moveComponentSpan[0];
            ref var hasCollisionComponent = ref hasCollisionComponentSpan[0];

            moveComponent.Dir = MoveComponent.Direction.None;
        }

        foreach (var (_, moveComponentSpan, positionComponentSpan) in _componentsFilter)
        {
            ref var moveComponent = ref moveComponentSpan[0];
            ref var positionComponent = ref positionComponentSpan[0];

            switch (moveComponent.Dir)
            {
                case MoveComponent.Direction.Up:
                    positionComponent.Point.Y -= 1;
                    break;

                case MoveComponent.Direction.Left:
                    positionComponent.Point.X -= 1;
                    break;

                case MoveComponent.Direction.Right:
                    positionComponent.Point.X += 1;
                    break;

                case MoveComponent.Direction.Down:
                    positionComponent.Point.X += 1;
                    break;
            }
        }
    }
}