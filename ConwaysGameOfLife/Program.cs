namespace ConwaysGameOfLife;

class Program
{
    static async Task Main(string[] args)
    {
        Game game = new Game();
        int height;
        int width;
        int iterationCount;

        Console.WriteLine("Hello, Welcom to the 'Game Of Life'!");
        Console.WriteLine("Please input height of field?");
        var heightInput = Console.ReadLine();
        if (!int.TryParse(heightInput, out height))
        {
            Console.WriteLine("Please enter valid input!");
            return;
        }

        Console.WriteLine("Please input width of field!");

        var widthInput = Console.ReadLine();
        if (!int.TryParse(widthInput, out width))
        {
            Console.WriteLine("Please enter valid input!");
            return;
        }

        Console.WriteLine("Please input iteration count for the game!");

        var countInput = Console.ReadLine();
        if (!int.TryParse(countInput, out iterationCount))
        {
            Console.WriteLine("Please enter valid input!");
            return;
        }

        Console.WriteLine("Let the game begin...");
        await game.CreateGrid(height, width);


        //Console.WriteLine("Count of cells to change");
        //game.MarkAllCellsThatNeedToChange();

        //Console.WriteLine("See next generation");

        int count = 0;
        while (count < iterationCount)
        {
            await game.DrawNextGeneration(height, width);
            count++;
        }

        Console.ReadLine();

        //Console.WriteLine("Count of cell 2:2");
        //var foundcell = game.FindCellByCoordinates(2, 2);
        //Console.WriteLine(game.CountLiveCells(foundcell));
    }
}


