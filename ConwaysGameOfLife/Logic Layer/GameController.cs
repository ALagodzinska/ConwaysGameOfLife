namespace ConwaysGameOfLife
{
    using ConwaysGameOfLife.Entities.Menu;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Class responsible for running the game.
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// Main game logic.
        /// </summary>
        GameLogic game = new();

        /// <summary>
        /// User output.
        /// </summary>
        UserOutput userOutput = new();

        /// <summary>
        /// Storing game data.
        /// </summary>
        GameData gameData = new();

        /// <summary>
        /// Displaying game field.
        /// </summary>
        DisplayGame displayGame = new();

        /// <summary>
        /// Stores menu data.
        /// </summary>
        GameMainMenu menu;

        /// <summary>
        /// Resource data.
        /// </summary>
        ResourceManager resourceManager = new ResourceManager("ConwaysGameOfLife.Resources.ResourceFile", Assembly.GetExecutingAssembly());

        /// <summary>
        /// Assign menu values to a menu field.
        /// </summary>
        public GameController()
        {
            menu = new GameMainMenu(resourceManager.GetString("MainMenuIntro"));
        }

        /// <summary>
        /// Display main menu and make it functional.
        /// </summary>
        public void RunGame()
        {
            Console.Title = "GAME OF LIFE";            

            var exit = "continue";
                        
            while (exit == "continue")
            {
                Console.Clear();
                var selectedOption = menu.SelectFromMenu();

                switch (selectedOption.Index)
                {
                    case MainMenuOptions.RandomGame:
                        var gridParametersRandomGame = userOutput.GetGridParametersFromInput(false);
                        var createdGridRandom = gridParametersRandomGame.ConvertGridOptionsToGrid();
                        var randomGrid = game.CreateRandomGrid(createdGridRandom);
                        displayGame.DisplayRandomGrid(randomGrid);
                        game.PlayGame(randomGrid);
                        break;

                    case MainMenuOptions.CustomGame:
                        var gridParametersCustomGame = userOutput.GetGridParametersFromInput(false);
                        var createdGridCustom = gridParametersCustomGame.ConvertGridOptionsToGrid();
                        game.ChooseLiveCells(createdGridCustom);
                        game.PlayGame(createdGridCustom);
                        break;

                    case MainMenuOptions.RestoredGame:
                        userOutput.DisplayGamesForUser();
                        var foundGrid = userOutput.RestoreGameFromUserInput();
                        if (foundGrid != null)
                        {
                            game.PlayGame(foundGrid);
                        }

                        break;

                    case MainMenuOptions.MultipleGames:
                        var gameCount = userOutput.GameCountInput();
                        var gridParametersMultipleGames = userOutput.GetGridParametersFromInput(true);
                        var listOfGames = game.MultipleGridList(gridParametersMultipleGames, gameCount);
                        game.PlayMultipleGames(listOfGames);
                        break;

                    case MainMenuOptions.ExitGame:
                        Console.WriteLine("Thank you for the game. Bye!");
                        gameData.SaveAllData();
                        exit = "exit";
                        Environment.Exit(0);
                        break;
                }
            }               
        }      
    }
}
