namespace BasicConsoleLibrary.Core.Live;
public sealed class Status
{
    /// <summary>
    /// Gets or sets the spinner.
    /// </summary>
    public Spinner? Spinner { get; set; }

    /// <summary>
    /// Gets or sets the spinner style.
    /// </summary>
    public Style? SpinnerStyle { get; set; } = cc2.Yellow;

    /// <summary>
    /// Gets or sets a value indicating whether or not status
    /// should auto refresh. Defaults to <c>true</c>.
    /// </summary>
    public bool AutoRefresh { get; set; } = true;
    public void Start(string status, Action<StatusContext> action)
    {
        RunAsync(status, ctx =>
        {
            action(ctx);
            return Task.CompletedTask;
        }).GetAwaiter().GetResult();
    }

    public Task StartAsync(string status, Func<StatusContext, Task> action)
    {
        return RunAsync(status, action);
    }

    private async Task RunAsync(string status, Func<StatusContext, Task> action)
    {
        _lastRenderedLine = "";
        _lastCursorTop = -1;
        Console.CursorVisible = false;
        Spinner spin;
        spin = Spinner is not null ? Spinner : Spinner.Known.Default;
        var context = new StatusContext
        {
            Status = status,
            Spinner = spin,
            SpinnerStyle = SpinnerStyle,
        };

        context.RequestRender = () => Render(context, 0);

        var stop = false;
        var spinnerThread = new Thread(() =>
        {
            int frame = 0;
            while (!stop)
            {
                Render(context, frame++);
                Thread.Sleep(context.Spinner?.Interval.Milliseconds ?? 100);
            }
        });

        spinnerThread.Start();

        try
        {
            await action(context);
        }
        finally
        {
            stop = true;
            spinnerThread.Join();
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, _lastCursorTop);
            Console.Clear();
        }
    }
    private static string _lastRenderedLine = "";
    private static int _lastCursorTop = -1;

    private static void Render(StatusContext context, int frame)
    {
        var frames = context.Spinner?.Frames ?? ["|"];
        var spinnerChar = frames[frame % frames.Count];

        string line;
        if (context.SpinnerStyle is not null)
        {
            line = AnsiCodeHelper.ToAnsiCode(context.SpinnerStyle) + $"{spinnerChar} " + AnsiCodeHelper.AnsiReset + context.Status;
        }
        else
        {
            line = $"{spinnerChar} {context.Status}";
        }

        // Pad with spaces if shorter than previous line
        if (line.Length < _lastRenderedLine.Length)
        {
            line = line.PadRight(_lastRenderedLine.Length);
        }

        // Only redraw if changed, to avoid flickering
        if (line != _lastRenderedLine)
        {
            // Save current cursor position
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            // Move cursor to the spinner line (top line, or fixed position)
            if (_lastCursorTop == -1)
            {
                _lastCursorTop = currentTop;
            }

            Console.SetCursorPosition(0, _lastCursorTop);
            Console.Write(line);

            // Restore cursor position below spinner line to avoid interfering with user input
            Console.SetCursorPosition(currentLeft, currentTop);

            _lastRenderedLine = line;
        }
    }


}