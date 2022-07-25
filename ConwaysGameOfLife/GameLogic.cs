using System.Diagnostics;

namespace ConwaysGameOfLife
{
    /// <summary>
    /// Contain methods that apply game rules and logic.
    /// </summary>
    public class GameLogic
    {
        UserOutput userOutput = new();
        GameDataSerializer dataSerializer = new();

        /// <summary>
        /// Draw grid based on the cells stored in the grid.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        public void DrawGrid(Grid grid)
        {
            Console.SetCursorPosition(0, 0);

            for (int h = 0; h < grid.Height; h++)
            {
                for (int w = 0; w < grid.Width; w++)
                {
                    Console.SetCursorPosition(w, h);
                    DrawCell(grid.Cells[h, w].IsLive);
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Draw one cell on the grid in certain style, depending on whether it is alive or not.
        /// </summary>
        /// <param name="isLive">Cell parameter that note if cell is live or not.</param>
        public void DrawCell(bool isLive)
        {
            string cellSymbol = isLive ? "*" : "-";
            Console.BackgroundColor = isLive ? ConsoleColor.Red : ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(cellSymbol);
            Console.ResetColor();
        }

        /// <summary>
        /// Create a random grid with where by random is placed live and dead cells.
        /// </summary>
        /// <param name="grid">Game grid.</param>
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

        public void DisplayRandomGrid(Grid grid)
        {
            Console.Clear();
            DrawGrid(grid);
            userOutput.MessageAfterEachIteration(grid);
        }

        /// <summary>
        /// Allow to move coursor around field and change dead cells to live.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        public void ChooseLiveCells(Grid grid)
        {
            Console.Clear();
            DrawGrid(grid);
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

                            DrawCell(cell.IsLive);

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
                        DrawGrid(grid);
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
        /// Apply all game rules for one cell, and change IsLive status for next iteration.
        /// </summary>
        /// <param name="cell">A cell from game grid.</param>
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
        /// Playing game until user wants to stop it or the game is over.
        /// </summary>
        /// <param name="grid">Game Grid.</param>
        public void PlayGame(Grid grid)
        {
            do
            {
                while (Console.KeyAvailable == false && grid.CountOfLiveCells() != 0 && !CheckIfGridIsSame(grid))
                {                   

                    DrawNextGeneration(grid);                    

                    Console.WriteLine("Press ESC to stop game");
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            UpdateGridList(grid);

            userOutput.GameOverMessage();
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
                if (grid.CountOfLiveCells() == 0 || CheckIfGridIsSame(grid))
                {
                    gridList.Remove(restoredGridFromTheList);
                }
            }
            else
            {
                if (grid.CountOfLiveCells() != 0 && !CheckIfGridIsSame(grid))
                {
                    gridList.Add(grid);
                }
            }
        }

        /// <summary>
        /// Check if unchanged cells count is the same with cells count in a grid. If all cells stay unchanged - game is over.
        /// </summary>
        /// <param name="grid">A game grid.</param>
        /// <returns>True if all cells stayed the same, false if grid have been changed.</returns>
        public bool CheckIfGridIsSame(Grid grid)
        {
            return grid.UncahngedCellsCount == grid.Height * grid.Width ? true : false;
        }

        /// <summary>
        /// Draw grid for next generation.
        /// </summary>
        /// <param name="grid">Game Grid.</param>
        public void DrawNextGeneration(Grid grid)
        {
            grid.UncahngedCellsCount = 0;

            ChangeGridForNextIteration(grid);

            Thread.Sleep(1000);
            DrawGrid(grid);

            int[] startCoordinates = { 0, 0 };
            userOutput.CleanLiveCellsCount(grid, startCoordinates);

            userOutput.MessageAfterEachIteration(grid);
        }

        public void ChangeGridForNextIteration(Grid grid)
        {
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

        public void PlayMultipleGames(List<Grid> multipleGameList)
        {
            Console.Clear();
            Console.WriteLine("Press ESC to stop games");
            Console.WriteLine($"You choose to play {multipleGameList.Count}");
            var listOfGamesToShow = ListOfSelectedGameGrids(multipleGameList);
            Console.Clear();
            do
            {
                while (Console.KeyAvailable == false)
                {
                    foreach (var game in multipleGameList)
                    {
                        ChangeGridForNextIteration(game);
                    }

                    DrawMultipleGrids(listOfGamesToShow);
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            userOutput.GameOverMessage();
        }

        public void DrawMultipleGrids(List<Grid> grids)
        {
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, 0);
            var gameCountInOneRow = HowManyGamesCanBeInOneRow(grids.First());

            foreach (var grid in grids)
            {
               var startPoint = StartPointForGrid(grid, grids.IndexOf(grid), gameCountInOneRow);
                DrawOneOfMultipleGrids(grid, startPoint);
            }            
        }

        public int HowManyGamesCanBeInOneRow(Grid grid)
        {
            var windowWidth = Console.WindowWidth;
            var paddingLeft = grid.Width + 10;
            return (windowWidth / paddingLeft);
        }

        public int[] StartPointForGrid(Grid grid, int gridIndex, int gameCountInOneRow)
        {
            
            int[] startCoordinates = new int[2];

            var paddingLeft = grid.Width + 10;

            var paddingTop = grid.Height + 5;
            var topPositionCount = gridIndex / gameCountInOneRow;

            startCoordinates[0] = paddingLeft * (gridIndex - (gameCountInOneRow * topPositionCount));
            startCoordinates[1] = paddingTop * topPositionCount;

            return startCoordinates;
        }

        public void DrawOneOfMultipleGrids(Grid grid, int[] startCoordinates)
        {
            var offsetFromLeft = startCoordinates[0];
            var offsetFromTop = startCoordinates[1];
            for (int h = offsetFromTop; h < (grid.Height + offsetFromTop); h++)
            {
                for (int w = offsetFromLeft; w < (grid.Width + offsetFromLeft); w++)
                {
                    Console.SetCursorPosition(w, h);
                    DrawCell(grid.Cells[(h - offsetFromTop), (w - offsetFromLeft)].IsLive);
                }

                Console.WriteLine();
            }

            //if width is too small will overlap game grid
            userOutput.CleanLiveCellsCount(grid, startCoordinates);
            userOutput.MultipleGameMessageAfterIteration(grid, startCoordinates);
        }

        /// <summary>
        /// Creates random game fields based on inoutted user data.
        /// </summary>
        /// <param name="grid">Grid where are saved base data to create multiple games.</param>
        /// <param name="gameCount">Count of the games that will play in parallel.</param>
        public List<Grid> MultipleGridList(GridOptions gridParameters, int gameCount)
        {
            var multipleGameList = new List<Grid>();

            for (int i = 0; i <= gameCount; i++)
            {
                var newGrid = Grid.CreateNewGrid(gridParameters.Height, gridParameters.Width, gridParameters.GameName + i.ToString());
                var randomGrid = CreateRandomGrid(newGrid);
                multipleGameList.Add(randomGrid);
            }

            return multipleGameList;
        }

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
    }
}
