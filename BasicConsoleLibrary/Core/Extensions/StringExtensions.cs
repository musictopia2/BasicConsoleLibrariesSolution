namespace BasicConsoleLibrary.Core.Extensions;
/// <summary>
/// Contains extension methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    // Cache whether or not internally normalized line endings
    // already are normalized. No reason to do yet another replace if it is.
    private static readonly bool _alreadyNormalized
        = Environment.NewLine.Equals("\n", StringComparison.OrdinalIgnoreCase);


    


    /// <summary>
    /// Gets the cell width of the specified text.
    /// </summary>
    /// <param name="text">The text to get the cell width of.</param>
    /// <returns>The cell width of the text.</returns>
    public static int GetCellWidth(this string text)
    {
        return Cell.GetCellLength(text);
    }


    internal static string NormalizeNewLines(this string? text, bool native = false)
    {
        text = text?.ReplaceExact("\r\n", "\n");
        text ??= string.Empty;
        if (native && !_alreadyNormalized)
        {
            text = text.ReplaceExact("\n", Environment.NewLine);
        }
        return text;
    }

    internal static string[] SplitLines(this string text)
    {
        var result = text?.NormalizeNewLines()?.Split(['\n'], StringSplitOptions.None);
        return result ?? [];
    }

    internal static string[] SplitWords(this string word, StringSplitOptions options = StringSplitOptions.None)
    {
        var result = new List<string>();

        static string Read(StringBuffer reader, Func<char, bool> criteria)
        {
            var buffer = new StringBuilder();
            while (!reader.Eof)
            {
                var current = reader.Peek();
                if (!criteria(current))
                {
                    break;
                }

                buffer.Append(reader.Read());
            }

            return buffer.ToString();
        }

        using (var reader = new StringBuffer(word))
        {
            while (!reader.Eof)
            {
                var current = reader.Peek();
                if (char.IsWhiteSpace(current))
                {
                    var x = Read(reader, c => char.IsWhiteSpace(c));
                    if (options != StringSplitOptions.RemoveEmptyEntries)
                    {
                        result.Add(x);
                    }
                }
                else
                {
                    result.Add(Read(reader, c => !char.IsWhiteSpace(c)));
                }
            }
        }

        return [.. result];
    }

    internal static string Repeat(this string text, int count)
    {
        ArgumentNullException.ThrowIfNull(text);

        if (count <= 0)
        {
            return string.Empty;
        }

        if (count == 1)
        {
            return text;
        }

        return string.Concat(Enumerable.Repeat(text, count));
    }

    internal static string ReplaceExact(this string text, string oldValue, string? newValue)
    {
        return text.Replace(oldValue, newValue, StringComparison.Ordinal);
    }

    internal static bool ContainsExact(this string text, string value)
    {
        return text.Contains(value, StringComparison.Ordinal);
    }
}