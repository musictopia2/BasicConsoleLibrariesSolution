namespace BasicConsoleLibrary.Core;
public class StyledTextBuilder(bool autoReset = true)
{
    private readonly BasicList<StyledRun> _segments = [];
    private Style _currentStyle = new();
    public StyledTextBuilder Append(string text)
    {
        _segments.Add(new StyledRun(text, _currentStyle));
        if (autoReset)
        {
            ResetStyle();
        }
        return this;
    }
    public StyledTextBuilder AddLine()
    {
        _segments.Add(new StyledRun(Constants.VBCrLf, _currentStyle));
        return this;
    }
    public StyledTextBuilder AppendEmoji(string emoji)
    {
        emoji = emoji.Trim(':'); // Just in case user types ":smile:"
        string text = $":{emoji}:";
        _segments.Add(new StyledRun(text, _currentStyle));
        return this;
    }
    public StyledTextBuilder SetForeground(string color)
    {
        _currentStyle = _currentStyle.With(fg: color);
        return this;
    }

    public StyledTextBuilder SetBackground(string color)
    {
        _currentStyle = _currentStyle.With(bg: color);
        return this;
    }
    public StyledTextBuilder AddDecoration(EnumTextDecoration decoration)
    {
        FlagsWrapper<EnumTextDecoration> newItem = new();
        if (_currentStyle.Decoration is not null)
        {
            var olds = _currentStyle.Decoration.RawValue;
            newItem.SetRawValue(olds);
        }
        newItem.Add(decoration);
        _currentStyle = _currentStyle.With(decoration: newItem);
        return this;
    }
    public StyledTextBuilder ResetStyle()
    {
        _currentStyle = new Style();
        return this;
    }
    public StyledTextBuilder ResetDecorations()
    {
        FlagsWrapper<EnumTextDecoration> newItem = new();
        _currentStyle = _currentStyle.With(decoration: newItem);
        return this;
    }
    public StyledTextBuilder SetAutoReset(bool value)
    {
        autoReset = value;
        return this;
    }
    public BasicList<StyledRun> Build() => _segments;
    public StyledTextBuilder Clear()
    {
        _segments.Clear();
        ResetStyle();
        return this;
    }
}