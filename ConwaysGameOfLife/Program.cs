namespace ConwaysGameOfLife;

class Program
{
    static void Main(string[] args)
    {
        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

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
                    var gridParametersRandomGame = userOutput.GetGridParametersFromInput();
                    var createdGridRandom = Grid.CreateNewGrid(gridParametersRandomGame.Height, gridParametersRandomGame.Width, gridParametersRandomGame.GameName);
                    var randomGrid = game.CreateRandomGrid(createdGridRandom);
                    ///do i even need this method
                    game.DisplayRandomGrid(randomGrid);
                    game.PlayGame(randomGrid);
                    break;

                case "2":
                    var gridParametersCustomGame = userOutput.GetGridParametersFromInput();
                    var createdGridCustom = Grid.CreateNewGrid(gridParametersCustomGame.Height, gridParametersCustomGame.Width, gridParametersCustomGame.GameName);
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
                    var listOfGames = game.GenerateGridsForMultipleGames(gridParametersMultipleGames, gameCount);
                    game.PlayMultipleGames(listOfGames);
                    //choose how many games you want to play
                    //choose size of the field for games
                    //generate random grid
                    //play games without drawing
                    //on esc stop playing

                    //save games to array
                    //allow to choose up to 8 games for displaying 
                    //choose array by index

                    //change games that are showed on a screen
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


