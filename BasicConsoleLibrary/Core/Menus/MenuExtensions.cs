namespace BasicConsoleLibrary.Core.Menus;
public static class MenuExtensions
{
    extension (ComboMenu menu)
    {
        public ComboMenu Add(string title, Func<Task> action)
        {
            menu.Items.Add(new()
            {
                Title = title,
                Action = action
            });
            return menu;
        }
        public ComboMenu Add(string title, Func<Task> action, bool enabled)
        {
            menu.Items.Add(new()
            {
                Title = title,
                Action = action,
                Enabled = enabled
            });
            return menu;
        }
    }
}