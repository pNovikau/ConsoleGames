using Core;
using Core.Common;
using Core.Ecs.ComponentFilters;
using Core.Ecs.Extensions;
using PingPong.Components;
using Math = Core.Common.Math;

namespace PingPong.Systems;

public class CollisionDetectionSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<BoxColliderComponent> _componentsFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = context.GameWorld.CreateComponentFilter<BoxColliderComponent>();
    }

    public override void Update(GameContext context)
    {
        foreach (var (sourceEntityId, sourceCollidedComponentSpan) in _componentsFilter)
        {
            ref var sourceCollidedComponent = ref sourceCollidedComponentSpan[0];

            foreach (var (targetEntityId, targetBoxColliderComponentSpan) in _componentsFilter)
            {
                if (sourceEntityId == targetEntityId)
                    continue;

                ref var targetBoxColliderComponent = ref targetBoxColliderComponentSpan[0];

                var overlap = Math.GetOverlappingArea(sourceCollidedComponent.Rectangle, targetBoxColliderComponent.Rectangle);

                if (overlap != Rectangle<int>.Empty)
                {
                    ref var sourceHasCollisionComponent = ref context.GameWorld.GetEntity(sourceEntityId).AddComponent<HasCollisionComponent>();

                    sourceHasCollisionComponent.Overlap = overlap;
                    sourceHasCollisionComponent.TargetEntityId = targetEntityId;
                }
            }
        }
    }
}