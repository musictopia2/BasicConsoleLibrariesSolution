namespace BasicConsoleLibrary.Core.Widgets.Table;
public class ManualTable : BaseTable
{
    /// <summary>
    /// Adds a column to the table.
    /// </summary>
    /// <param name="column">The column to add.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public ManualTable AddColumn(TableColumn column)
    {
        ArgumentNullException.ThrowIfNull(column);

        if (Rows.Count > 0)
        {
            throw new InvalidOperationException("Cannot add new columns to table with existing rows.");
        }
        _columns.Add(column);
        return this;
    }

}