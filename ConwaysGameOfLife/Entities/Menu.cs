namespace ConwaysGameOfLife.Entities
{
    /// <summary>
    /// Stores menu data and implement menu methods.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Set initial values for menu fields.
        /// </summary>
        /// <param name="options">Menu items.</param>
        /// <param name="menuIntro">Inscription above the menu.</param>
        public Menu(string[] options, string menuIntro)
        {
            SelectedOptionIndex = 0;
            Options = options;
            MenuIntro = menuIntro;
        }

        /// <summary>
        /// Option index that has been selected.
        /// </summary>
        private int SelectedOptionIndex;

        /// <summary>
        /// Menu items to choose from.
        /// </summary>
        private string[] Options;

        /// <summary>
        /// Inscription above the menu.
        /// </summary>
        private string MenuIntro;

        /// <summary>
        /// Shows menu on a screen.
        /// </summary>
        private void DisplayMenu()
        {
            Console.WriteLine(MenuIntro);
            for (int i = 0; i < Options.Length; i++)
            {
                bool isSelected = i == SelectedOptionIndex;
                OptionStyle(isSelected, Options[i]);
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Style menu options depending on where user hovered.
        /// </summary>
        /// <param name="isSelelcted">Is this option selected by a user.</param>
        /// <param name="currentOption">Option to display.</param>
        public void OptionStyle(bool isSelelcted, string currentOption)
        {
            string sideSymbol = isSelelcted ? Convert.ToChar(3).ToString() : Convert.ToChar(4).ToString();
            string prefix = isSelelcted ? Convert.ToChar(16).ToString() : " ";
            Console.BackgroundColor = isSelelcted ? ConsoleColor.White : ConsoleColor.Black;
            Console.ForegroundColor = isSelelcted ? ConsoleColor.Black : ConsoleColor.White;
            Console.WriteLine($"{prefix} {sideSymbol} {currentOption} {sideSymbol}");
        }

        /// <summary>
        /// Allows to select an option from the menu.
        /// </summary>
        /// <returns>Selected option index.</returns>
        public int SelectFromMenu()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.SetCursorPosition(0, 0);
                DisplayMenu();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedOptionIndex--;
                    if (SelectedOptionIndex == -1)
                    {
                        SelectedOptionIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedOptionIndex++;
                    if (SelectedOptionIndex == Options.Length)
                    {
                        SelectedOptionIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            return SelectedOptionIndex;
        }
    }
}
