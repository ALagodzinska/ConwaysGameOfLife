namespace ConwaysGameOfLife
{
    /// <summary>
    /// Stores users data from input. Used as a base for creating Grid object.
    /// </summary>
    public class GridOptions
    {
        /// <summary>
        /// Name of the Game.
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// Height of grid.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Width of grid.
        /// </summary>
        public int Width { get; set; }
    }
}
