namespace BasicConsoleLibrary.Core.Widgets;
public class JsonText(string json) : JustInTimeRenderable
{
    public Style BracesStyle { get; set; } = cc2.Gray;
    public Style BracketsStyle { get; set; } = cc2.Gray;
    public Style MemberStyle { get; set; } = cc2.Blue;
    public Style ColonStyle { get; set; } = cc2.Yellow;
    public Style CommaStyle { get; set; } = cc2.Gray;
    public Style StringStyle { get; set; } = cc2.Red;
    public Style NumberStyle { get; set; } = cc2.Green;
    public Style BooleanStyle { get; set; } = cc2.Green;
    public Style NullStyle { get; set; } = cc2.Gray;
    protected override IRenderable Build()
    {
        StyledTextBlock builder = new();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        BuildJson(root, builder, indentLevel: 0, isLast: true, ending: true);
        return builder;
    }
    private void BuildJson(JsonElement element, StyledTextBlock builder, int indentLevel, bool isLast, bool ending = false)
    {
        string indent = new(' ', indentLevel * 2);
        void AddLine(string content, Style style, bool addComma = false)
        {
            string extras = "";
            if (addComma)
            {
                extras = ",";
            }
            builder.Add($"{content}{extras}", style);
            builder.AddLineBreak(); //i think.
        }
        string otherIndent;
        otherIndent = new(' ', indentLevel * 2);
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                builder.Add("{ ", BracesStyle);
                builder.AddLineBreak();
                var props = element.EnumerateObject().ToList();
                indentLevel++;
                otherIndent = new(' ', indentLevel * 2);
                for (int i = 0; i < props.Count; i++)
                {
                    var prop = props[i];
                    bool lastProp = i == props.Count - 1;
                    //builder.Add()
                    string prints;


                    builder.Add(otherIndent);
                    prints = $"""
                        "{prop.Name}"
                        """;

                    builder.Add(prints, MemberStyle);


                    builder.Add(": ", ColonStyle);


                    BuildJson(prop.Value, builder, indentLevel + 1, lastProp);
                }
                if (ending == false)
                {
                    builder.Add(indent);
                }
                else
                {
                    builder.AddLineBreak();
                }
                builder.Add("}", BracesStyle);
                if (isLast == false)
                {
                    builder.Add(",", BracesStyle);
                }
                if (ending)
                {
                    builder.AddLineBreak(); //i think.
                }
                //AddLine("}" + (isLast ? "" : ","), BracesStyle, true);
                break;

            case JsonValueKind.Array:
                //can be questionable.
                builder.Add("[ ", BracketsStyle);
                builder.AddLineBreak();
                //indentLevel++;
                //otherIndent = new(' ', indentLevel * 2);

                var items = element.EnumerateArray().ToList();
                for (int i = 0; i < items.Count; i++)
                {
                    bool lastItem = i == items.Count - 1;
                    builder.Add(indent);
                    BuildJson(items[i], builder, indentLevel + 1, lastItem);
                }

                builder.Add($"{otherIndent}]", BracketsStyle);
                builder.AddLineBreak();
                break;

            case JsonValueKind.String:
                AddLine($"\"{element.GetString()}\"", StringStyle, !isLast);
                break;

            case JsonValueKind.Number:
                AddLine(element.ToString(), NumberStyle, !isLast);
                break;

            case JsonValueKind.True:
            case JsonValueKind.False:
                AddLine(element.GetBoolean().ToString().ToLower(), BooleanStyle, !isLast);
                break;

            case JsonValueKind.Null:
                AddLine("null", NullStyle, !isLast);
                break;
        }
    }
}