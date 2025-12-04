namespace BasicConsoleLibrary.Core.Extensions;
public static class StatusExtensions
{
    extension (Status status)
    {
        /// <summary>
        /// Sets the spinner.
        /// </summary>
        /// <param name="spinner">The spinner.</param>
        /// <returns>The same instance so that multiple calls can be chained.</returns>
        public Status Spinner(Spinner spinner)
        {
            ArgumentNullException.ThrowIfNull(status);

            status.Spinner = spinner;
            return status;
        }
    }   
}