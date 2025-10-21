namespace BasicConsoleLibrary.Core.Widgets.Table;
internal abstract class TableAccessor(BaseTable table, RenderOptions options)
{
    private readonly BaseTable _table = table ?? throw new ArgumentNullException(nameof(table));

    public RenderOptions Options { get; } = options ?? throw new ArgumentNullException(nameof(options));
    public IReadOnlyList<TableColumn> Columns => _table.Columns;
    public virtual IReadOnlyList<TableRow> Rows => _table.Rows;
    public bool Expand => _table.Expand || _table.Width != null;
}