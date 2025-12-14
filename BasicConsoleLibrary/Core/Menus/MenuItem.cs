namespace BasicConsoleLibrary.Core.Menus;
public class MenuItem
{
    public string Title { get; set; } = "";
    public Func<Task>? Action { get; set; }
    public bool Enabled { get; set; } = true; //defaults to being enabled.
}