using Core;
using Core.Ecs.ComponentFilters;
using PingPong.Components;

namespace PingPong.Systems;

public class MoveSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<MoveComponent, PositionComponent, SizeComponent> _componentsFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = new ComponentsFilter<MoveComponent, PositionComponent, SizeComponent>(context.GameWorld.EntityManager, context.GameWorld.ComponentManager);
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, moveComponentSpan, positionComponentSpan, sizeComponentSpan) in _componentsFilter)
        {
            ref var moveComponent = ref moveComponentSpan[0];
            ref var positionComponent = ref positionComponentSpan[0];
            ref var sizeComponent = ref sizeComponentSpan[0];

            switch (moveComponent.Dir)
            {
                case MoveComponent.Direction.Up when positionComponent.Y == 0:
                    moveComponent.Dir = MoveComponent.Direction.None;
                    break;
                case MoveComponent.Direction.Up:
                    positionComponent.Y -= 1;
                    break;

                case MoveComponent.Direction.Left when positionComponent.X == 0:
                    moveComponent.Dir = MoveComponent.Direction.None;
                    break;
                case MoveComponent.Direction.Left:
                    positionComponent.X -= 1;
                    break;
                
                case MoveComponent.Direction.Right when positionComponent.X + sizeComponent.Width == context.Renderer.Width:
                    moveComponent.Dir = MoveComponent.Direction.None;
                    break;
                case MoveComponent.Direction.Right:
                    positionComponent.X += 1;
                    break;
                
                case MoveComponent.Direction.Down when positionComponent.Y + sizeComponent.Height == context.Renderer.Height:
                    moveComponent.Dir = MoveComponent.Direction.None;
                    break;
                case MoveComponent.Direction.Down:
                    positionComponent.X += 1;
                    break;
            }
        }
    }
}