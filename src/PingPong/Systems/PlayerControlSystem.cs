﻿using Core;
using Core.Ecs.ComponentFilters;
using Core.Ecs.Extensions;
using PingPong.Components;

namespace PingPong.Systems;

public class PlayerControlSystem : Core.Ecs.Systems.System
{
    private ComponentsFilter<PlayerComponent, MoveComponent> _componentsFilter = default!;

    public override void Initialize(GameContext context)
    {
        _componentsFilter = context.GameWorld.CreateComponentFilter<PlayerComponent, MoveComponent>();
    }
    
    public override void Update(GameContext context)
    {
        foreach (var (_, _, moveComponentSpan) in _componentsFilter)
        {
            ref var moveComponent = ref moveComponentSpan[0];

            if (Keyboard.IsKeyPressed(ConsoleKey.A))
                moveComponent.Dir = MoveComponent.Direction.Left;
            else if (Keyboard.IsKeyPressed(ConsoleKey.D))
                moveComponent.Dir = MoveComponent.Direction.Right;
            else
                moveComponent.Dir = MoveComponent.Direction.None;
        }
    }
}