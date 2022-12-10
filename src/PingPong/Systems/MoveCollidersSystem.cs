using System.Diagnostics;
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

            switch (moveComponent.Dir)
            {
                case MoveComponent.Direction.Up:
                    boxColliderComponent.Rectangle.Position.Y -= 1;
                    Debug.WriteLine("[MoveCollidersSystem]: {0}", boxColliderComponent.Rectangle.Position);
                    break;

                case MoveComponent.Direction.Left:
                    boxColliderComponent.Rectangle.Position.X -= 1;
                    Debug.WriteLine("[MoveCollidersSystem]: {0}", boxColliderComponent.Rectangle.Position);
                    break;

                case MoveComponent.Direction.Right:
                    boxColliderComponent.Rectangle.Position.X += 1;
                    Debug.WriteLine("[MoveCollidersSystem]: {0}", boxColliderComponent.Rectangle.Position);
                    break;

                case MoveComponent.Direction.Down:
                    boxColliderComponent.Rectangle.Position.Y += 1;
                    Debug.WriteLine("[MoveCollidersSystem]: {0}", boxColliderComponent.Rectangle.Position);
                    break;
            }
        }
    }
}