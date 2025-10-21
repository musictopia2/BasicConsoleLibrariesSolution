namespace BasicConsoleLibrary.Core.Prompts;

/// <summary>
/// Represents a prompt.
/// </summary>
/// <typeparam name="T">The prompt result type.</typeparam>
public interface IPrompt<T>
{
    /// <summary>
    /// Shows the prompt.
    /// </summary>
    /// <returns>The prompt input result.</returns>
    T Show();

    /// <summary>
    /// Shows the prompt asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The prompt input result.</returns>
    Task<T> ShowAsync(CancellationToken cancellationToken);
}