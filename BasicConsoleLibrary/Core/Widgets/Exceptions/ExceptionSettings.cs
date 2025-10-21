namespace BasicConsoleLibrary.Core.Widgets.Exceptions;
public class ExceptionSettings
{
    /// <summary>
    /// Gets or sets the exception format.
    /// </summary>
    public FlagsWrapper<EnumExceptionFormats> Format { get; set; }

    /// <summary>
    /// Gets or sets the exception style.
    /// </summary>
    public ExceptionStyle Style { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionSettings"/> class.
    /// </summary>
    public ExceptionSettings()
    {
        Format = new();
        Style = new ExceptionStyle();
    }
}