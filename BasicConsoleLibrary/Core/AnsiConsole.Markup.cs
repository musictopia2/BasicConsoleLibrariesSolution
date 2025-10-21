namespace BasicConsoleLibrary.Core;
public partial class AnsiConsole
{
    public static void Markup(StyledTextBuilder builder)
    {
        Markup marks = new(builder);
        Write(marks);
    }
    public static void MarkupLine(StyledTextBuilder builder)
    {
        builder.Append(Constants.VBCrLf);
        Markup(builder);
    }
}