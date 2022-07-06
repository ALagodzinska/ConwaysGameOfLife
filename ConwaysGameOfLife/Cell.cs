namespace ConwaysGameOfLife
{
    public class Cell
    {
        public Cell(int height, int width, bool isLive)
        {
            Height = height;
            Width = width;
            IsLive = isLive;
        }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool IsLive { get; set; }
    }
}
