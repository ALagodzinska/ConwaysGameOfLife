namespace ConwaysGameOfLife.Entities
{
    using Newtonsoft.Json;

    /// <summary>
    /// Stores data about game field and game grid methods.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Grid
    {
        /// <summary>
        /// Set initial values for grid fields.
        /// </summary>
        /// <param name="height">Height of grid.</param>
        /// <param name="width">Width of grid.</param>
        /// <param name="gameName">Name of the game.</param>
        public Grid(int height, int width, string gameName)
        {
            Cell[,] cells = new Cell[height, width];
            Cells = cells;
            List<Cell> cellsList = new List<Cell>();
            SerializableCells = cellsList;
            GameName = gameName;
            Height = height;
            Width = width;
            IterationCount = 0;
        }

        /// <summary>
        /// Name of the Game. To identify what game you want to replay.
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

        /// <summary>
        /// How many iterations grid had.
        /// </summary>
        public int IterationCount { get; set; }

        /// <summary>
        /// How many cells didn't change in this iteration.
        /// When unchanged cells count equals to all cells count in a grid, game is over.
        /// </summary>
        public int UncahngedCellsCount { get; set; }

        /// <summary>
        /// Stores all cells inside of this grid as collection of rows and columns.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Cell[,] Cells { get; set; }

        /// <summary>
        /// List to serialize data of what is stored in two-dimensional array.
        /// </summary>
        public List<Cell> SerializableCells { get; set; }

        /// <summary>
        /// Create new grid and all cells inside of it.
        /// </summary>
        /// <param name="height">Height of the grid.</param>
        /// <param name="width">Width of the grid.</param>
        /// <param name="gameName">Name of the game.</param>
        /// <returns>New grid created by parameters.</returns>
        public static Grid CreateNewGrid(int height, int width, string gameName)
        {
            var grid = new Grid(height, width, gameName);
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

        /// <summary>
        /// Count live cells in the game grid.
        /// </summary>
        /// <returns>Count of live cells(int) in the current grid.</returns>
        public int CountOfLiveCells() => Cells.OfType<Cell>().Where(c => c.IsLive == true).Count();

        /// <summary>
        /// Check if unchanged cells count is the same with cells count in a grid. If all cells stay unchanged - game is over.
        /// </summary>
        /// <returns>True if all cells stayed the same, false if grid have been changed.</returns>
        public bool CheckIfGridIsSame() => UncahngedCellsCount == Height * Width;

        /// <summary>
        /// Count all live neighbours of one cell.
        /// </summary>
        /// <param name="cell">A cell from a grid.</param>
        /// <returns>Count of life neighbours around this cell.</returns>
        public int LiveNeighboursCount(Cell cell)
        {
            var liveCellsCount = 0;

            for (int h = cell.Height - 1; h < cell.Height + 2; h++)
            {
                for (int w = cell.Width - 1; w < cell.Width + 2; w++)
                {
                    if (h >= Height || h < 0
                        || w >= Width || w < 0
                        || h == cell.Height && w == cell.Width)
                    {
                        continue;
                    }

                    var foundCell = Cells[h, w];

                    if (foundCell.IsLive)
                    {
                        liveCellsCount++;
                    }
                }
            }

            return liveCellsCount;
        }
    }
}
