namespace BasicConsoleLibrary.Core;
public static partial class AnsiConsole
{
    public static Task ShowMenuAsync(IMenu menu, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(menu);
        return menu.ShowAsync(cancellationToken);
    }
}