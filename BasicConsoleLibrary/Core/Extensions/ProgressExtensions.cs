namespace BasicConsoleLibrary.Core.Extensions;
public static class ProgressExtensions
{
    extension (Progress progress)
    {
        public Progress AutoClear(bool clear)
        {
            progress.AutoClear = clear;
            return progress;
        }
        public Progress CompletedColor(ConsoleColor color)
        {
            ArgumentNullException.ThrowIfNull(progress);
            progress.CompletedColor = color;
            return progress;
        }
        public Progress FinishedColor(ConsoleColor color)
        {
            ArgumentNullException.ThrowIfNull(progress);
            progress.FinishedColor = color;
            return progress;
        }
        public Progress RemainingColor(ConsoleColor color)
        {
            ArgumentNullException.ThrowIfNull(progress);
            progress.RemainingColor = color;
            return progress;
        }
    }
}