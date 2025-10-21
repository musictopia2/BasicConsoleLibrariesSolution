namespace BasicConsoleLibrary.Core.Extensions;

/// <summary>
/// Contains extension methods for <see cref="Table"/>.
/// </summary>
public static class TableExtensions
{
    /// <summary>
    /// Adds multiple columns to the table.
    /// </summary>
    /// <param name="table">The table to add the column to.</param>
    /// <param name="columns">The columns to add.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable AddColumns(this ManualTable table, params TableColumn[] columns)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(columns);

        foreach (var column in columns)
        {
            table.AddColumn(column);
        }

        return table;
    }

    /// <summary>
    /// Adds a row to the table.
    /// </summary>
    /// <param name="table">The table to add the row to.</param>
    /// <param name="columns">The row columns to add.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable AddRow(this ManualTable table, IEnumerable<IRenderable> columns)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(columns);

        table.Rows.Add([.. columns]);
        return table;
    }

    /// <summary>
    /// Adds a row to the table.
    /// </summary>
    /// <param name="table">The table to add the row to.</param>
    /// <param name="columns">The row columns to add.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable AddRow(this ManualTable table, params IRenderable[] columns)
    {
        ArgumentNullException.ThrowIfNull(table);

        return table.AddRow((IEnumerable<IRenderable>)columns);
    }

    /// <summary>
    /// Adds an empty row to the table.
    /// </summary>
    /// <param name="table">The table to add the row to.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable AddEmptyRow(this ManualTable table)
    {
        ArgumentNullException.ThrowIfNull(table);

        var columns = new IRenderable[table.Columns.Count];
        Enumerable.Range(0, table.Columns.Count).ForEach(index => columns[index] = Text.Empty);
        table.AddRow(columns);
        return table;
    }

    /// <summary>
    /// Adds a column to the table.
    /// </summary>
    /// <param name="table">The table to add the column to.</param>
    /// <param name="column">The column to add.</param>
    /// <param name="configure">Delegate that can be used to configure the added column.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable AddColumn(this ManualTable table, string column, Action<TableColumn>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(column);

        var tableColumn = new TableColumn(column);
        configure?.Invoke(tableColumn);
        table.AddColumn(tableColumn);
        return table;
    }

    /// <summary>
    /// Adds multiple columns to the table.
    /// </summary>
    /// <param name="table">The table to add the columns to.</param>
    /// <param name="columns">The columns to add.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable AddColumns(this ManualTable table, params string[] columns)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(columns);

        foreach (var column in columns)
        {
            table.AddColumn(column);
        }

        return table;
    }

    /// <summary>
    /// Adds a row to the table.
    /// </summary>
    /// <param name="table">The table to add the row to.</param>
    /// <param name="columns">The row columns to add.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable AddRow(this ManualTable table, params string[] columns)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(columns);

        table.AddRow(columns.Select(column => new Text(column)).ToArray()); //well see  was markup
        return table;
    }

    /// <summary>
    /// Inserts a row in the table at the specified index.
    /// </summary>
    /// <param name="table">The table to add the row to.</param>
    /// <param name="index">The index to insert the row at.</param>
    /// <param name="columns">The row columns to add.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable InsertRow(this ManualTable table, int index, IEnumerable<IRenderable> columns)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(columns);

        table.Rows.Insert(index, [.. columns]);
        return table;
    }

    /// <summary>
    /// Updates a tables cell.
    /// </summary>
    /// <param name="table">The table to update.</param>
    /// <param name="rowIndex">The index of row to update.</param>
    /// <param name="columnIndex">The index of column to update.</param>
    /// <param name="cellData">New cell data.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable UpdateCell(this ManualTable table, int rowIndex, int columnIndex, IRenderable cellData)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(cellData);

        table.Rows.Update(rowIndex, columnIndex, cellData);

        return table;
    }

    /// <summary>
    /// Updates a tables cell.
    /// </summary>
    /// <param name="table">The table to update.</param>
    /// <param name="rowIndex">The index of row to update.</param>
    /// <param name="columnIndex">The index of column to update.</param>
    /// <param name="cellData">New cell data.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable UpdateCell(this ManualTable table, int rowIndex, int columnIndex, string cellData)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(cellData);

        table.Rows.Update(rowIndex, columnIndex, new Text(cellData)); //well see.  was markup previously.

        return table;
    }

    /// <summary>
    /// Inserts a row in the table at the specified index.
    /// </summary>
    /// <param name="table">The table to add the row to.</param>
    /// <param name="index">The index to insert the row at.</param>
    /// <param name="columns">The row columns to add.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable InsertRow(this ManualTable table, int index, params IRenderable[] columns)
    {
        ArgumentNullException.ThrowIfNull(table);

        return table.InsertRow(index, (IEnumerable<IRenderable>)columns);
    }

    /// <summary>
    /// Inserts a row in the table at the specified index.
    /// </summary>
    /// <param name="table">The table to add the row to.</param>
    /// <param name="index">The index to insert the row at.</param>
    /// <param name="columns">The row columns to add.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable InsertRow(this ManualTable table, int index, params string[] columns)
    {
        ArgumentNullException.ThrowIfNull(table);

        return InsertRow(table, index, columns.Select(column => new Text(column))); //was markup previously.
    }

    /// <summary>
    /// Removes a row from the table with the specified index.
    /// </summary>
    /// <param name="table">The table to add the row to.</param>
    /// <param name="index">The index to remove the row at.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable RemoveRow(this ManualTable table, int index)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.Rows.RemoveAt(index);
        return table;
    }

    /// <summary>
    /// Sets the table width.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <param name="width">The width.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable Width(this ManualTable table, int? width)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.Width = width;
        return table;
    }

    /// <summary>
    /// Shows table headers.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable ShowHeaders(this ManualTable table)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.ShowHeaders = true;
        return table;
    }

    /// <summary>
    /// Hides table headers.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable HideHeaders(this ManualTable table)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.ShowHeaders = false;
        return table;
    }

    /// <summary>
    /// Shows row separators.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable ShowRowSeparators(this ManualTable table)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.ShowRowSeparators = true;
        return table;
    }

    /// <summary>
    /// Hides row separators.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable HideRowSeparators(this ManualTable table)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.ShowRowSeparators = false;
        return table;
    }

    /// <summary>
    /// Shows table footers.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable ShowFooters(this ManualTable table)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.ShowFooters = true;
        return table;
    }

    /// <summary>
    /// Hides table footers.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable HideFooters(this ManualTable table)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.ShowFooters = false;
        return table;
    }

    /// <summary>
    /// Sets the table title.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <param name="text">The table title markup text.</param>
    /// <param name="style">The table title style.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable Title(this ManualTable table, string text, Style? style = null)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(text);

        return table.Title(new TableTitle(text, style));
    }

    /// <summary>
    /// Sets the table title.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <param name="title">The table title.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable Title(this ManualTable table, TableTitle title)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.Title = title;
        return table;
    }

    /// <summary>
    /// Sets the table caption.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <param name="text">The caption markup text.</param>
    /// <param name="style">The style.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable Caption(this ManualTable table, string text, Style? style = null)
    {
        ArgumentNullException.ThrowIfNull(table);

        ArgumentNullException.ThrowIfNull(text);

        return table.Caption(new TableTitle(text, style));
    }

    /// <summary>
    /// Sets the table caption.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <param name="caption">The caption.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static ManualTable Caption(this ManualTable table, TableTitle caption)
    {
        ArgumentNullException.ThrowIfNull(table);

        table.Caption = caption;
        return table;
    }
}