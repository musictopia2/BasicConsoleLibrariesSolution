namespace BasicConsoleLibrary.Core;

/// <summary>
/// Represents padding.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Padding"/> struct.
/// </remarks>
/// <param name="left">The left padding.</param>
/// <param name="top">The top padding.</param>
/// <param name="right">The right padding.</param>
/// <param name="bottom">The bottom padding.</param>
public readonly struct Padding(int left, int top, int right, int bottom) : IEquatable<Padding>
{
    /// <summary>
    /// Gets the left padding.
    /// </summary>
    public int Left { get; } = left;

    /// <summary>
    /// Gets the top padding.
    /// </summary>
    public int Top { get; } = top;

    /// <summary>
    /// Gets the right padding.
    /// </summary>
    public int Right { get; } = right;

    /// <summary>
    /// Gets the bottom padding.
    /// </summary>
    public int Bottom { get; } = bottom;

    /// <summary>
    /// Initializes a new instance of the <see cref="Padding"/> struct.
    /// </summary>
    /// <param name="size">The padding for all sides.</param>
    public Padding(int size)
        : this(size, size, size, size)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Padding"/> struct.
    /// </summary>
    /// <param name="horizontal">The left and right padding.</param>
    /// <param name="vertical">The top and bottom padding.</param>
    public Padding(int horizontal, int vertical)
        : this(horizontal, vertical, horizontal, vertical)
    {
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Padding padding && Equals(padding);
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Left, Top, Right, Bottom);
    }

    /// <inheritdoc/>
    public bool Equals(Padding other)
    {
        return Left == other.Left
            && Top == other.Top
            && Right == other.Right
            && Bottom == other.Bottom;
    }

    /// <summary>
    /// Checks if two <see cref="Padding"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Padding"/> instance to compare.</param>
    /// <param name="right">The second <see cref="Padding"/> instance to compare.</param>
    /// <returns><c>true</c> if the two instances are equal, otherwise <c>false</c>.</returns>
    public static bool operator ==(Padding left, Padding right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Checks if two <see cref="Padding"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Padding"/> instance to compare.</param>
    /// <param name="right">The second <see cref="Padding"/> instance to compare.</param>
    /// <returns><c>true</c> if the two instances are not equal, otherwise <c>false</c>.</returns>
    public static bool operator !=(Padding left, Padding right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Gets the padding width.
    /// </summary>
    /// <returns>The padding width.</returns>
    public int GetWidth()
    {
        return Left + Right;
    }

    /// <summary>
    /// Gets the padding height.
    /// </summary>
    /// <returns>The padding height.</returns>
    public int GetHeight()
    {
        return Top + Bottom;
    }
}