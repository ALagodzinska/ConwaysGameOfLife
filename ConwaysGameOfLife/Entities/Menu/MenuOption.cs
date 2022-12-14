namespace ConwaysGameOfLife.Entities.Menu
{
    /// <summary>
    /// Stores option data for a menu.
    /// </summary>
    /// <typeparam name="T">Enum of options.</typeparam>
    public class MenuOption
    {
        /// <summary>
        /// Option from enum T.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Title of option to display to a user.
        /// </summary>
        public string? Title { get; set; }
    }
}
