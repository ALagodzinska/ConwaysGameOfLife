namespace ConwaysGameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            //setting window size to maximum, makes it easier to play multiple games.
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            GameData gameData = new();

            gameData.ReadDataFromTheFile();

            GameController game = new GameController();
            game.RunGame();
        }
    }
}


