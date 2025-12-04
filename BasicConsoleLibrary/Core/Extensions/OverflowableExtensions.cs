namespace BasicConsoleLibrary.Core.Extensions;
/// <summary>
/// Contains extension methods for <see cref="IOverflowable"/>.
/// </summary>
public static class OverflowableExtensions
{
    extension <T>(T obj)
        where T: class, IOverflowable
    {
        /// <summary>
        /// Folds any overflowing text.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IOverflowable"/>.</typeparam>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Fold()
        {
            ArgumentNullException.ThrowIfNull(obj);
            return Overflow(obj, EnumOverflow.Fold);
        }

        /// <summary>
        /// Crops any overflowing text.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IOverflowable"/>.</typeparam>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Crop()
        {
            ArgumentNullException.ThrowIfNull(obj);
            return Overflow(obj, EnumOverflow.Crop);
        }

        /// <summary>
        /// Crops any overflowing text and adds an ellipsis to the end.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IOverflowable"/>.</typeparam>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Ellipsis()
        {
            ArgumentNullException.ThrowIfNull(obj);
            return Overflow(obj, EnumOverflow.Ellipsis);
        }

        /// <summary>
        /// Sets the overflow strategy.
        /// </summary>
        /// <typeparam name="T">An object implementing <see cref="IOverflowable"/>.</typeparam>
        /// <param name="overflow">The overflow strategy to use.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public T Overflow(EnumOverflow overflow)
        {
            ArgumentNullException.ThrowIfNull(obj);
            obj.Overflow = overflow;
            return obj;
        }
    }
}