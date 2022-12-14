namespace ConwaysGameOfLife
{
    using ConwaysGameOfLife.Entities.Menu;
    using ConwaysGameOfLife.Resources;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Class responsible for running the game.
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// Storing game data.
        /// </summary>
        GameData GameData;

        /// <summary>
        /// Main game logic.
        /// </summary>
        GameLogic game;

        /// <summary>
        /// User output.
        /// </summary>
        UserOutput userOutput;

        /// <summary>
        /// Stores menu data.
        /// </summary>
        GameMainMenu menu;

        /// <summary>
        /// Displaying game field.
        /// </summary>
        DisplayGame displayGame;

        /// <summary>
        /// Assign menu values to a menu field.
        /// </summary>
        public GameController(GameData gameData)
        {
            GameData = gameData;
            menu = new GameMainMenu(ResourceFile.MainMenuIntro);
            userOutput = new UserOutput(gameData);            
            displayGame = new DisplayGame(userOutput);
            game = new GameLogic(gameData, userOutput, displayGame);
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
                    case (int)MainMenuOptions.RandomGame:
                        var gridParametersRandomGame = userOutput.GetGridParametersFromInput(false);
                        var createdGridRandom = gridParametersRandomGame.ConvertGridOptionsToGrid();
                        var randomGrid = game.CreateRandomGrid(createdGridRandom);
                        displayGame.DisplayRandomGrid(randomGrid);
                        game.PlayGame(randomGrid);
                        break;

                    case (int)MainMenuOptions.CustomGame:
                        var gridParametersCustomGame = userOutput.GetGridParametersFromInput(false);
                        var createdGridCustom = gridParametersCustomGame.ConvertGridOptionsToGrid();
                        game.ChooseLiveCells(createdGridCustom);
                        game.PlayGame(createdGridCustom);
                        break;

                    case (int)MainMenuOptions.RestoredGame:
                        userOutput.DisplayGamesForUser();
                        var foundGrid = userOutput.RestoreGameFromUserInput();
                        if (foundGrid != null)
                        {
                            game.PlayGame(foundGrid);
                        }

                        break;

                    case (int)MainMenuOptions.MultipleGames:
                        var gameCount = userOutput.GameCountInput();
                        var gridParametersMultipleGames = userOutput.GetGridParametersFromInput(true);
                        var listOfGames = game.MultipleGridList(gridParametersMultipleGames, gameCount);
                        game.PlayMultipleGames(listOfGames);
                        break;

                    case (int)MainMenuOptions.ExitGame:
                        Console.WriteLine("Thank you for the game. Bye!");
                        GameData.SaveAllData();
                        exit = "exit";
                        Environment.Exit(0);
                        break;
                }
            }               
        }      
    }
}
