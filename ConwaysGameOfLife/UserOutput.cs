namespace ConwaysGameOfLife
{
    /// <summary>
    /// Class responsible for displaying information to user.
    /// </summary>
    public class UserOutput
    {
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
            Console.WriteLine("3. Exit Game");
        }

        /// <summary>
        /// Display to user what values should be inputted and processes data input.
        /// </summary>
        /// <returns>New grid</returns>
        public Grid CreateGameGridFromUserInput()
        {
            Console.Clear();
            Console.WriteLine("Please Remember that MAX Height is 30 and MAX Width is 120");
            Console.WriteLine();

            Console.WriteLine("Input height of field:");
            var heightInput = Console.ReadLine();
            var validHeight = CheckForValidInput(heightInput, "height");

            Console.WriteLine("Input width of field:");
            var widthInput = Console.ReadLine();
            var validWidth = CheckForValidInput(widthInput, "width");

            return Grid.CreateNewGrid(validHeight, validWidth);
        }

        /// <summary>
        /// Check if inputted value is valid and if it is not asks for valid input.
        /// </summary>
        /// <param name="input">The string value that was inputted by user</param>
        /// <param name="typeOfInput">Allows to understand what type of input it is(height or width)</param>
        /// <returns>Returns an integer after correct input.</returns>
        public int CheckForValidInput(string input, string typeOfInput)
        {
            while (!int.TryParse(input, out validInput) || validInput <= 0
                || typeOfInput == "height" && validInput > 30
                || typeOfInput == "width" && validInput > 120)
            {
                Console.WriteLine("Please enter valid input!");
                input = Console.ReadLine();
            }

            return validInput;
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
