namespace BasicConsoleLibrary.Core;
public static class AnsiCodeHelper
{
    // Resets all styles and colors (SGR reset)
    public const string AnsiReset = "\u001b[0m";

    

    
    public static string ToAnsiCode(Style style)
    {
        var parts = new List<string>();

        if (style.Decoration is not null)
        {
            var dec = style.Decoration;

            if (dec.Has(EnumTextDecoration.Dim))
            {
                parts.Add("2");
            }

            if (dec.Has(EnumTextDecoration.Bold))
            {
                parts.Add("1");
            }

            if (dec.Has(EnumTextDecoration.Italic))
            {
                parts.Add("3");
            }

            if (dec.Has(EnumTextDecoration.Underline))
            {
                parts.Add("4");
            }

            if (dec.Has(EnumTextDecoration.Blink))
            {
                parts.Add("5");
            }
            if (dec.Has(EnumTextDecoration.Invert))
            {
                parts.Add("7");
            }

            if (dec.Has(EnumTextDecoration.Conceal))
            {
                parts.Add("8");
            }

            if (dec.Has(EnumTextDecoration.Strikethrough))
            {
                parts.Add("9");
            }
            if (dec.Has(EnumTextDecoration.Overline))
            {
                parts.Add("53");
            }
            if (dec.Has(EnumTextDecoration.DoubleUnderline))
            {
                parts.Add("21");
            }
            // Note: Overline is not widely supported in ANSI codes, so usually no standard code.
        }
        if (!string.IsNullOrEmpty(style.Foreground))
        {
            var fgCode = style.Foreground!.ToAnsiForeground; // Your extension method
            parts.AddRange(ExtractAnsiParameters(fgCode));
        }

        // Add background color if set
        if (!string.IsNullOrEmpty(style.Background))
        {
            var bgCode = style.Background!.ToAnsiBackground; // Your extension method
            parts.AddRange(ExtractAnsiParameters(bgCode));
        }

        return parts.Count == 0 ? "" : $"\u001b[{string.Join(";", parts)}m";
    }



    /// <summary>
    /// Extracts the numeric parts from ANSI code like "\u001b[38;2;255;0;0m"
    /// and returns them as ["38", "2", "255", "0", "0"]
    /// </summary>
    private static IEnumerable<string> ExtractAnsiParameters(string ansiCode)
    {
        if (ansiCode.StartsWith("\u001b[") && ansiCode.EndsWith("m"))
        {
            var inner = ansiCode.Substring(2, ansiCode.Length - 3); // strip \u001b[ and trailing 'm'
            return inner.Split(';');
        }

        return Enumerable.Empty<string>();
    }

}