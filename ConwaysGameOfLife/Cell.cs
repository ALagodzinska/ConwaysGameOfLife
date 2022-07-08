namespace ConwaysGameOfLife
{
    public class Cell
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int LiveNeighbours { get; set; }
        public bool IsLive { get; set; }
        public bool Change { get; set; }
    }
}
