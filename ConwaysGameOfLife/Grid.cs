namespace ConwaysGameOfLife
{
    /// <summary>
    /// Stores data about game field.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Set initial values for grid fields
        /// </summary>
        /// <param name="height">Height of grid</param>
        /// <param name="width">Width of grid</param>
        public Grid(int height, int width)
        {
            Cell[,] cells = new Cell[height, width];
            Cells = cells;
            Height = height;
            Width = width;
            IterationCount = 1;
        }

        /// <summary>
        /// Height of grid
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Width of grid
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// How many iterations grid had.
        /// </summary>w
        public int IterationCount { get; set; }
        /// <summary>
        /// Stores all cells inside of this grid as collection of rows and columns
        /// </summary>
        public Cell[,] Cells { get; set; }

        /// <summary>
        /// Creates new grid and all cells inside of it.
        /// </summary>
        /// <param name="height">Height of the grid</param>
        /// <param name="width">Width of the grid</param>
        /// <returns>New grid created by parameters</returns>
        public static Grid CreateNewGrid(int height, int width)
        {
            var grid = new Grid(height, width);
            for (int h = 0; h < grid.Height; h++)
            {
                for (int w = 0; w < grid.Width; w++)
                {
                    grid.Cells[h, w] = new Cell()
                    {
                        Height = h,
                        Width = w,                        
                    };
                }
            }

            return grid;
        }
    }
}
