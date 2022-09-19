namespace ConwaysGameOfLife
{
    using ConwaysGameOfLife.Entities;
    using ConwaysGameOfLife.Entities.Menu;
    using ConwaysGameOfLife.Resources;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Class responsible for displaying information to user.
    /// </summary>
    public class UserOutput
    {
        GameData GameData;

        DataValidation validation;

        MenuOnStop menu;

        public UserOutput(GameData gameData)
        {
            GameData = gameData;
            validation = new DataValidation(gameData);
        }

        /// <summary>
        /// Display to user what values should be inputted for game and processes data input.
        /// </summary>
        /// <param name="isMultipleGames">Help to distinguish if it is single or multiple games play. True - multiple grids game. False - One grid game.</param>
        /// <returns>Return base parameters(GridOptions) for future game grid.</returns>
        public GridOptions GetGridParametersFromInput(bool isMultipleGames)
        {
            Console.Clear();

            var message = isMultipleGames ? ResourceFile.MessageForMultipleGamesFieldSize + "\n" :
                ResourceFile.MessageForOneGameFieldSize + "\n";
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
            var gridList = GameData.ReturnListOfExistingGrids();

            Console.WriteLine(ResourceFile.ListOfSavedGamesIntro + "\n");

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
            var listOfGames = GameData.ReturnListOfExistingGrids();

            if(listOfGames.Count != 0)
            {                
                Console.WriteLine(ResourceFile.RuleForRestoringGame);
                var userInputtedNumber = Console.ReadLine();

                var existingGame = validation.GameToRestore(userInputtedNumber, listOfGames);
                Console.Clear();

                return listOfGames[existingGame - 1];
            }
            else
            {
                Console.WriteLine(ResourceFile.MessageForEmptyGamesList);
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
            Console.WriteLine(ResourceFile.GameCountInputMessage);            

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
            Console.WriteLine(ResourceFile.ChooseMultipleGamesMessage);            

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
        public void DisplayRulesForCreateCustomGrid()
        {
            Console.WriteLine("\nTo move use ARROWS. To make cell live use SPACE. To stop setting field use ENTER.");
        }

        /// <summary>
        /// Display count of iteration and count of live cells to user.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        public void DisplayGameStatistics(Grid grid)
        {
            Console.SetCursorPosition(0, grid.Height);
            Console.WriteLine();
            Console.WriteLine($"Iteration count: {grid.IterationCount}");
            Console.WriteLine(ResourceFile.LiveCellsMessage + grid.CountOfLiveCells());
        }

        /// <summary>
        /// Clean live cells count after each iteration to change displayed value.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        /// <param name="startCoordinates">Start coordinates of a displayed grid.</param>
        public void CleanLiveCellsCount(Grid grid, int[] startCoordinates)
        {
            var mainPartOfMessage = ResourceFile.LiveCellsMessage;
            var countLength = grid.CountOfLiveCells().ToString().Length;

            Console.SetCursorPosition(startCoordinates[0] + mainPartOfMessage.Length, startCoordinates[1] + grid.Height + 2);
            Console.Write(new string(' ', countLength + 1));
        }

        /// <summary>
        /// Intoduction message for multiple games option.
        /// </summary>
        /// <param name="multipleGameList">List of games played in parallel.</param>
        public void DisplayMultipleGamesIntro(List<Grid> multipleGameList)
        {
            Console.Clear();
            Console.WriteLine($"You choose to play {multipleGameList.Count} games.");
        }

        /// <summary>
        /// Statistics message, display how many games are live and how many live cells in total they have.
        /// </summary>
        /// <param name="liveGamesCount">Count of live games.</param>
        /// <param name="totalLiveCellsCount">Total of all live cells.</param>
        public void DisplayGeneralStatisticsForMultipleGames(int liveGamesCount, int totalLiveCellsCount)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(ResourceFile.MessageToStopForMultipleGames);
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
        public void DisplayStatisticsForEachGameInMultipleGames(Grid grid, int[] startCoordinates)
        {
            Console.SetCursorPosition(startCoordinates[0], grid.Height + startCoordinates[1] + 1);
            Console.Write($"Game name: {grid.GameName}");
            Console.SetCursorPosition(startCoordinates[0], grid.Height + startCoordinates[1] + 2);
            Console.Write(ResourceFile.LiveCellsMessage + grid.CountOfLiveCells());
        }

        /// <summary>
        /// Show to user actions after stopping the game. Validate users choise.
        /// </summary>
        /// <param name="gameList">List of played game grids.</param>
        /// <returns>Return true if user chooses to exit. Return false if user wants to continue and change game grids.</returns>
        public bool DisplayMenuOnStop(List<Grid> gameList)
        {
            Console.Clear();

            var menuIntro = @$"You have stopped the game. Live games count - {gameList.Count()}" + "\n" +
                            ResourceFile.MenuOnStopIntro;

            menu = new MenuOnStop(menuIntro);

            var selectedOption = menu.SelectFromMenu();
            var exit = selectedOption.Index == (int)MenuOnStopOptions.BackToMainMenu ? true : false;

            return exit;
        }

        /// <summary>
        /// Message that is showed to user after the game has been finished.
        /// </summary>
        public void BackToMainMenuMessage()
        {
            Console.WriteLine("\n" + ResourceFile.BackToMainMenuMessage);
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Message display user rules how to go back to main menu.
        /// </summary>
        public void GoBackMessage()
        {
            Console.WriteLine(ResourceFile.GoBackMessage);
        }

        /// <summary>
        /// Message shows that game is over.
        /// </summary>
        public void GameIsOverMessage()
        {
            Console.WriteLine("\n" + ResourceFile.GameIsOverMessage);
        }
    }
}
