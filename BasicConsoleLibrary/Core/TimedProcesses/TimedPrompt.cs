namespace BasicConsoleLibrary.Core.TimedProcesses;
public sealed class TimedPrompt<T> where T : notnull
{
    public async Task<TimedPromptResult<T>> ShowAsync(string promptText, int timeoutSeconds, CancellationToken cancellationToken = default)
    {
        var parser = CustomTypeParsingHelpers<T>.MasterContext
            ?? throw new CustomBasicException("No parser registered for this type.");
        var deadline = DateTime.Now.AddSeconds(timeoutSeconds);
        do
        {
            //AnsiConsole.WriteLine($"{promptText} (Time left: {remaining}s)");
            AnsiConsole.WriteLine(promptText);
            // Read with remaining time
            string? input = await TimedPrompt<T>.ReadLineWithTimeoutAsync(deadline - DateTime.Now, cancellationToken);
            if (input is null)
            {
                AnsiConsole.WriteLine("Time was up.");
                return new TimedPromptResult<T>(default!, false);
            }

            if (parser.TryParse(input, out T result, out string? errorMessage))
            {
                return new TimedPromptResult<T>(result, true);
            }
            else
            {
                Text text = new($"Invalid input {errorMessage}", cc2.Red);
                AnsiConsole.Write(text);
                AnsiConsole.WriteLine();
            }
        } while (true);
    }
    private static async Task<string?> ReadLineWithTimeoutAsync(TimeSpan timeout, CancellationToken cancellationToken)
    {
        var input = string.Empty;
        var deadline = DateTime.Now + timeout;

        while (DateTime.Now < deadline && !cancellationToken.IsCancellationRequested)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return input;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input[..^1];
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
            else
            {
                await Task.Delay(50, cancellationToken); // Short delay to avoid busy-waiting
            }
        }

        return null; // Timeout or cancellation occurred
    }
}