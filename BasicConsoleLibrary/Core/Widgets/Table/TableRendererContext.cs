namespace BasicConsoleLibrary.Core.Widgets.Table;
internal sealed class TableRendererContext : TableAccessor
{
    private readonly BaseTable _table;
    private readonly List<TableRow> _rows;
    public override IReadOnlyList<TableRow> Rows => _rows;
    public TableBorder Border { get; }
    public Style BorderStyle { get; }
    public bool ShowBorder { get; }
    public bool ShowRowSeparators { get; }
    public bool HasRows { get; }
    public bool HasFooters { get; }

    /// <summary>
    /// Gets the max width of the destination area.
    /// The table might take up less than this.
    /// </summary>
    public int MaxWidth { get; }

    /// <summary>
    /// Gets the width of the table.
    /// </summary>
    public int TableWidth { get; }

    public bool HideBorder => !ShowBorder;
    public bool ShowHeaders => _table.ShowHeaders;
    public bool ShowFooters => _table.ShowFooters;
    public bool IsGrid => _table.IsGrid;
    public bool PadRightCell => _table.PadRightCell;
    public TableTitle? Title => _table.Title;
    public TableTitle? Caption => _table.Caption;
    public EnumJustify? Alignment => _table.Alignment;
    public TableRendererContext(BaseTable table, RenderOptions options, IEnumerable<TableRow> rows, int tableWidth, int maxWidth)
        : base(table, options)
    {
        _table = table ?? throw new ArgumentNullException(nameof(table));
        _rows = [.. rows ?? []];

        ShowBorder = _table.Border.Visible;
        HasRows = Rows.Any(row => !row.IsHeader && !row.IsFooter);
        HasFooters = Rows.Any(column => column.IsFooter);
        Border = table.Border;
        BorderStyle = table.BorderStyle ?? Style.Plain;
        ShowRowSeparators = table.ShowRowSeparators;
        TableWidth = tableWidth;
        MaxWidth = maxWidth;
    }
}