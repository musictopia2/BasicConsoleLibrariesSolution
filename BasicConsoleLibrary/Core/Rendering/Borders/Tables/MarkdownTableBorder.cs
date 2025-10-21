namespace BasicConsoleLibrary.Core.Rendering.Borders.Tables;

/// <summary>
/// Represents a Markdown border.
/// </summary>
public sealed class MarkdownTableBorder : TableBorder
{
    /// <inheritdoc />
    public override bool SupportsRowSeparator => false;

    /// <inheritdoc/>
    public override string GetPart(EnumTableBorderPart part)
    {
        return part switch
        {
            EnumTableBorderPart.HeaderTopLeft => " ",
            EnumTableBorderPart.HeaderTop => " ",
            EnumTableBorderPart.HeaderTopSeparator => " ",
            EnumTableBorderPart.HeaderTopRight => " ",
            EnumTableBorderPart.HeaderLeft => "|",
            EnumTableBorderPart.HeaderSeparator => "|",
            EnumTableBorderPart.HeaderRight => "|",
            EnumTableBorderPart.HeaderBottomLeft => "|",
            EnumTableBorderPart.HeaderBottom => "-",
            EnumTableBorderPart.HeaderBottomSeparator => "|",
            EnumTableBorderPart.HeaderBottomRight => "|",
            EnumTableBorderPart.CellLeft => "|",
            EnumTableBorderPart.CellSeparator => "|",
            EnumTableBorderPart.CellRight => "|",
            EnumTableBorderPart.FooterTopLeft => " ",
            EnumTableBorderPart.FooterTop => " ",
            EnumTableBorderPart.FooterTopSeparator => " ",
            EnumTableBorderPart.FooterTopRight => " ",
            EnumTableBorderPart.FooterBottomLeft => " ",
            EnumTableBorderPart.FooterBottom => " ",
            EnumTableBorderPart.FooterBottomSeparator => " ",
            EnumTableBorderPart.FooterBottomRight => " ",
            EnumTableBorderPart.RowLeft => " ",
            EnumTableBorderPart.RowCenter => " ",
            EnumTableBorderPart.RowSeparator => " ",
            EnumTableBorderPart.RowRight => " ",
            _ => throw new InvalidOperationException("Unknown border part."),
        };
    }

    /// <inheritdoc/>
    public override string GetColumnRow(EnumTablePart part, IReadOnlyList<int> widths, IReadOnlyList<IColumn> columns)
    {
        if (part == EnumTablePart.FooterSeparator)
        {
            return string.Empty;
        }

        if (part != EnumTablePart.HeaderSeparator)
        {
            return base.GetColumnRow(part, widths, columns);
        }

        var (left, center, separator, right) = GetTableParts(part);

        var builder = new StringBuilder();
        builder.Append(left);

        foreach (var (columnIndex, _, lastColumn, columnWidth) in widths.Enumerate())
        {
            var padding = columns[columnIndex].Padding;

            if (padding != null && padding.Value.Left > 0)
            {
                // Left padding
                builder.Append(" ".Repeat(padding.Value.Left));
            }

            var justification = columns[columnIndex].Alignment;
            if (justification == null)
            {
                // No alignment
                builder.Append(center.Repeat(columnWidth));
            }
            else if (justification.Value == EnumJustify.Left)
            {
                // Left
                builder.Append(':');
                builder.Append(center.Repeat(columnWidth - 1));
            }
            else if (justification.Value == EnumJustify.Center)
            {
                // Centered
                builder.Append(':');
                builder.Append(center.Repeat(columnWidth - 2));
                builder.Append(':');
            }
            else if (justification.Value == EnumJustify.Right)
            {
                // Right
                builder.Append(center.Repeat(columnWidth - 1));
                builder.Append(':');
            }

            // Right padding
            if (padding != null && padding.Value.Right > 0)
            {
                builder.Append(" ".Repeat(padding.Value.Right));
            }

            if (!lastColumn)
            {
                builder.Append(separator);
            }
        }

        builder.Append(right);
        return builder.ToString();
    }
}