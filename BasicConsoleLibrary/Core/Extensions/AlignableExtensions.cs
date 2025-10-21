namespace BasicConsoleLibrary.Core.Extensions;

/// <summary>
/// Contains extension methods for <see cref="IAlignable"/>.
/// </summary>
public static class AlignableExtensions
{
    /// <summary>
    /// Sets the alignment for an <see cref="IAlignable"/> object.
    /// </summary>
    /// <typeparam name="T">The alignable object type.</typeparam>
    /// <param name="obj">The alignable object.</param>
    /// <param name="alignment">The alignment.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static T Alignment<T>(this T obj, EnumJustify? alignment)
        where T : class, IAlignable
    {
        ArgumentNullException.ThrowIfNull(obj);

        obj.Alignment = alignment;
        return obj;
    }

    /// <summary>
    /// Sets the <see cref="IAlignable"/> object to be left aligned.
    /// </summary>
    /// <typeparam name="T">The alignable type.</typeparam>
    /// <param name="obj">The alignable object.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static T LeftAligned<T>(this T obj)
        where T : class, IAlignable
    {
        ArgumentNullException.ThrowIfNull(obj);

        obj.Alignment = EnumJustify.Left;
        return obj;
    }

    /// <summary>
    /// Sets the <see cref="IAlignable"/> object to be centered.
    /// </summary>
    /// <typeparam name="T">The alignable type.</typeparam>
    /// <param name="obj">The alignable object.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static T Centered<T>(this T obj)
        where T : class, IAlignable
    {
        ArgumentNullException.ThrowIfNull(obj);

        obj.Alignment = EnumJustify.Center;
        return obj;
    }

    /// <summary>
    /// Sets the <see cref="IAlignable"/> object to be right aligned.
    /// </summary>
    /// <typeparam name="T">The alignable type.</typeparam>
    /// <param name="obj">The alignable object.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static T RightAligned<T>(this T obj)
        where T : class, IAlignable
    {
        ArgumentNullException.ThrowIfNull(obj);

        obj.Alignment = EnumJustify.Right;
        return obj;
    }
}
