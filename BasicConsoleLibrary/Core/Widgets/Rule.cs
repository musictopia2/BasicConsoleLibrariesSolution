namespace BasicConsoleLibrary.Core.Widgets;

/// <summary>
/// A renderable horizontal rule.
/// </summary>
public sealed class Rule : Renderable, IHasJustification, IHasBoxBorder
{
    /// <summary>
    /// Gets or sets the rule title markup text.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the rule style.
    /// </summary>
    public Style? Style { get; set; }

    /// <summary>
    /// Gets or sets the rule's title justification.
    /// </summary>
    public EnumJustify? Justification { get; set; }

    /// <inheritdoc/>
    public BoxBorder Border { get; set; } = BoxBorder.Square;

    internal int TitlePadding { get; set; } = 2;
    internal int TitleSpacing { get; set; } = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="Rule"/> class.
    /// </summary>
    public Rule()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rule"/> class.
    /// </summary>
    /// <param name="title">The rule title markup text.</param>
    public Rule(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    /// <inheritdoc/>
    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        var extraLength = 2 * TitlePadding + 2 * TitleSpacing;

        if (Title == null || maxWidth <= extraLength)
        {
            return GetLineWithoutTitle(maxWidth);
        }

        // Get the title and make sure it fits.
        var title = GetTitleSegments(options, Title, maxWidth - extraLength);
        if (Segment.CellCount(title) > maxWidth - extraLength)
        {
            // Truncate the title
            title = Segment.TruncateWithEllipsis(title, maxWidth - extraLength);
            if (!title.Any())
            {
                // We couldn't fit the title at all.
                return GetLineWithoutTitle(maxWidth);
            }
        }

        var (left, right) = GetLineSegments(maxWidth, title);

        var segments = new List<Segment>();
        segments.Add(left);
        segments.AddRange(title);
        segments.Add(right);
        segments.Add(Segment.LineBreak);

        return segments;
    }

    private IEnumerable<Segment> GetLineWithoutTitle(int maxWidth)
    {
        var text = Border.GetPart(EnumBoxBorderPart.Top).Repeat(maxWidth);

        return
        [
            new Segment(text, Style ?? Style.Plain),
            Segment.LineBreak,
        ];
    }

    private IEnumerable<Segment> GetTitleSegments(RenderOptions options, string title, int width)
    {
        title = title.NormalizeNewLines().ReplaceExact("\n", " ").Trim();
        var text = new Text(title, Style);
        return ((IRenderable)text).Render(options with { SingleLine = true }, width);
    }

    private (Segment Left, Segment Right) GetLineSegments(int width, IEnumerable<Segment> title)
    {
        var titleLength = Segment.CellCount(title);

        var borderPart = Border.GetPart(EnumBoxBorderPart.Top);

        var alignment = Justification ?? EnumJustify.Center;
        if (alignment == EnumJustify.Left)
        {
            var left = new Segment(borderPart.Repeat(TitlePadding) + new string(' ', TitleSpacing), Style ?? Style.Plain);

            var rightLength = width - titleLength - left.CellCount() - TitleSpacing;
            var right = new Segment(new string(' ', TitleSpacing) + borderPart.Repeat(rightLength), Style ?? Style.Plain);

            return (left, right);
        }
        else if (alignment == EnumJustify.Center)
        {
            var leftLength = (width - titleLength) / 2 - TitleSpacing;
            var left = new Segment(borderPart.Repeat(leftLength) + new string(' ', TitleSpacing), Style ?? Style.Plain);

            var rightLength = width - titleLength - left.CellCount() - TitleSpacing;
            var right = new Segment(new string(' ', TitleSpacing) + borderPart.Repeat(rightLength), Style ?? Style.Plain);

            return (left, right);
        }
        else if (alignment == EnumJustify.Right)
        {
            var right = new Segment(new string(' ', TitleSpacing) + borderPart.Repeat(TitlePadding), Style ?? Style.Plain);

            var leftLength = width - titleLength - right.CellCount() - TitleSpacing;
            var left = new Segment(borderPart.Repeat(leftLength) + new string(' ', TitleSpacing), Style ?? Style.Plain);

            return (left, right);
        }

        throw new NotSupportedException("Unsupported alignment.");
    }
}