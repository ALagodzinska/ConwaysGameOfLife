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
        /// Show menu to user, where are listed actions that user is able to choose to play or exit the game. 
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
        /// Display to user what values should be inputted and processes data input.
        /// </summary>
        /// <returns>New grid.</returns>
        public Grid CreateGameGridFromUserInput()
        {
            Console.Clear();
            Console.WriteLine("Please Remember that MAX Height is 30 and MAX Width is 60" + "\n");

            Console.WriteLine("Create name for this game:");
            var gameNameInput = Console.ReadLine();
            var validName = CheckForValidGameNameOnCreate(gameNameInput);

            Console.WriteLine("Input height of field:");
            var heightInput = Console.ReadLine();
            var validHeight = CheckForValidDimensionInput(heightInput, "height");

            Console.WriteLine("Input width of field:");
            var widthInput = Console.ReadLine();
            var validWidth = CheckForValidDimensionInput(widthInput, "width");

            return Grid.CreateNewGrid(validHeight, validWidth, validName);
        }

        public int GameCountInput()
        {
            Console.Clear();
            Console.WriteLine("How many games you want to play?");
            Console.WriteLine("Input number from 2 to 1000");

            var gameCountInput = Console.ReadLine();
            var validGameCountInput = CheckForValidGameCountInput(gameCountInput);

            return validGameCountInput;
        }

        

        public int CheckForValidGameCountInput(string gameCountInput)
        {
            while (!int.TryParse(gameCountInput, out validInput) || validInput <= 0
                || validInput < 2
                || validInput > 1000)
            {
                Console.WriteLine("Please enter valid input!");
                gameCountInput = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// Check if inputted numeric value is valid and if it is not asks for valid input.
        /// </summary>
        /// <param name="numberInput">The string value that was inputted by user.</param>
        /// <param name="typeOfInput">Allows to understand what type of input it is(height or width). Used for validation.</param>
        /// <returns>Returns an integer after correct input.</returns>
        public int CheckForValidDimensionInput(string numberInput, string typeOfInput)
        {
            while (!int.TryParse(numberInput, out validInput) || validInput <= 0
                || typeOfInput == "height" && validInput > 30
                || typeOfInput == "width" && validInput > 60)
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
        public string CheckForValidGameNameOnCreate(string gameName)
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
        public string CheckValidGameNameInputOnSearch(string gameName)
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

            var existingGame = CheckValidGameNameInputOnSearch(userInputtedName);

            return dataSerializer.FindGameGridByName(existingGame);
        }

        /// <summary>
        /// Message that is showed to user after the game has been finished.
        /// </summary>
        public void GameOverMessage()
        {
            Console.WriteLine("\n" + "Game is over! You will be sent to main menu ater 5 seconds");
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
        /// <param name="iterationCount">Count of game iteration.</param>
        /// <param name="liveCellsCount">Count of live cells on a grid at this iteration.</param>
        public void MessageAfterEachIteration(int iterationCount, int liveCellsCount)
        {
            Console.WriteLine("\n" + $"Count of iteration: {iterationCount}");
            Console.WriteLine($"Count of live cells: {liveCellsCount}");
        }
        
        public void ShowGamesStatistics(List<Grid> gridList)
        {
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine($"Played games:{gridList.Count()}");
            Console.WriteLine($"Live cells count in total:");
        }
    }
}
