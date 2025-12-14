namespace BasicConsoleLibrary.Core.Menus;
/// <summary>
/// Represents a menu.
/// </summary>
public interface IMenu
{
    /// <summary>
    /// Shows the menu asynchronously.
    /// </summary>
    Task ShowAsync(CancellationToken cancellationToken);
}