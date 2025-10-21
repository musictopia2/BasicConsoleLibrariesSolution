namespace BasicConsoleLibrary.Core.Extensions;
public static class ProgressExtensions
{
    public static Progress AutoClear(this Progress progress, bool clear)
    {
        progress.AutoClear = clear;
        return progress;
    }
    public static Progress CompletedColor(this Progress column, ConsoleColor color)
    {
        ArgumentNullException.ThrowIfNull(column);
        column.CompletedColor = color;
        return column;
    }
    public static Progress FinishedColor(this Progress column, ConsoleColor color)
    {
        ArgumentNullException.ThrowIfNull(column);
        column.FinishedColor = color;
        return column;
    }
    public static Progress RemainingColor(this Progress column, ConsoleColor color)
    {
        ArgumentNullException.ThrowIfNull(column);
        column.RemainingColor = color;
        return column;
    }
}