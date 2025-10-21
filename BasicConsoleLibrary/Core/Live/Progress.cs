namespace BasicConsoleLibrary.Core.Live;
public sealed class Progress
{
    //
    // Summary:
    //     Gets or sets a value indicating whether or not the task list should be cleared
    //     once it completes. Defaults to false.
    public bool AutoClear { get; set; }
    //
    // Summary:
    //     Gets or sets the refresh rate if AutoRefresh is enabled. Defaults to 10 times/second.
    public TimeSpan RefreshRate { get; set; } = TimeSpan.FromMilliseconds(100L, 0L);


    /// <summary>
    /// Gets or sets the color of completed portions of the progress bar.
    /// </summary>
    public ConsoleColor CompletedColor { get; set; } = ConsoleColor.Yellow;

    /// <summary>
    /// Gets or sets the color of a finished progress bar.
    /// </summary>
    public ConsoleColor FinishedColor { get; set; } = ConsoleColor.Green;

    /// <summary>
    /// Gets or sets the color of remaining portions of the progress bar.
    /// </summary>
    public ConsoleColor RemainingColor { get; set; } = ConsoleColor.DarkGray;


    public void Start(Action<ProgressContext> action)
    {
        RunAsync(ctx =>
        {
            action(ctx);
            return Task.CompletedTask;
        }).GetAwaiter().GetResult();
    }
    public Task StartAsync(Func<ProgressContext, Task> action)
    {
        return RunAsync(action);
    }

    private async Task RunAsync(Func<ProgressContext, Task> action)
    {
        var context = new ProgressContext();

        // Capture initial cursor position before rendering
        int startRow = Console.CursorTop;

        // Hide cursor for smoothness
        Console.CursorVisible = false;

        var stop = false;

        Thread renderThread = new(() =>
        {
            while (!stop)
            {
                // Jump cursor back to the start row to overwrite progress bars
                Console.SetCursorPosition(0, startRow);

                int maxs = context.Tasks.Any() ? context.Tasks.Max(x => x.Name.Length) : 0;

                foreach (var task in context.Tasks)
                {
                    RenderTask(task, maxs);
                }

                // Move cursor back up so next render overwrites same lines
                Console.SetCursorPosition(0, startRow);

                Thread.Sleep(100);
            }
        });

        renderThread.Start();

        try
        {
            await action(context);
        }
        finally
        {
            stop = true;
            renderThread.Join();

            // Show cursor again
            Console.CursorVisible = true;

            if (AutoClear)
            {
                // Clear the task lines area
                Console.SetCursorPosition(0, startRow);
                for (int i = 0; i < context.Tasks.Count; i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }
                Console.SetCursorPosition(0, startRow);
            }
            else
            {
                Console.SetCursorPosition(0, startRow + context.Tasks.Count);
            }
        }
    }


    private void RenderTask(ProgressTask task, int maxPadLength)
    {
        const int progressBarWidth = 40;
        int filledLength = (int)(task.Percentage / 100.0 * progressBarWidth);
        int emptyLength = progressBarWidth - filledLength;

        string taskName = task.Name.PadRight(maxPadLength);

        // Write task name
        Console.Write(taskName + " ");

        // Save current color
        var prevColor = Console.ForegroundColor;

        // Write filled part of the bar in Yellow or Green if complete
        Console.ForegroundColor = task.Percentage >= 100 ? FinishedColor : CompletedColor;
        Console.Write(new string('━', filledLength));

        // Write empty part of the bar in DarkGray
        Console.ForegroundColor = RemainingColor;
        Console.Write(new string('━', emptyLength));

        // Reset color
        Console.ForegroundColor = prevColor;

        // Write the percentage aligned to the right, padded so leftover chars get cleared
        string percentText = $" {task.Percentage:0}%".PadRight(6);
        Console.WriteLine(percentText);
    }
}