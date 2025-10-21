namespace BasicConsoleLibrary.Core.Widgets.Exceptions;
[Flags]
public enum EnumExceptionFormats
{
    /// <summary>
    /// The default formatting.
    /// </summary>
    Default = 0,
    /// <summary>
    /// Whether or not paths should be shortened.
    /// </summary>
    ShortenPaths = 1,

    /// <summary>
    /// Whether or not types should be shortened.
    /// </summary>
    ShortenTypes = 2,

    /// <summary>
    /// Whether or not methods should be shortened.
    /// </summary>
    ShortenMethods = 4,
    /// <summary>
    /// Whether or not to show the exception stack trace.
    /// </summary>
    NoStackTrace = 8
    //not sure if need something to show i can shorten everything (?)
}