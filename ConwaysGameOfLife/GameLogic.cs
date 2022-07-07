namespace ConwaysGameOfLife
{
    public class GameLogic
    {
        public List<Cell> cells = new List<Cell>();
        public void CreateGrid(int height, int width)
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
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Count of iteration: 0");
            Console.WriteLine($"Count of live cells: {CountOfLiveCells()}");
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
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Count of iteration: {count}");
            Console.WriteLine($"Count of live cells: {CountOfLiveCells()}");            
        }

        public int CountOfLiveCells()
        {
            return cells.Count(c => c.IsLive);
        }
    }
}
