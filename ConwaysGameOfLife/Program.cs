namespace ConwaysGameOfLife;

class Program
{
    static void Main(string[] args)
    {
        //setting window size to maximum, makes it easier to play multiple games.
        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

        GameLogic game = new();
        UserOutput userOutput = new();
        GameData dataSerializer = new();
        DisplayGame displayGame = new();

        var exit = "continue";

        dataSerializer.ReadDataFromTheFile();

        while (exit == "continue")
        {
            userOutput.ShowMenu();
            var option = Console.ReadLine();
            
            switch (option)
            {
                case "1":
                    var gridParametersRandomGame = userOutput.GetGridParametersFromInput();
                    var createdGridRandom = gridParametersRandomGame.ConvertGridOptionsToGrid();
                    var randomGrid = game.CreateRandomGrid(createdGridRandom);
                    displayGame.DisplayRandomGrid(randomGrid);
                    game.PlayGame(randomGrid);
                    break;

                case "2":
                    var gridParametersCustomGame = userOutput.GetGridParametersFromInput();
                    var createdGridCustom = gridParametersCustomGame.ConvertGridOptionsToGrid();
                    game.ChooseLiveCells(createdGridCustom);
                    game.PlayGame(createdGridCustom);
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
                    var gameCount = userOutput.GameCountInput();
                    var gridParametersMultipleGames = userOutput.GetMultipleGamesParametersFromInput();
                    var listOfGames = game.MultipleGridList(gridParametersMultipleGames, gameCount);
                    game.PlayMultipleGames(listOfGames);
                    break;

                case "5":
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


