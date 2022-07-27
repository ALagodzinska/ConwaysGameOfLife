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
        /// Show created random grid.
        /// </summary>
        /// <param name="grid">Game grid to display.</param>
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
        /// Playing game until user wants to stop it or the game is over.
        /// </summary>
        /// <param name="grid">Game Grid.</param>
        public void PlayGame(Grid grid)
        {
            do
            {
                while (Console.KeyAvailable == false && grid.CountOfLiveCells() != 0 && !grid.CheckIfGridIsSame())
                {
                    DrawNextGeneration(grid);

                    Console.WriteLine("Press ESC to stop game and go back to main menu.");
                }

                Console.WriteLine();
                Console.WriteLine("Game is over!");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            UpdateGridList(grid);

            userOutput.GameOverMessage();
        }

        /// <summary>
        /// Draw grid for next generation.
        /// </summary>
        /// <param name="grid">Game Grid.</param>
        public void DrawNextGeneration(Grid grid)
        {
            ChangeGridForNextIteration(grid);

            Thread.Sleep(1000);
            DrawGrid(grid);

            //clean live cells value
            int[] startCoordinates = { 0, 0 };
            userOutput.CleanLiveCellsCount(grid, startCoordinates);

            userOutput.MessageAfterEachIteration(grid);
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
                while (Console.KeyAvailable == false)
                {
                    foreach (var game in multipleGameList)
                    {
                        ChangeGridForNextIteration(game);
                    }

                    DrawMultipleGrids(listOfGamesToShow);

                    userOutput.MessageForMultipleGames(multipleGameList.Count, TotalOfLiveCells(multipleGameList));

                    multipleGameList.RemoveAll(g => g.CountOfLiveCells() == 0 || g.CheckIfGridIsSame());                    
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Spacebar);

            var isExit = userOutput.DecisionOnStop(multipleGameList);

            if (isExit)
            {
                foreach(var game in multipleGameList)
                {
                    UpdateGridList(game);
                }
                
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
        /// Display multiple game grids on one screen.
        /// </summary>
        /// <param name="grids">List of games to play at the same time.</param>
        public void DrawMultipleGrids(List<Grid> grids)
        {
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, 0);

            //used to make padding between games, not only based on grid size, but also on message length.
            var commonLengthOfMessage = $"Game name: {grids.First().GameName}".Length + 3;
            var startPointForPadding = Math.Max(commonLengthOfMessage, grids.First().Width);

            var gameCountInOneRow = HowManyGamesCanBeInOneRow(startPointForPadding);

            foreach (var grid in grids)
            {
                var startPoint = StartPointForGrid(grid, grids.IndexOf(grid), gameCountInOneRow, startPointForPadding);

                DrawOneOfMultipleGrids(grid, startPoint);

                if (grid.CountOfLiveCells() == 0 || grid.CheckIfGridIsSame())
                {
                    //display game over message above game grid.
                    Console.SetCursorPosition(startPoint[0], startPoint[1] - 1);
                    Console.Write("GameOver!");
                }               
            }
        }

        /// <summary>
        /// Counts how many playing grids fit on a certain screen width.
        /// </summary>
        /// <param name="startPointForLeftPadding">Width to back off for drawing next grid.</param>
        /// <returns>Count of grids that fit in such screen width.</returns>
        public int HowManyGamesCanBeInOneRow(int startPointForLeftPadding)
        {
            var windowWidth = Console.WindowWidth;
            var paddingLeft = startPointForLeftPadding + 10;

            return (windowWidth / paddingLeft);
        }

        /// <summary>
        /// Finds start coordinates to start drawing grid on multiple games screen.
        /// </summary>
        /// <param name="grid">A grid to draw.</param>
        /// <param name="gridIndex">Index number of this grid in a list.</param>
        /// <param name="gameCountInOneRow">Maimum games count in one row.</param>
        /// <param name="startPointForLeftPadding">Start point for left padding.</param>
        /// <returns>Array of coordinates. First coordinate is left, second is top.</returns>
        public int[] StartPointForGrid(Grid grid, int gridIndex, int gameCountInOneRow, int startPointForLeftPadding)
        {
            int[] startCoordinates = new int[2];

            var paddingLeft = startPointForLeftPadding + 10;

            var paddingTop = grid.Height + 5;
            var topPositionCount = gridIndex / gameCountInOneRow;

            startCoordinates[0] = paddingLeft * (gridIndex - (gameCountInOneRow * topPositionCount));
            //+6 to display on top of the screen statistics about games.
            startCoordinates[1] = paddingTop * topPositionCount + 6;

            return startCoordinates;
        }

        /// <summary>
        /// Display on a screen onre of game grids on a multiple games field.
        /// </summary>
        /// <param name="grid">Grid to draw.</param>
        /// <param name="startCoordinates">Array of coordinates to start drawing. [0]-left, [1]-top.</param>
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

            userOutput.CleanLiveCellsCount(grid, startCoordinates);
            userOutput.MultipleGameMessageAfterIteration(grid, startCoordinates);
        }

        /// <summary>
        /// Count total number of live cells in multiple games.
        /// </summary>
        /// <param name="multipleGamesList">List of the games played.</param>
        /// <returns>Count of live cells in all games.</returns>
        public int TotalOfLiveCells(List<Grid> multipleGamesList)
        {
            var totalLiveCellsCount = 0;

            foreach(var game in multipleGamesList)
            {
                totalLiveCellsCount += game.CountOfLiveCells();
            }

            return totalLiveCellsCount;
        }
    }
}
