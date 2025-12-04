namespace BasicConsoleLibrary.Core.Extensions;
/// <summary>
/// Contains extension methods for <see cref="IAlignable"/>.
/// </summary>
public static class AlignableExtensions
{
    extension <T>(T obj)
        where T: class, IAlignable
    {
        /// <summary>
        /// Sets the alignment for an <see cref="IAlignable"/> object.
        /// </summary>
        /// <param name="obj">The alignable object.</param>
        /// <param name="alignment">The alignment.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Alignment(EnumJustify? alignment)
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.Alignment = alignment;
            return obj;
        }

        /// <summary>
        /// Sets the <see cref="IAlignable"/> object to be left aligned.
        /// </summary>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T LeftAligned()
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.Alignment = EnumJustify.Left;
            return obj;
        }

        /// <summary>
        /// Sets the <see cref="IAlignable"/> object to be centered.
        /// </summary>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Centered()
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.Alignment = EnumJustify.Center;
            return obj;
        }

        /// <summary>
        /// Sets the <see cref="IAlignable"/> object to be right aligned.
        /// </summary>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T RightAligned()
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.Alignment = EnumJustify.Right;
            return obj;
        }
    }   
}