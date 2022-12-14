namespace ConwaysGameOfLife.Entities
{
    using Newtonsoft.Json;

    /// <summary>
    /// Stores data about a cell in a grid.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Cell
    {
        /// <summary>
        /// Position in the grid. Height of cell.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Position in the grid. Width of cell.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Count of live neighbours around cell.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public int LiveNeighbours { get; set; }

        /// <summary>
        /// Check if cell is live. If false - cell is dead.
        /// </summary>
        public bool IsLive { get; set; }
    }
}
