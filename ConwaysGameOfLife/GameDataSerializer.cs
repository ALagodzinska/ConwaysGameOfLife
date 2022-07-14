namespace ConwaysGameOfLife
{
    public class GameDataSerializer
    {        
        List<Grid> gridList = new();
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

        public void SaveDataToTheFile(Grid grid)
        {
            gridList.Add(grid);
            List<string> gridListForFile = new();
            ConvertCellsArrayToList(grid);
            string jsonString = System.Text.Json.JsonSerializer.Serialize(grid);
            gridListForFile.Add(jsonString);
            File.AppendAllLines(FilePath, gridListForFile);            
        }

        public void ReadDataFromTheFile()
        {
            var jsonData = File.ReadAllLines(FilePath);
            foreach (var oneGridData in jsonData)
            {
                var gridObject = System.Text.Json.JsonSerializer.Deserialize<Grid>(oneGridData);
                ConvertCellsListToArray(gridObject);
                gridList.Add(gridObject);
            }
        }

        public Grid? FindGameGridByName(string name)
        {
           return gridList.FirstOrDefault(g => g.GameName == name);
        }
    }
}
