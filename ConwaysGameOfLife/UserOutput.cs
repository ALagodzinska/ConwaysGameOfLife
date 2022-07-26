using System.Linq;

namespace ConwaysGameOfLife
{
    /// <summary>
    /// Class responsible for displaying information to user.
    /// </summary>
    public class UserOutput
    {
        GameDataSerializer dataSerializer = new();

        /// <summary>
        /// Field that stores number input if it meet all requirements.
        /// </summary>
        int validInput;

        /// <summary>
        /// Show menu to user, where are listed actions that user is able to choose. 
        /// </summary>
        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Hello, Welcome to the 'Game Of Life'!" + "\n");
            Console.WriteLine("Please choose action what you want to do?(INPUT NUMBER)");
            Console.WriteLine("1. Play Game: Create Random field");
            Console.WriteLine("2. Play Game: Create Customized field");
            Console.WriteLine("3. Restore Game: Continue to play one of the previous games");
            Console.WriteLine("4. Play multiple games at once");
            Console.WriteLine("5. Exit Game");
        }

        /// <summary>
        /// Display to user what values should be inputted for one game and processes data input.
        /// </summary>
        /// <returns>Return base parameters(GridOptions) for future game grid.</returns>
        public GridOptions GetGridParametersFromInput()
        {
            Console.Clear();
            Console.WriteLine("Please Remember that MAX Height is 30 and MAX Width is 60" + "\n");

            Console.WriteLine("Create name for this game:");
            var gameNameInput = Console.ReadLine();
            var validName = GameNameOnCreateValidation(gameNameInput);

            Console.WriteLine("Input height of field:");
            var heightInput = Console.ReadLine();
            var validHeight = DimensionInputValidation(heightInput, "height");

            Console.WriteLine("Input width of field:");
            var widthInput = Console.ReadLine();
            var validWidth = DimensionInputValidation(widthInput, "width");

            var gridParameters = new GridOptions()
            {
                GameName = validName,
                Height = validHeight,
                Width = validWidth,
            };

            return gridParameters;
        }

        /// <summary>
        /// Display to user what values should be inputted for multiple games and processes data input.
        /// </summary>
        /// <returns>Return base parameters(GridOptions) for future game grid.</returns>
        public GridOptions GetMultipleGamesParametersFromInput()
        {
            Console.Clear();
            Console.WriteLine("Please Remember that MAX Height is 15 and MAX Width is 20" + "\n");

            Console.WriteLine("Create name for this game:");
            var gameNameInput = Console.ReadLine();
            var validName = GameNameOnCreateValidation(gameNameInput);

            Console.WriteLine("Input height of field:");
            var heightInput = Console.ReadLine();
            var validHeight = DimensionInputValidation(heightInput, "multipleGameHeight");

            Console.WriteLine("Input width of field:");
            var widthInput = Console.ReadLine();
            var validWidth = DimensionInputValidation(widthInput, "multipleGameWidth");

            var gridParameters = new GridOptions()
            {
                GameName = validName,
                Height = validHeight,
                Width = validWidth,
            };

            return gridParameters;
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
            var validGameCountInput = GamesCountToPlayValidation(gameCountInput);

            return validGameCountInput;
        }

        /// <summary>
        /// Get data from user about what games should be displayed on the screen.
        /// </summary>
        /// <param name="countOfAllGames">Count of all games that are playing in parallel.</param>
        /// <returns>Array of numbers, number - indicate location in list of all games played at the same time.</returns>
        public int[] ChooseMultipleGames(int countOfAllGames)
        {
            Console.WriteLine("No more than 8 games can be shown on the screen.");
            Console.WriteLine("How many games you want to see on the screen?");

            var gamesToDisplayCount = Console.ReadLine();
            var countOfSelectedGames = SelectedGamesCountValidation(gamesToDisplayCount, countOfAllGames);

            int[] gamesArray = new int[countOfSelectedGames];

            for (int i = 0; i < countOfSelectedGames; i++)
            {
                Console.WriteLine($"To choose game input a number from 1 to {countOfAllGames}");
                var gameNumberInput = Console.ReadLine();
                var validGameNumber = GameNumberValidation(gameNumberInput, countOfAllGames, gamesArray);
                gamesArray[i] = validGameNumber;
            }

            return gamesArray;
        }

        /// <summary>
        /// Check if inputted numeric value is valid, is included in boundaries of chosen game list. If not asks for valid input.
        /// </summary>
        /// <param name="gameNumberInput">Serial number of game to choose from list.</param>
        /// <param name="countOfAllGames">Count of all games in a list</param>
        /// <returns>Returns valid game numeric identifier.</returns>
        public int GameNumberValidation(string gameNumberInput, int countOfAllGames, int[] gamesToShowArray)
        {
            while (!int.TryParse(gameNumberInput, out validInput) || validInput <= 0
                || validInput > countOfAllGames || gamesToShowArray.Contains(validInput))
            {
                var message = gamesToShowArray.Contains(validInput) && validInput != 0 ? 
                    "You already chose this game, try one more time!" : "Such game don't exist, try one more time!";

                Console.WriteLine(message);
                gameNumberInput = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// Check if inputted numeric value for games to display is valid. If not asks for valid input.
        /// </summary>
        /// <param name="gamesCountInput">Number of the game that user wants to see on screen.</param>
        /// <param name="countOfAllGames">Count of all games played in parallel.</param>
        /// <returns>Returns a valid count of games to display on screen.</returns>
        public int SelectedGamesCountValidation(string gamesCountInput, int countOfAllGames)
        {
            // 8 because no more than 8 games can be displayed
            while (!int.TryParse(gamesCountInput, out validInput) || validInput <= 0
                || validInput > countOfAllGames
                || validInput > 8)
            {
                Console.WriteLine("Please enter valid input!");
                gamesCountInput = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// Check if inputted numeric value for games to play in parallel is valid. If not asks for valid input.
        /// </summary>
        /// <param name="countOfAllGames">User input for count of all games played in parallel.</param>
        /// <returns>Returns an valid count of games to play in parallel.</returns>
        public int GamesCountToPlayValidation(string countOfAllGames)
        {
            while (!int.TryParse(countOfAllGames, out validInput) || validInput <= 0
                || validInput < 2
                || validInput > 1000)
            {
                Console.WriteLine("Please enter valid input!");
                countOfAllGames = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// Check if inputted numeric value for grid dimensions is valid. If not asks for valid input.
        /// </summary>
        /// <param name="numberInput">Parameter for grid that was inputted by user.</param>
        /// <param name="typeOfInput">Allows to understand what type of input it is(height or width). Used for validation.</param>
        /// <returns>Returns a valid integer - Grid parameter.</returns>
        public int DimensionInputValidation(string numberInput, string typeOfInput)
        {
            while (!int.TryParse(numberInput, out validInput) || validInput <= 0
                || typeOfInput == "height" && validInput > 30
                || typeOfInput == "width" && validInput > 60
                || typeOfInput == "multipleGameWidth" && validInput > 20
                || typeOfInput == "multipleGameHeight" && validInput > 15)
            {
                Console.WriteLine("Please enter valid input!");
                numberInput = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// On creating the new game check if inputted game name already exsists. If name is taken asks for new game name.
        /// </summary>
        /// <param name="gameName">Inputted name for the grid.</param>
        /// <returns>Return valid(not taken) name for game grid.</returns>
        public string GameNameOnCreateValidation(string gameName)
        {
            var exitName = "exit";
            while (dataSerializer.FindGameGridByName(gameName) != null ||
                gameName == null || gameName.ToLower() == exitName)
            {
                Console.WriteLine("That name is taken" + "\n" + "Please enter valid input!");
                gameName = Console.ReadLine();
            }

            return gameName;
        }

        /// <summary>
        /// Check if the inputted name matches one of the saved game names. Return name only if there is a match.
        /// </summary>
        /// <param name="gameName">Inputted name for the grid.</param>
        /// <returns>Return valid name(exsisting name of the grid from the saved grid list).</returns>
        public string GameNameOnSearchValidation(string gameName)
        {
            while (dataSerializer.FindGameGridByName(gameName) == null)
            {
                Console.WriteLine("There is no game with such name" + "\n" + "Please enter one of the names from the list!");
                gameName = Console.ReadLine();
            }

            return gameName;
        }

        /// <summary>
        /// Show to user all names of the grids that are stored in a list.
        /// </summary>
        public void DisplayGamesForUser()
        {
            Console.Clear();

            int numberInList = 1;
            var gridList = dataSerializer.ReturnListOfExistingGrids();

            Console.WriteLine("Please choose one of the games from the list" + "\n" + "List of saved games:" + "\n");

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
            Console.WriteLine("\n" + "Please input NAME of the game you want to restore." + "\n" + "Type EXIT if you want to go back to main menu.");
            var userInputtedName = Console.ReadLine();

            if (userInputtedName.ToLower() == "exit")
            {
                return null;
            }

            var existingGame = GameNameOnSearchValidation(userInputtedName);

            Console.Clear();

            return dataSerializer.FindGameGridByName(existingGame);
        }

        /// <summary>
        /// Message that is showed to user after the game has been finished.
        /// </summary>
        public void GameOverMessage()
        {
            Console.WriteLine("\n" + "You will be sent to main menu ater 5 seconds.");
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Message to display rules for customazing the grid.
        /// </summary>
        public void CustomGameGridRulesMessage()
        {
            Console.WriteLine("\n" + "To move use ARROWS. To make cell live use SPACE. To stop setting field use ENTER.");
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
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", $"Count of live games: {liveGamesCount}"));
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", $"Total count of live cells: {totalLiveCellsCount}"));
            Console.WriteLine();
        }
    }
}
