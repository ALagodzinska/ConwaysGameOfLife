namespace ConwaysGameOfLife.Entities.Menu
{
    /// <summary>
    /// Stores main menu options.
    /// </summary>
    public enum MainMenuOptions
    {
        RandomGame = 0,
        CustomGame = 1,
        RestoredGame = 2,
        MultipleGames = 3,
        ExitGame = 4,
    }

    /// <summary>
    /// Stores menu on stop options.
    /// </summary>
    public enum MenuOnStopOptions
    {
        BackToMainMenu = 0,
        ContinueToPlay = 1,
    }

    /// <summary>
    /// Contains method to create main menu.
    /// </summary>
    public class GameMainMenu : GameMenu
    {
        /// <summary>
        /// Creates main menu.
        /// </summary>
        /// <param name="intro">Menu introduction text.</param>
        public GameMainMenu(string intro) : base(new MenuOption[Enum.GetNames(typeof(MainMenuOptions)).Length], intro)
        {
            Options[0] = new MenuOption()
            {
                Index = (int)MainMenuOptions.RandomGame,
                Title = "Play Game: Create Random field"
            };
            Options[1] = new MenuOption()
            {
                Index = (int)MainMenuOptions.CustomGame,
                Title = "Play Game: Create Customized field"
            };
            Options[2] = new MenuOption()
            {
                Index = (int)MainMenuOptions.RestoredGame,
                Title = "Restore Game: Continue to play one of the previous games"
            };
            Options[3] = new MenuOption()
            {
                Index = (int)MainMenuOptions.MultipleGames,
                Title = "Play multiple games at once"
            };
            Options[4] = new MenuOption()
            {
                Index = (int)MainMenuOptions.ExitGame,
                Title = "Exit Game"
            };
        }
    }

    /// <summary>
    /// Contains method to create menu on a game stop.
    /// </summary>
    public class MenuOnStop : GameMenu
    {
        /// <summary>
        /// Creates menu on stop.
        /// </summary>
        /// <param name="intro">Menuintroduction text.</param>
        public MenuOnStop(string intro) : base(new MenuOption[Enum.GetNames(typeof(MenuOnStopOptions)).Length], intro)
        {
            Options[0] = new MenuOption()
            {
                Index = (int)MenuOnStopOptions.BackToMainMenu,
                Title = "GO BACK TO MAIN MENU"
            };
            Options[1] = new MenuOption()
            {
                Index = (int)MenuOnStopOptions.ContinueToPlay,
                Title = "CONTINUE TO PLAY"
            };
        }
    }
}
