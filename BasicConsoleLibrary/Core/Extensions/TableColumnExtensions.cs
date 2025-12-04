namespace BasicConsoleLibrary.Core.Extensions;
/// <summary>
/// Contains extension methods for <see cref="TableColumn"/>.
/// </summary>
public static class TableColumnExtensions
{
    extension (TableColumn column)
    {
        /// <summary>
        /// Sets the table column header.
        /// </summary>
        /// <param name="header">The table column header markup text.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TableColumn Header(string header)
        {
            ArgumentNullException.ThrowIfNull(column);

            ArgumentNullException.ThrowIfNull(header);

            column.Header = new Text(header);
            return column;
        }

        /// <summary>
        /// Sets the table column header.
        /// </summary>
        /// <param name="header">The table column header.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TableColumn Header(IRenderable header)
        {
            ArgumentNullException.ThrowIfNull(column);

            ArgumentNullException.ThrowIfNull(header);

            column.Header = header;
            return column;
        }

        /// <summary>
        /// Sets the table column footer.
        /// </summary>
        /// <param name="footer">The table column footer markup text.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TableColumn Footer(string footer)
        {
            ArgumentNullException.ThrowIfNull(column);

            ArgumentNullException.ThrowIfNull(footer);

            column.Footer = new Text(footer);
            return column;
        }

        /// <summary>
        /// Sets the table column footer.
        /// </summary>
        /// <param name="footer">The table column footer.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public TableColumn Footer(IRenderable footer)
        {
            ArgumentNullException.ThrowIfNull(column);

            ArgumentNullException.ThrowIfNull(footer);

            column.Footer = footer;
            return column;
        }
    }
    
}