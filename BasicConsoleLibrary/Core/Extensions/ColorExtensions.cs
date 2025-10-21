namespace BasicConsoleLibrary.Core.Extensions;
public static class ColorExtensions
{

    /// <summary>
    /// Converts ARGB hex string (e.g. "#FFFF6347") to a (R,G,B) tuple.
    /// Expects the color to start with "#FF" and be 9 characters long.
    /// </summary>
    private static (byte R, byte G, byte B) ParseArgbToRgb(string argbColor)
    {
        if (string.IsNullOrWhiteSpace(argbColor))
            throw new ArgumentException("Color string is empty.");

        if (!argbColor.StartsWith("#FF") || argbColor.Length != 9)
            throw new ArgumentException("Color must be in #FFRRGGBB format with no transparency.");

        byte r = Convert.ToByte(argbColor.Substring(3, 2), 16);
        byte g = Convert.ToByte(argbColor.Substring(5, 2), 16);
        byte b = Convert.ToByte(argbColor.Substring(7, 2), 16);

        return (r, g, b);
    }

    /// <summary>
    /// Generates ANSI escape sequence for setting foreground color.
    /// </summary>
    public static string ToAnsiForeground(this string argbColor)
    {
        var (r, g, b) = ParseArgbToRgb(argbColor);
        return $"\u001b[38;2;{r};{g};{b}m";
    }

    /// <summary>
    /// Generates ANSI escape sequence for setting background color.
    /// </summary>
    public static string ToAnsiBackground(this string argbColor)
    {
        var (r, g, b) = ParseArgbToRgb(argbColor);
        return $"\u001b[48;2;{r};{g};{b}m";
    }
}