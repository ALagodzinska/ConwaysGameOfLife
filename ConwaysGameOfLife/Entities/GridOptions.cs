namespace ConwaysGameOfLife.Entities
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

        /// <summary>
        /// Converts user input into Grid object.
        /// </summary>
        /// <param name="gridOptions">User inputted parameters for grid.</param>
        /// <returns>Return created grid object.</returns>
        public Grid ConvertGridOptionsToGrid() => Grid.CreateNewGrid(Height, Width, GameName);
    }
}
