namespace MazeGame;

class Program
{
    static void Main(string[] args)
    {
        var bootstrapper = new GameBootstrapper();
        bootstrapper.Start();
    }
}