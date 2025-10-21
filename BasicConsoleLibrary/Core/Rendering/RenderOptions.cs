namespace BasicConsoleLibrary.Core.Rendering;
public record class RenderOptions
{
    /// <summary>
    /// Gets the requested height.
    /// </summary>
    public int? Height { get; init; }
    /// <summary>
    /// Gets the current justification.
    /// </summary>
    public EnumJustify? Justification { get; init; }
    /// <summary>
    /// Gets a value indicating whether the context want items to render without
    /// line breaks and return a single line where applicable.
    /// </summary>
    internal bool SingleLine { get; init; }
    //unicode is always supported.

}
