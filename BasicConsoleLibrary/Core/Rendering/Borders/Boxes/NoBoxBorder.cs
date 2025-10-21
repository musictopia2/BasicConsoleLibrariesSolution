namespace BasicConsoleLibrary.Core.Rendering.Borders.Boxes;

/// <summary>
/// Represents an invisible border.
/// </summary>
public sealed class NoBoxBorder : BoxBorder
{
    /// <inheritdoc/>
    public override string GetPart(EnumBoxBorderPart part)
    {
        return " ";
    }
}