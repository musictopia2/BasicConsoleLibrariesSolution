namespace BasicConsoleLibrary.Core;
public partial class AnsiConsole
{
    public static void WriteException(Exception ex)
    {
        ExceptionSettings settings = new();
        WriteException(ex, settings);
    }
    public static void WriteException(Exception ex, ExceptionSettings settings)
    {
        Write(ex.GetRenderable(settings));
    }
    public static void WriteException(Exception ex, FlagsWrapper<EnumExceptionFormats> format)
    {
        Write(ex.GetRenderable(format));
    }
}