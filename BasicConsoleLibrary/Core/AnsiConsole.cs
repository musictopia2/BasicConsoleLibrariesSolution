namespace BasicConsoleLibrary.Core;
public static partial class AnsiConsole
{
    static AnsiConsole()
    {
        //decided to go ahead and register simple types here globally.
        rr2.RegisterSimpleTypeClasses();
        System.Console.OutputEncoding = Encoding.UTF8;
    }
    private static readonly BasicList<IRenderable> _history = [];
    private static void RenderToConsole(IRenderable renderable)
    {
        var segments = renderable.Render(new RenderOptions(), System.Console.WindowWidth);
        foreach (var segment in segments)
        {
            var ansiStart = AnsiCodeHelper.ToAnsiCode(segment.Style);
            System.Console.Write(ansiStart);
            System.Console.Write(segment.Text); // Optional coloring/styling
            System.Console.Write(AnsiCodeHelper.AnsiReset);
        }
    }
    public static string ExportText(bool alsoAnsiCodes = false)
    {
        return ExportText(alsoAnsiCodes, _history);
    }
    public static string ExportText(bool alsoAnsiCodes, params BasicList<IRenderable> renderables)
    {
        RenderOptions options = new();
        var sb = new StringBuilder();
        foreach (var renderable in renderables)
        {
            var segments = renderable.Render(options, int.MaxValue);
            foreach (var segment in segments)
            {
                if (alsoAnsiCodes)
                {
                    var ansiStart = AnsiCodeHelper.ToAnsiCode(segment.Style);
                    sb.Append(ansiStart);
                }
                sb.Append(segment.Text); // You can add line breaks or layout here
                if (alsoAnsiCodes)
                {
                    sb.Append(AnsiCodeHelper.AnsiReset);
                }
            }
        }
        _history.Clear(); //implies you want to clear after export
        return sb.ToString();
    }
    public static void ClearHistory()
    {
        _history.Clear();
    }
}