namespace BasicConsoleLibrary.Core.Widgets;

/// <summary>
/// Represents a panel header.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PanelHeader"/> class.
/// </remarks>
/// <param name="text">The panel header text.</param>
/// <param name="alignment">The panel header alignment.</param>
public sealed class PanelHeader(string text, EnumJustify? alignment = null) : IHasJustification
{
    /// <summary>
    /// Gets the panel header text.
    /// </summary>
    public string Text { get; } = text ?? throw new ArgumentNullException(nameof(text));

    /// <summary>
    /// Gets or sets the panel header alignment.
    /// </summary>
    public EnumJustify? Justification { get; set; } = alignment;

    /// <summary>
    /// Sets the panel header alignment.
    /// </summary>
    /// <param name="alignment">The panel header alignment.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public PanelHeader SetAlignment(EnumJustify alignment)
    {
        Justification = alignment;
        return this;
    }
}