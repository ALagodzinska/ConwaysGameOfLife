namespace ConwaysGameOfLife
{
    /// <summary>
    /// Containes methods that apply game rules and logic.
    /// </summary>
    public class GameLogic
    {
        GameDataSerializer dataSerializer = new GameDataSerializer();
        UserOutput userOutput = new UserOutput();     

        /// <summary>
        /// Draws grid based on the cells stored in the grid.
        /// </summary>
        /// <param name="grid">Game grid</param>
        public void DrawGrid(Grid grid)
        {  
            Console.Clear();

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
        /// Draws one cell on the grid in certain style, depending on whether it is alive or not.
        /// </summary>
        /// <param name="isLive">Cell parameter that tells if it is live or not</param>
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
        /// <param name="grid">Game grid</param>
        public void CreateRandomGrid(Grid grid)
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
            
            DrawGrid(grid);
            userOutput.MessageAfterEachIteration(0, CountOfLiveCells(grid));
        }

        /// <summary>
        /// Allow to move coursor around field and change dead cells to live.
        /// </summary>
        /// <param name="grid">Game grid</param>
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
        /// Counts all live neighbours of the cell.
        /// </summary>
        /// <param name="cell">One cell from grid</param>
        /// <param name="grid">Game grid</param>
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
        /// Apply all game rules for one cell, and mark if it is going to change in next iteration.
        /// </summary>
        /// <param name="cell">One cell from game grid</param>
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
        }

        /// <summary>
        /// Playing game until user wants to stop it
        /// </summary>
        /// <param name="grid">Game Grid</param>
        public void PlayGame(Grid grid)
        {
            do
            {
                while (Console.KeyAvailable == false && CountOfLiveCells(grid) != 0)
                {
                    DrawNextGeneration(grid);
                    Console.WriteLine("Press ESC to stop game");
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            //save data on the exit
            if(CountOfLiveCells(grid) != 0)
            {
                dataSerializer.SaveDataToTheFile(grid);
            }            

            userOutput.GameOverMessage();
        }        

        /// <summary>
        /// Drawing grid for next generation
        /// </summary>
        /// <param name="grid">Game Grid</param>
        public void DrawNextGeneration(Grid grid)
        {
            foreach (var cell in grid.Cells)
            {
                CountLiveNeighbours(cell, grid);
            }

            foreach (var cell in grid.Cells)
            {
                ApplyGameRules(cell);
            }

            Thread.Sleep(1000);
            DrawGrid(grid);
            userOutput.MessageAfterEachIteration(grid.IterationCount++, CountOfLiveCells(grid));
        }

        /// <summary>
        /// Count live cells in the game grid.
        /// </summary>
        /// <param name="grid">Game grid</param>
        /// <returns>Count of live cells(int) in the current grid</returns>
        public int CountOfLiveCells(Grid grid)
        {
            return grid.Cells.OfType<Cell>().Where(c => c.IsLive == true).Count();
        }
    }
}
