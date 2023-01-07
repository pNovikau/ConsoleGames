using Core;
using Core.Ecs;
using Core.Render;
using DebugViewer.Api;
using PingPong.Entities;
using PingPong.Systems;

namespace PingPong;

public class Game
{
    private IGameWorld _gameWorld = default!;
    private IRenderer _renderer  = default!;

    public void Init()
    {
        using var _ = DebugViewerApi.Profiler.BeginScope("Game.Init");
        
        _gameWorld = new GameWorld();
        _renderer = new ConsoleRenderer(180, 800);

        var context = new GameContext
        {
            GameWorld = _gameWorld,
            Renderer = _renderer
        };

        _gameWorld.RegisterSystem<FpsSystem>(context);
        _gameWorld.RegisterSystem<PlayerControlSystem>(context);
        _gameWorld.RegisterSystem<MoveCollidersSystem>(context);
        _gameWorld.RegisterSystem<CollisionDetectionSystem>(context);
        _gameWorld.RegisterSystem<CollisionResolverSystem>(context);
        _gameWorld.RegisterSystem<MoveSystem>(context);
        _gameWorld.RegisterSystem<DrawFpsSystem>(context);
        _gameWorld.RegisterSystem<DrawSystem>(context);

        _gameWorld.Initialize(context);

        //FpsEntity.Create(_gameWorld);
        PlayerEntity.Create(context);
        BoxEntities.Create(context);
        BallEntity.Create(context);
    }

    public void Run()
    {

        var context = new GameContext
        {
            GameWorld = _gameWorld,
            Renderer = _renderer,
            GameTime = new GameTime()
        };

        while (true)
        {
            Thread.Sleep(5);

            context.Renderer.Clear();

            using (DebugViewerApi.Profiler.BeginScope("Game.Run.Update"))
                _gameWorld.Update(context);

            context.GameTime.Update();
        }
    }
}