namespace BasicConsoleLibrary.Core.Menus;
internal class MenuSelectionEngine(string main, BasicList<MenuItem> items, int pageSize = 5)
{
    //i like the idea of just returning the number.
    public async Task<int?> ShowAsync(CancellationToken cancellationToken)
    {
        int selectedIndex = 0;
        int visibleStart = 0;
        StringBuilder numberBuffer = new();

        ConsoleKeyInfo keyInfo;
        do
        {
            Render(items, selectedIndex, visibleStart);

            keyInfo = Console.ReadKey(intercept: true);

            if (char.IsDigit(keyInfo.KeyChar))
            {
                numberBuffer.Append(keyInfo.KeyChar);

                if (int.TryParse(numberBuffer.ToString(), out int typedIndex))
                {
                    typedIndex = Math.Max(1, typedIndex);
                    typedIndex = Math.Min(items.Count, typedIndex);
                    selectedIndex = typedIndex - 1;

                    // Adjust visibleStart so typed selection is at the top of the page
                    visibleStart = selectedIndex;

                    // But make sure we don't go past the last page
                    if (visibleStart + pageSize > items.Count)
                    {
                        visibleStart = Math.Max(0, items.Count - pageSize);
                    }
                }

                continue; // Skip switch and re-render
            }
            else
            {
                numberBuffer.Clear(); // Non-digit resets buffer
            }

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedIndex > 0)
                    {
                        selectedIndex--;
                    }

                    if (selectedIndex < visibleStart)
                    {
                        visibleStart--;
                    }

                    break;

                case ConsoleKey.DownArrow:
                    if (selectedIndex < items.Count - 1)
                    {
                        selectedIndex++;
                    }

                    if (selectedIndex >= visibleStart + pageSize)
                    {
                        visibleStart++;
                    }

                    break;

                case ConsoleKey.Escape:
                    return null; // user backed out

                case ConsoleKey.Enter:
                    return selectedIndex; // selected item
            }

        } while (true);
    }
    private int? _menuStartLine;
    private void Render(BasicList<MenuItem> items, int selectedIndex, int visibleStart)
    {
        int suggestionStartLine = _menuStartLine ??= Console.CursorTop;

        // Clear previous menu
        int totalLines = 1 + Math.Min(pageSize, items.Count); // title + visible items
        for (int i = 0; i < totalLines; i++)
        {
            Console.SetCursorPosition(0, suggestionStartLine + i);
            ClearCurrentConsoleLine();
        }

        // Render main title
        Console.SetCursorPosition(0, suggestionStartLine);
        AnsiConsole.Write(new Text(main, cc1.Cyan));

        // Render visible items below the main title
        for (int i = 0; i < pageSize; i++)
        {
            int itemIndex = visibleStart + i;
            if (itemIndex >= items.Count) break;

            Console.SetCursorPosition(0, suggestionStartLine + 1 + i);

            var item = items[itemIndex];
            string numberPrefix = $"{itemIndex + 1}. ";
            string prefix = itemIndex == selectedIndex ? "> " : "  ";

            if (itemIndex == selectedIndex)
            {
                AnsiConsole.Write(new Text($"{prefix}{numberPrefix}{item.Title}", cc1.Lime));
            }
            else
            {
                AnsiConsole.Write($"{prefix}{numberPrefix}{item.Title}");
            }
        }

        // Restore cursor position at selected item line
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