namespace BasicConsoleLibrary.Core.Widgets;
public class StyledTextBlock : Renderable
{
    private readonly List<(string Text, Style Style)> _segments;

    public StyledTextBlock()
    {
        _segments = [];
    }

    public void Add(string text, Style style)
    {
        _segments.Add((text, style));
    }
    public void Add(string text)
    {
        _segments.Add((text, Style.Plain));
    }

    public void AddLineBreak()
    {
        _segments.Add(("\n", Style.Plain));
    }

    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        foreach (var (text, style) in _segments)
        {
            // You can choose to split lines or handle newlines specially
            if (text.Contains('\n'))
            {
                var lines = text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    yield return new Segment(lines[i], style);
                    if (i < lines.Length - 1)
                    {
                        yield return Segment.LineBreak;
                    }
                }
            }
            else
            {
                yield return new Segment(text, style);
            }
        }
    }
}