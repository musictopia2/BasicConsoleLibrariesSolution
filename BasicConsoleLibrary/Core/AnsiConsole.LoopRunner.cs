namespace BasicConsoleLibrary.Core;
public static partial class AnsiConsole
{
    public static void RunRepeatedly(
        Action action,
        string repeatPrompt = "Do you want to run again?",
        string? goodbyeMessage = "Goodbye!",
        bool clearAfterEachRun = false,
        bool waitForExitKey = true)
    {
        do
        {
            action.Invoke();
            if (Confirm(repeatPrompt) == false)
            {
                break;
            }
            if (clearAfterEachRun)
            {
                Clear();
            }
        } while (true);

        if (string.IsNullOrWhiteSpace(goodbyeMessage) == false)
        {
            WriteLine(goodbyeMessage);
        }
        if (waitForExitKey)
        {
            WriteLine("Press Enter to exit....");
            ReadLine();
        }
    }
    public static async Task RunRepeatedlyAsync(
        Func<Task> action,
        string repeatPrompt = "Do you want to run again?",
        string? goodbyeMessage = "Goodbye!",
        bool clearAfterEachRun = false,
        bool waitForExitKey = true)
    {
        do
        {
            await action.Invoke();
            if (Confirm(repeatPrompt) == false)
            {
                break;
            }
            if (clearAfterEachRun)
            {
                Clear();
            }
        } while (true);

        if (string.IsNullOrWhiteSpace(goodbyeMessage) == false)
        {
            WriteLine(goodbyeMessage);
        }
        if (waitForExitKey)
        {
            WriteLine("Press Enter to exit....");
            ReadLine();
        }
    }
}