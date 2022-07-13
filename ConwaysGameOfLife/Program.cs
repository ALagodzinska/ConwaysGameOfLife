namespace ConwaysGameOfLife;

class Program
{
    static void Main(string[] args)
    {
        GameLogic game = new GameLogic();
        UserOutput userOutput = new UserOutput();
        
        var exit = "continue";        

        while(exit == "continue")
        {
            userOutput.ShowMenu();
            var option = Console.ReadLine();
            
            switch (option)
            {
                case "1":
                    Console.WriteLine("Let the game begin...");
                    var createdGridRandomGame = userOutput.CreateGameGridFromUserInput();
                    game.CreateRandomGrid(createdGridRandomGame);
                    game.PlayGame(createdGridRandomGame);
                    break;

                case "2":
                    var createdGridCustomGame = userOutput.CreateGameGridFromUserInput();
                    game.ChooseLiveCells(createdGridCustomGame);
                    game.PlayGame(createdGridCustomGame);
                    break;

                case "3":
                    Console.WriteLine("Thank you for the game. Bye!");
                    exit = "exit";
                    break;

                default:
                    Console.WriteLine("Wrong input! Please Try Again.");
                    break;
            }
        }

        Console.ReadLine();
    }
}


