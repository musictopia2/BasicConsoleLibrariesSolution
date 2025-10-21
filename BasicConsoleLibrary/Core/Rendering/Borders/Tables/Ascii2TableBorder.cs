namespace BasicConsoleLibrary.Core.Rendering.Borders.Tables;

/// <summary>
/// Represents another old school ASCII border.
/// </summary>
public sealed class Ascii2TableBorder : TableBorder
{
    /// <inheritdoc/>
    public override string GetPart(EnumTableBorderPart part)
    {
        return part switch
        {
            EnumTableBorderPart.HeaderTopLeft => "+",
            EnumTableBorderPart.HeaderTop => "-",
            EnumTableBorderPart.HeaderTopSeparator => "+",
            EnumTableBorderPart.HeaderTopRight => "+",
            EnumTableBorderPart.HeaderLeft => "|",
            EnumTableBorderPart.HeaderSeparator => "|",
            EnumTableBorderPart.HeaderRight => "|",
            EnumTableBorderPart.HeaderBottomLeft => "|",
            EnumTableBorderPart.HeaderBottom => "-",
            EnumTableBorderPart.HeaderBottomSeparator => "+",
            EnumTableBorderPart.HeaderBottomRight => "|",
            EnumTableBorderPart.CellLeft => "|",
            EnumTableBorderPart.CellSeparator => "|",
            EnumTableBorderPart.CellRight => "|",
            EnumTableBorderPart.FooterTopLeft => "|",
            EnumTableBorderPart.FooterTop => "-",
            EnumTableBorderPart.FooterTopSeparator => "+",
            EnumTableBorderPart.FooterTopRight => "|",
            EnumTableBorderPart.FooterBottomLeft => "+",
            EnumTableBorderPart.FooterBottom => "-",
            EnumTableBorderPart.FooterBottomSeparator => "+",
            EnumTableBorderPart.FooterBottomRight => "+",
            EnumTableBorderPart.RowLeft => "|",
            EnumTableBorderPart.RowCenter => "-",
            EnumTableBorderPart.RowSeparator => "+",
            EnumTableBorderPart.RowRight => "|",
            _ => throw new InvalidOperationException("Unknown border part."),
        };
    }
}