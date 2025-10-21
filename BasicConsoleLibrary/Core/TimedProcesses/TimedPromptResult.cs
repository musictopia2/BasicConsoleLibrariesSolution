namespace BasicConsoleLibrary.Core.TimedProcesses;
public record TimedPromptResult<T>(T Value, bool HasValue)
    where T : notnull;