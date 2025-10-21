namespace BasicConsoleLibrary.Core;

/// <summary>
/// Represents vertical overflow.
/// </summary>
public enum EnumVerticalOverflow
{
    /// <summary>
    /// Crop overflow.
    /// </summary>
    Crop = 0,

    /// <summary>
    /// Add an ellipsis at the end.
    /// </summary>
    Ellipsis = 1,

    /// <summary>
    /// Do not do anything about overflow.
    /// </summary>
    Visible = 2,
}