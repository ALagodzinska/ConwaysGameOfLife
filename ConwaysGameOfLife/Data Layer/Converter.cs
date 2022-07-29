using ConwaysGameOfLife.Entities;

namespace ConwaysGameOfLife
{
    public class Converter
    { 
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

        public string[] ConvertGridListToStringArray(List<Grid> gridList)
        {
            string[] gridNamesArray = new string[gridList.Count];
            for(int i = 0; i < gridList.Count; i++)
            {
                gridNamesArray[i] = gridList[i].GameName;
            }

            return gridNamesArray;
        }
    }
}
