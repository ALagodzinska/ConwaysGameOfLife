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
        /// Stores the number of cells that didnt change on each iteration.
        /// </summary>
        int unchangedCellsCount;

        int startLeftPoint = 0;

        int startTopPoint = 0;

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
            DrawGrid(grid);
            userOutput.MessageAfterEachIteration(0, CountOfLiveCells(grid));
        }

        /// <summary>
        /// Allow to move coursor around field and change dead cells to live.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        public void ChooseLiveCells(Grid grid)
        {
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
        }

        /// <summary>
        /// Count all live neighbours of the cell.
        /// </summary>
        /// <param name="cell">A cell from grid.</param>
        /// <param name="grid">Game grid.</param>
        public void CountLiveNeighbours(Cell cell, Grid grid)
        {
            var liveCellsCount = 0;

            for (int h = cell.Height - 1; h < cell.Height + 2; h++)
            {
                for (int w = cell.Width - 1; w < cell.Width + 2; w++)
                {
                    if (h >= grid.Height || h < 0
                        || w >= grid.Width || w < 0
                        || (h == cell.Height && w == cell.Width))
                    {
                        continue;
                    }

                    var foundCell = grid.Cells[h, w];

                    if (foundCell.IsLive)
                    {
                        liveCellsCount++;
                    }
                }
            }

            cell.LiveNeighbours = liveCellsCount;
        }

        /// <summary>
        /// Apply all game rules for one cell, and change IsLive status for next iteration.
        /// </summary>
        /// <param name="cell">A cell from game grid.</param>
        public void ApplyGameRules(Cell cell)
        {
            if (cell.LiveNeighbours < 2 && cell.IsLive)
            {
                //one or no neighbour - dies
                cell.IsLive = false;
            }
            else if (cell.LiveNeighbours > 3 && cell.IsLive)
            {
                //four or more - dies
                cell.IsLive = false;
            }
            else if (cell.LiveNeighbours == 3 && cell.IsLive == false)
            {
                //empty cell with three neighbours - populated
                cell.IsLive = true;
            }
            else
            {
                unchangedCellsCount++;
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
                while (Console.KeyAvailable == false && CountOfLiveCells(grid) != 0 && !CheckIfGridIsSame(grid))
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
                if (CountOfLiveCells(grid) == 0 || CheckIfGridIsSame(grid))
                {
                    gridList.Remove(restoredGridFromTheList);
                }
            }
            else
            {
                if (CountOfLiveCells(grid) != 0 && !CheckIfGridIsSame(grid))
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
            return unchangedCellsCount == grid.Height * grid.Width ? true : false;
        }

        /// <summary>
        /// Draw grid for next generation.
        /// </summary>
        /// <param name="grid">Game Grid.</param>
        public void DrawNextGeneration(Grid grid)
        {
            unchangedCellsCount = 0;

            ChangeGridForNextIteration(grid);

            Thread.Sleep(1000);
            DrawGrid(grid);
            userOutput.MessageAfterEachIteration(grid.IterationCount, CountOfLiveCells(grid));
        }

        public void ChangeGridForNextIteration(Grid grid)
        {
            foreach (var cell in grid.Cells)
            {
                CountLiveNeighbours(cell, grid);
            }

            foreach (var cell in grid.Cells)
            {
                ApplyGameRules(cell);
            }
            grid.IterationCount++;
        }

        public void PlayMultipleGames(List<Grid> multipleGameList)
        {
            Console.Clear();
            Console.WriteLine("Press ESC to stop games");
            Console.WriteLine($"You choose to play {multipleGameList.Count}");
            var listOfGamesToShow = SelectedGameGrids(multipleGameList);
            Console.Clear();
            do
            {
                while (Console.KeyAvailable == false)
                {
                    foreach (var game in multipleGameList)
                    {
                        ChangeGridForNextIteration(game);
                        //if (CountOfLiveCells(game) == 0)
                        //{
                        //    multipleGameList.Remove(game);
                        //}
                    }

                    DrawMultipleGrids(listOfGamesToShow);
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            //Console.ReadLine();
            userOutput.GameOverMessage();
        }

        public void DrawMultipleGrids(List<Grid> grids)
        {
            Thread.Sleep(1000);
            Console.SetCursorPosition(0,0);
            startLeftPoint = 0;
            foreach (var grid in grids)
            {
                DrawOneOfMultipleGrids(grid);
            }
        }

        public void DrawOneOfMultipleGrids(Grid grid)
        {
            var iterationMessage = $"Iteration count: {grid.IterationCount}";
            var liveCellsMessage = $"Live Cells count: {CountOfLiveCells(grid)}";

            var paddingBetweenGames = 5;
            var messageLength = Math.Max(iterationMessage.Length, liveCellsMessage.Length); // get longest message
            var paddigForGame = Math.Max(messageLength, grid.Width); // get longest between message and grid width

            if (startLeftPoint + grid.Width < Console.WindowWidth)
            {
                for (int h = 0; h < grid.Height; h++)
                {
                    for (int w = startLeftPoint; w < grid.Width + startLeftPoint; w++)
                    {
                        Console.SetCursorPosition(w, h);
                        DrawCell(grid.Cells[h, w - startLeftPoint].IsLive);
                    }

                    Console.WriteLine();
                }

                Console.SetCursorPosition(startLeftPoint, grid.Height + 1);
                Console.Write(iterationMessage);
                Console.SetCursorPosition(startLeftPoint, grid.Height + 2);
                Console.Write(liveCellsMessage);

                startLeftPoint += paddigForGame + paddingBetweenGames;
            }
            else
            {
                startTopPoint = grid.Height + 5;
                startLeftPoint = 0;

                var endTopPoint = grid.Height + startTopPoint;

                for (int h = startTopPoint; h < endTopPoint; h++)
                {
                    for (int w = startLeftPoint; w < grid.Width + startLeftPoint; w++)
                    {
                        Console.SetCursorPosition(w, h);
                        DrawCell(grid.Cells[h - startTopPoint, w - startLeftPoint].IsLive);
                    }

                    Console.WriteLine();
                }

                Console.SetCursorPosition(startLeftPoint, endTopPoint + 1);
                Console.Write(iterationMessage);
                Console.SetCursorPosition(startLeftPoint, endTopPoint + 2);
                Console.Write(liveCellsMessage);

                startLeftPoint += paddigForGame + paddingBetweenGames;
            }

            // ...
        }

        /// <summary>
        /// Creates random game fields based on inoutted user data.
        /// </summary>
        /// <param name="grid">Grid where are saved base data to create multiple games.</param>
        /// <param name="gameCount">Count of the games that will play in parallel.</param>
        public List<Grid> GenerateGridsForMultipleGames(GridOptions gridParameters, int gameCount)
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

        /// <summary>
        /// Count live cells in the game grid.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        /// <returns>Count of live cells(int) in the current grid.</returns>
        public int CountOfLiveCells(Grid grid)
        {
            return grid.Cells.OfType<Cell>().Where(c => c.IsLive == true).Count();
        }

        public List<Grid> SelectedGameGrids(List<Grid> multipleGamesList)
        {
            var chosenGames = userOutput.ChooseMultipleGames(multipleGamesList.Count);
            var games = new List<Grid>();

            foreach (var gameNumber in chosenGames)
            {
                var foundGame = FindGameFromMultipleGameList(gameNumber, multipleGamesList);
                if (foundGame != null)
                {
                    games.Add(foundGame);
                }
            }

            return games;
        }

        public Grid? FindGameFromMultipleGameList(int gameNumber, List<Grid> multipleGameList)
        {
            return multipleGameList[gameNumber--];
        }
    }
}
