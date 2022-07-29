using ConwaysGameOfLife.Entities;

namespace ConwaysGameOfLife
{
    public class DataValidation
    {
        GameData gameData = new();

        const int maxHeightForOneGame = 30;
        const int maxWidthForOneGame = 60;

        const int maxHeightForManyGames = 15;
        const int maxWidthForManyGames = 20;

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
            while (gameData.FindGameGridByName(gameName) != null || gameName == null)
            {
                Console.WriteLine(@"That name is taken
Please enter valid input!");
                gameName = Console.ReadLine();
            }

            return gameName;
        }

        public int GridHeightInput(string numberInput, bool isMultipleGames)
        {
            while (!int.TryParse(numberInput, out validInput) || validInput <= 0
                || isMultipleGames == false && validInput > maxHeightForOneGame
                || isMultipleGames == true && validInput > maxHeightForManyGames)
            {
                var message = isMultipleGames ? $"Height of the field should be less or equals to {maxHeightForManyGames}" : $"Height of the field should be less or equals to {maxHeightForOneGame}";
                Console.WriteLine(message);
                numberInput = Console.ReadLine();
            }

            return validInput;
        }

        public int GridWidthInput(string numberInput, bool isMultipleGames)
        {
            while (!int.TryParse(numberInput, out validInput) || validInput <= 0
                || isMultipleGames == false && validInput > maxWidthForOneGame
                || isMultipleGames == true && validInput > maxWidthForManyGames)
            {
                var message = isMultipleGames ? $"Width of the field should be less or equals to {maxWidthForManyGames}" : $"Width of the field should be less or equals to {maxWidthForOneGame}";
                Console.WriteLine(message);
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
