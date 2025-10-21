namespace BasicConsoleLibrary.Core;

/// <summary>
/// A console capable of writing ANSI escape sequences.
/// </summary>
public static partial class AnsiConsole
{
    internal static Style CurrentStyle { get; private set; } = Style.Plain;
    internal static bool Created { get; private set; }

    /// <summary>
    /// Gets or sets the foreground color.
    /// </summary>
    public static string? Foreground
    {
        get => CurrentStyle.Foreground;
        set => CurrentStyle = CurrentStyle.With(value);
    }

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    public static string? Background
    {
        get => CurrentStyle.Background;
        set => CurrentStyle = CurrentStyle.With(bg: value);
    }

    /// <summary>
    /// Gets or sets the text decoration.
    /// </summary>
    public static FlagsWrapper<EnumTextDecoration>? Decoration => CurrentStyle.Decoration;
    public static void AddDecoration(EnumTextDecoration decoration)
    {
        FlagsWrapper<EnumTextDecoration> newItem = new();
        if (CurrentStyle.Decoration is not null)
        {
            var olds = CurrentStyle.Decoration.RawValue;
            newItem.SetRawValue(olds);
        }
        newItem.Add(decoration);
        CurrentStyle = CurrentStyle.With(decoration: newItem);
    }

    /// <summary>
    /// Resets colors and text decorations.
    /// </summary>
    public static void Reset()
    {
        ResetColors();
        ResetDecoration();
    }

    /// <summary>
    /// Resets the current applied text decorations.
    /// </summary>
    public static void ResetDecoration()
    {
        CurrentStyle.Decoration = null; //i think.
    }

    /// <summary>
    /// Resets the current applied foreground and background colors.
    /// </summary>
    public static void ResetColors()
    {
        CurrentStyle.Background = null;
        CurrentStyle.Foreground = null;
    }
}