namespace BasicConsoleLibrary.Core;

/// <summary>
/// A console capable of writing ANSI escape sequences.
/// </summary>
public static partial class AnsiConsole
{
    public static string Console => "Bad";
    public static void Write(string content, Style style)
    {
        Text text = new(content, style);
        Write(text);
    }

    /// <summary>
    /// Renders the specified <see cref="IRenderable"/> to the console.
    /// </summary>
    /// <param name="renderable">The object to render.</param>
    public static void Write(IRenderable renderable)
    {
        ArgumentNullException.ThrowIfNull(renderable);
        _history.Add(renderable);
        RenderToConsole(renderable);
    }

    /// <summary>
    /// Writes the specified string value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(string value)
    {
        Write(value, CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified 32-bit
    /// signed integer value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(int value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified 32-bit
    /// signed integer value to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, int value)
    {
        Write(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified 32-bit
    /// unsigned integer value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(uint value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified 32-bit
    /// unsigned integer value to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, uint value)
    {
        Write(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified 64-bit
    /// signed integer value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(long value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified 64-bit
    /// signed integer value to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, long value)
    {
        Write(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified 64-bit
    /// unsigned integer value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(ulong value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified 64-bit
    /// unsigned integer value to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, ulong value)
    {
        Write(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified single-precision
    /// floating-point value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(float value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified single-precision
    /// floating-point value to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, float value)
    {
        Write(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified double-precision
    /// floating-point value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(double value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified double-precision
    /// floating-point value to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, double value)
    {
        Write(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified decimal value, to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(decimal value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified decimal value, to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, decimal value)
    {
        Write(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the text representation of the specified boolean value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(bool value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the text representation of the specified boolean value to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, bool value)
    {
        Write(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the specified Unicode character to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(char value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the specified Unicode character to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, char value)
    {
        Write(value.ToString(provider), CurrentStyle);
    }

    /// <summary>
    /// Writes the specified array of Unicode characters to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public static void Write(char[] value)
    {
        Write(CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the specified array of Unicode characters to the console.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The value to write.</param>
    public static void Write(IFormatProvider provider, char[] value)
    {
        ArgumentNullException.ThrowIfNull(value);
        for (var index = 0; index < value.Length; index++)
        {
            Write(value[index].ToString(provider), CurrentStyle);
        }
    }

    /// <summary>
    /// Writes the text representation of the specified array of objects,
    /// to the console using the specified format information.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to write.</param>
    public static void Write(string format, params object[] args)
    {
        Write(CultureInfo.CurrentCulture, format, args);
    }

    /// <summary>
    /// Writes the text representation of the specified array of objects,
    /// to the console using the specified format information.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to write.</param>
    public static void Write(IFormatProvider provider, string format, params object[] args)
    {
        Write(string.Format(provider, format, args), CurrentStyle);
    }
}