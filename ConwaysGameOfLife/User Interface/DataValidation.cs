namespace ConwaysGameOfLife
{
    using ConwaysGameOfLife.Entities;

    /// <summary>
    /// Stores methods used for validating user input.
    /// </summary>
    public class DataValidation
    {
        GameData GameData;
        public DataValidation(GameData gameData)
        {
            GameData = gameData;
        }

        /// <summary>
        /// Maximum grid height allowed for playing single game.
        /// </summary>
        const int maxHeightForOneGame = 30;

        /// <summary>
        /// Maximum grid width allowed for playing single game.
        /// </summary>
        const int maxWidthForOneGame = 60;

        /// <summary>
        /// Maximum grid height allowed for playing multiple games.
        /// </summary>
        const int maxHeightForManyGames = 15;

        /// <summary>
        /// Maximum grid width allowed for playing multiple games.
        /// </summary>
        const int maxWidthForManyGames = 20;

        const string enterValidInput = "Please enter valid input!";

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
            while (GameData.FindGameGridByName(gameName) != null || gameName == null)
            {
                Console.WriteLine(@$"That name is taken
{enterValidInput}");
                gameName = Console.ReadLine();
            }

            return gameName;
        }

        /// <summary>
        /// Check if height input meet all requirements.
        /// </summary>
        /// <param name="heightInput">User input for height parameter.</param>
        /// <param name="isMultipleGames">Help to distinguish if it is single or multiple games play. True - multiple grids game. False - One grid game.</param>
        /// <returns>Valid height value.</returns>
        public int GridHeightInput(string heightInput, bool isMultipleGames)
        {
            while (!int.TryParse(heightInput, out validInput) || validInput <= 0
                || isMultipleGames == false && validInput > maxHeightForOneGame
                || isMultipleGames == true && validInput > maxHeightForManyGames)
            {
                var message = isMultipleGames ? $"Height of the field should be less or equals to {maxHeightForManyGames}" : $"Height of the field should be less or equals to {maxHeightForOneGame}";
                Console.WriteLine(message);
                heightInput = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// Check if width input meet all requirements.
        /// </summary>
        /// <param name="widthInput">User input for width parameter.</param>
        /// <param name="isMultipleGames">Help to distinguish if it is single or multiple games play. True - multiple grids game. False - One grid game.</param>
        /// <returns>Valid width value.</returns>
        public int GridWidthInput(string widthInput, bool isMultipleGames)
        {
            while (!int.TryParse(widthInput, out validInput) || validInput <= 0
                || isMultipleGames == false && validInput > maxWidthForOneGame
                || isMultipleGames == true && validInput > maxWidthForManyGames)
            {
                var message = isMultipleGames ? $"Width of the field should be less or equals to {maxWidthForManyGames}" : $"Width of the field should be less or equals to {maxWidthForOneGame}";
                Console.WriteLine(message);
                widthInput = Console.ReadLine();
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
                Console.WriteLine(enterValidInput);
                countOfAllGames = Console.ReadLine();
            }

            return validInput;
        }

        /// <summary>
        /// Check if inputted value is valid integer.
        /// </summary>
        /// <param name="gameToRestore">User inputted to choose game.</param>
        /// <param name="gridList">List of all available games.</param>
        /// <returns>Return valid numeric value that represents game in a list.</returns>
        public int GameToRestore(string gameToRestore, List<Grid> gridList)
        {
            while (!int.TryParse(gameToRestore, out validInput) || validInput <= 0
                || validInput > gridList.Count)
            {
                Console.WriteLine(enterValidInput);
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
                Console.WriteLine(enterValidInput);
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
