namespace ConwaysGameOfLife
{
    public class Game
    {
        public List<Cell> cells = new List<Cell>();
        public void CreateGrid(int height, int width)
        {
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
        }
        public int CountLiveCells(Cell cell)
        {
            var liveCellsCount = 0;
            for (int h = cell.Height - 1; h < cell.Height + 2; h++)
            {
                for (int w = cell.Width - 1; w < cell.Width + 2; w++)
                {
                    var foundCell = FindCellByCoordinates(h, w);
                    if (foundCell == null)
                    {
                        continue;
                    }
                    if (h == cell.Height && w == cell.Width)
                    {
                        continue;
                    }
                    if (foundCell.IsLive)
                    {
                        liveCellsCount++;
                    }
                }
            }
            return liveCellsCount;
        }
        public Cell? FindCellByCoordinates(int height, int width)
        {
            return cells.FirstOrDefault(c => c.Height == height && c.Width == width);
        }
    }
}
