using Core;
using Core.Common;
using Core.Ecs.ComponentFilters;
using Core.Ecs.Extensions;
using PingPong.Components;

namespace PingPong.Systems;

public class FpsSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<FpsComponent> _fpsComponentFilter = default!;
    private IClock _clock = default!;
    
    public override void Initialize(GameContext context)
    {
        _fpsComponentFilter = context.GameWorld.CreateComponentFilter<FpsComponent>();
        _clock = new Clock();
    }

    public override void Update(GameContext context)
    {
        foreach (var (_, fpsComponentSpan) in _fpsComponentFilter)
        {
            ref var fpsComponent = ref fpsComponentSpan[0];

            if (_clock.ElapsedTime.Seconds < 1.0f)
                continue;

            var totalFrames = context.GameTime.TotalFrames;
            fpsComponent.Fps = totalFrames - fpsComponent.LastTotalFrames;
            fpsComponent.LastTotalFrames = totalFrames;

            _clock.Restart();
        }
    }
}