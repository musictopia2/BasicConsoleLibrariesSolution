namespace BasicConsoleLibrary.Core.Widgets.Table;

/// <summary>
/// Represents a table column.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TableColumn"/> class.
/// </remarks>
/// <param name="header">The <see cref="IRenderable"/> instance to use as the table column header.</param>
public sealed class TableColumn(IRenderable renderable) : IColumn
{
    /// <summary>
    /// Gets or sets the column header.
    /// </summary>
    public IRenderable Header { get; set; } = renderable;

    /// <summary>
    /// Gets or sets the column footer.
    /// </summary>
    public IRenderable? Footer { get; set; } //later figure out this part.

    /// <summary>
    /// Gets or sets the alignment of the column.
    /// </summary>
    public EnumJustify? Alignment { get; set; } = null;

    /// <summary>
    /// Gets or sets the width of the column.
    /// If <c>null</c>, the column will adapt to its contents.
    /// </summary>
    public int? Width { get; set; } = null;

    /// <summary>
    /// Gets or sets the padding of the column.
    /// Vertical padding (top and bottom) is ignored.
    /// </summary>
    public Padding? Padding { get; set; } = new Padding(1, 0, 1, 0);

    

    


    /// <summary>
    /// Gets or sets a value indicating whether wrapping of
    /// text within the column should be prevented.
    /// </summary>
    public bool NoWrap { get; set; } = false;

    public TableColumn(string text)
        : this(new Text(text))
    {

    }
    public TableColumn(StyledTextBuilder builder) : this(new Markup(builder))
    {

    }

    public TableColumn(string text, Style style)
        : this(new Text(text, style))
    {

    }
}