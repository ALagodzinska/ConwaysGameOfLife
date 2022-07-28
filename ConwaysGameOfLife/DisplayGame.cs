namespace ConwaysGameOfLife
{
    /// <summary>
    /// Contains methods to display game on the screen.
    /// </summary>
    public class DisplayGame
    {
        /// <summary>
        /// Side spacing between grids.
        /// </summary>
        const int indentLeft = 10;

        /// <summary>
        /// Top spacing between grids.
        /// </summary>
        const int indentTop = 5;

        /// <summary>
        /// Indentation from top of the console.
        /// </summary>
        const int topLines = 6;

        UserOutput userOutput = new();

        /// <summary>
        /// Draw grid based on the cells stored in the grid.
        /// </summary>
        /// <param name="grid">Game grid.</param>
        public void DrawGrid(Grid grid)
        {
            Console.SetCursorPosition(0, 0);

            for (int h = 0; h < grid.Height; h++)
            {
                for (int w = 0; w < grid.Width; w++)
                {
                    Console.SetCursorPosition(w, h);
                    DrawCell(grid.Cells[h, w].IsLive);
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Draw one cell on the grid in certain style, depending on whether it is alive or not.
        /// </summary>
        /// <param name="isLive">Cell parameter that note if cell is live or not.</param>
        public void DrawCell(bool isLive)
        {
            string cellSymbol = isLive ? "*" : "-";
            Console.BackgroundColor = isLive ? ConsoleColor.Red : ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(cellSymbol);
            Console.ResetColor();
        }

        /// <summary>
        /// Show created random grid.
        /// </summary>
        /// <param name="grid">Game grid to display.</param>
        public void DisplayRandomGrid(Grid grid)
        {
            Console.Clear();
            DrawGrid(grid);
            userOutput.MessageAfterEachIteration(grid);
        }

        /// <summary>
        /// Draw grid for next generation.
        /// </summary>
        /// <param name="grid">Game Grid.</param>
        public void DrawNextGeneration(Grid grid)
        {
            Thread.Sleep(1000);
            DrawGrid(grid);

            //clean live cells value
            int[] startCoordinates = { 0, 0 };
            userOutput.CleanLiveCellsCount(grid, startCoordinates);

            userOutput.MessageAfterEachIteration(grid);
        }

        /// <summary>
        /// Display multiple game grids on one screen.
        /// </summary>
        /// <param name="grids">List of games to play at the same time.</param>
        public void DrawMultipleGrids(List<Grid> grids)
        {
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, 0);

            //used to make padding between games, not only based on grid size, but also on message length.
            var commonLengthOfMessage = $"Game name: {grids.First().GameName}".Length + 3;
            var startPointForPadding = Math.Max(commonLengthOfMessage, grids.First().Width);

            var gameCountInOneRow = HowManyGamesCanBeInOneRow(startPointForPadding);

            foreach (var grid in grids)
            {
                var startPoint = StartPointForGrid(grid, grids.IndexOf(grid), gameCountInOneRow, startPointForPadding);

                DrawOneOfMultipleGrids(grid, startPoint);

                if (grid.CountOfLiveCells() == 0 || grid.CheckIfGridIsSame())
                {
                    //display game over message above game grid.
                    Console.SetCursorPosition(startPoint[0], startPoint[1] - 1);
                    Console.Write("GameOver!");
                }
            }
        }

        /// <summary>
        /// Counts how many playing grids fit on a certain screen width.
        /// </summary>
        /// <param name="startPointForLeftPadding">Width to back off for drawing next grid.</param>
        /// <returns>Count of grids that fit in such screen width.</returns>
        public int HowManyGamesCanBeInOneRow(int startPointForLeftPadding)
        {
            var windowWidth = Console.WindowWidth;
            var paddingLeft = startPointForLeftPadding + indentLeft;

            return (windowWidth / paddingLeft);
        }

        /// <summary>
        /// Finds start coordinates to start drawing grid on multiple games screen.
        /// </summary>
        /// <param name="grid">A grid to draw.</param>
        /// <param name="gridIndex">Index number of this grid in a list.</param>
        /// <param name="gameCountInOneRow">Maimum games count in one row.</param>
        /// <param name="startPointForLeftPadding">Start point for left padding.</param>
        /// <returns>Array of coordinates. First coordinate is left, second is top.</returns>
        public int[] StartPointForGrid(Grid grid, int gridIndex, int gameCountInOneRow, int startPointForLeftPadding)
        {
            int[] startCoordinates = new int[2];

            var paddingLeft = startPointForLeftPadding + indentLeft;

            var paddingTop = grid.Height + indentTop;
            var topPositionCount = gridIndex / gameCountInOneRow;

            startCoordinates[0] = paddingLeft * (gridIndex - (gameCountInOneRow * topPositionCount));
            startCoordinates[1] = paddingTop * topPositionCount + topLines;

            return startCoordinates;
        }

        /// <summary>
        /// Display on a screen onre of game grids on a multiple games field.
        /// </summary>
        /// <param name="grid">Grid to draw.</param>
        /// <param name="startCoordinates">Array of coordinates to start drawing. [0]-left, [1]-top.</param>
        public void DrawOneOfMultipleGrids(Grid grid, int[] startCoordinates)
        {
            var offsetFromLeft = startCoordinates[0];
            var offsetFromTop = startCoordinates[1];
            for (int h = offsetFromTop; h < (grid.Height + offsetFromTop); h++)
            {
                for (int w = offsetFromLeft; w < (grid.Width + offsetFromLeft); w++)
                {
                    Console.SetCursorPosition(w, h);
                    DrawCell(grid.Cells[(h - offsetFromTop), (w - offsetFromLeft)].IsLive);
                }

                Console.WriteLine();
            }

            userOutput.CleanLiveCellsCount(grid, startCoordinates);
            userOutput.MultipleGameMessageAfterIteration(grid, startCoordinates);
        }
    }
}
