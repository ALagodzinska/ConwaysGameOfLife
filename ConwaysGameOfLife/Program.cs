namespace ConwaysGameOfLife;

class Program
{
    static void Main(string[] args)
    {
        GameLogic game = new GameLogic();
        UserOutput userOutput = new UserOutput();
        GameDataSerializer dataSerializer = new GameDataSerializer();

        var exit = "continue";

        var listOfPlayedGames = dataSerializer.ReadDataFromTheFile();

        while (exit == "continue")
        {
            userOutput.ShowMenu();
            var option = Console.ReadLine();
            
            switch (option)
            {
                case "1":
                    Console.WriteLine("Let the game begin...");
                    var createdGridRandomGame = userOutput.CreateGameGridFromUserInput(listOfPlayedGames);
                    game.CreateRandomGrid(createdGridRandomGame);
                    game.PlayGame(createdGridRandomGame, listOfPlayedGames);
                    break;

                case "2":
                    var createdGridCustomGame = userOutput.CreateGameGridFromUserInput(listOfPlayedGames);
                    game.ChooseLiveCells(createdGridCustomGame);
                    game.PlayGame(createdGridCustomGame, listOfPlayedGames);
                    break;

                case "3":
                    userOutput.DisplayGamesForUser(listOfPlayedGames);
                    var foundGrid = userOutput.RestoreGameFromUserInput(listOfPlayedGames);
                    game.PlayGame(foundGrid, listOfPlayedGames);
                    break;

                case "4":
                    Console.WriteLine("Thank you for the game. Bye!");
                    dataSerializer.SaveAllData(listOfPlayedGames);
                    exit = "exit";
                    break;

                default:
                    Console.WriteLine("Wrong input! Please Try Again.");
                    break;
            }
        }

        Console.ReadLine();
    }
}


