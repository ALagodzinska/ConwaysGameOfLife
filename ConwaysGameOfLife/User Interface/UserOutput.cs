using ConwaysGameOfLife.Entities;

namespace ConwaysGameOfLife
{

    /// <summary>
    /// Class responsible for displaying information to user.
    /// </summary>
    public class UserOutput
    {
        GameData gameData = new();

        DataValidation validation = new();               

        /// <summary>
        /// Display to user what values should be inputted for one game and processes data input.
        /// </summary>
        /// <returns>Return base parameters(GridOptions) for future game grid.</returns>
        public GridOptions GetGridParametersFromInput(bool isMultipleGames)
        {
            Console.Clear();
            var message = isMultipleGames ? "Please Remember that MAX Height is 15 and MAX Width is 20\n" : "Please Remember that MAX Height is 30 and MAX Width is 60\n";
            Console.WriteLine(message);

            Console.WriteLine("Create name for this game:");
            var gameNameInput = Console.ReadLine();
            var validName = validation.GameNameOnCreate(gameNameInput);

            Console.WriteLine("Input height of field:");
            var heightInput = Console.ReadLine();
            var validHeight = validation.GridHeightInput(heightInput, isMultipleGames);

            Console.WriteLine("Input width of field:");
            var widthInput = Console.ReadLine();
            var validWidth = validation.GridWidthInput(widthInput, isMultipleGames);

            var gridParameters = new GridOptions()
            {
                GameName = validName,
                Height = validHeight,
                Width = validWidth,
            };

            return gridParameters;
        }

        /// <summary>
        /// Show to user all names of the grids that are stored in a list.
        /// </summary>
        public void DisplayGamesForUser()
        {
            Console.Clear();

            int numberInList = 1;
            var gridList = gameData.ReturnListOfExistingGrids();

            Console.WriteLine(@"Please choose one of the games from the list.
List of saved games:" + "\n");

            foreach (var grid in gridList)
            {
                Console.WriteLine($"{numberInList}. {grid.GameName} : Iteration count - {grid.IterationCount}");
                numberInList++;
            }
        }

        /// <summary>
        /// Get game grid which user want to restore.
        /// </summary>
        /// <returns>A game grid.</returns>
        public Grid? RestoreGameFromUserInput()
        {
            var listOfGames = gameData.ReturnListOfExistingGrids();

            if(listOfGames.Count != 0)
            {
                Console.WriteLine("\nPlease input NUMBER of the game you want to restore.");
                var userInputtedNumber = Console.ReadLine();

                var existingGame = validation.GameToRestore(userInputtedNumber, listOfGames);
                Console.Clear();

                return listOfGames[existingGame - 1];
            }
            else
            {
                Console.WriteLine("You dont have saved games! You will be sent back to main menu");
                Thread.Sleep(3000);
                return null;
            }
        }

        /// <summary>
        /// Get user input about how many games will be played at the same time.
        /// </summary>
        /// <returns>Count of the games to be played in parallel.</returns>
        public int GameCountInput()
        {
            Console.Clear();
            Console.WriteLine("How many games you want to play?");
            Console.WriteLine("Input number from 2 to 1000");

            var gameCountInput = Console.ReadLine();
            var validGameCountInput = validation.GamesCountToPlay(gameCountInput);

            return validGameCountInput;
        }

        /// <summary>
        /// Get data from user about what games should be displayed on the screen.
        /// </summary>
        /// <param name="countOfAllGames">Count of all games that are playing in parallel.</param>
        /// <returns>Array of numbers, number - indicate location in list of all games played at the same time.</returns>
        public int[] ChooseMultipleGames(int countOfAllGames)
        {
            Console.WriteLine();
            Console.WriteLine("No more than 8 games can be shown on the screen.");
            Console.WriteLine("How many games you want to see on the screen?");

            var gamesToDisplayCount = Console.ReadLine();
            var countOfSelectedGames = validation.SelectedGamesCount(gamesToDisplayCount, countOfAllGames);

            int[] gamesArray = new int[countOfSelectedGames];

            Console.WriteLine($"To choose game input any NUMBER from 1 to {countOfAllGames}. Choose {countOfSelectedGames} numbers.");

            for (int i = 0; i < countOfSelectedGames; i++)
            {
                var gameNumberInput = Console.ReadLine();
                var validGameNumber = validation.GameNumber(gameNumberInput, countOfAllGames, gamesArray);
                gamesArray[i] = validGameNumber;
            }

            return gamesArray;
        }        

        /// <summary>
        /// Message to display rules for customazing the grid.
        /// </summary>
        public void CustomGameGridRulesMessage()
        {
            Console.WriteLine("\nTo move use ARROWS. To make cell live use SPACE. To stop setting field use ENTER.");
        }

        /// <summary>
        /// Display count of iteration and count of live cells to user.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        public void MessageAfterEachIteration(Grid grid)
        {
            Console.SetCursorPosition(0, grid.Height);
            Console.WriteLine();
            Console.WriteLine($"Iteration count: {grid.IterationCount}");
            Console.WriteLine($"Live cells: {grid.CountOfLiveCells()}");
        }

        /// <summary>
        /// Clean live cells count after each iteration to change displayed value.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        /// <param name="startCoordinates">Start coordinates of a displayed grid.</param>
        public void CleanLiveCellsCount(Grid grid, int[] startCoordinates)
        {
            var mainPartOfMessage = "Live cells: ";
            var countLength = grid.CountOfLiveCells().ToString().Length;

            Console.SetCursorPosition(startCoordinates[0] + mainPartOfMessage.Length, startCoordinates[1] + grid.Height + 2);
            Console.Write(new string(' ', countLength + 1));
        }

        /// <summary>
        /// Intoduction message for multiple games option.
        /// </summary>
        /// <param name="multipleGameList">List of games played in parallel.</param>
        public void MultipleGamesIntro(List<Grid> multipleGameList)
        {
            Console.Clear();
            Console.WriteLine($"You choose to play {multipleGameList.Count} games.");
        }

        /// <summary>
        /// Statistics message, display how many games are live and how many live cells in total they have.
        /// </summary>
        /// <param name="liveGamesCount">Count of live games.</param>
        /// <param name="totalLiveCellsCount">Total of all live cells.</param>
        public void MessageForMultipleGames(int liveGamesCount, int totalLiveCellsCount)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Press SPACE to stop the game. If you want to save or change displayed grids.");
            Console.WriteLine();
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", $"Count of live games: {liveGamesCount}"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", $"Total count of live cells: {totalLiveCellsCount}"));
            Console.WriteLine();
        }

        /// <summary>
        /// Display game names and count of live cells of one grid on a multiple games field.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        /// <param name="startCoordinates">Start coordinates of a displayed grid.</param>
        public void MultipleGameMessageAfterIteration(Grid grid, int[] startCoordinates)
        {
            Console.SetCursorPosition(startCoordinates[0], grid.Height + startCoordinates[1] + 1);
            Console.Write($"Game name: {grid.GameName}");
            Console.SetCursorPosition(startCoordinates[0], grid.Height + startCoordinates[1] + 2);
            Console.Write($"Live cells: {grid.CountOfLiveCells()}");
        }

        /// <summary>
        /// Show to user actions after stopping the game. Validate users choise.
        /// </summary>
        /// <param name="gameList">List of played game grids.</param>
        /// <returns>Return true if user chooses to exit. Return false if user wants to continue and change game grids.</returns>
        public bool DecisionOnStop(List<Grid> gameList)
        {
            Console.Clear();

            var menuIntro = @$"You have stopped the game. Live games count - {gameList.Count()}.
Choose if you want EXIT game ang go back to main menu. Or CONTINUE and choose different cells to display on a screen.";
            string[] options = { "GO BACK TO MAIN MENU", "CONTINUE TO PLAY" };

            Menu mainMenu = new Menu(options, menuIntro);
            var selectedIndex = mainMenu.SelectFromMenu();
            var exit = selectedIndex == 0 ? true : false;

            return exit;
        }

        /// <summary>
        /// Message that is showed to user after the game has been finished.
        /// </summary>
        public void GameOverMessage()
        {
            Console.WriteLine("\nYou will be sent to main menu ater 5 seconds.");
            Thread.Sleep(5000);
        }
    }
}
