namespace BasicConsoleLibrary.Core.Widgets.Table;
public sealed class AutoTable<T> : BaseTable
    where T : notnull
{
    public AutoTable(BasicList<T> items)
    {
        IFlatDataProvider<T> provider = FlatDataHelpers<T>.MasterContext ?? throw new InvalidOperationException("No provider registered.");
        int columnCount = provider.ColumnCount;
        for (int i = 0; i < columnCount; i++)
        {
            string header = provider.GetHeader(i);
            _columns.Add(new(header)); //well see.
        }
        foreach (var item in items)
        {
            BasicList<IRenderable> values = [];
            for (int i = 0; i < columnCount; i++)
            {
                string toUse = provider.GetValue(item, i);
                Text text = new(toUse);
                values.Add(text);
            }
            Rows.Add(values);
        }
    }
    //the part that is iffy is if i wanted paging.
    //for now, won't have it.   later attempt to add it.

}