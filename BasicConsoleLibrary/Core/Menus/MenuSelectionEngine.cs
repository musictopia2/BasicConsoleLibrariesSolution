namespace BasicConsoleLibrary.Core.Menus;
internal class MenuSelectionEngine(string main, BasicList<MenuItem> items, int pageSize = 5)
{
    //i like the idea of just returning the number.
    public async Task<int?> ShowAsync(CancellationToken cancellationToken)
    {
        int selectedIndex = 0;
        int visibleStart = 0;

        ConsoleKey key;
        do
        {
            Render(items, selectedIndex, visibleStart);
            key = Console.ReadKey(intercept: true).Key;

            switch (key)
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
    private void Render(BasicList<MenuItem> items, int selectedIndex, int visibleStart)
    {
        int suggestionStartLine = Console.CursorTop;
        AnsiConsole.Write(new Text(main, cc1.Cyan));
        // Render visible items
        for (int i = 0; i < pageSize; i++)
        {
            int itemIndex = visibleStart + i;

            Console.SetCursorPosition(0, suggestionStartLine + i);
            ClearCurrentConsoleLine();

            if (itemIndex >= items.Count)
            {
                continue;
            }

            var item = items[itemIndex];
            string prefix = itemIndex == selectedIndex ? "> " : "  ";

            if (itemIndex == selectedIndex)
            {
                // Highlight selected item
                Text text = new($"{prefix}{item.Title}", cc1.Lime);
                AnsiConsole.Write(text);
            }
            else
            {
                // Regular item
                AnsiConsole.Write(prefix + item.Title);
            }
        }

        // Clear any remaining lines if fewer than pageSize items visible
        for (int i = items.Count - visibleStart; i < pageSize; i++)
        {
            Console.SetCursorPosition(0, suggestionStartLine + i);
            ClearCurrentConsoleLine();
        }

        // Restore cursor position (so typing/number input can appear if needed)
        Console.SetCursorPosition(0, suggestionStartLine + selectedIndex - visibleStart);
    }

    private static void ClearCurrentConsoleLine()
    {
        int currentLine = Console.CursorTop;
        Console.SetCursorPosition(0, currentLine);
        AnsiConsole.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLine);
    }
}