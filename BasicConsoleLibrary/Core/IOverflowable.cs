namespace BasicConsoleLibrary.Core;

/// <summary>
/// Represents something that can overflow.
/// </summary>
public interface IOverflowable
{
    /// <summary>
    /// Gets or sets the text overflow strategy.
    /// </summary>
    EnumOverflow? Overflow { get; set; }
}