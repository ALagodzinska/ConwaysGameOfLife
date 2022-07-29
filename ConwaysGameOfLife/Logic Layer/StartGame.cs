namespace ConwaysGameOfLife
{
    using ConwaysGameOfLife.Entities;

    /// <summary>
    /// Class responsible for running the game.
    /// </summary>
    public class StartGame
    {
        GameLogic game = new();
        UserOutput userOutput = new();
        GameData gameData = new();
        DisplayGame displayGame = new();

        /// <summary>
        /// Starts game.
        /// </summary>
        public void Start()
        {
            Console.Title = "GAME OF LIFE";
            RunMainMenu();
        }

        /// <summary>
        /// Display main menu.
        /// </summary>
        private void RunMainMenu()
        {
            var menuIntro = @"
 ██████╗  █████╗ ███╗   ███╗███████╗     ██████╗ ███████╗    ██╗     ██╗███████╗███████╗
██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔═══██╗██╔════╝    ██║     ██║██╔════╝██╔════╝
██║  ███╗███████║██╔████╔██║█████╗      ██║   ██║█████╗      ██║     ██║█████╗  █████╗  
██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ██║   ██║██╔══╝      ██║     ██║██╔══╝  ██╔══╝  
╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ╚██████╔╝██║         ███████╗██║██║     ███████╗
 ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝ ╚═╝         ╚══════╝╚═╝╚═╝     ╚══════╝

Hello, Welcome to the 'Game Of Life'!;
Please choose what you want to do?
(Use the arrow to cycle through options and press enter to select an option.)" + "\n";
            string[] options = { "Play Game: Create Random field", "Play Game: Create Customized field", "Restore Game: Continue to play one of the previous games", "Play multiple games at once", "Exit Game" };

            var exit = "continue";
                        
            while (exit == "continue")
            {
                Console.Clear();
                Menu mainMenu = new Menu(options, menuIntro);
                var selectedIndex = mainMenu.SelectFromMenu();

                switch (selectedIndex)
                {
                    case 0:
                        var gridParametersRandomGame = userOutput.GetGridParametersFromInput(false);
                        var createdGridRandom = gridParametersRandomGame.ConvertGridOptionsToGrid();
                        var randomGrid = game.CreateRandomGrid(createdGridRandom);
                        displayGame.DisplayRandomGrid(randomGrid);
                        game.PlayGame(randomGrid);
                        break;

                    case 1:
                        var gridParametersCustomGame = userOutput.GetGridParametersFromInput(false);
                        var createdGridCustom = gridParametersCustomGame.ConvertGridOptionsToGrid();
                        game.ChooseLiveCells(createdGridCustom);
                        game.PlayGame(createdGridCustom);
                        break;

                    case 2:
                        userOutput.DisplayGamesForUser();
                        var foundGrid = userOutput.RestoreGameFromUserInput();
                        if (foundGrid != null)
                        {
                            game.PlayGame(foundGrid);
                        }

                        break;

                    case 3:
                        var gameCount = userOutput.GameCountInput();
                        var gridParametersMultipleGames = userOutput.GetGridParametersFromInput(true);
                        var listOfGames = game.MultipleGridList(gridParametersMultipleGames, gameCount);
                        game.PlayMultipleGames(listOfGames);
                        break;

                    case 4:
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
