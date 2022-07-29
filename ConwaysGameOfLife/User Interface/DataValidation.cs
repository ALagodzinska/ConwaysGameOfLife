using ConwaysGameOfLife.Entities;

namespace ConwaysGameOfLife
{
    public class DataValidation
    {
        GameData gameData = new();

        /// <summary>
        /// Additional parameter to apply validation for grid height when playing one game.
        /// </summary>
        const string heightOneGame = "height";

        /// <summary>
        /// Additional parameter to apply validation for grid width when playing one game.
        /// </summary>
        const string widthOneGame = "width";

        /// <summary>
        /// Additional parameter to apply validation for grid height when playing multiple games.
        /// </summary>
        const string heightManyGames = "multipleGameHeight";

        /// <summary>
        /// Additional parameter to apply validation for grid width when playing multiple games.
        /// </summary>
        const string widthManyGames = "multipleGameWidth";

        const string exitChoise = "exit";

        const string continueChoise = "continue";

        /// <summary>
        /// Field that stores number input if it meet all requirements.
        /// </summary>
        int validInput;

        /// <summary>
        /// On creating the new game check if inputted game name already exsists. If name is taken asks for new game name.
        /// </summary>
        /// <param name="gameName">Inputted name for the grid.</param>
        /// <returns>Return valid(not taken) name for game grid.</returns>
        public string GameNameOnCreate(string gameName)
        {
            while (gameData.FindGameGridByName(gameName) != null ||
                gameName == null || gameName.ToLower() == exitChoise)
            {
                Console.WriteLine(@"That name is taken
Please enter valid input!");
                gameName = Console.ReadLine();
            }

            return gameName;
        }

        /// <summary>
        /// Check if inputted numeric value for grid dimensions is valid. If not asks for valid input.
        /// </summary>
        /// <param name="numberInput">Parameter for grid that was inputted by user.</param>
        /// <param name="typeOfInput">Allows to understand what type of input it is(height or width). Used for validation.</param>
        /// <returns>Returns a valid integer - Grid parameter.</returns>
        public int DimensionInput(string numberInput, string typeOfInput)
        {
            while (!int.TryParse(numberInput, out validInput) || validInput <= 0
                || typeOfInput == heightOneGame && validInput > 30
                || typeOfInput == widthOneGame && validInput > 60
                || typeOfInput == widthManyGames && validInput > 20
                || typeOfInput == heightManyGames && validInput > 15)
            {
                Console.WriteLine("Please enter valid input!");
                numberInput = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// Check if inputted numeric value for games to play in parallel is valid. If not asks for valid input.
        /// </summary>
        /// <param name="countOfAllGames">User input for count of all games played in parallel.</param>
        /// <returns>Returns an valid count of games to play in parallel.</returns>
        public int GamesCountToPlay(string countOfAllGames)
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

        public int GameToRestore(string gameToRestore, List<Grid> gridList)
        {
            while (!int.TryParse(gameToRestore, out validInput) || validInput <= 0
                || validInput > gridList.Count)
            {
                Console.WriteLine("Please enter valid input!");
                gameToRestore = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// Check if inputted numeric value for games to display is valid. If not asks for valid input.
        /// </summary>
        /// <param name="gamesCountInput">Number of the game that user wants to see on screen.</param>
        /// <param name="countOfAllGames">Count of all games played in parallel.</param>
        /// <returns>Returns a valid count of games to display on screen.</returns>
        public int SelectedGamesCount(string gamesCountInput, int countOfAllGames)
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
        /// Check if inputted numeric value is valid, is included in boundaries of chosen game list. If not asks for valid input.
        /// </summary>
        /// <param name="gameNumberInput">Serial number of game to choose from list.</param>
        /// <param name="countOfAllGames">Count of all games in a list</param>
        /// <returns>Returns valid game numeric identifier.</returns>
        public int GameNumber(string gameNumberInput, int countOfAllGames, int[] gamesToShowArray)
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
    }
}
