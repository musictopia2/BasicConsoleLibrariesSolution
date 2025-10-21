namespace BasicConsoleLibrary.Core.Prompts;
/// <summary>
/// Allows selecting multiple items from a provided list, with optional grouping.
/// </summary>
/// <typeparam name="T">Type of selectable items.</typeparam>
public class MultiSelectionPrompt<T>(string prompt) : IPrompt<BasicList<T>>
    where T : notnull
{
    private sealed record RenderItem(string? GroupName, T? Value, bool IsGroupHeader, bool IsStandalone = false);

    private readonly List<RenderItem> _items = [];
    private readonly string _prompt = prompt ?? throw new ArgumentNullException(nameof(prompt));

    private int _cursorIndex = 0;
    private int _pageStartIndex = 0; // For paging

    private readonly HashSet<int> _selectedIndices = [];

    /// <summary>
    /// Gets or sets the prompt style.
    /// </summary>
    public Style? PromptStyle { get; set; }

    public Style? SelectionStyle { get; set; } = cc2.Blue;

    /// <summary>
    /// Gets or sets the validation error message.
    /// </summary>
    public string ValidationErrorMessage { get; set; } = "Invalid input";

    /// <summary>
    /// Gets or sets the validation error style.
    /// </summary>
    public Style ValidationErrorStyle { get; set; } = cc2.Red;

    /// <summary>
    /// Gets or sets the validator for selected items.
    /// </summary>
    public Func<BasicList<T>, ValidationResult>? Validator { get; set; }

    /// <summary>
    /// Page size for visible items. Default is 10.
    /// </summary>
    public int PageSize { get; set; } = 10;
    public void AddChoices(params BasicList<T> choices)
    {
        foreach (var item in choices)
        {
            _items.Add(new RenderItem(null, item, false, true));
        }
    }

    public void AddChoiceGroup(string groupName, params BasicList<T> items)
    {
        if (string.IsNullOrWhiteSpace(groupName))
        {
            throw new ArgumentException("Group name cannot be null or whitespace.", nameof(groupName));
        }

        _items.Add(new RenderItem(groupName, default, true)); // group header

        foreach (var item in items)
        {
            _items.Add(new RenderItem(groupName, item, false));
        }
    }

    public BasicList<T> Show()
    {
        return ShowAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    public async Task<BasicList<T>> ShowAsync(CancellationToken cancellationToken)
    {

        if (_items.Count == 0)
        {
            throw new CustomBasicException("No choices have been added.");
        }

        return await ConsoleExclusive.RunAsync(async () =>
        {
            while (true)
            {
                Console.Clear();
                RenderPrompt();

                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        _cursorIndex = _cursorIndex == 0 ? _items.Count - 1 : _cursorIndex - 1;
                        AdjustPageStartForCursor();
                        break;

                    case ConsoleKey.DownArrow:
                        _cursorIndex = (_cursorIndex + 1) % _items.Count;
                        AdjustPageStartForCursor();
                        break;

                    case ConsoleKey.Spacebar:
                        HandleToggleSelection();
                        break;

                    case ConsoleKey.Enter:
                        var selected = _selectedIndices
                            .Select(index => _items[index])
                            .Where(item => item.Value is not null)
                            .Select(item => item.Value!)
                            .ToBasicList();

                        if (Validator is not null)
                        {
                            var result = Validator.Invoke(selected);
                            if (!result.Successful)
                            {
                                AnsiConsole.WriteLine();
                                AnsiConsole.Write(new Text(result.Message!, ValidationErrorStyle));
                                await Task.Delay(1500);
                                continue;
                            }
                        }
                        AnsiConsole.WriteLine(); // Line break
                        return selected;
                }
            }
        }) ?? throw new CustomBasicException("Prompt failed unexpectedly.");
    }
    private void AdjustPageStartForCursor()
    {
        int middleOffset = PageSize / 2;
        // Ensure we never go below 0 or above max page start
        int maxPageStart = Math.Max(0, _items.Count - PageSize);

        if (_cursorIndex <= _pageStartIndex + middleOffset && _pageStartIndex > 0)
        {
            // Cursor is in the upper half of the page and page can scroll up
            _pageStartIndex = Math.Max(0, _cursorIndex - middleOffset);
        }
        else if (_cursorIndex >= _pageStartIndex + PageSize - middleOffset && _pageStartIndex < maxPageStart)
        {
            // Cursor is in the lower half of the page and page can scroll down
            _pageStartIndex = Math.Min(maxPageStart, _cursorIndex - middleOffset);
        }
    }
    private void HandleToggleSelection()
    {
        var currentItem = _items[_cursorIndex];

        if (currentItem.IsGroupHeader)
        {
            string? groupName = currentItem.GroupName;

            if (groupName is null)
            {
                return;
            }

            // Toggle selection for all items in the group
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].GroupName == groupName && !_items[i].IsGroupHeader)
                {
                    if (!_selectedIndices.Remove(i))
                    {
                        _selectedIndices.Add(i);
                    }
                }
            }
        }
        else
        {
            if (!_selectedIndices.Remove(_cursorIndex))
            {
                _selectedIndices.Add(_cursorIndex);
            }
        }
    }

    private void RenderPrompt()
    {
        var promptStyle = PromptStyle ?? Style.Plain;
        AnsiConsole.Write(new Text(_prompt + Environment.NewLine, promptStyle));
        int pageEndIndex = Math.Min(_pageStartIndex + PageSize, _items.Count);

        for (int i = _pageStartIndex; i < pageEndIndex; i++)
        {
            var item = _items[i];
            bool isCurrent = i == _cursorIndex;

            string cursor = isCurrent ? ">" : " ";
            string box = _selectedIndices.Contains(i) ? "[x]" : "[ ]";
            var style = isCurrent ? SelectionStyle : Style.Plain;
            string line;

            if (item.IsGroupHeader)
            {
                int startIndex = i + 1;
                int selected = 0;
                int total = 0;

                for (int j = startIndex; j < _items.Count; j++)
                {
                    if (_items[j].IsGroupHeader || _items[j].IsStandalone)
                    {
                        break;
                    }

                    total++;
                    if (_selectedIndices.Contains(j))
                    {
                        selected++;
                    }
                }

                string groupBox = total > 0 && selected == total ? "[x]" : "[ ]";
                line = $"{cursor} {groupBox} {item.GroupName}";
            }
            else if (item.IsStandalone)
            {
                string label = item.Value?.ToString() ?? "";
                line = $"{cursor} {box} {label}";
            }
            else
            {
                string label = item.Value?.ToString() ?? "";
                line = $"{cursor}   {box} {label}";
            }
            AnsiConsole.Write(new Text(line + Environment.NewLine, style));
        }
        var footer = $"(Use ↑/↓ to move, space to toggle, enter to confirm) Showing {_pageStartIndex + 1} to {pageEndIndex} of {_items.Count}";
        AnsiConsole.Write(new Text(Environment.NewLine + footer + Environment.NewLine, cc2.Gray));
    }
}