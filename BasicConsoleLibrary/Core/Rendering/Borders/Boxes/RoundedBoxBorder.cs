namespace BasicConsoleLibrary.Core.Rendering.Borders.Boxes;

/// <summary>
/// Represents a rounded border.
/// </summary>
public sealed class RoundedBoxBorder : BoxBorder
{

    /// <inheritdoc/>
    public override string GetPart(EnumBoxBorderPart part)
    {
        return part switch
        {
            EnumBoxBorderPart.TopLeft => "╭",
            EnumBoxBorderPart.Top => "─",
            EnumBoxBorderPart.TopRight => "╮",
            EnumBoxBorderPart.Left => "│",
            EnumBoxBorderPart.Right => "│",
            EnumBoxBorderPart.BottomLeft => "╰",
            EnumBoxBorderPart.Bottom => "─",
            EnumBoxBorderPart.BottomRight => "╯",
            _ => throw new InvalidOperationException("Unknown border part."),
        };
    }
}