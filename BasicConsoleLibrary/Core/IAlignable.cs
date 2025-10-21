namespace BasicConsoleLibrary.Core;

/// <summary>
/// Represents something that is alignable.
/// </summary>
public interface IAlignable
{
    /// <summary>
    /// Gets or sets the alignment.
    /// </summary>
    EnumJustify? Alignment { get; set; }
}