namespace ConwaysGameOfLife
{
    /// <summary>
    /// Class responsible for displaying information to user.
    /// </summary>
    public class UserOutput
    {
        GameDataSerializer dataSerializer = new GameDataSerializer();

        /// <summary>
        /// Field that stores input if it meet all requirements.
        /// </summary>
        int validInput;

        /// <summary>
        /// Shows menu to user, where are listed actions that user is able to choose to play or exit the game. 
        /// </summary>
        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Hello, Welcome to the 'Game Of Life'!");
            Console.WriteLine();
            Console.WriteLine("Please choose action what you want to do?(INPUT NUMBER)");
            Console.WriteLine("1. Play Game: Create Random field");
            Console.WriteLine("2. Play Game: Create Customized field");
            Console.WriteLine("3. Restore Game: Continue to play one of the previous games");
            Console.WriteLine("4. Exit Game");
        }

        /// <summary>
        /// Display to user what values should be inputted and processes data input.
        /// </summary>
        /// <param name="listOfExsistingGrids">List of the saved grids</param>
        /// <returns>New grid</returns>
        public Grid CreateGameGridFromUserInput(List<Grid> listOfExsistingGrids)
        {
            Console.Clear();
            Console.WriteLine("Please Remember that MAX Height is 30 and MAX Width is 120");
            Console.WriteLine();

            Console.WriteLine("Create name for this game:");
            var gameNameInput = Console.ReadLine();
            var validName = CheckForValidGameNameOnCreate(gameNameInput, listOfExsistingGrids);

            Console.WriteLine("Input height of field:");
            var heightInput = Console.ReadLine();
            var validHeight = CheckForValidNumberInput(heightInput, "height");

            Console.WriteLine("Input width of field:");
            var widthInput = Console.ReadLine();
            var validWidth = CheckForValidNumberInput(widthInput, "width");

            return Grid.CreateNewGrid(validHeight, validWidth, validName);
        }

        /// <summary>
        /// Check if inputted numeric value is valid and if it is not asks for valid input.
        /// </summary>
        /// <param name="numberInput">The string value that was inputted by user</param>
        /// <param name="typeOfInput">Allows to understand what type of input it is(height or width)</param>
        /// <returns>Returns an integer after correct input.</returns>
        public int CheckForValidNumberInput(string numberInput, string typeOfInput)
        {
            while (!int.TryParse(numberInput, out validInput) || validInput <= 0
                || typeOfInput == "height" && validInput > 30
                || typeOfInput == "width" && validInput > 120)
            {
                Console.WriteLine("Please enter valid input!");
                numberInput = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// On creating the new game check if inputted game name already exsists. If name is taken asks for new game name.
        /// </summary>
        /// <param name="gameName">Inputted name for the grid</param>
        /// /// <param name="gridList">List of the saved grids</param>
        /// <returns>Return valid(not taken) name for game grid</returns>
        public string CheckForValidGameNameOnCreate(string gameName, List<Grid> gridList)
        {
            while (dataSerializer.FindGameGridByName(gameName, gridList) != null ||
                gameName == null)
            {
                Console.WriteLine("That name is taken" + "\n" + "Please enter valid input!");
                gameName = Console.ReadLine();
            }

            return gameName;
        }

        /// <summary>
        /// Check if the inputted name matches one of the saved game names. Return name only if there is a match.
        /// </summary>
        /// <param name="gameName">Inputted name for the grid</param>
        /// <param name="gridList">List of the saved grids</param>
        /// <returns>Return valid name(exsisting name of the grid from the saved grid list) for game grid</returns>
        public string CheckValidGameNameInputOnSearch(string gameName, List<Grid> gridList)
        {
            while (dataSerializer.FindGameGridByName(gameName, gridList) == null)
            {
                Console.WriteLine("There is no game with such name" + "\n" + "Please enter one of the names from the list!");
                gameName = Console.ReadLine();
            }

            return gameName;
        }

        /// <summary>
        /// Shows to user all names of the grids that are stored in a list.
        /// </summary>
        /// <param name="gridList">List of the saved game grids</param>
        public void DisplayGamesForUser(List<Grid> gridList)
        {
            Console.Clear();

            int numberInList = 1;

            Console.WriteLine("Please choose one of the games from the list" + "\n" + "List of saved games:");
            Console.WriteLine();

            foreach (var grid in gridList)
            {
                Console.WriteLine($"{numberInList}. {grid.GameName} : Iteration count - {grid.IterationCount}");
                numberInList++;
            }
        }

        /// <summary>
        /// Get game grid which user want to restore.
        /// </summary>
        /// <param name="gridList">List of the saved game grids</param>
        /// <returns>One game grid</returns>
        public Grid RestoreGameFromUserInput(List<Grid> gridList)
        {
            Console.WriteLine();
            Console.WriteLine("Please input NAME of the game you want to restore.");
            var userInputtedName = Console.ReadLine();

            var exsistingdGame = CheckValidGameNameInputOnSearch(userInputtedName, gridList);

            return dataSerializer.FindGameGridByName(exsistingdGame, gridList);
        }

        /// <summary>
        /// Message that is showed to user after the game has been finished.
        /// </summary>
        public void GameOverMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Game is over! You will be sent to main menu ater 5 seconds");
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Message to display rules for customazing the grid.
        /// </summary>
        public void CustomGameGridRulesMessage()
        {
            Console.WriteLine();
            Console.WriteLine("To move use ARROWS. To make cell live use SPACE. To stop setting field use ENTER.");
        }

        /// <summary>
        /// Displays count of iteration and count of live cells to user.
        /// </summary>
        /// <param name="iterationCount">Count of game iteration</param>
        /// <param name="liveCellsCount">Count of live cells on a grid at this iteration</param>
        public void MessageAfterEachIteration(int iterationCount, int liveCellsCount)
        {
            Console.WriteLine();
            Console.WriteLine($"Count of iteration: {iterationCount}");
            Console.WriteLine($"Count of live cells: {liveCellsCount}");
        }
    }
}
