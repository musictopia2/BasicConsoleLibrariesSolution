namespace BasicConsoleLibrary.Core.Internal;
internal static class ConsoleExclusive
{
    private static readonly SemaphoreSlim _semaphore;
    static ConsoleExclusive()
    {
        _semaphore = new SemaphoreSlim(1, 1);
    }

    public static T Run<T>(Func<T> func)
    {
        // Try acquiring the exclusivity semaphore
        if (!_semaphore.Wait(0))
        {
            throw CreateExclusivityException();
        }

        try
        {
            return func();
        }
        finally
        {
            _semaphore.Release(1);
        }
    }

    public static async Task<T> RunAsync<T>(Func<Task<T>> func)
    {
        // Try acquiring the exclusivity semaphore
        if (!await _semaphore.WaitAsync(0).ConfigureAwait(false))
        {
            throw CreateExclusivityException();
        }

        try
        {
            return await func().ConfigureAwait(false);
        }
        finally
        {
            _semaphore.Release(1);
        }
    }


    private static InvalidOperationException CreateExclusivityException() => new(
        "Trying to run one or more interactive functions concurrently. " +
        "Operations with dynamic displays (e.g. a prompt and a progress display) " +
        "cannot be running at the same time.");
}