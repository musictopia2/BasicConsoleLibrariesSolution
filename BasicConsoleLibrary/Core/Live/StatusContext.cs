namespace BasicConsoleLibrary.Core.Live;
/// <summary>
/// Represents a context that can be used to interact with a <see cref="Status"/>.
/// </summary>
public sealed class StatusContext
{
    public string Status { get; set; } = "";
    public Spinner Spinner { get; set; } = Spinner.Known.Default;
    public Style? SpinnerStyle { get; set; }
    internal Action? RequestRender { get; set; }
}