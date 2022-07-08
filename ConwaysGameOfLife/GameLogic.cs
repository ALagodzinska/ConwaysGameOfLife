namespace ConwaysGameOfLife
{
    public class GameLogic
    {
        UserOutput userOutput = new UserOutput();

        public List<Cell> cells = new List<Cell>();

        /// <summary>
        /// Create a random grid with where by random is placed live and dead cells.
        /// </summary>
        /// <param name="grid">Game grid</param>
        public void CreateRandomGrid(Grid grid)
        {
            Thread.Sleep(1000);
            Console.Clear();

            var random = new Random();

            for (int h = 0; h < grid.Height; h++)
            {
                for (int w = 0; w < grid.Width; w++)
                {
                    Cell cell = new Cell 
                    { 
                        Height = h,
                        Width = w,
                    };

                    if (random.Next(2) != 0)
                    {
                        cell.IsLive = true;
                        cells.Add(cell);
                        Console.Write("X");
                    }
                    else
                    {
                        cells.Add(cell);
                        Console.Write("O");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Count of iteration: 0");
            Console.WriteLine($"Count of live cells: {CountOfLiveCells()}");
        }

        /// <summary>
        /// Creates an empty game grid with all dead cells, base for marking life cells.
        /// </summary>
        /// <param name="grid">Game grid(including such data as size of the grid)</param>
        public void CreateEmptyGrid(Grid grid)
        {
            Thread.Sleep(1000);
            Console.Clear();

            for (int h = 0; h < grid.Height; h++)
            {
                for (int w = 0; w < grid.Width; w++)
                {
                    Cell cell = new Cell
                    {
                        Height = h,
                        Width = w,
                    };

                    cells.Add(cell);
                    Console.Write("O");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("To move use ARROWS. To make cell live use SPACE. To stop setting field use ENTER.");
        }

        /// <summary>
        /// Allow to move coursor around field and change dead cells to live.
        /// </summary>
        /// <param name="grid">Game grid</param>
        public void ChooseLiveCells(Grid grid)
        {
            CreateEmptyGrid(grid);
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
                        var cell = FindCellByCoordinates(startPosition.Top, startPosition.Left);
                        Console.Write("X");
                        cell.IsLive = true;
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

                        //think of default action to ignore any others keys
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Counts all live neighbours of the cell
        /// </summary>
        /// <param name="cell">One cell from the grid(game field)</param>
        public void CountLiveNeighbours(Cell cell)
        {
            var liveCellsCount = 0;

            for (int h = cell.Height - 1; h < cell.Height + 2; h++)
            {
                for (int w = cell.Width - 1; w < cell.Width + 2; w++)
                {
                    var foundCell = FindCellByCoordinates(h, w);
                    if (foundCell == null || h == cell.Height && w == cell.Width)
                    {
                        continue;
                    }
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
            //one or no neighbour - dies
            if (cell.LiveNeighbours < 2 && cell.IsLive)
            {
                cell.Change = true;
            }
            //four or more - dies
            if (cell.LiveNeighbours > 3 && cell.IsLive)
            {
                cell.Change = true;
            }
            //empty cell with three neighbours - populated
            if (cell.LiveNeighbours == 3 && cell.IsLive == false)
            {
                cell.Change = true;
            }
        }

        /// <summary>
        /// Drawing grid for next generation
        /// </summary>
        /// <param name="grid">GameGrid</param>
        public void DrawNextGeneration(Grid grid)
        {
            var currentIterationCount = 1;

            while (currentIterationCount <= grid.IterationCount)
            {
                foreach (var cell in cells)
                {
                    CountLiveNeighbours(cell);
                    ApplyGameRules(cell);
                }

                Thread.Sleep(1000);
                Console.Clear();

                for (int h = 0; h < grid.Height; h++)
                {
                    for (int w = 0; w < grid.Width; w++)
                    {
                        var foundCell = FindCellByCoordinates(h, w);

                        if (foundCell.Change == true)
                        {
                            foundCell.IsLive = !foundCell.IsLive;
                            foundCell.Change = false;
                        }
                        if (foundCell.IsLive)
                        {
                            Console.Write("X");
                        }
                        else
                        {
                            Console.Write("O");
                        }
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine($"Count of iteration: {currentIterationCount}");
                Console.WriteLine($"Count of live cells: {CountOfLiveCells()}");
                currentIterationCount++;
            }

            userOutput.GameOverMessage();
        }

        public Cell? FindCellByCoordinates(int height, int width)
        {
            return cells.FirstOrDefault(c => c.Height == height && c.Width == width);
        }        

        public int CountOfLiveCells()
        {
            return cells.Count(c => c.IsLive);
        }
    }
}
