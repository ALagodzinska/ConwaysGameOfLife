using ConwaysGameOfLife.Entities;

namespace ConwaysGameOfLife
{
    /// <summary>
    /// Contain methods that apply game rules and logic.
    /// </summary>
    public class GameLogic
    {
        DisplayGame displayGame = new();
        GameData dataSerializer = new();
        UserOutput userOutput = new();

        /// <summary>
        /// Create a random grid with where by random is placed live and dead cells.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        /// <returns>Game grid with randomly placed live and dead cells.</returns>
        public Grid CreateRandomGrid(Grid grid)
        {
            var random = new Random();

            for (int h = 0; h < grid.Height; h++)
            {
                for (int w = 0; w < grid.Width; w++)
                {
                    if (random.Next(2) != 0)
                    {
                        grid.Cells[h, w].IsLive = true;
                    }
                }
            }

            return grid;
        }

        /// <summary>
        /// Allow to move coursor around field and change dead cells to live.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        public void ChooseLiveCells(Grid grid)
        {
            Console.Clear();
            displayGame.DrawGrid(grid);
            userOutput.CustomGameGridRulesMessage();
            bool setField = true;

            Console.SetCursorPosition(0, 0);

            while (setField)
            {
                var startPosition = Console.GetCursorPosition();
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Spacebar:
                        Console.SetCursorPosition(startPosition.Left, startPosition.Top);
                        var cell = grid.Cells[startPosition.Top, startPosition.Left];
                        if (cell != null)
                        {
                            cell.IsLive = !cell.IsLive;

                            displayGame.DrawCell(cell.IsLive);

                            if (cell.Width == grid.Width - 1 || cell.Height == grid.Height - 1)
                            {
                                Console.SetCursorPosition(startPosition.Left, startPosition.Top);
                            }
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (startPosition.Top - 1 < 0)
                        {
                            Console.SetCursorPosition(startPosition.Left, startPosition.Top = grid.Height - 1);
                        }
                        else
                        {
                            Console.SetCursorPosition(startPosition.Left, startPosition.Top - 1);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (startPosition.Top + 1 >= grid.Height)
                        {
                            Console.SetCursorPosition(startPosition.Left, startPosition.Top = 0);
                        }
                        else
                        {
                            Console.SetCursorPosition(startPosition.Left, startPosition.Top + 1);
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (startPosition.Left - 1 < 0)
                        {
                            Console.SetCursorPosition(startPosition.Left = grid.Width - 1, startPosition.Top);
                        }
                        else
                        {
                            Console.SetCursorPosition(startPosition.Left - 1, startPosition.Top);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (startPosition.Left + 1 >= grid.Width)
                        {
                            Console.SetCursorPosition(startPosition.Left = 0, startPosition.Top);
                        }
                        else
                        {
                            Console.SetCursorPosition(startPosition.Left + 1, startPosition.Top);
                        }
                        break;

                    case ConsoleKey.Enter:
                        setField = false;
                        break;

                    default:
                        displayGame.DrawGrid(grid);
                        Console.SetCursorPosition(startPosition.Left, startPosition.Top);
                        userOutput.CustomGameGridRulesMessage();
                        break;
                }
            }

            //clean line with rules
            Console.SetCursorPosition(0, grid.Height + 1);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        /// <summary>
        /// Playing game until user wants to stop it or the game is over.
        /// </summary>
        /// <param name="grid">Game Grid.</param>
        public void PlayGame(Grid grid)
        {
            do
            {
                while (Console.KeyAvailable == false && grid.CountOfLiveCells() != 0 && !grid.CheckIfGridIsSame())
                {
                    ChangeGridForNextIteration(grid);
                    displayGame.DrawNextGeneration(grid);

                    Console.WriteLine("Press ESC to stop game and go back to main menu.");
                }

                Console.WriteLine();
                Console.WriteLine("Game is over!");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            UpdateGridList(grid);

            userOutput.GameOverMessage();
        }

        /// <summary>
        /// Change grid for next iteration.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        public void ChangeGridForNextIteration(Grid grid)
        {
            grid.UncahngedCellsCount = 0;

            foreach (var cell in grid.Cells)
            {
                cell.LiveNeighbours = grid.LiveNeighboursCount(cell);
            }

            foreach (var cell in grid.Cells)
            {
                var cellLife = DecideIfIsLive(cell);

                var isCellChanged = cellLife == cell.IsLive ? 1 : 0;
                grid.UncahngedCellsCount += isCellChanged;

                cell.IsLive = cellLife;
            }

            grid.IterationCount++;
        }

        /// <summary>
        /// Apply all game rules for one cell, and change IsLive status for next iteration.
        /// </summary>
        /// <param name="cell">A cell from a game grid.</param>
        /// <returns>Value for cell IsLive parameter.</returns>
        public bool DecideIfIsLive(Cell cell)
        {
            if ((cell.LiveNeighbours < 2 && cell.IsLive) || (cell.LiveNeighbours > 3 && cell.IsLive))
            {
                //cell is live has one or no neighbour || or four or more neighbours - it dies
                return false;
            }
            else if (cell.LiveNeighbours == 3 && cell.IsLive == false)
            {
                //empty cell with three neighbours - populated
                return true;
            }
            else
            {
                return cell.IsLive;
            }
        }

        /// <summary>
        /// Save stopped game in the list where are stored all previous games. Or if this game has been restored replace with updated data.
        /// </summary>
        /// <param name="grid">A played game grid.</param>
        public void UpdateGridList(Grid grid)
        {
            var gridList = dataSerializer.ReturnListOfExistingGrids();
            var restoredGridFromTheList = gridList.FirstOrDefault(g => g.GameName == grid.GameName);

            if (restoredGridFromTheList != null)
            {
                restoredGridFromTheList = grid;
                if (grid.CountOfLiveCells() == 0 || grid.CheckIfGridIsSame())
                {
                    gridList.Remove(restoredGridFromTheList);
                }
            }
            else
            {
                if (grid.CountOfLiveCells() != 0 && !grid.CheckIfGridIsSame())
                {
                    gridList.Add(grid);
                }
            }
        }

        /// <summary>
        /// Creates multiple random game fields based on inputted user data.
        /// </summary>
        /// <param name="gridParameters">Grid parameters to create multiple random game grids.</param>
        /// <param name="gameCount">Count of the grids to create.</param>
        /// <returns>List of created game grids.</returns>
        public List<Grid> MultipleGridList(GridOptions gridParameters, int gameCount)
        {
            var multipleGameList = new List<Grid>();

            for (int i = 0; i < gameCount; i++)
            {
                var newGrid = Grid.CreateNewGrid(gridParameters.Height, gridParameters.Width, gridParameters.GameName + i.ToString());
                var randomGrid = CreateRandomGrid(newGrid);
                multipleGameList.Add(randomGrid);
            }

            return multipleGameList;
        }

        /// <summary>
        /// Play games in parallel.
        /// </summary>
        /// <param name="multipleGameList">List of games to play at the same time.</param>
        public void PlayMultipleGames(List<Grid> multipleGameList)
        {
            userOutput.MultipleGamesIntro(multipleGameList);
            var listOfGamesToShow = ListOfSelectedGameGrids(multipleGameList);
            Console.Clear();

            do
            {
                while (Console.KeyAvailable == false && multipleGameList.Count != 0)
                {
                    foreach (var game in multipleGameList)
                    {
                        ChangeGridForNextIteration(game);
                    }

                    displayGame.DrawMultipleGrids(listOfGamesToShow);                    

                    multipleGameList.RemoveAll(g => g.CountOfLiveCells() == 0 || g.CheckIfGridIsSame());

                    userOutput.MessageForMultipleGames(multipleGameList.Count, TotalOfLiveCells(multipleGameList));
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Spacebar);

            var isExit = multipleGameList.Count > 0 ? userOutput.DecisionOnStop(multipleGameList) : true;

            if (isExit)
            {
                foreach (var game in multipleGameList)
                {
                    UpdateGridList(game);
                }

                Console.Clear();

                userOutput.GameOverMessage();
            }
            else
            {
                PlayMultipleGames(multipleGameList);
            }
        }

        /// <summary>
        /// Select games to display and store them in a list.
        /// </summary>
        /// <param name="multipleGamesList">List of all games played in parallel.</param>
        /// <returns>List of games to show on a screen.</returns>
        public List<Grid> ListOfSelectedGameGrids(List<Grid> multipleGamesList)
        {
            var chosenGames = userOutput.ChooseMultipleGames(multipleGamesList.Count);
            var games = new List<Grid>();

            foreach (var gameNumber in chosenGames)
            {
                var foundGame = multipleGamesList[gameNumber - 1];
                if (foundGame != null)
                {
                    games.Add(foundGame);
                }
            }

            return games;
        }

        /// <summary>
        /// Count total number of live cells in multiple games.
        /// </summary>
        /// <param name="multipleGamesList">List of the games played.</param>
        /// <returns>Count of live cells in all games.</returns>
        public int TotalOfLiveCells(List<Grid> multipleGamesList) => multipleGamesList.Sum(game => game.CountOfLiveCells());
    }
}