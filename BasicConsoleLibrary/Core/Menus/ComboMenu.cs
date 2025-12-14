namespace BasicConsoleLibrary.Core.Menus;
public class ComboMenu(string menu, int pageSize = 5) : IMenu
{
    public BasicList<MenuItem> Items { get; set; } = [];
    public int PageSize { get; set; } = pageSize;
    private void FinishMenuRender()
    {
        // Move cursor below menu header + number of items visible (pageSize) to avoid corruption
        int finalLine = Console.CursorTop + PageSize + 1;
        Console.SetCursorPosition(0, finalLine);
    }
    async Task IMenu.ShowAsync(CancellationToken cancellationToken)
    {
        await ConsoleExclusive.RunAsync(async () =>
        {
            MenuSelectionEngine engine = new(menu, Items, PageSize);
            int? value = await engine.ShowAsync(cancellationToken);
            FinishMenuRender();
            if (value.HasValue == false)
            {
                return;
            }
            MenuItem item = Items[value.Value];
            await item.Action?.Invoke()!; //i think.
        }).ConfigureAwait(false);
    }
}