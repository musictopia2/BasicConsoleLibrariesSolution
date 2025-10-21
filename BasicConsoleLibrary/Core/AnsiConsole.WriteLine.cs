namespace BasicConsoleLibrary.Core;

/// <summary>
/// A console capable of writing ANSI escape sequences.
/// </summary>
public static partial class AnsiConsole
{

    public static void WriteLine(string content, Style style)
    {
        Text text = new($"{content}{Constants.VBCrLf}", style);
        Write(text);
    }

    /// <summary>
    /// Writes an empty line to the console.
    /// </summary>
    public static void WriteLine()
    {
        WriteLine("");
    }

    /// <summary>
    /// Writes the specified string value, followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(string value)
    {
        WriteLine(value, CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified 32-bit signed integer value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(int value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified 32-bit signed integer value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, int value)
    {
        WriteLine(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified 32-bit unsigned integer value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(uint value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified 32-bit unsigned integer value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, uint value)
    {
        WriteLine(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified 64-bit signed integer value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(long value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified 64-bit signed integer value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, long value)
    {
        WriteLine(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified 64-bit unsigned integer value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(ulong value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified 64-bit unsigned integer value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, ulong value)
    {
        WriteLine(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified single-precision floating-point
    /// value, followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(float value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified single-precision floating-point
    /// value, followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, float value)
    {
        WriteLine(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified double-precision floating-point
    /// value, followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(double value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified double-precision floating-point
    /// value, followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, double value)
    {
        WriteLine(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified decimal value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(decimal value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified decimal value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, decimal value)
    {
        WriteLine(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified boolean value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(bool value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified boolean value,
    /// followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, bool value)
    {
        WriteLine(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the specified Unicode character, followed by the current
    /// line terminator, value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(char value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the specified Unicode character, followed by the current
    /// line terminator, value to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, char value)
    {
        WriteLine(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the specified array of Unicode characters, followed by the current
    /// line terminator, value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(char[] value)
    {
        WriteLine(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the specified array of Unicode characters, followed by the current
    /// line terminator, value to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void WriteLine(IFormatProvider provider, char[] value)
    {
        ArgumentNullException.ThrowIfNull(value);
        for (var index = 0; index < value.Length; index++)
        {
            Write(value[index].ToString(provider), CurrentStyle);
        }
        WriteLine();
    }

    /// <summary>
    /// Writes the text representation of the specified array of objects,
    /// followed by the current line terminator, to the console
    /// using the specified format information.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to write.</param>
    public static void WriteLine(string format, params object[] args)
    {
        WriteLine(CultureInfo.CurrentCulture, format, args);
    }

    /// <summary>
    /// Writes the text representation of the specified array of objects,
    /// followed by the current line terminator, to the console
    /// using the specified format information.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to write.</param>
    public static void WriteLine(IFormatProvider provider, string format, params object[] args)
    {
        WriteLine(string.Format(provider, format, args), CurrentStyle);
    }
}