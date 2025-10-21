namespace BasicConsoleLibrary.Core.Rendering.Borders.Boxes;

/// <summary>
/// Represents an old school ASCII border.
/// </summary>
public sealed class AsciiBoxBorder : BoxBorder
{
    /// <inheritdoc/>
    public override string GetPart(EnumBoxBorderPart part)
    {
        return part switch
        {
            EnumBoxBorderPart.TopLeft => "+",
            EnumBoxBorderPart.Top => "-",
            EnumBoxBorderPart.TopRight => "+",
            EnumBoxBorderPart.Left => "|",
            EnumBoxBorderPart.Right => "|",
            EnumBoxBorderPart.BottomLeft => "+",
            EnumBoxBorderPart.Bottom => "-",
            EnumBoxBorderPart.BottomRight => "+",
            _ => throw new InvalidOperationException("Unknown border part."),
        };
    }
}