namespace BasicConsoleLibrary.Core.Widgets;
public class StyledRun(string content, Style? style = null)
{
    public string Content { get; } = content;
    public Style Style { get; } = style ?? new Style();
    public override string ToString()
    {
        var ansi = AnsiCodeHelper.ToAnsiCode(Style);
        return $"{ansi}{Content}{AnsiCodeHelper.AnsiReset}";
    }
}