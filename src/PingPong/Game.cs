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

        _gameWorld.SystemManager.RegisterSystem<FpsSystem>(context);
        _gameWorld.SystemManager.RegisterSystem<PlayerControlSystem>(context);
        _gameWorld.SystemManager.RegisterSystem<MoveSystem>(context);
        _gameWorld.SystemManager.RegisterSystem<DrawFpsSystem>(context);
        _gameWorld.SystemManager.RegisterSystem<DrawSystem>(context);

        _gameWorld.Initialize(context);

        FpsEntity.Create(_gameWorld);
        PlayerEntity.Create(context);
        BoxEntity.Create(context);
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

            foreach (var system in _gameWorld.SystemManager.Systems)
                system.Update(context);

            context.GameTime.Update();
            context.Renderer.Display();
        }
    }
}