namespace BasicConsoleLibrary.Core.Live;

/// <summary>
/// Represents a live display.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LiveDisplay"/> class.
/// </remarks>
/// <param name="console">The console.</param>
/// <param name="target">The target renderable to update.</param>
public sealed class LiveDisplay
{
    private IRenderable _currentRenderable;
    private BasicList<SegmentLine> _virtualBuffer = [];
    private BasicList<SegmentLine> _previousRenderedLines = [];
    private readonly Lock _lock = new();

    private readonly int _viewportTop;
    private readonly int _viewportHeight;
    private int _scrollOffset;

    public LiveDisplay(IRenderable renderable)
    {
        _currentRenderable = renderable;
        _viewportTop = Console.CursorTop;
        _viewportHeight = Console.BufferHeight - _viewportTop - 1;
        _scrollOffset = 0;
    }


    /// <summary>
    /// Gets or sets a value indicating whether or not the live display should
    /// be cleared when it's done.
    /// Defaults to <c>false</c>.
    /// </summary>
    public bool AutoClear { get; set; }

    public void Start(Action<LiveDisplayContext> action)
    {
        Console.CursorVisible = false;
        var context = new LiveDisplayContext(this, _currentRenderable);
        Update(_currentRenderable);
        action(context);
        RenderFinal();
        Console.CursorVisible = true;
    }

    public async Task StartAsync(Func<LiveDisplayContext, Task> action)
    {
        Console.CursorVisible = false;
        var context = new LiveDisplayContext(this, _currentRenderable);
        Update(_currentRenderable);
        await action(context);
        RenderFinal();
        Console.CursorVisible = true;
    }
    internal void Update(IRenderable renderable)
    {
        ArgumentNullException.ThrowIfNull(renderable);
        _currentRenderable = renderable;

        RenderOptions renderOptions = new();
        var segments = renderable.Render(renderOptions, Console.BufferWidth);
        var newLines = Segment.SplitLines(segments);

        _virtualBuffer = newLines;

        // Resize previous rendered buffer to match virtual buffer
        while (_previousRenderedLines.Count < _virtualBuffer.Count)
        {
            _previousRenderedLines.Add([.. new List<Segment>()]);
        }

        while (_previousRenderedLines.Count > _virtualBuffer.Count)
        {
            _previousRenderedLines.RemoveAt(_previousRenderedLines.Count - 1);
        }

        // Determine scroll position
        if (_virtualBuffer.Count <= _viewportHeight)
        {
            // Content fits, show from top
            _scrollOffset = 0;
        }
        else
        {
            // Scroll to bottom (auto-scroll behavior)
            _scrollOffset = _virtualBuffer.Count - _viewportHeight;
        }

        RenderViewport();
    }
    private void RenderViewport()
    {
        // Move cursor to start of viewport
        Console.SetCursorPosition(0, _viewportTop);

        for (int i = 0; i < _viewportHeight; i++)
        {
            int lineIndex = _scrollOffset + i;

            SegmentLine? newLine = lineIndex < _virtualBuffer.Count ? _virtualBuffer[lineIndex] : null;
            SegmentLine? oldLine = lineIndex < _previousRenderedLines.Count ? _previousRenderedLines[lineIndex] : null;

            if (newLine == null)
            {
                // Clear the line
                ClearCurrentLine();
                if (lineIndex < _previousRenderedLines.Count)
                {
                    _previousRenderedLines[lineIndex] = [.. new List<Segment>()];
                }
            }
            else if (oldLine == null)
            {
                ClearCurrentLine();
                WriteSegmentLine(newLine);
                if (lineIndex < _previousRenderedLines.Count)
                {
                    _previousRenderedLines[lineIndex] = newLine;
                }
                else
                {
                    _previousRenderedLines.Add(newLine);
                }
            }
            else
            {
                WriteSegmentLineDiff(oldLine, newLine);
                _previousRenderedLines[lineIndex] = newLine;
            }

            if (i < _viewportHeight - 1)
            {
                Console.Write("\x1b[1B"); // Move cursor down 1 line
            }
        }

        // Reset cursor below viewport after rendering
        Console.SetCursorPosition(0, _viewportTop + _viewportHeight);
    }
    private void RenderFinal()
    {
        lock (_lock)
        {
            // Render the final content completely
            RenderOptions renderOptions = new();
            var segments = _currentRenderable.Render(renderOptions, Console.BufferWidth);
            var finalLines = Segment.SplitLines(segments);

            // Move to the start of where live rendering began
            Console.SetCursorPosition(0, _viewportTop);

            // Clear all lines used previously
            for (int i = 0; i < _previousRenderedLines.Count; i++)
            {
                ClearCurrentLine();
                if (i < _previousRenderedLines.Count - 1)
                {
                    Console.Write("\x1b[1B"); // Move down
                }
            }

            // Ensure cursor is on a fresh line for final output
            Console.SetCursorPosition(0, _viewportTop);

            // Write out all final lines, letting console scroll naturally
            foreach (var line in finalLines)
            {
                Console.Write("\r"); // Return to start of line
                WriteSegmentLine(line);
                Console.WriteLine(); // Let console handle scrolling
            }

            // Reset buffers for consistency (not strictly needed after final)
            _virtualBuffer = finalLines;
            _previousRenderedLines = [.. finalLines];
        }
    }

    private static void WriteSegmentLineDiff(SegmentLine oldLine, SegmentLine newLine)
    {
        int oldCount = oldLine.Count;
        int newCount = newLine.Count;
        int maxCount = Math.Max(oldCount, newCount);

        Console.Write("\r"); // Move to start of line

        for (int i = 0; i < maxCount; i++)
        {
            Segment? oldSegment = i < oldCount ? oldLine[i] : null;
            Segment? newSegment = i < newCount ? newLine[i] : null;

            if (SegmentsEqual(oldSegment, newSegment))
            {
                if (newSegment != null)
                {
                    Console.Write(newSegment.Text);
                }
            }
            else
            {
                if (newSegment != null)
                {
                    Console.Write(newSegment.Text);
                }
                else if (oldSegment != null)
                {
                    Console.Write(new string(' ', oldSegment.Text.Length));
                }
            }
        }

        // If old line had extra segments, clear them
        if (newCount < oldCount)
        {
            for (int i = newCount; i < oldCount; i++)
            {
                var oldText = oldLine[i].Text;
                Console.Write(new string(' ', oldText.Length));
            }
        }
    }

    private static void WriteSegmentLine(SegmentLine line)
    {
        Console.Write("\r");
        foreach (var segment in line)
        {
            Console.Write(segment.Text); // Add styling here if needed
        }
    }

    private static void ClearCurrentLine()
    {
        Console.Write("\x1b[2K\r"); // Clear entire line and return to start
    }

    private static bool SegmentsEqual(Segment? a, Segment? b)
    {
        if (a == null && b == null)
        {
            return true;
        }

        if (a == null || b == null)
        {
            return false;
        }

        return a.Text == b.Text && a.Style.Equals(b.Style);
    }
}