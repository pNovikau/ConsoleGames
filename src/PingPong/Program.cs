// See https://aka.ms/new-console-template for more information

using PingPong;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var game = new Game();
        
        game.Init();
        game.Run();
    }
}

