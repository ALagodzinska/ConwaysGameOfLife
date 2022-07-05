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
                        cell.LiveOrDead = true;
                        cells.Add(cell);
                        Console.Write("X");
                    }
                    else
                    {
                        cells.Add(cell);
                        Console.Write("Y");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
