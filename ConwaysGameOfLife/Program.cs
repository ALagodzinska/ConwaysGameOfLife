namespace ConwaysGameOfLife;

class Program
{
    static async Task Main(string[] args)
    {
        GameLogic game = new GameLogic();
        int height;
        int width;
        int iterationCount;
        //bool userRequestedStop = false;

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
        game.CreateGrid(height, width);

        int count = 1;
        while (count <= iterationCount)
        {
            game.DrawNextGeneration(height, width, count);
            count++;
        }

        Console.ReadLine();
    }
}


