namespace BasicConsoleLibrary.Core.Internal;
internal static class EnumerableExtensions
{
    public static int IndexOf<T>(this IEnumerable<T> source, T item)
    {
        var index = 0;
        foreach (var candidate in source)
        {
            if (Equals(candidate, item))
            {
                return index;
            }

            index++;
        }

        return -1;
    }
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }
    public static bool AnyTrue(this IEnumerable<bool> source)
    {
        return source.Any(b => b);
    }
    public static IEnumerable<(int Index, bool First, bool Last, T Item)> Enumerate<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return source.GetEnumerator().Enumerate();
    }
    public static IEnumerable<(int Index, bool First, bool Last, T Item)> Enumerate<T>(this IEnumerator<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        var first = true;
        var last = !source.MoveNext();
        T current;

        for (var index = 0; !last; index++)
        {
            current = source.Current;
            last = !source.MoveNext();
            yield return (index, first, last, current);
            first = false;
        }
    }
    public static IEnumerable<TResult> SelectIndex<T, TResult>(this IEnumerable<T> source, Func<T, int, TResult> func)
    {
        return source.Select((value, index) => func(value, index));
    }
    public static IEnumerable<(TFirst First, TSecond Second, TThird Third)> ZipThree<TFirst, TSecond, TThird>(
        this IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
    {
        return first.Zip(second, (a, b) => (a, b))
            .Zip(third, (a, b) => (a.a, a.b, b));
    }
}