namespace BasicConsoleLibrary.Core.Widgets;
public class Markup : Renderable, IHasJustification, IOverflowable
{
    /// <inheritdoc/>
    public EnumJustify? Justification
    {
        get => _paragraph.Justification;
        set => _paragraph.Justification = value;
    }

    /// <inheritdoc/>
    public EnumOverflow? Overflow
    {
        get => _paragraph.Overflow;
        set => _paragraph.Overflow = value;
    }
    private readonly Paragraph _paragraph;
    public Markup(StyledTextBuilder builder)
    {
        _paragraph = BuildParagraph(builder);
    }
    public Markup(Func<StyledTextBuilder, StyledTextBuilder> builderFunc)
    {
        StyledTextBuilder block = new();
        builderFunc.Invoke(block);
        _paragraph = BuildParagraph(block);
    }
    private static Paragraph BuildParagraph(StyledTextBuilder builder)
    {
        var emojiParser = bb1.EmojiParserHook;
        var segments = new List<Segment>();

        foreach (var run in builder.Build())
        {
            var content = emojiParser?.ReplaceEmojis(run.Content) ?? run.Content;
            segments.Add(new Segment(content, run.Style));
        }

        var line = new SegmentLine(segments);

        var paragraph = new Paragraph(line);
        

        return paragraph;
    }
    /// <inheritdoc/>
    protected override Measurement Measure(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)_paragraph).Measure(options, maxWidth);
    }

    /// <inheritdoc/>
    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)_paragraph).Render(options, maxWidth);
    }
}