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

            var direction = moveComponent.Dir switch
            {
                MoveComponent.Direction.Up => MoveComponent.Direction.Down,
                MoveComponent.Direction.Left => MoveComponent.Direction.Right,
                MoveComponent.Direction.Right => MoveComponent.Direction.Left,
                MoveComponent.Direction.Down => MoveComponent.Direction.Up,
                _ => MoveComponent.Direction.None
            };
            moveComponent.Dir = MoveComponent.Direction.None;

            switch (direction)
            {
                case MoveComponent.Direction.Up:
                    boxColliderComponent.Rectangle.Position.Y -= 1;
                    Debug.WriteLine("[CollisionResolverSystem]: {0}", boxColliderComponent.Rectangle.Position);
                    break;

                case MoveComponent.Direction.Left:
                    boxColliderComponent.Rectangle.Position.X -= 1;
                    Debug.WriteLine("[CollisionResolverSystem]: {0}", boxColliderComponent.Rectangle.Position);
                    break;

                case MoveComponent.Direction.Right:
                    boxColliderComponent.Rectangle.Position.X += 1;
                    Debug.WriteLine("[CollisionResolverSystem]: {0}", boxColliderComponent.Rectangle.Position);
                    break;

                case MoveComponent.Direction.Down:
                    boxColliderComponent.Rectangle.Position.Y += 1;
                    Debug.WriteLine("[CollisionResolverSystem]: {0}", boxColliderComponent.Rectangle.Position);
                    break;
            }
        }
    }
}