namespace BasicConsoleLibrary.Core.Live;

/// <summary>
/// Represents a context that can be used to interact with a <see cref="LiveDisplay"/>.
/// </summary>
public sealed class LiveDisplayContext
{

    private readonly Lock _lock = new();
    private readonly LiveDisplay _display;
    private IRenderable _renderable;



    //figure out how to new this up (?)
    internal LiveDisplayContext(LiveDisplay display, IRenderable renderable)
    {
        _display = display;
        _renderable = renderable;
    }

    /// <summary>
    /// Updates the live display target.
    /// </summary>
    /// <param name="target">The new live display target.</param>
    public void UpdateTarget(IRenderable target)
    {
        lock (_lock)
        {
            _renderable = target;
            _display.Update(target);
        }
    }

    /// <summary>
    /// Refreshes the live display.
    /// </summary>
    public void Refresh()
    {
        lock (_lock)
        {
            _display.Update(_renderable);
            //_console.Write(new ControlCode(string.Empty));
        }
    }

}