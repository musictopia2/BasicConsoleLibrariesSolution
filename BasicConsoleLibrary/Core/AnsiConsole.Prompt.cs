namespace BasicConsoleLibrary.Core;
public static partial class AnsiConsole
{
    /// <summary>
    /// Displays a prompt to the user.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="prompt">The prompt markup text.</param>
    /// <returns>The prompt input result.</returns>
    public static T Ask<T>(string prompt)
        where T: notnull
    {

        return new TextPrompt<T>(prompt).Show();
    }

    /// <summary>
    /// Displays a prompt to the user.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="prompt">The prompt markup text.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The prompt input result.</returns>
    public static Task<T> AskAsync<T>(string prompt, CancellationToken cancellationToken = default)
        where T : notnull
    {
        return new TextPrompt<T>(prompt).ShowAsync(cancellationToken);
    }

    /// <summary>
    /// Displays a prompt to the user.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="prompt">The prompt to display.</param>
    /// <returns>The prompt input result.</returns>
    public static T Prompt<T>(IPrompt<T> prompt)
    {
        ArgumentNullException.ThrowIfNull(prompt);
        return prompt.Show();
    }

    /// <summary>
    /// Displays a prompt to the user.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="prompt">The prompt to display.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The prompt input result.</returns>
    public static Task<T> PromptAsync<T>(IPrompt<T> prompt, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(prompt);
        return prompt.ShowAsync(cancellationToken);
    }
    public static bool Confirm(string question, bool defaultValue = true)
    {
        return new TextPrompt<bool>(question)
        {
            DefaultValue = defaultValue,
            ShowDefaultValue = true
        }.Show();
    }
    public static bool Confirm(string question)
    {
        return new TextPrompt<bool>(question)
        {
            ShowDefaultValue = false
        }.Show();
    }
}