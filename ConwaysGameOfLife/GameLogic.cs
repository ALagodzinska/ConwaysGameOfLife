namespace ConwaysGameOfLife
{
    public class GameLogic
    {
        public List<Cell> cells = new List<Cell>();
        public void CreateRandomGrid(int height, int width)
        {
            Thread.Sleep(1000);
            Console.Clear();
            var random = new Random();
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    Cell cell = new Cell(h, w, false);
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

        public void CreateYourOwnGrid(int maxHeight, int maxWidth)
        {
            CreateEmptyGrid(maxHeight, maxWidth);
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
                            Console.SetCursorPosition(startPosition.Left, startPosition.Top = maxHeight - 1);
                        }
                        else
                        {
                            Console.SetCursorPosition(startPosition.Left, startPosition.Top - 1);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (startPosition.Top + 1 >= maxHeight)
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
                            Console.SetCursorPosition(startPosition.Left = maxWidth - 1, startPosition.Top);
                        }
                        else
                        {
                            Console.SetCursorPosition(startPosition.Left - 1, startPosition.Top);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (startPosition.Left + 1 >= maxWidth)
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

        public void CreateEmptyGrid(int height, int width)
        {
            Thread.Sleep(1000);
            Console.Clear();
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    Cell cell = new Cell(h, w, false);
                    cells.Add(cell);
                    Console.Write("O");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("To move use ARROWS. To make cell live use SPACE. To stop setting field use ENTER.");
        }

        //at the moment + applying rules for 2nd generation
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

        public void ApplyGameRules(Cell cell)
        {
            //apply game rules
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

        public Cell? FindCellByCoordinates(int height, int width)
        {
            return cells.FirstOrDefault(c => c.Height == height && c.Width == width);
        }

        public void DrawNextGeneration(int height, int width, int count)
        { 
            foreach (var cell in cells)
            {
                CountLiveNeighbours(cell);
                ApplyGameRules(cell);
            }
            Thread.Sleep(1000);
            Console.Clear();
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    var foundCell = FindCellByCoordinates(h, w);
                    if(foundCell.Change == true)
                    {
                        foundCell.IsLive= !foundCell.IsLive;
                        foundCell.Change= false;
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
            Console.WriteLine($"Count of iteration: {count}");
            Console.WriteLine($"Count of live cells: {CountOfLiveCells()}");            
        }

        public int CountOfLiveCells()
        {
            return cells.Count(c => c.IsLive);
        }
    }
}
