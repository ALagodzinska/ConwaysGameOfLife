namespace ConwaysGameOfLife
{
    public class Grid
    {
        public Grid(int height, int width)
        {
            Cell[,] cells = new Cell[height, width];
            Cells = cells;
            Height = height;
            Width = width;           
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public Cell[,] Cells { get; set; }
    }
}
