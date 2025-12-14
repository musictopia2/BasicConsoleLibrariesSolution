namespace BasicConsoleLibrary.Core.Menus;

internal class MenuSelectionEngine(string main, BasicList<MenuItem> items, int pageSize = 5)
{
    public async Task<int?> ShowAsync(CancellationToken cancellationToken)
    {
        int selectedIndex = GetNextEnabledIndex(0, 1) ?? 0; // first enabled item
        int visibleStart = 0;
        StringBuilder numberBuffer = new();

        ConsoleKeyInfo keyInfo;

        do
        {
            Render(items, selectedIndex, visibleStart, numberBuffer.ToString());

            keyInfo = Console.ReadKey(intercept: true);

            if (char.IsDigit(keyInfo.KeyChar))
            {
                // Append typed number
                numberBuffer.Append(keyInfo.KeyChar);

                if (int.TryParse(numberBuffer.ToString(), out int typedIndex))
                {
                    // Clamp typed index
                    typedIndex = Math.Max(1, typedIndex);
                    typedIndex = Math.Min(items.Count, typedIndex);

                    // Jump to first enabled item >= typedIndex
                    int? nextEnabled = GetNextEnabledIndex(typedIndex - 1, 1);
                    if (nextEnabled.HasValue)
                        selectedIndex = nextEnabled.Value;

                    // Scroll typed selection to top of page
                    visibleStart = selectedIndex;
                    if (visibleStart + pageSize > items.Count)
                        visibleStart = Math.Max(0, items.Count - pageSize);
                }

                continue; // skip switch
            }

            // Clear number buffer if arrow key is pressed
            if (keyInfo.Key is ConsoleKey.UpArrow or ConsoleKey.DownArrow)
            {
                numberBuffer.Clear();
            }

            // Handle navigation & actions
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    {
                        int? prev = GetNextEnabledIndex(selectedIndex - 1, -1);
                        if (prev.HasValue)
                        {
                            selectedIndex = prev.Value;
                            if (selectedIndex < visibleStart)
                                visibleStart = selectedIndex;
                        }
                    }
                    break;

                case ConsoleKey.DownArrow:
                    {
                        int? next = GetNextEnabledIndex(selectedIndex + 1, 1);
                        if (next.HasValue)
                        {
                            selectedIndex = next.Value;
                            if (selectedIndex >= visibleStart + pageSize)
                                visibleStart = selectedIndex - pageSize + 1;
                        }
                    }
                    break;

                case ConsoleKey.Escape:
                    return null;

                case ConsoleKey.Enter:
                    return selectedIndex;
            }

        } while (true);

        // Helper: get next enabled index in the given direction
        int? GetNextEnabledIndex(int start, int direction)
        {
            int idx = start;
            while (idx >= 0 && idx < items.Count)
            {
                if (items[idx].Enabled)
                    return idx;
                idx += direction;
            }
            return null;
        }
    }

    private int? _menuStartLine;

    private void Render(BasicList<MenuItem> items, int selectedIndex, int visibleStart, string typedNumber)
    {
        int suggestionStartLine = _menuStartLine ??= Console.CursorTop;

        // Clear previous menu + typed number line
        int totalLines = 1 + Math.Min(pageSize, items.Count);
        for (int i = 0; i < totalLines + 1; i++)
        {
            Console.SetCursorPosition(0, suggestionStartLine + i);
            ClearCurrentConsoleLine();
        }

        // Render title
        Console.SetCursorPosition(0, suggestionStartLine);
        AnsiConsole.Write(new Text(main, cc1.Cyan));

        // Render visible items
        for (int i = 0; i < pageSize; i++)
        {
            int itemIndex = visibleStart + i;
            if (itemIndex >= items.Count) break;

            Console.SetCursorPosition(0, suggestionStartLine + 1 + i);

            var item = items[itemIndex];
            string numberPrefix = $"{itemIndex + 1}. ";
            string prefix = itemIndex == selectedIndex ? "> " : "  ";

            if (!item.Enabled)
            {
                AnsiConsole.Write(new Text($"{prefix}{numberPrefix}{item.Title}", cc1.DarkGray));
            }
            else if (itemIndex == selectedIndex)
            {
                AnsiConsole.Write(new Text($"{prefix}{numberPrefix}{item.Title}", cc1.Lime));
            }
            else
            {
                AnsiConsole.Write($"{prefix}{numberPrefix}{item.Title}");
            }
        }

        // Render typed number at bottom, only if buffer has content
        if (!string.IsNullOrEmpty(typedNumber))
        {
            Console.SetCursorPosition(0, suggestionStartLine + totalLines);
            ClearCurrentConsoleLine();
            AnsiConsole.Write(new Text($"Typed: {typedNumber}", cc1.Yellow));
        }

        // Restore cursor to selected item
        Console.SetCursorPosition(0, suggestionStartLine + 1 + selectedIndex - visibleStart);
    }

    private static void ClearCurrentConsoleLine()
    {
        int currentLine = Console.CursorTop;
        Console.SetCursorPosition(0, currentLine);
        AnsiConsole.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLine);
    }
}