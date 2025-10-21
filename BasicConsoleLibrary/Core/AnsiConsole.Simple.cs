namespace BasicConsoleLibrary.Core;
public static partial class AnsiConsole
{
    public static string ReadLine()
    {
        string output = System.Console.ReadLine() ?? "";
        return output;
    }
    public static void PauseAtEnd()
    {
        System.Console.ReadLine();
    }
    public static void Clear()
    {
        System.Console.Clear();
    }
}