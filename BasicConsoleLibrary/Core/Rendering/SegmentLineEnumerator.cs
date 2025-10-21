namespace BasicConsoleLibrary.Core.Rendering;

/// <summary>
/// An enumerator for <see cref="SegmentLine"/> collections.
/// </summary>
public sealed class SegmentLineEnumerator : IEnumerable<Segment>
{
    private readonly List<SegmentLine> _lines;

    /// <summary>
    /// Initializes a new instance of the <see cref="SegmentLineEnumerator"/> class.
    /// </summary>
    /// <param name="lines">The lines to enumerate.</param>
    public SegmentLineEnumerator(IEnumerable<SegmentLine> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        _lines = [.. lines];
    }

    /// <inheritdoc/>
    public IEnumerator<Segment> GetEnumerator()
    {
        return new SegmentLineIterator(_lines);
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}