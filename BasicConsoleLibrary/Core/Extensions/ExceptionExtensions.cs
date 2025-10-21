namespace BasicConsoleLibrary.Core.Extensions;
public static class ExceptionExtensions
{
    public static IRenderable GetRenderable(this Exception exception, FlagsWrapper<EnumExceptionFormats> format)
    {
        ArgumentNullException.ThrowIfNull(exception);
        return GetRenderable(exception, new ExceptionSettings
        {
            Format = format,
        });
    }
    public static IRenderable GetRenderable(this Exception exception, ExceptionSettings settings)
    {
        ArgumentNullException.ThrowIfNull(exception);
        ArgumentNullException.ThrowIfNull(settings);
        return new ExceptionRenderClass(exception, settings);
    }
}