namespace BasicConsoleLibrary.Core;
public static partial class AnsiConsole
{
    public static void RunUntilExit(Func<bool> shouldExit, params BasicList<Action> actions)
    {
        if (actions.Count == 0)
        {
            throw new ArgumentException("At least one action is required.", nameof(actions));
        }

        do
        {
            foreach (Action action in actions)
            {
                action.Invoke();

                if (shouldExit.Invoke())
                {
                    return;
                }
            }
        } while (true);
    }
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