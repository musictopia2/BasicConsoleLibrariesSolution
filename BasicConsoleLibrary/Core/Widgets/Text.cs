namespace BasicConsoleLibrary.Core.Widgets;
public class Text : Renderable, IHasJustification, IOverflowable
{

    /// <summary>
    /// Gets an empty <see cref="Text"/> instance.
    /// </summary>
    public static Text Empty { get; } = new Text(string.Empty);

    /// <summary>
    /// Gets an instance of <see cref="Text"/> containing a new line.
    /// </summary>
    public static Text NewLine { get; } = new Text(Environment.NewLine, Style.Plain);
    private readonly Paragraph _paragraph;

    public EnumJustify? Justification
    {
        get => _paragraph.Justification;
        set => _paragraph.Justification = value ?? EnumJustify.Left;
    }

    public EnumOverflow? Overflow
    {
        get => _paragraph.Overflow;
        set => _paragraph.Overflow = value ?? EnumOverflow.Fold;
    }
    public Text(string content, Style? style = null)
    {
        _paragraph = new(content, style);
    }
    public Text(BasicList<string> list, Style? style = null)
    {
        string content = string.Join("\n", list);
        _paragraph = new(content, style);
    }


    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)_paragraph).Render(options, maxWidth);
    }

    protected override Measurement Measure(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)_paragraph).Measure(options, maxWidth);
    }
}