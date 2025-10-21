namespace BasicConsoleLibrary.Core.Extensions;
public static class StatusExtensions
{

    /// <summary>
    /// Sets the spinner.
    /// </summary>
    /// <param name="status">The <see cref="Status"/> instance.</param>
    /// <param name="spinner">The spinner.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static Status Spinner(this Status status, Spinner spinner)
    {
        ArgumentNullException.ThrowIfNull(status);

        status.Spinner = spinner;
        return status;
    }
}