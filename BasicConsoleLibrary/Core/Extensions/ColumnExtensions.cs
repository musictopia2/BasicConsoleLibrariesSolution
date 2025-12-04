namespace BasicConsoleLibrary.Core.Extensions;
/// <summary>
/// Contains extension methods for <see cref="IColumn"/>.
/// </summary>
public static class ColumnExtensions
{
    extension <T>(T obj)
        where T: class, IColumn
    {
        /// <summary>
        /// Prevents a column from wrapping.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IColumn"/>.</typeparam>
        /// <param name="obj">The column.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T NoWrap()
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.NoWrap = true;
            return obj;
        }
        /// <summary>
        /// Sets the width of the column.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IColumn"/>.</typeparam>
        /// <param name="obj">The column.</param>
        /// <param name="width">The column width.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Width(int? width)
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.Width = width;
            return obj;
        }
    }
    
}