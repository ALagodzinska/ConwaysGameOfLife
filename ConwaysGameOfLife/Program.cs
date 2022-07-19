namespace ConwaysGameOfLife;

class Program
{
    static void Main(string[] args)
    {
        GameLogic game = new();
        UserOutput userOutput = new();
        GameDataSerializer dataSerializer = new();

        var exit = "continue";

        dataSerializer.ReadDataFromTheFile();

        while (exit == "continue")
        {
            userOutput.ShowMenu();
            var option = Console.ReadLine();
            
            switch (option)
            {
                case "1":
                    Console.WriteLine("Let the game begin...");
                    var createdGridRandomGame = userOutput.CreateGameGridFromUserInput();
                    game.CreateRandomGrid(createdGridRandomGame);
                    game.PlayGame(createdGridRandomGame);
                    break;

                case "2":
                    var createdGridCustomGame = userOutput.CreateGameGridFromUserInput();
                    game.ChooseLiveCells(createdGridCustomGame);
                    game.PlayGame(createdGridCustomGame);
                    break;

                case "3":
                    userOutput.DisplayGamesForUser();
                    var foundGrid = userOutput.RestoreGameFromUserInput();
                    if(foundGrid != null)
                    {
                        game.PlayGame(foundGrid);
                    }

                    break;

                case "4":
                    Console.WriteLine("Thank you for the game. Bye!");
                    dataSerializer.SaveAllData();
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


