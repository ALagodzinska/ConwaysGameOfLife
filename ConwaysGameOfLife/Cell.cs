namespace ConwaysGameOfLife
{
    public class Cell
    {
        public Cell(int height, int width, bool liveOrDead)
        {
            Height = height;
            Width = width;
            LiveOrDead = liveOrDead;
        }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool LiveOrDead { get; set; }
    }
}
