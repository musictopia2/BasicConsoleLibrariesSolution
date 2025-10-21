namespace BasicConsoleLibrary.Core.Prompts;
public class ComboPrompt<T>(string prompt, IEnumerable<T> choices, bool allowCustom = false, int pageSize = 5) : IPrompt<T>
    where T : notnull
{
    public Style PromptStyle { get; set; } = Style.Plain;
    public Style SelectedStyle { get; set; } = cc2.Green;
    public Style GhostStyle { get; set; } = cc2.DarkGray;

    /// <summary>
    /// Optional default value. Only used if allowCustom is true and the user presses enter without typing.
    /// </summary>
    public T? DefaultValue { get; set; }

    /// <summary>
    /// If true, allow user to enter custom values instead of selecting.
    /// </summary>
    public bool AllowCustom { get; set; } = allowCustom;

    public int PageSize { get; set; } = pageSize;

    public T Show()
    {
        return ShowAsync(CancellationToken.None).GetAwaiter().GetResult();
    }
    public async Task<T> ShowAsync(CancellationToken cancellationToken)
    {
        var output = await ConsoleExclusive.RunAsync(async () =>
        {
            // First, convert choices to string and map them back to T
            BasicList<string> stringChoices = choices
                .Select(x => x?.ToString() ?? string.Empty)
                .Distinct()
                .ToBasicList();

            Dictionary<string, T> choiceMap = choices
                .GroupBy(x => x?.ToString() ?? string.Empty)
                .ToDictionary(g => g.Key, g => g.First()); // map string to original object

            var combo = new ConsoleComboBox(stringChoices, AllowCustom, PageSize)
            {
                PromptString = prompt,
                PromptStyle = PromptStyle ?? Style.Plain,
                GhostStyle = GhostStyle ?? cc2.DarkGray,
                SelectedStyle = SelectedStyle ?? cc2.Green
            };

            string? result = await combo.ShowInsideExclusive(cancellationToken);

            if (result is null)
            {
                if (AllowCustom && DefaultValue is not null)
                {
                    return DefaultValue;
                }

                throw new CustomBasicException("No selection was made.");
            }

            // If it's in the original list, return the mapped value
            if (choiceMap.TryGetValue(result, out T? match))
            {
                return match;
            }

            // If not, and custom allowed, try to parse it
            if (AllowCustom)
            {
                //return result.ToString(); //just do tostring because no enums for this.
                ITypeParsingProvider<T>? parser = CustomTypeParsingHelpers<T>.MasterContext ?? throw new CustomBasicException("No registered parser found for type.");

                if (parser.TryParse(result, out T parsed, out string? error) == false)
                {
                    throw new CustomBasicException($"Invalid input: {error}");
                }

                return parsed;
            }

            throw new CustomBasicException("Invalid selection.");
        }).ConfigureAwait(false);
        return output;
    }
}