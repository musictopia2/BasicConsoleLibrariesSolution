namespace BasicConsoleLibrary.Core.Widgets;
/// <summary>
/// Represents a renderable used to align content.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Align"/> class.
/// </remarks>
/// <param name="renderable">The renderable to align.</param>
/// <param name="horizontal">The horizontal alignment.</param>
/// <param name="vertical">The vertical alignment, or <c>null</c> if none.</param>
public sealed class Align(IRenderable renderable, EnumHorizontalAlignment? horizontal = null, EnumVerticalAlignment? vertical = null) : Renderable
{
    private readonly IRenderable _renderable = renderable ?? throw new ArgumentNullException(nameof(renderable));

    /// <summary>
    /// Gets or sets the horizontal alignment.
    /// </summary>
    public EnumHorizontalAlignment? Horizontal { get; set; } = horizontal;

    /// <summary>
    /// Gets or sets the vertical alignment.
    /// </summary>
    public EnumVerticalAlignment? Vertical { get; set; } = vertical;

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Align"/> class that is left aligned.
    /// </summary>
    /// <param name="renderable">The <see cref="IRenderable"/> to align.</param>
    /// <param name="vertical">The vertical alignment, or <c>null</c> if none.</param>
    /// <returns>A new <see cref="Align"/> object.</returns>
    public static Align Left(IRenderable renderable, EnumVerticalAlignment? vertical = null)
    {
        return new Align(renderable, EnumHorizontalAlignment.Left, vertical);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Align"/> class that is center aligned.
    /// </summary>
    /// <param name="renderable">The <see cref="IRenderable"/> to align.</param>
    /// <param name="vertical">The vertical alignment, or <c>null</c> if none.</param>
    /// <returns>A new <see cref="Align"/> object.</returns>
    public static Align Center(IRenderable renderable, EnumVerticalAlignment? vertical = null)
    {
        return new Align(renderable, EnumHorizontalAlignment.Center, vertical);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Align"/> class that is right aligned.
    /// </summary>
    /// <param name="renderable">The <see cref="IRenderable"/> to align.</param>
    /// <param name="vertical">The vertical alignment, or <c>null</c> if none.</param>
    /// <returns>A new <see cref="Align"/> object.</returns>
    public static Align Right(IRenderable renderable, EnumVerticalAlignment? vertical = null)
    {
        return new Align(renderable, EnumHorizontalAlignment.Right, vertical);
    }

    /// <inheritdoc/>
    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        var rendered = _renderable.Render(options with { Height = null }, maxWidth);
        var lines = Segment.SplitLines(rendered);

        var width = Math.Min(Width ?? maxWidth, maxWidth);
        var height = Height ?? options.Height;

        var blank = new SegmentLine([new Segment(new string(' ', width))]);

        // Align vertically
        if (Vertical != null && height != null)
        {
            switch (Vertical)
            {
                case EnumVerticalAlignment.Top:
                    {
                        var diff = height - lines.Count;
                        for (var i = 0; i < diff; i++)
                        {
                            lines.Add(blank);
                        }

                        break;
                    }

                case EnumVerticalAlignment.Middle:
                    {
                        var top = (height - lines.Count) / 2;
                        var bottom = height - top - lines.Count;

                        for (var i = 0; i < top; i++)
                        {
                            lines.InsertBeginning(blank);
                        }

                        for (var i = 0; i < bottom; i++)
                        {
                            lines.Add(blank);
                        }

                        break;
                    }

                case EnumVerticalAlignment.Bottom:
                    {
                        var diff = height - lines.Count;
                        for (var i = 0; i < diff; i++)
                        {
                            lines.InsertBeginning(blank);
                        }

                        break;
                    }

                default:
                    throw new NotSupportedException("Unknown vertical alignment");
            }
        }

        // Align horizontally
        foreach (var line in lines)
        {
            EnumHorizontalAlignment reals;
            if (Horizontal is null)
            {
                reals = EnumHorizontalAlignment.Left;
            }
            else
            {
                reals = Horizontal.Value;
            }
            Aligner.AlignHorizontally(line, reals, width);
        }
        return new SegmentLineEnumerator(lines);

    }
}