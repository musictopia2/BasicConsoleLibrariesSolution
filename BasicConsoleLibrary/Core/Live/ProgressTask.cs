namespace BasicConsoleLibrary.Core.Live;
public sealed class ProgressTask
{
    public string Name { get; }
    public double Value { get; private set; }
    public bool IsFinished => Value >= 100;
    public double Percentage => Math.Min(Value, 100);
    internal ProgressTask(string name)
    {
        Name = name;
    }
    public void Increment(double amount)
    {
        Value = Math.Min(Value + amount, 100);
    }
}