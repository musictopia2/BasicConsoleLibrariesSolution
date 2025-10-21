namespace BasicConsoleLibrary.Core;

/// <summary>
/// Represents a border.
/// </summary>
public abstract partial class TableBorder
{
    /// <summary>
    /// Gets a value indicating whether or not the border is visible.
    /// </summary>
    public virtual bool Visible { get; } = true;

    /// <summary>
    /// Gets a value indicating whether the border supports row separators or not.
    /// </summary>
    public virtual bool SupportsRowSeparator { get; } = true;

    /// <summary>
    /// Gets the string representation of a specified table border part.
    /// </summary>
    /// <param name="part">The part to get the character representation for.</param>
    /// <returns>A character representation of the specified border part.</returns>
    public abstract string GetPart(EnumTableBorderPart part);

    /// <summary>
    /// Gets a whole column row for the specific column row part.
    /// </summary>
    /// <param name="part">The column row part.</param>
    /// <param name="widths">The column widths.</param>
    /// <param name="columns">The columns.</param>
    /// <returns>A string representing the column row.</returns>
    public virtual string GetColumnRow(EnumTablePart part, IReadOnlyList<int> widths, IReadOnlyList<IColumn> columns)
    {
        ArgumentNullException.ThrowIfNull(widths);

        ArgumentNullException.ThrowIfNull(columns);

        var (left, center, separator, right) = GetTableParts(part);

        var builder = new StringBuilder();
        builder.Append(left);

        foreach (var (columnIndex, _, lastColumn, columnWidth) in widths.Enumerate())
        {
            var padding = columns[columnIndex].Padding;
            var centerWidth = padding.GetLeftSafe() + columnWidth + padding.GetRightSafe();
            builder.Append(center.Repeat(centerWidth));

            if (!lastColumn)
            {
                builder.Append(separator);
            }
        }

        builder.Append(right);
        return builder.ToString();
    }

    /// <summary>
    /// Gets the table parts used to render a specific table row.
    /// </summary>
    /// <param name="part">The table part.</param>
    /// <returns>The table parts used to render the specific table row.</returns>
    protected (string Left, string Center, string Separator, string Right) GetTableParts(EnumTablePart part)
    {
        return part switch
        {
            // Top part
            EnumTablePart.Top =>
                (GetPart(EnumTableBorderPart.HeaderTopLeft), GetPart(EnumTableBorderPart.HeaderTop),
                GetPart(EnumTableBorderPart.HeaderTopSeparator), GetPart(EnumTableBorderPart.HeaderTopRight)),

            // Separator between header and cells
            EnumTablePart.HeaderSeparator =>
                (GetPart(EnumTableBorderPart.HeaderBottomLeft), GetPart(EnumTableBorderPart.HeaderBottom),
                GetPart(EnumTableBorderPart.HeaderBottomSeparator), GetPart(EnumTableBorderPart.HeaderBottomRight)),

            // Separator between header and cells
            EnumTablePart.RowSeparator =>
                (GetPart(EnumTableBorderPart.RowLeft), GetPart(EnumTableBorderPart.RowCenter),
                    GetPart(EnumTableBorderPart.RowSeparator), GetPart(EnumTableBorderPart.RowRight)),

            // Separator between footer and cells
            EnumTablePart.FooterSeparator =>
                (GetPart(EnumTableBorderPart.FooterTopLeft), GetPart(EnumTableBorderPart.FooterTop),
                GetPart(EnumTableBorderPart.FooterTopSeparator), GetPart(EnumTableBorderPart.FooterTopRight)),

            // Bottom part
            EnumTablePart.Bottom =>
                (GetPart(EnumTableBorderPart.FooterBottomLeft), GetPart(EnumTableBorderPart.FooterBottom),
                GetPart(EnumTableBorderPart.FooterBottomSeparator), GetPart(EnumTableBorderPart.FooterBottomRight)),

            // Unknown
            _ => throw new NotSupportedException("Unknown column row part"),
        };
    }
}