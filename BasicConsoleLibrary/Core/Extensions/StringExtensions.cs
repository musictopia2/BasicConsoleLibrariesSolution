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


    extension (string payLoad)
    {
        /// <summary>
        /// Gets the cell width of the specified text.
        /// </summary>
        /// <returns>The cell width of the text.</returns>
        public int GetCellWidth()
        {
            return Cell.GetCellLength(payLoad);
        }
        internal string NormalizeNewLines(bool native = false)
        {
            ArgumentNullException.ThrowIfNull(payLoad);
            payLoad = payLoad.ReplaceExact("\r\n", "\n");
            payLoad ??= string.Empty;
            if (native && !_alreadyNormalized)
            {
                payLoad = payLoad.ReplaceExact("\n", Environment.NewLine);
            }
            return payLoad;
        }

        internal string[] SplitLines()
        {
            var result = payLoad?.NormalizeNewLines()?.Split(['\n'], StringSplitOptions.None);
            return result ?? [];
        }

        internal string[] SplitWords(StringSplitOptions options = StringSplitOptions.None)
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

            using (var reader = new StringBuffer(payLoad))
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

        internal string Repeat(int count)
        {
            ArgumentNullException.ThrowIfNull(payLoad);

            if (count <= 0)
            {
                return string.Empty;
            }

            if (count == 1)
            {
                return payLoad;
            }

            return string.Concat(Enumerable.Repeat(payLoad, count));
        }

        internal string ReplaceExact(string oldValue, string? newValue)
        {
            return payLoad.Replace(oldValue, newValue, StringComparison.Ordinal);
        }
        internal bool ContainsExact(string value)
        {
            return payLoad.Contains(value, StringComparison.Ordinal);
        }
    }   
}