namespace BasicConsoleLibrary.Core.Extensions;
/// <summary>
/// Contains extension methods for <see cref="Padding"/>.
/// </summary>
public static class PaddingExtensions
{
    extension (Padding? padding)
    {
        /// <summary>
        /// Gets the left padding.
        /// </summary>
        /// <returns>The left padding or zero if <c>padding</c> is null.</returns>
        public int GetLeftSafe => padding?.Left ?? 0;
        

        /// <summary>
        /// Gets the right padding.
        /// </summary>
        /// <returns>The right padding or zero if <c>padding</c> is null.</returns>
        public int GetRightSafe => padding?.Right ?? 0;
       

        /// <summary>
        /// Gets the top padding.
        /// </summary>
        /// <returns>The top padding or zero if <c>padding</c> is null.</returns>
        public int GetTopSafe => padding?.Top ?? 0;
        

        /// <summary>
        /// Gets the bottom padding.
        /// </summary>
        /// <returns>The bottom padding or zero if <c>padding</c> is null.</returns>
        public int GetBottomSafe => padding?.Bottom ?? 0;
    }
    
}