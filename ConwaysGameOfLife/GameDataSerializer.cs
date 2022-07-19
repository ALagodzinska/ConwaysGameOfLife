namespace ConwaysGameOfLife
{
    /// <summary>
    /// Contains methods to work with file - get data, remove and add.
    /// </summary>
    public class GameDataSerializer
    {
        /// <summary>
        /// List to store played game grids.
        /// </summary>
        public static List<Grid> gridList = new();

        /// <summary>
        /// Name of the folder where file with data will be stored.
        /// </summary>
        private static string folderName = "GameOfLife";

        /// <summary>
        /// Name of the file where game data will be stored.
        /// </summary>
        private static string fileName = "GameOfLife.json";

        /// <summary>
        /// Path to the file where the data will be stored.
        /// </summary>
        private readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folderName, fileName);        

        /// <summary>
        /// Convert a field that contains two dimensional array to a list.
        /// </summary>
        /// <param name="grid">A game grid.</param>
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
        /// <param name="grid">A game grid.</param>
        public void ConvertCellsListToArray(Grid grid)
        {
            foreach (Cell cell in grid.SerializableCells)
            {
                grid.Cells[cell.Height, cell.Width] = cell;
            }
        }

        /// <summary>
        /// Save data about one grid in one line of the file.
        /// </summary>
        /// <param name="grid">A game grid.</param>
        public void SaveGameDataToFile(Grid grid)
        {
            List<string> gridListForFile = new();

            ConvertCellsArrayToList(grid);
            string jsonString = System.Text.Json.JsonSerializer.Serialize(grid);

            gridListForFile.Add(jsonString);
            File.AppendAllLines(FilePath, gridListForFile);            
        }

        /// <summary>
        /// Write data about all saved grids to the file.
        /// </summary>
        public void SaveAllData()
        {
            ClearFile();
            var gridList = ReturnListOfExistingGrids();
            foreach(var grid in gridList)
            {
                SaveGameDataToFile(grid);
            }
        }

        /// <summary>
        /// Create a folder and a file if not exists.
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
        /// Return a list of played game grids.
        /// </summary>
        /// <returns>List of game grids.</returns>
        public List<Grid> ReturnListOfExistingGrids()
        {
            return gridList;
        }

        /// <summary>
        /// Read all data from file and convert them into Grid objects.
        /// </summary>
        public void ReadDataFromTheFile()
        {
            CreateDirectoryAndFile();            

            var jsonData = File.ReadAllLines(FilePath);
            foreach (var oneGridData in jsonData)
            {
                var gridObject = System.Text.Json.JsonSerializer.Deserialize<Grid>(oneGridData);
                ConvertCellsListToArray(gridObject);
                gridList.Add(gridObject);
            }
        }

        /// <summary>
        /// Clear all the data that was written inside file.
        /// </summary>
        public void ClearFile()
        {
            if (!File.Exists(FilePath))
                File.Create(FilePath);

            TextWriter textWriter = new StreamWriter(FilePath, false);
            textWriter.Write(string.Empty);
            textWriter.Close();
        }

        /// <summary>
        /// Find grid by the grid name.
        /// </summary>
        /// <param name="name">Name of the grid.</param>
        /// <returns>If exists return object if not return null.</returns>
        public Grid? FindGameGridByName(string name)
        {
           return gridList.FirstOrDefault(g => g.GameName == name);
        }
    }
}
