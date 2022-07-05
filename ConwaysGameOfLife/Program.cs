namespace ConwaysGameOfLife;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        int height;
        int width;

        Console.WriteLine("Hello, Welcom to the 'Game Of Life'!");
        Console.WriteLine("Please input height of field?");
        var heightInput = Console.ReadLine();
        if (!int.TryParse(heightInput, out height))
        {
            Console.WriteLine("Please enter valid input!");
            return;
        }

        Console.WriteLine("Please input width of field?");
        var widthInput = Console.ReadLine();
        if (!int.TryParse(widthInput, out width))
        {
            Console.WriteLine("Please enter valid input!");
            return;
        }

        game.CreateGrid(height, width);
        Console.ReadLine();
    }
}


