namespace ConwaysGameOfLife
{
    public class UserOutput
    {
        int validInput;

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Hello, Welcome to the 'Game Of Life'!");
            Console.WriteLine();
            Console.WriteLine("Please choose action what you want to do?(INPUT NUMBER)");
            Console.WriteLine("1. Play Game: Create Random field");
            Console.WriteLine("2. Play Game: Create Customized field");
            Console.WriteLine("3. Exit Game");
        }

        public Grid GameIntro(string choise)
        {
            Console.Clear();
            if (choise == "1")
            {
                Console.WriteLine("Your Game Field live cells will be created Randomly");
            }
            else
            {
                Console.WriteLine("Your Game Field live cells will be created By You");
            }
            Console.WriteLine();
            return InputGridParameters(choise);
        }

        public Grid InputGridParameters(string choise)
        {            
            Console.WriteLine("Please Remember that MAX Height is 30 and MAX Width is 120");
            Console.WriteLine();
            Console.WriteLine("Input height of field:");
            var heightInput = Console.ReadLine();
            var validHeight = CheckForValidInput(choise, heightInput, "height");

            Console.WriteLine("Input width of field:");
            var widthInput = Console.ReadLine();
            var validWidth = CheckForValidInput(choise, widthInput, "width");

            Console.WriteLine("Input iteration count for the game:");
            var countInput = Console.ReadLine();
            var validCount = CheckForValidInput(choise, countInput, "count");

            return CreateNewGrid(validHeight, validWidth, validCount);
        }

        public int CheckForValidInput(string choise, string input, string typeOfInput)
        {
            if (!int.TryParse(input, out validInput) || validInput <= 0
                || typeOfInput == "height" && validInput > 30
                || typeOfInput == "width" && validInput > 120)
            {
                Console.WriteLine("Please enter valid input!");
                Console.WriteLine("Try one more time after 5 seconds");
                Thread.Sleep(5000);
                GameIntro(choise);
            }
            return validInput;
        }

        public void GameOverMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Game is over! You will be sent to main menu ater 5 seconds");
            Thread.Sleep(5000);
        }

        public Grid CreateNewGrid(int height, int width, int count)
        {
            var createdGrid = new Grid(height, width, count);
            for (int h = 0; h < createdGrid.Height; h++)
            {
                for (int w = 0; w < createdGrid.Width; w++)
                {
                    createdGrid.Cells[h, w] = new Cell()
                    {
                        Height = h,
                        Width = w,
                    };
                }
            }

            return createdGrid;
        }

        public void CustomGridRules()
        {
            Console.WriteLine();
            Console.WriteLine("To move use ARROWS. To make cell live use SPACE. To stop setting field use ENTER.");
        }

        public void MessageAfterEachIteration(int iterationCount, int liveCellsCount)
        {
            Console.WriteLine();
            Console.WriteLine($"Count of iteration: {iterationCount}");
            Console.WriteLine($"Count of live cells: {liveCellsCount}");
        }
    }
}
