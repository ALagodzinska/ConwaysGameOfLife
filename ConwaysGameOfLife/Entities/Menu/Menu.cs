namespace ConwaysGameOfLife.Entities.Menu
{
    /// <summary>
    /// Used for creating and displaying menu to user.
    /// </summary>
    /// <typeparam name="T">Enum of options.</typeparam>
    public class Menu<T>
    { 
        /// <summary>
        /// Option index that has been selected.
        /// </summary>
        private int SelectedOptionIndex;

        /// <summary>
        /// Menu options to choose from.
        /// </summary>
        protected MenuOption<T>[] Options;

        /// <summary>
        /// Inscription above the menu.
        /// </summary>
        private string MenuIntro;

        /// <summary>
        /// Set initial values for menu fields.
        /// </summary>
        /// <param name="options">Menu options to choose from.</param>
        /// <param name="menuIntro">Title and menu control rules.</param>
        public Menu(MenuOption<T>[] options, string menuIntro)
        {
            Options = options;
            SelectedOptionIndex = 0;
            MenuIntro = menuIntro;
        }

        /// <summary>
        /// Shows menu on a screen.
        /// </summary>
        private void DisplayMenu()
        {
            Console.WriteLine(MenuIntro);
            for (int i = 0; i < Options.Length; i++)
            {
                bool isSelected = i == SelectedOptionIndex;
                OptionStyle(isSelected, Options[i].Title);
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
        public MenuOption<T> SelectFromMenu()
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

            return Options[SelectedOptionIndex];
        }
    }
}
