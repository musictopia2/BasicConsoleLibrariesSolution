namespace BasicConsoleLibrary.Core.Widgets.Table;
public abstract class BaseTable : Renderable, IExpandable, IHasTableBorder, IAlignable
{
    internal readonly protected BasicList<TableColumn> _columns = [];
    public IReadOnlyList<TableColumn> Columns => _columns;
    public TableBorder Border { get; set; } = TableBorder.Square;
    public Style? BorderStyle { get; set; }
    public bool UseSafeBorder { get; set; }

    // Whether this is a grid or not.
    internal bool IsGrid { get; set; }

    // Whether or not the most right cell should be padded.
    // This is almost always the case, unless we're rendering
    // a grid without explicit padding in the last cell.
    internal bool PadRightCell { get; set; } = true;


    /// <summary>
    /// Gets the table rows.
    /// </summary>
    public TableRowCollection Rows { get; }
    /// <summary>
    /// Gets or sets a value indicating whether or not the table should
    /// fit the available space. If <c>false</c>, the table width will be
    /// auto calculated. Defaults to <c>false</c>.
    /// </summary>
    public bool Expand { get; set; }


    /// <summary>
    /// Gets or sets the width of the table.
    /// </summary>
    public int? Width { get; set; }


    /// <summary>
    /// Gets or sets a value indicating whether or not table headers should be shown.
    /// </summary>
    public bool ShowHeaders { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether or not row separators should be shown.
    /// </summary>
    public bool ShowRowSeparators { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not table footers should be shown.
    /// </summary>
    public bool ShowFooters { get; set; } = true;


    /// <summary>
    /// Gets or sets the table title.
    /// </summary>
    public TableTitle? Title { get; set; }

    /// <summary>
    /// Gets or sets the table footnote.
    /// </summary>
    public TableTitle? Caption { get; set; }
    public EnumJustify? Alignment { get; set; }
    internal static List<BasicList<SegmentLine>> MakeSameHeight(int cellHeight, List<BasicList<SegmentLine>> cells)
    {
        ArgumentNullException.ThrowIfNull(cells);

        foreach (var cell in cells)
        {
            if (cell.Count < cellHeight)
            {
                while (cell.Count != cellHeight)
                {
                    cell.Add([]);
                }
            }
        }

        return cells;
    }

    protected BaseTable()
    {
        Rows = new(this);
    }


    /// <inheritdoc/>
    protected override Measurement Measure(RenderOptions options, int maxWidth)
    {
        ArgumentNullException.ThrowIfNull(options);

        var measurer = new TableMeasurer(this, options);

        // Calculate the total cell width
        var totalCellWidth = measurer.CalculateTotalCellWidth(maxWidth);

        // Calculate the minimum and maximum table width
        var measurements = _columns.Select(column => measurer.MeasureColumn(column, totalCellWidth));
        var minTableWidth = measurements.Sum(x => x.Min) + measurer.GetNonColumnWidth();
        var maxTableWidth = Width ?? measurements.Sum(x => x.Max) + measurer.GetNonColumnWidth();
        return new Measurement(minTableWidth, maxTableWidth);
    }

    /// <inheritdoc/>
    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        ArgumentNullException.ThrowIfNull(options);

        var measurer = new TableMeasurer(this, options);

        // Calculate the column and table width
        var totalCellWidth = measurer.CalculateTotalCellWidth(maxWidth);
        var columnWidths = measurer.CalculateColumnWidths(totalCellWidth);
        var tableWidth = columnWidths.Sum() + measurer.GetNonColumnWidth();

        // Get the rows to render
        var rows = GetRenderableRows();

        // Render the table
        return TableRenderer.Render(
            new TableRendererContext(this, options, rows, tableWidth, maxWidth),
            columnWidths);
    }

    private List<TableRow> GetRenderableRows()
    {
        var rows = new List<TableRow>();

        // Show headers?
        if (ShowHeaders)
        {
            rows.Add(TableRow.Header(_columns.Select(c => c.Header)));
        }

        // Add rows
        rows.AddRange(Rows);

        // Show footers?
        if (ShowFooters && _columns.Any(c => c.Footer != null))
        {
            rows.Add(TableRow.Footer(_columns.Select(c => c.Footer ?? Text.Empty)));
        }

        return rows;
    }

}