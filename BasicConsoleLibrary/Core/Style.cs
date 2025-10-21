namespace BasicConsoleLibrary.Core;
public class Style(string? fg = null, string? bg = null, FlagsWrapper<EnumTextDecoration>? decoration = null)
{
    public string? Foreground { get; set; } = fg;
    public string? Background { get; set; } = bg;
    public FlagsWrapper<EnumTextDecoration>? Decoration { get; set; } = decoration;
    /// <summary>
    /// Gets a <see cref="Style"/> with the
    /// default colors and without text decoration.
    /// </summary>
    public static Style Plain { get; } = new Style();
    public Style With(
       string? fg = null,
       string? bg = null,
       FlagsWrapper<EnumTextDecoration>? decoration = null)
    {
        return new Style(
            fg ?? Foreground,
            bg ?? Background,
            decoration ?? Decoration
        );
    }
    public static implicit operator Style(string color) => new (color);
}