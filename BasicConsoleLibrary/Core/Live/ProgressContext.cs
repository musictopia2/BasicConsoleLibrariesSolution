namespace BasicConsoleLibrary.Core.Live;
public sealed class ProgressContext
{
    private readonly BasicList<ProgressTask> _tasks = [];
    internal IReadOnlyList<ProgressTask> Tasks => _tasks;
    public ProgressTask AddTask(string name)
    {
        var task = new ProgressTask(name);
        _tasks.Add(task);
        return task;
    }
    public bool IsFinished => _tasks.All(t => t.IsFinished);
}