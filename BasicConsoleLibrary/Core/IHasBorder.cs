namespace BasicConsoleLibrary.Core;

/// <summary>
/// Represents something that has a border.
/// </summary>
public interface IHasBorder
{
    /// <summary>
    /// Gets or sets the box style.
    /// </summary>
    public Style? BorderStyle { get; set; }
}