namespace ConwaysGameOfLife
{
    public class GameDataSerializer
    {
        private readonly string FilePath = @"C:\Users\a.lagodzinska\source\repos\ConwaysGameOfLife\ConwaysGameOfLife\GameOfLifeData.json";
        public void ConvertCellsArrayToList(Grid grid)
        {
            foreach (Cell cell in grid.Cells)
            {
                grid.SerializableCells.Add(cell);
            }
        }

        public void ConvertCellsListToArray(Grid grid)
        {
            foreach (Cell cell in grid.SerializableCells)
            {
                grid.Cells[cell.Height, cell.Width] = cell;
            }
        }

        public void SaveOneGameDataToTheFile(Grid grid)
        {
            List<string> gridListForFile = new();
            ConvertCellsArrayToList(grid);
            string jsonString = System.Text.Json.JsonSerializer.Serialize(grid);
            gridListForFile.Add(jsonString);
            File.AppendAllLines(FilePath, gridListForFile);            
        }

        public void SaveAllData(List<Grid> gridList)
        {
            ClearFile();
            foreach(var grid in gridList)
            {
                SaveOneGameDataToTheFile(grid);
            }
        }

        public List<Grid> ReadDataFromTheFile()
        {
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

        public void ClearFile()
        {
            if (!File.Exists(FilePath))
                File.Create(FilePath);

            TextWriter tw = new StreamWriter(FilePath, false);
            tw.Write(string.Empty);
            tw.Close();
        }

        public Grid? FindGameGridByName(string name, List<Grid> gridList)
        {
           return gridList.FirstOrDefault(g => g.GameName == name);
        }
    }
}
