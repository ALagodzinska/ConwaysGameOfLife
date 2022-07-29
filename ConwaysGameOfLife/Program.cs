namespace ConwaysGameOfLife;

using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        //setting window size to maximum, makes it easier to play multiple games.
        SetWindowSize(LargestWindowWidth, LargestWindowHeight);

        GameData gameData = new();

        gameData.ReadDataFromTheFile();

        StartGame game = new StartGame();
        game.Start();        
    }
}


