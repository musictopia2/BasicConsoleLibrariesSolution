namespace BasicConsoleLibrary.Core;
/// <summary>
/// Represents a border.
/// </summary>
public abstract partial class BoxBorder
{
    /// <summary>
    /// Gets the string representation of the specified border part.
    /// </summary>
    /// <param name="part">The part to get the character representation for.</param>
    /// <returns>A character representation of the specified border part.</returns>
    public abstract string GetPart(EnumBoxBorderPart part);
}