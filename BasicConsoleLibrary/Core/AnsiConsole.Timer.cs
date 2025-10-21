namespace BasicConsoleLibrary.Core;
public static partial class AnsiConsole
{
    //so i can do something else if needed.
    public static async Task<int?> GetTimedIntegerAsync(string promptText, int timeoutSeconds)
    {
        var prompt = new TimedPrompt<int>();

        var result = await prompt.ShowAsync(promptText, timeoutSeconds);
        return result.HasValue ? result.Value : null;
    }
}
