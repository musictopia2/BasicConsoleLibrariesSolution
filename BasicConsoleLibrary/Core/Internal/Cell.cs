namespace BasicConsoleLibrary.Core.Internal;
internal static class Cell
{
    private const sbyte _sentinel = -2;

    /// <summary>
    /// UnicodeCalculator.GetWidth documents the width as (-1, 0, 1, 2). We only need space for these values and a sentinel for uninitialized values.
    /// This is only five values in total so we are storing one byte per value. We could store 2 per byte but that would add more logic to the retrieval.
    /// We should add one to char.MaxValue because the total number of characters includes \0 too so there are 65536 valid chars.
    /// </summary>
    private static readonly sbyte[] _runeWidthCache = new sbyte[char.MaxValue + 1];

    static Cell()
    {
        for (var i = 0; i < _runeWidthCache.Length; i++)
        {
            _runeWidthCache[i] = _sentinel;
        }
    }

    public static int GetCellLength(string text)
    {
        var sum = 0;
        for (var index = 0; index < text.Length; index++)
        {
            var rune = text[index];
            sum += GetCellLength(rune);
        }

        return sum;
    }

    public static int GetCellLength(char rune)
    {
        // Special case for newline: override to 1
        if (rune == '\n')
        {
            return 1;
        }

        var width = _runeWidthCache[rune];
        if (width == _sentinel)
        {
            // Compute width using your simplified calculator
            var calculatedWidth = SimpleCharWidth.GetWidth(rune);

            // Cache the value (cast to sbyte)
            _runeWidthCache[rune] = (sbyte)calculatedWidth;

            return calculatedWidth;
        }

        return width;
    }
}