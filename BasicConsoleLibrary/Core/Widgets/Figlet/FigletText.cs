namespace BasicConsoleLibrary.Core.Widgets.Figlet;
public class FigletText(string text) : Renderable, IHasJustification
{
    public EnumJustify? Justification { get; set; } = EnumJustify.Left;
    public string Text { get; } = text;
    public Style Style { get; set; } = Style.Plain;
    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        BasicList<char> ourCharacters = Text.Select(x => x).ToBasicList();
        BasicList<FigletCharacter> completeList = rr1.GetResource<BasicList<FigletCharacter>>();
        char possibleTab = char.Parse(Constants.VBTab);
        int lineCount = completeList.First().List.Count;
        var outputLines = new string[lineCount];
        for (int i = 0; i < lineCount; i++)
        {
            outputLines[i] = string.Empty;
        }
        foreach (char item in ourCharacters)
        {
            if (item == possibleTab)
            {
                var spaceChar = completeList.Single(x => x.Character == ' ');
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < lineCount; i++)
                    {
                        outputLines[i] += spaceChar.List[i];
                    }
                }
            }
            else
            {
                var found = completeList.SingleOrDefault(x => x.Character == item);
                if (found is not null)
                {
                    for (int i = 0; i < lineCount; i++)
                    {
                        outputLines[i] += found.List[i];
                    }
                }
            }
        }

        foreach (string line in outputLines)
        {
            string justified = line;

            int lineLength = line.GetCellWidth();
            int space = Math.Max(0, maxWidth - lineLength);

            switch (Justification)
            {
                case EnumJustify.Center:
                    int padLeft = space / 2;
                    justified = new string(' ', padLeft) + line;
                    break;

                case EnumJustify.Right:
                    justified = new string(' ', space) + line;
                    break;

                    // Left justification needs no change
            }

            yield return new Segment(justified, Style);
            yield return Segment.LineBreak;
        }
    }
}