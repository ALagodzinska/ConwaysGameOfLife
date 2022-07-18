namespace ConwaysGameOfLife
{
    /// <summary>
    /// Contains methods to work with file - get data, remove and add.
    /// </summary>
    public class GameDataSerializer
    {
        /// <summary>
        /// Path to the file where the data is stored.
        /// </summary>
        private readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GameOfLife", "GameOfLife.json");
        

        /// <summary>
        /// Convert a field that contains two dimensional array to a list.
        /// </summary>
        /// <param name="grid">One game grid</param>
        public void ConvertCellsArrayToList(Grid grid)
        {
            foreach (Cell cell in grid.Cells)
            {
                grid.SerializableCells.Add(cell);
            }
        }

        /// <summary>
        /// Convert a field with a list to two dimensioanl array.
        /// </summary>
        /// <param name="grid">One game grid</param>
        public void ConvertCellsListToArray(Grid grid)
        {
            foreach (Cell cell in grid.SerializableCells)
            {
                grid.Cells[cell.Height, cell.Width] = cell;
            }
        }

        /// <summary>
        /// Saves data about one grid in one line of the file.
        /// </summary>
        /// <param name="grid">One game grid</param>
        public void SaveOneGameDataToTheFile(Grid grid)
        {
            List<string> gridListForFile = new();

            ConvertCellsArrayToList(grid);
            string jsonString = System.Text.Json.JsonSerializer.Serialize(grid);

            gridListForFile.Add(jsonString);
            File.AppendAllLines(FilePath, gridListForFile);            
        }

        /// <summary>
        /// Write data about all saved grids to the file
        /// </summary>
        /// <param name="gridList">List of played game grids</param>
        public void SaveAllData(List<Grid> gridList)
        {
            ClearFile();

            foreach(var grid in gridList)
            {
                SaveOneGameDataToTheFile(grid);
            }
        }

        /// <summary>
        /// Creates a folder and a file if not exsists.
        /// </summary>
        public void CreateDirectoryAndFile()
        {
            var pathToFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GameOfLife");
            if (!Directory.Exists(pathToFolder))
            {
                Directory.CreateDirectory(pathToFolder);
            }
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath);
            }
        }

        /// <summary>
        /// Read all data from file and convert them into Grid objects.
        /// </summary>
        /// <returns>List of stored game grids</returns>
        public List<Grid> ReadDataFromTheFile()
        {
            CreateDirectoryAndFile();
            List<Grid> gridList = new();

            var jsonData = File.ReadAllLines(FilePath);
            foreach (var oneGridData in jsonData)
            {
                var gridObject = System.Text.Json.JsonSerializer.Deserialize<Grid>(oneGridData);
                ConvertCellsListToArray(gridObject);
                gridList.Add(gridObject);
            }
            return gridList;
        }

        /// <summary>
        /// Clear all the data that was written inside file.
        /// </summary>
        public void ClearFile()
        {
            if (!File.Exists(FilePath))
                File.Create(FilePath);

            TextWriter tw = new StreamWriter(FilePath, false);
            tw.Write(string.Empty);
            tw.Close();
        }

        /// <summary>
        /// Finds grid by the grid name.
        /// </summary>
        /// <param name="name">Name of the grid</param>
        /// <param name="gridList">List of the exsisting grids</param>
        /// <returns>If exsists return object if not return null</returns>
        public Grid? FindGameGridByName(string name, List<Grid> gridList)
        {
           return gridList.FirstOrDefault(g => g.GameName == name);
        }
    }
}
