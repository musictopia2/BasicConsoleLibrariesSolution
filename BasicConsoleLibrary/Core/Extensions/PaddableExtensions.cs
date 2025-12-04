namespace BasicConsoleLibrary.Core.Extensions;
/// <summary>
/// Contains extension methods for <see cref="IPaddable"/>.
/// </summary>
public static class PaddableExtensions
{
    extension <T>(T obj)
        where T: class, IPaddable
    {
        /// <summary>
        /// Sets the left padding.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IPaddable"/>.</typeparam>
        /// <param name="left">The left padding.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T PadLeft(int left)
        {
            ArgumentNullException.ThrowIfNull(obj);
            return obj.Padding(new Padding(left, obj.Padding.GetTopSafe, obj.Padding.GetRightSafe, obj.Padding.GetBottomSafe));
        }

        /// <summary>
        /// Sets the top padding.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IPaddable"/>.</typeparam>
        /// <param name="top">The top padding.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T PadTop(int top)
        {
            ArgumentNullException.ThrowIfNull(obj);
            return obj.Padding(new Padding(obj.Padding.GetLeftSafe, top, obj.Padding.GetRightSafe, obj.Padding.GetBottomSafe));
        }

        /// <summary>
        /// Sets the right padding.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IPaddable"/>.</typeparam>
        /// <param name="right">The right padding.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T PadRight(int right)
        {
            ArgumentNullException.ThrowIfNull(obj);

            return obj.Padding(new Padding(obj.Padding.GetLeftSafe, obj.Padding.GetTopSafe, right, obj.Padding.GetBottomSafe));
        }

        /// <summary>
        /// Sets the bottom padding.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IPaddable"/>.</typeparam>
        /// <param name="bottom">The bottom padding.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T PadBottom(int bottom)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return obj.Padding(new Padding(obj.Padding.GetLeftSafe, obj.Padding.GetTopSafe, obj.Padding.GetRightSafe, bottom));
        }

        /// <summary>
        /// Sets the left, top, right and bottom padding.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IPaddable"/>.</typeparam>
        /// <param name="left">The left padding to apply.</param>
        /// <param name="top">The top padding to apply.</param>
        /// <param name="right">The right padding to apply.</param>
        /// <param name="bottom">The bottom padding to apply.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Padding(int left, int top, int right, int bottom)
        {
            return obj.Padding(new Padding(left, top, right, bottom));
        }

        /// <summary>
        /// Sets the horizontal and vertical padding.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IPaddable"/>.</typeparam>
        /// <param name="horizontal">The left and right padding.</param>
        /// <param name="vertical">The top and bottom padding.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Padding(int horizontal, int vertical)
        {
            return obj.Padding(new Padding(horizontal, vertical));
        }

        /// <summary>
        /// Sets the padding.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IPaddable"/>.</typeparam>
        /// <param name="obj">The paddable object instance.</param>
        /// <param name="padding">The padding to apply.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Padding(Padding padding)
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.Padding = padding;
            return obj;
        }
    }
    
}