namespace BasicConsoleLibrary.Core.Widgets.Exceptions;
public class ExceptionStyle
{
    private static FlagsWrapper<EnumTextDecoration> GetBold
    {
        get
        {
            FlagsWrapper<EnumTextDecoration> result = new();
            result.Add(EnumTextDecoration.Bold);
            return result;
        }
    }
    /// <summary>
    /// Gets or sets the message color.
    /// </summary>
    public Style Message { get; set; } = new Style(cc2.Red, null, GetBold);

    /// <summary>
    /// Gets or sets the exception color.
    /// </summary>
    public Style Exception { get; set; } = cc2.White;

    /// <summary>
    /// Gets or sets the method color.
    /// </summary>
    public Style Method { get; set; } = cc2.Yellow;

    /// <summary>
    /// Gets or sets the parameter type color.
    /// </summary>
    public Style ParameterType { get; set; } = cc2.Blue;

    /// <summary>
    /// Gets or sets the parameter name color.
    /// </summary>
    public Style ParameterName { get; set; } = cc1.Silver;

    /// <summary>
    /// Gets or sets the parenthesis color.
    /// </summary>
    public Style Parenthesis { get; set; } = cc1.Silver;

    /// <summary>
    /// Gets or sets the path color.
    /// </summary>
    public Style Path { get; set; } = new Style(cc2.Yellow, null, GetBold);

    /// <summary>
    /// Gets or sets the line number color.
    /// </summary>
    public Style LineNumber { get; set; } = cc2.Blue;

    /// <summary>
    /// Gets or sets the color for dimmed text such as "at" or "in".
    /// </summary>
    public Style Dimmed { get; set; } = cc2.Gray;

    /// <summary>
    /// Gets or sets the color for non emphasized items.
    /// </summary>
    public Style NonEmphasized { get; set; } = cc1.Silver;
}