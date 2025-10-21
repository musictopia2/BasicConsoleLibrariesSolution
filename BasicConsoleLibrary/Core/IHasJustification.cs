namespace BasicConsoleLibrary.Core;

/// <summary>
/// Represents something that has justification.
/// </summary>
public interface IHasJustification
{
    /// <summary>
    /// Gets or sets the justification.
    /// </summary>
    EnumJustify? Justification { get; set; }
}