using Newtonsoft.Json;

namespace ConwaysGameOfLife
{
    /// <summary>
    /// Stores data about game field.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Grid
    {
        /// <summary>
        /// Set initial values for grid fields
        /// </summary>
        /// <param name="height">Height of grid</param>
        /// <param name="width">Width of grid</param>
        public Grid(int height, int width, string gameName)
        {
            Cell[,] cells = new Cell[height, width];
            Cells = cells;
            List<Cell> cellsList = new List<Cell>();
            SerializableCells = cellsList;
            GameName = gameName;
            Height = height;
            Width = width;
            IterationCount = 1;
            
        }

        /// <summary>
        /// Name of the Game. To identify what game you want to replay.
        /// </summary>
        public string GameName { get; set; }
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
        [System.Text.Json.Serialization.JsonIgnore]
        public Cell[,] Cells { get; set; }
        /// <summary>
        /// List to serialize data of what is stored in two-dimensional array.
        /// </summary>
        public List<Cell> SerializableCells { get; set; }

        /// <summary>
        /// Creates new grid and all cells inside of it.
        /// </summary>
        /// <param name="height">Height of the grid</param>
        /// <param name="width">Width of the grid</param>
        /// <returns>New grid created by parameters</returns>
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
    }
}
