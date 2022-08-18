namespace ConwaysGameOfLife.Entities.Menu
{
    /// <summary>
    /// Stores main menu options.
    /// </summary>
    public enum MainMenuOptions
    {
        RandomGame,
        CustomGame,
        RestoredGame,
        MultipleGames,
        ExitGame
    }

    /// <summary>
    /// Stores menu on stop options.
    /// </summary>
    public enum MenuOnStopOptions
    {
        BackToMainMenu,
        ContinueToPlay
    }

    /// <summary>
    /// Contains method to create main menu.
    /// </summary>
    public class GameMainMenu : Menu<MainMenuOptions>
    {
        /// <summary>
        /// Creates main menu.
        /// </summary>
        /// <param name="intro">Menu introduction text.</param>
        public GameMainMenu(string intro) : base(new MenuOptions<MainMenuOptions>[Enum.GetNames(typeof(MainMenuOptions)).Length], intro)
        {
            Options[0] = new MenuOptions<MainMenuOptions>()
            {
                Index = MainMenuOptions.RandomGame,
                Title = "Play Game: Create Random field"
            };
            Options[1] = new MenuOptions<MainMenuOptions>()
            {
                Index = MainMenuOptions.CustomGame,
                Title = "Play Game: Create Customized field"
            };
            Options[2] = new MenuOptions<MainMenuOptions>()
            {
                Index = MainMenuOptions.RestoredGame,
                Title = "Restore Game: Continue to play one of the previous games"
            };
            Options[3] = new MenuOptions<MainMenuOptions>()
            {
                Index = MainMenuOptions.MultipleGames,
                Title = "Play multiple games at once"
            };
            Options[4] = new MenuOptions<MainMenuOptions>()
            {
                Index = MainMenuOptions.ExitGame,
                Title = "Exit Game"
            };
        }
    }

    /// <summary>
    /// Contains method to create menu on a game stop.
    /// </summary>
    public class MenuOnStop : Menu<MenuOnStopOptions>
    {
        /// <summary>
        /// Creates menu on stop.
        /// </summary>
        /// <param name="intro">Menuintroduction text.</param>
        public MenuOnStop(string intro) : base(new MenuOptions<MenuOnStopOptions>[Enum.GetNames(typeof(MenuOnStopOptions)).Length], intro)
        {
            Options[0] = new MenuOptions<MenuOnStopOptions>()
            {
                Index = MenuOnStopOptions.BackToMainMenu,
                Title = "GO BACK TO MAIN MENU"
            };
            Options[1] = new MenuOptions<MenuOnStopOptions>()
            {
                Index = MenuOnStopOptions.ContinueToPlay,
                Title = "CONTINUE TO PLAY"
            };
        }
    }
}
