namespace BasicConsoleLibrary.Core.Internal;
internal static class EnumerableExtensions
{
    extension (IEnumerable<bool> source)
    {
        public bool AnyTrue => source.Any(b => b);
    }
    extension <T>(IEnumerable<T> source)
    {
        public int IndexOf(T item)
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

        public IEnumerable<(int Index, bool First, bool Last, T Item)>
            Enumerate()
        {
            ArgumentNullException.ThrowIfNull(source);

            using var enumerator = source.GetEnumerator();

            bool first = true;
            bool hasMore = enumerator.MoveNext();

            for (int index = 0; hasMore; index++)
            {
                T current = enumerator.Current;
                hasMore = enumerator.MoveNext();
                yield return (index, first, !hasMore, current);
                first = false;
            }
        }
        public IEnumerable<TResult> SelectIndex<TResult>(Func<T, int, TResult> func)
        {
            return source.Select((value, index) => func(value, index));
        }
        public IEnumerable<(T First, TSecond Second, TThird Third)> ZipThree<TSecond, TThird>(
            IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return source.Zip(second, (a, b) => (a, b))
                .Zip(third, (a, b) => (a.a, a.b, b));
        }
    }
}