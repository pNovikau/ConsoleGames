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
            var hasCollision = false;
            var sourceEntity = context.GameWorld.GetEntity(sourceEntityId);
            
            ref var sourceCollidedComponent = ref sourceCollidedComponentSpan[0];

            foreach (var (targetEntityId, targetBoxColliderComponentSpan) in _componentsFilter)
            {
                if (sourceEntityId == targetEntityId)
                    continue;

                ref var targetBoxColliderComponent = ref targetBoxColliderComponentSpan[0];

                var overlap = Math.GetOverlappingArea(sourceCollidedComponent.Rectangle, targetBoxColliderComponent.Rectangle);

                if (overlap.IsEmpty) 
                    continue;

                ref var sourceHasCollisionComponent = ref sourceEntity.HasComponent<HasCollisionComponent>()
                    ? ref sourceEntity.GetComponent<HasCollisionComponent>()
                    : ref sourceEntity.AddComponent<HasCollisionComponent>();

                sourceHasCollisionComponent.Overlap = overlap;
                sourceHasCollisionComponent.TargetEntityId = targetEntityId;

                hasCollision = true;
                
                Debug.WriteLine("[CollisionDetectionSystem]: Collision added {0} target {1}", sourceEntityId, targetEntityId);
            }

            if (!hasCollision && sourceEntity.HasComponent<HasCollisionComponent>())
            {
                sourceEntity.RemoveComponent<HasCollisionComponent>();

                Debug.WriteLine("[CollisionDetectionSystem]: Collision removed sourceEntityId {0}", sourceEntityId);
            }
        }
    }
}