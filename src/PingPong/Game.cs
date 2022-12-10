using Core;
using Core.Ecs;
using PingPong.Entities;
using PingPong.Systems;

namespace PingPong;

public class Game
{
    private IGameWorld _gameWorld = default!;
    private IRenderer _renderer  = default!;

    public void Init()
    {
        _gameWorld = new GameWorld();
        _renderer = new ConsoleRenderer(30, 60);

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

        FpsEntity.Create(_gameWorld);
        PlayerEntity.Create(context);
        BoxEntities.Create(context);
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
            Thread.Sleep(25);

            context.Renderer.Clear();

            _gameWorld.Update(context);

            context.GameTime.Update();
            context.Renderer.Display();
        }
    }
}