namespace BasicConsoleLibrary.Core.Prompts;
public class ConsoleComboBox(BasicList<string> options, bool allowCustom = false, int pageSize = 5) : IPrompt<string?>
{
    private readonly List<string> _options = [.. options.OrderBy(x => x)];
    public int PageSize { get; set; } = pageSize;
    public string PromptString { get; set; } = "Select:  ";
    public Style PromptStyle { get; set; } = Style.Plain;
    public Style GhostStyle { get; set; } = cc2.DarkGray;
    public Style SelectedStyle { get; set; } = cc2.Green;
    public string? Show()
    {
        return ShowAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    public async Task<string?> ShowAsync(CancellationToken cancellationToken)
    {
        return await ConsoleExclusive.RunAsync(() => ShowInsideExclusive(cancellationToken));
    }

    public async Task<string?> ShowInsideExclusive(CancellationToken cancellationToken)
    {
        var input = new StringBuilder();
        int selectedIndex = -1;
        int visibleStart = 0;
        if (PromptString.EndsWith(' ') == false)
        {
            PromptString = $"{PromptString} ";
        }
        bool isGhostActive = false;
        bool ghostJustCancelled = false;

        await Task.Delay(0, cancellationToken);
        WritePrompt();
        int inputLine = Console.CursorTop;

        var filtered = GetFiltered(input.ToString());
        RenderSuggestions(filtered, selectedIndex, visibleStart, inputLine, PromptString, input.ToString(), isGhostActive);
        while (true)
        {
            var key = Console.ReadKey(intercept: true);

            switch (key.Key)
            {
                case ConsoleKey.Backspace:
                    if (isGhostActive && allowCustom)
                    {
                        isGhostActive = false;
                        ghostJustCancelled = true;
                    }
                    else if (input.Length > 0)
                    {
                        input.Remove(input.Length - 1, 1);
                        selectedIndex = input.Length == 0 ? -1 : 0;
                        visibleStart = 0;
                        ghostJustCancelled = false;
                    }
                    break;

                case ConsoleKey.Enter:
                    if (string.IsNullOrWhiteSpace(input.ToString()))
                    {
                        if (!allowCustom && selectedIndex == -1)
                        {
                            break;
                        }

                        if (!allowCustom)
                        {
                            Console.Clear(); //i think.
                            return _options[selectedIndex];
                        }
                        Console.Clear();
                        return null;
                    }

                    var filteredEnter = GetFiltered(input.ToString());

                    if (filteredEnter.Count > 0 && isGhostActive)
                    {
                        Console.Clear();
                        return filteredEnter[0];
                    }
                    else if (!allowCustom && filtered.Count > 0)
                    {
                        Console.Clear();
                        return filteredEnter[0];
                    }
                    else if (allowCustom)
                    {
                        Console.Clear();
                        return input.ToString();
                    }
                    break;

                case ConsoleKey.Tab:
                    if (isGhostActive && filtered.Count > 0)
                    {
                        isGhostActive = false;
                        ghostJustCancelled = true;
                    }
                    if (filtered.Count > 0)
                    {
                        input.Clear();
                        input.Append(filtered[0]);
                        selectedIndex = 0;
                        visibleStart = 0;
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (selectedIndex > 0)
                    {
                        selectedIndex--;
                        if (selectedIndex < visibleStart)
                        {
                            visibleStart = selectedIndex;
                        }
                    }
                    break;
                case ConsoleKey.DownArrow:
                    var filteredDown = GetFiltered(input.ToString());
                    if (selectedIndex < filteredDown.Count - 1)
                    {
                        selectedIndex++;
                        if (selectedIndex >= visibleStart + PageSize)
                        {
                            visibleStart = selectedIndex - PageSize + 1;
                        }
                    }
                    break;

                case ConsoleKey.Escape:
                    return null;

                default:
                    if (!char.IsControl(key.KeyChar))
                    {
                        input.Append(key.KeyChar);
                        selectedIndex = 0;
                        visibleStart = 0;

                        var filteredTyped = GetFiltered(input.ToString());
                        isGhostActive = allowCustom && filteredTyped.Count > 0;
                        ghostJustCancelled = false;
                    }
                    break;
            }

            filtered = GetFiltered(input.ToString());

            if (!ghostJustCancelled && allowCustom && filtered.Count > 0)
            {
                isGhostActive = true;
            }

            Console.SetCursorPosition(0, inputLine);
            ClearCurrentConsoleLine();
            WritePrompt(input.ToString());

            RenderSuggestions(filtered, selectedIndex, visibleStart, inputLine, PromptString, input.ToString(), isGhostActive);
        }
    }

    /// <summary>
    /// Writes the prompt to the console.
    /// </summary>
    /// <param name="console">The console to write the prompt to.</param>
    private void WritePrompt(string extras = "")
    {
        var promptStyle = PromptStyle ?? Style.Plain;
        var builder = new StringBuilder();
        builder.Append(PromptString);
        builder.Append(extras);
        var markup = builder.ToString().TrimStart();
        Text text = new(markup, promptStyle);
        AnsiConsole.Write(text); //hopefully this works (?)
    }
    private List<string> GetFiltered(string filter)
    {
        return [.. _options
            .Where(x => x.StartsWith(filter, StringComparison.OrdinalIgnoreCase))
            .Take(100)];
    }

    private void RenderSuggestions(List<string> options, int selectedIndex, int visibleStart, int inputLine, string prompt, string input, bool isGhostActive)
    {
        int suggestionStartLine = inputLine + 1;

        // Inline ghost suggestion
        if (allowCustom && isGhostActive && options.Count > 0)
        {
            string firstOption = options[0];
            if (firstOption.StartsWith(input, StringComparison.OrdinalIgnoreCase) && input.Length < firstOption.Length)
            {
                Console.SetCursorPosition(prompt.Length + input.Length, inputLine);
                Text text = new(firstOption.Substring(input.Length), GhostStyle);
                AnsiConsole.Write(text);
                AnsiConsole.ResetColors();
            }
        }

        // Render suggestion list
        for (int i = 0; i < PageSize; i++)
        {
            Console.SetCursorPosition(0, suggestionStartLine + i);
            ClearCurrentConsoleLine();

            int optionIndex = visibleStart + i;
            if (optionIndex >= options.Count)
            {
                continue;
            }

            string prefix = optionIndex == selectedIndex ? "> " : "  ";

            if (optionIndex == selectedIndex)
            {
                Text text = new(prefix + options[optionIndex], SelectedStyle);
                AnsiConsole.Write(text);
                AnsiConsole.ResetColors();
            }
            else
            {
                AnsiConsole.Write(prefix + options[optionIndex]);
            }
        }

        // Clear unused lines
        for (int i = options.Count - visibleStart; i < PageSize; i++)
        {
            Console.SetCursorPosition(0, suggestionStartLine + i);
            ClearCurrentConsoleLine();
        }

        // Restore cursor
        Console.SetCursorPosition(prompt.Length + input.Length, inputLine);
    }

    private static void ClearCurrentConsoleLine()
    {
        int currentLine = Console.CursorTop;
        Console.SetCursorPosition(0, currentLine);

        AnsiConsole.Write(new string(' ', Console.WindowWidth)); //taking risk here.
        Console.SetCursorPosition(0, currentLine);
    }
}