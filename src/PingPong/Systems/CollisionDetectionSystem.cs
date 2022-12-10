using System.Diagnostics;
using Core;
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
                var entity = context.GameWorld.GetEntity(sourceEntityId);

                var overlap = Math.GetOverlappingArea(sourceCollidedComponent.Rectangle, targetBoxColliderComponent.Rectangle);

                if (!overlap.IsEmpty)
                {
                    ref var sourceHasCollisionComponent = ref entity.HasComponent<HasCollisionComponent>()
                        ? ref entity.GetComponent<HasCollisionComponent>()
                        : ref entity.AddComponent<HasCollisionComponent>();

                    sourceHasCollisionComponent.Overlap = overlap;
                    sourceHasCollisionComponent.TargetEntityId = targetEntityId;
                    
                    Debug.WriteLine("[CollisionDetectionSystem]: Collision added {0} target {1}", sourceEntityId, targetEntityId);
                }
                else if (entity.HasComponent<HasCollisionComponent>())
                {
                    entity.RemoveComponent<HasCollisionComponent>();
                    
                    Debug.WriteLine("[CollisionDetectionSystem]: Collision removed {0} target {1}", sourceEntityId, targetEntityId);
                }
            }
        }
    }
}