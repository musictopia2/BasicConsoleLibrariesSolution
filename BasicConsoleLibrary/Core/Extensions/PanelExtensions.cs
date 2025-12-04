namespace BasicConsoleLibrary.Core.Extensions;
/// <summary>
/// Contains extension methods for <see cref="Panel"/>.
/// </summary>
public static class PanelExtensions
{
    extension (Panel panel)
    {
        /// <summary>
        /// Sets the panel header.
        /// </summary>
        /// <param name="text">The header text.</param>
        /// <param name="alignment">The header alignment.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public Panel Header(string text, EnumJustify? alignment = null)
        {
            ArgumentNullException.ThrowIfNull(panel);

            ArgumentNullException.ThrowIfNull(text);

            alignment ??= panel.Header?.Justification;
            return panel.Header(new PanelHeader(text, alignment));
        }

        /// <summary>
        /// Sets the panel header alignment.
        /// </summary>
        /// <param name="alignment">The header alignment.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public Panel HeaderAlignment(EnumJustify alignment)
        {
            ArgumentNullException.ThrowIfNull(panel);

            if (panel.Header != null)
            {
                // Update existing style
                panel.Header.Justification = alignment;
            }
            else
            {
                // Create header
                Header(panel, string.Empty, alignment);
            }

            return panel;
        }

        /// <summary>
        /// Sets the panel header.
        /// </summary>
        /// <param name="header">The header to use.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public Panel Header(PanelHeader header)
        {
            ArgumentNullException.ThrowIfNull(panel);
            panel.Header = header;
            return panel;
        }
    }
    
}