namespace BasicConsoleLibrary.Core.Menus;
public class ComboMenu(string menu, int pageSize = 5) : IMenu
{
    public BasicList<MenuItem> Items { get; set; } = [];
    public int PageSize { get; set; } = pageSize;
    async Task IMenu.ShowAsync(CancellationToken cancellationToken)
    {
        await ConsoleExclusive.RunAsync(async () =>
        {
            MenuSelectionEngine engine = new(menu, Items, PageSize);
            int? value = await engine.ShowAsync(cancellationToken);
            if (value.HasValue == false)
            {
                return;
            }
            MenuItem item = Items[value.Value];
            await item.Action?.Invoke()!; //i think.

            throw new CustomBasicException("Invalid selection.");
        }).ConfigureAwait(false);
    }
}