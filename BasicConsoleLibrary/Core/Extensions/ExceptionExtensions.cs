namespace BasicConsoleLibrary.Core.Extensions;
public static class ExceptionExtensions
{
    extension (Exception exception)
    {
        public IRenderable GetRenderable(FlagsWrapper<EnumExceptionFormats> format)
        {
            ArgumentNullException.ThrowIfNull(exception);
            return GetRenderable(exception, new ExceptionSettings
            {
                Format = format,
            });
        }
        public IRenderable GetRenderable(ExceptionSettings settings)
        {
            ArgumentNullException.ThrowIfNull(exception);
            ArgumentNullException.ThrowIfNull(settings);
            return new ExceptionRenderClass(exception, settings);
        }
    }
}