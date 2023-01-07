// See https://aka.ms/new-console-template for more information

using DebugViewer.Api;
using PingPong;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        DebugViewerApi.Initialize();
        DebugViewerApi.Connect();

        var game = new Game();
        
        game.Init();
        game.Run();
    }
}

