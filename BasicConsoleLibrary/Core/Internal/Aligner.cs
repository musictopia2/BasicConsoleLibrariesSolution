namespace BasicConsoleLibrary.Core.Internal;
internal static class Aligner
{
    public static string Align(string text, EnumJustify? alignment, int maxWidth)
    {
        if (alignment == null || alignment == EnumJustify.Left)
        {
            return text;
        }

        var width = Cell.GetCellLength(text);
        if (width >= maxWidth)
        {
            return text;
        }

        switch (alignment)
        {
            case EnumJustify.Right:
                {
                    var diff = maxWidth - width;
                    return new string(' ', diff) + text;
                }

            case EnumJustify.Center:
                {
                    var diff = (maxWidth - width) / 2;

                    var left = new string(' ', diff);
                    var right = new string(' ', diff);

                    // Right side
                    var remainder = (maxWidth - width) % 2;
                    if (remainder != 0)
                    {
                        right += new string(' ', remainder);
                    }

                    return left + text + right;
                }

            default:
                throw new NotSupportedException("Unknown alignment");
        }
    }

    public static void Align<T>(T segments, EnumJustify? alignment, int maxWidth)
        where T : BasicList<Segment>
    {
        if (alignment == null || alignment == EnumJustify.Left)
        {
            return;
        }

        var width = Segment.CellCount(segments);
        if (width >= maxWidth)
        {
            return;
        }

        switch (alignment)
        {
            case EnumJustify.Right:
                {
                    var diff = maxWidth - width;
                    segments.InsertBeginning(Segment.Padding(diff));
                    break;
                }

            case EnumJustify.Center:
                {
                    // Left side.
                    var diff = (maxWidth - width) / 2;
                    segments.InsertBeginning(Segment.Padding(diff));

                    // Right side
                    segments.Add(Segment.Padding(diff));
                    var remainder = (maxWidth - width) % 2;
                    if (remainder != 0)
                    {
                        segments.Add(Segment.Padding(remainder));
                    }

                    break;
                }

            default:
                throw new NotSupportedException("Unknown alignment");
        }
    }

    public static void AlignHorizontally<T>(T segments, EnumHorizontalAlignment alignment, int maxWidth)
        where T : BasicList<Segment>
    {
        var width = Segment.CellCount(segments);
        if (width >= maxWidth)
        {
            return;
        }

        switch (alignment)
        {
            case EnumHorizontalAlignment.Left:
                {
                    var diff = maxWidth - width;
                    segments.Add(Segment.Padding(diff));
                    break;
                }

            case EnumHorizontalAlignment.Right:
                {
                    var diff = maxWidth - width;
                    segments.InsertBeginning(Segment.Padding(diff));
                    break;
                }

            case EnumHorizontalAlignment.Center:
                {
                    // Left side.
                    var diff = (maxWidth - width) / 2;
                    segments.InsertBeginning(Segment.Padding(diff));

                    // Right side
                    segments.Add(Segment.Padding(diff));
                    var remainder = (maxWidth - width) % 2;
                    if (remainder != 0)
                    {
                        segments.Add(Segment.Padding(remainder));
                    }

                    break;
                }

            default:
                throw new NotSupportedException("Unknown alignment");
        }
    }
}