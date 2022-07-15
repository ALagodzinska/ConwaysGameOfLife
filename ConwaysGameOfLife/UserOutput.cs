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
        /// <returns>New grid</returns>
        public Grid CreateGameGridFromUserInput(List<Grid> exsistingGrids)
        {
            Console.Clear();
            Console.WriteLine("Please Remember that MAX Height is 30 and MAX Width is 120");
            Console.WriteLine();

            Console.WriteLine("Create name for this game:");
            var gameNameInput = Console.ReadLine();
            var validName = CheckForValidGameNameOnCreate(gameNameInput, exsistingGrids);            

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
        /// Check if inputted game name already exsists and if it is not empty input.
        /// </summary>
        /// <param name="gameNameInput">Name of the game from user</param>
        /// <returns>Return valid name</returns>
        public string CheckForValidGameNameOnCreate(string gameNameInput, List<Grid> gridList)
        {
            dataSerializer.ReadDataFromTheFile();
            while (dataSerializer.FindGameGridByName(gameNameInput, gridList) != null ||
                gameNameInput == null)
            {
                Console.WriteLine("That name is taken" + "\n" + "Please enter valid input!");
                gameNameInput = Console.ReadLine();
            }
            return gameNameInput;
        }

        public string CheckValidGameNameInput(string gameName, List<Grid> gridList)
        {
            dataSerializer.ReadDataFromTheFile();
            while (dataSerializer.FindGameGridByName(gameName, gridList) == null)
            {
                Console.WriteLine("There is no game with such name" + "\n" + "Please enter one of the names from the list!");
                gameName = Console.ReadLine();
            }
            return gameName;
        }

        public void DisplayGamesForUser(List<Grid> gridList)
        {
            Console.Clear();
            int numberInList = 1;
            Console.WriteLine("Please choose one of the games from the list by typing its name" + "\n" + "List of saved games:");
            Console.WriteLine();
            foreach(var grid in gridList)
            {
                Console.WriteLine($"{numberInList}. {grid.GameName} : Iteration count - {grid.IterationCount}");
                numberInList++;
            }
        }

        public Grid RestoreGameFromUserInput(List<Grid> gridList)
        {
            Console.WriteLine();
            Console.WriteLine("Please input name of the game you want to restore.");
            var userInputtedName = Console.ReadLine();
            var exsistingdGame = CheckValidGameNameInput(userInputtedName, gridList);
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
