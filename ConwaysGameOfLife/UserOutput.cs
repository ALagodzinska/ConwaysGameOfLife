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
            var createdGrid = new Grid();
            Console.WriteLine("Please Remember that MAX Height is 30 and MAX Width is 120");
            Console.WriteLine();
            Console.WriteLine("Input height of field:");
            var heightInput = Console.ReadLine();
            var validHeight = CheckForValidInput(choise, heightInput, "height");
            createdGrid.Height = validHeight;

            Console.WriteLine("Input width of field:");
            var widthInput = Console.ReadLine();
            var validWidth = CheckForValidInput(choise, widthInput, "width");
            createdGrid.Width = validWidth;

            Console.WriteLine("Input iteration count for the game:");
            var countInput = Console.ReadLine();
            var validCount = CheckForValidInput(choise, countInput, "count");
            createdGrid.IterationCount = validCount;
            return createdGrid;
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
    }
}
