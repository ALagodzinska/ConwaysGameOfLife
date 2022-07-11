namespace ConwaysGameOfLife
{
    public class Grid
    {
        public Grid(int height, int width, int iterationCount)
        {
            Cell[,] cells = new Cell[height, width];
            Cells = cells;
            Height = height;
            Width = width;
            IterationCount = iterationCount;            
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public int IterationCount { get; set; }
        public Cell[,] Cells { get; set; }
    }
}
